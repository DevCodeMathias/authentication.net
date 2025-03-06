using authentication_API.domain.entities;
using authentication_API.infrastructure.data;
using Microsoft.EntityFrameworkCore;

namespace authentication_API.infrastructure.repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task AddSync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();


        }

        public async Task<User> getUserByEmail(string email)
        {

            return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
