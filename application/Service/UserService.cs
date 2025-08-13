using API_AUTENTICATION.application.dto;
using API_AUTENTICATION.application.mapper;
using API_AUTENTICATION.domain.entities;
using API_AUTENTICATION.domain.exception;
using API_AUTENTICATION.domain.Interfaces.Repository;
using API_AUTENTICATION.domain.Interfaces.Service;
using authentication_API.domain.entities;
using Microsoft.AspNetCore.Identity;

namespace API_AUTENTICATION.application.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IUserQueueSender _queueSender;
        public UserService(
            IUserRepository userRepository,
            IUserQueueSender queueSender
            )
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
            _queueSender = queueSender;

        }

        public async Task AddUser(UserDto userDto)
        {

            try
            {
                await ValidateEmailNotExistsAsync(userDto.Email);
                var user = ToUser(userDto);

                await _userRepository.AddSync(user);
                var envelope = createEnvelope(user, "UserCreation");

                await _queueSender.SendUserToQueueAsync(envelope);


                return;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding user: {ex.Message}", ex);

            }
        }

        private async Task ValidateEmailNotExistsAsync(string email)
        {
            User user = await _userRepository.getUserByEmail(email);

            if (user != null)
            {
                throw new EmailAlreadyExistsException();
            }
        }


        private MessageEnvelope<User> createEnvelope(User user, string eventType)
        {
            MessageEnvelope<User> envelope = new MessageEnvelope<User>
            {
                MessageId = Guid.NewGuid(),
                TimesTamps = DateTime.UtcNow,
                EventType = eventType,
                source = "API_AUTENTICATION.application.Service.UserService",
                Payload = user
            };
            return envelope;
        }


        private User ToUser(UserDto userDto)
        {
            User user = mapperUser.toDomain(userDto);
            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.PasswordHash);

            return user;
        }

        public async Task CheckserExists(string userId)
        {
            try
            {
                await _userRepository.SetUserAsVerifiedAsync(userId);

                int IdNumber = Convert.ToInt32(userId);

                var user = await _userRepository.GetUserByIdAsync(IdNumber);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {IdNumber} not found.");
                }

                var UserEnvelope = createEnvelope(user, "UserVerification");

                await _queueSender.SendUserToQueueAsync(UserEnvelope);

                return;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking user existence: {ex.Message}", ex);
            }
         
        }

    }
}
