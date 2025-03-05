using authentication_API.domain.dto;
using authentication_API.domain.entities;
using authentication_API.domain.mapper;
using authentication_API.infrastructure.repositories;
using Microsoft.AspNetCore.Identity;

namespace authentication_API.domain.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();

        }

        public async Task AddUser(UserDto userDto)
        {
            User user = mapperUser.toDomain(userDto);
            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.PasswordHash);
            await _userRepository.AddSync(user);

        }
    }
}
