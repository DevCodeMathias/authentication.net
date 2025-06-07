using authentication_API.domain.entities;

namespace API_AUTENTICATION.domain.Interfaces.Repository
{
    public interface IUserRepository
    {
       Task AddSync(User user);

        Task<User> getUserByEmail(string email);

    }
}
