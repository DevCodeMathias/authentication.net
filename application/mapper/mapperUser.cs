using API_AUTENTICATION.application.dto;
using authentication_API.domain.entities;

namespace API_AUTENTICATION.application.mapper
{
    public class mapperUser
    {
        public static User toDomain(UserRequestDto userRequestDto)
        {
            return new User
            {
                Email = userRequestDto.Email,
                PasswordHash = userRequestDto.PasswordHash,
                IsVerified = false,
            };
        }
    }
}
