using API_AUTENTICATION.application.dto;
using authentication_API.domain.entities;

namespace API_AUTENTICATION.domain.Interfaces.Service
{
    public interface IUserService
    {
        Task<User> AddUser(UserDto userDto);
    }
}
