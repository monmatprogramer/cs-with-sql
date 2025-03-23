using System.Collections.Generic;
using UserManagementApp.Models;

namespace UserManagementApp.Data.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
    }
}