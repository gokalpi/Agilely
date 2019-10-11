using Agilely.BoardApi.Domain.Entity;
using Agilely.Common.Contracts;

namespace Agilely.BoardApi.Contracts
{
    public interface IBoardManager : IRepository<Board>
    {
    }
}