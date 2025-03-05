using Microsoft.EntityFrameworkCore;

namespace authentication_API.infrastructure.data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<domain.entities.User> User { get; set; }
    }
}
