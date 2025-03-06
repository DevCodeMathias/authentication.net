using API_AUTENTICATION.domain.exception;
using authentication_API.domain.dto;
using authentication_API.domain.entities;
using authentication_API.domain.mapper;
using authentication_API.infrastructure.repositories;
using Microsoft.AspNetCore.Identity;
using static System.Net.WebRequestMethods;

namespace API_AUTENTICATION.domain.Service
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

            await ValidateEmailNotExistsAsync(userDto.Email);
            User user = mapperUser.toDomain(userDto);
            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.PasswordHash);
            await _userRepository.AddSync(user);
            return;

        }

       private async Task ValidateEmailNotExistsAsync(string email)
        {
            User user = await _userRepository.getUserByEmail(email);

            if (user != null)
            {
                throw new EmailAlreadyExistsException();
            }
        }
    }
}
