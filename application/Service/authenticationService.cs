﻿using API_AUTENTICATION.domain.Interfaces.Repository;
using API_AUTENTICATION.domain.Interfaces.Service;
using authentication_API.domain.entities;
using authentication_API.infrastructure.repositories;
using Microsoft.AspNetCore.Identity;

namespace API_AUTENTICATION.application.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<bool> login(string email, string password)
        {
            User user = await _userRepository.getUserByEmail(email);
            if (user == null)
            {
                return false;
            }

            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }

    }
}
