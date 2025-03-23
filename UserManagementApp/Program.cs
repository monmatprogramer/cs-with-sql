using System;
using UserManagementApp.Data.Repositories;
using UserManagementApp.Services;
using UserManagementApp.UI;
using UserManagementApp.Utils;

namespace UserManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Set up dependencies
                IUserRepository userRepository = new UserRepository();
                IUserService userService = new UserService(userRepository);
                
                // Create and start UI
                ConsoleUI ui = new ConsoleUI(userService);
                ui.Start();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Application error: {ex.Message}");
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Please check the application log for details.");
            }
        }
    }
}