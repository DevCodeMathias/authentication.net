using API_AUTENTICATION.application.dto;
using API_AUTENTICATION.domain.entities;
using authentication_API.domain.entities;

namespace API_AUTENTICATION.domain.Interfaces.Service
{
    public interface IUserService
    {
        Task AddUser(UserRequestDto userRequestDto);

        Task CheckserExists(String userId);
    }
}
