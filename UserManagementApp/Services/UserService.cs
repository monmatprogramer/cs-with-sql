using System;
using System.Collections.Generic;
using UserManagementApp.Data.Repositories;
using UserManagementApp.Models;
using UserManagementApp.Utils;

namespace UserManagementApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public bool RegisterUser(User user, string plainPassword)
        {
            // Hash the password before storing
            user.Password = PasswordHasher.HashPassword(plainPassword);
            user.CreatedAt = DateTime.Now;
            
            return _userRepository.AddUser(user);
        }

        public bool UpdateUser(User user)
        {
            // Note: This assumes the password in user.Password is already hashed
            // If you're updating with a plain password, you need to hash it first
            return _userRepository.UpdateUser(user);
        }

        public bool DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public bool AuthenticateUser(string username, string password)
        {
            User user = _userRepository.GetUserByUsername(username);
            
            if (user == null)
                return false;
                
            // Verify the password against the stored hash
            return PasswordHasher.VerifyPassword(password, user.Password);
        }
    }
}