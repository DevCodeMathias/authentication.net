using API_AUTENTICATION.application.dto;
using API_AUTENTICATION.application.mapper;
using API_AUTENTICATION.domain.exception;
using authentication_API.domain.entities;
using authentication_API.infrastructure.repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using static System.Net.WebRequestMethods;

namespace API_AUTENTICATION.application.Service
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
            User user = Create(userDto);

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

        private User Create(UserDto userDto)
        {
            User user = mapperUser.toDomain(userDto);
            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.PasswordHash);

            return user;
        }
    }
}
