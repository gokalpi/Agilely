using Agilely.BoardApi.Contracts;
using Agilely.BoardApi.Domain.Entity;
using Agilely.BoardApi.DTO;
using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Agilely.BoardApi.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly ILogger<BoardsController> _logger;
        private readonly IBoardManager _boardManager;
        private readonly IMapper _mapper;

        public BoardsController(IBoardManager boardManager, IMapper mapper, ILogger<BoardsController> logger)
        {
            _boardManager = boardManager;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Boards
        [HttpGet]
        public async Task<IActionResult> GetBoards()
        {
            return new OkObjectResult(await _boardManager.GetAllAsync());
        }

        // GET: api/Boards/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoard(string id)
        {
            var board = await _boardManager.GetByIdAsync(id);
            if (board == null)
                throw new ApiException($"Board with id: {id} does not exist.", 404);

            return new OkObjectResult(board);
        }

        // PUT: api/Boards/5
        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdateBoard(string id, [FromBody] BoardDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var board = _mapper.Map<Board>(dto);
                    board.Id = id;

                    var updatedBoard = await _boardManager.UpdateAsync(board);
                    return new ApiResponse($"Board with id {id} updated successfully.", updatedBoard);
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, ex, $"Error when trying to update board with id {id}.");
                    throw;
                }
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        // POST: api/Boards
        [HttpPost]
        public async Task<ApiResponse> CreateBoard([FromBody] BoardDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var board = _mapper.Map<Board>(dto);
                    return new ApiResponse($"Board '{board.Name}' created successfully.", await _boardManager.CreateAsync(board), 201);
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, ex, $"Error when trying to create board with name {dto.Name}.");
                    throw;
                }
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        // DELETE: api/Boards/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteBoard(string id)
        {
            try
            {
                if (await _boardManager.DeleteAsync(id))
                    return new ApiResponse($"Board {id} deleted successfully.", true);
                else
                    throw new ApiException($"Board {id} can not be deleted.", 400);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, $"Error when trying to delete with id {id}.");
                throw;
            }
        }
    }
}