using Agilely.BoardApi.Domain.Entity;
using Agilely.BoardApi.DTO;
using AutoMapper;

namespace Agilely.BoardApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Board, BoardDTO>().ReverseMap();
        }
    }
}