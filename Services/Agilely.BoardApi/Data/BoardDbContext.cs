using Agilely.BoardApi.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Agilely.BoardApi.Data
{
    public class BoardDbContext : DbContext
    {
        public virtual DbSet<Board> Boards { get; set; }

        public BoardDbContext(DbContextOptions<BoardDbContext> options) : base(options)
        {
        }
    }
}