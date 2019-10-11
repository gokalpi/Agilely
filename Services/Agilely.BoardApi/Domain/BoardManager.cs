using Agilely.BoardApi.Contracts;
using Agilely.BoardApi.Data;
using Agilely.BoardApi.Domain.Entity;
using Agilely.Common;

namespace Agilely.BoardApi.Domain
{
    public class BoardManager : Repository<BoardDbContext, Board>, IBoardManager
    {
        public BoardManager(BoardDbContext context) : base(context)
        {
        }
    }
}