using API_AUTENTICATION.domain.Interfaces.Repository;
using authentication_API.domain.entities;
using authentication_API.infrastructure.data;
using Microsoft.EntityFrameworkCore;

namespace authentication_API.infrastructure.repositories
{
    public class UserRepository: IUserRepository
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


        public async Task SetUserAsVerifiedAsync(string userId)
        {
            if (!int.TryParse(userId, out int id))
            {
                throw new ArgumentException("UserId inválido.");
            }

            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            user.IsVerified = true;
            await _context.SaveChangesAsync();

        }

        public Task<User> GetUserByIdAsync(int id)
        {
            var user = _context.User.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }



    }
}
