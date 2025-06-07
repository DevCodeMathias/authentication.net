using authentication_API.domain.entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace authentication_API.infrastructure.data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            

        }
        public DbSet<User> User { get; set; }

    }
}
