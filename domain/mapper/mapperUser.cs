using authentication_API.domain.dto;
using authentication_API.domain.entities;

namespace authentication_API.domain.mapper
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
