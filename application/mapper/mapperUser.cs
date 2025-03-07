using API_AUTENTICATION.application.dto;
using authentication_API.domain.entities;

namespace API_AUTENTICATION.application.mapper
{
    public class mapperUser
    {
        public static User toDomain(UserDto userDto)
        {
            return new User
            {
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                IsVerified = false,
            };
        }
    }
}
