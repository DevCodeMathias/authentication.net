using API_AUTENTICATION.application.dto;
using API_AUTENTICATION.application.mapper;
using API_AUTENTICATION.domain.exception;
using API_AUTENTICATION.domain.Interfaces.Repository;
using API_AUTENTICATION.domain.Interfaces.Service;
using authentication_API.domain.entities;
using authentication_API.infrastructure.repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using static System.Net.WebRequestMethods;

namespace API_AUTENTICATION.application.Service
{
    public class UserService : IUserService
    {
     
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IUserQueueSender _queueSender;
        public UserService(
            IUserRepository userRepository,
            IUserQueueSender queueSender)
        {
            _userRepository = userRepository;
            _queueSender = queueSender;
            _passwordHasher = new PasswordHasher<User>();

        }

        public async Task<User> AddUser(UserDto userDto)
        {

            await ValidateEmailNotExistsAsync(userDto.Email);
            User user = Create(userDto);

            await _userRepository.AddSync(user);


            await _queueSender.SendUserToQueueAsync(user);
            return user;
            

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
