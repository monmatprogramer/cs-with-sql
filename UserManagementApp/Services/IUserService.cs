using System.Collections.Generic;
using UserManagementApp.Models;

namespace UserManagementApp.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
        bool RegisterUser(User user, string plainPassword);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
        bool AuthenticateUser(string username, string password);
    }
}