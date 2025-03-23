using System;
using System.Collections.Generic;
using UserManagementApp.Models;
using UserManagementApp.Services;

namespace UserManagementApp.UI
{
    public class ConsoleUI
    {
        private readonly IUserService _userService;

        public ConsoleUI(IUserService userService)
        {
            _userService = userService;
        }

        public void Start()
        {
            bool exit = false;

            while (!exit)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListAllUsers();
                        break;
                    case "2":
                        GetUserById();
                        break;
                    case "3":
                        AddNewUser();
                        break;
                    case "4":
                        UpdateExistingUser();
                        break;
                    case "5":
                        DeleteUser();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("===== User Management System =====");
            Console.WriteLine("1. List All Users");
            Console.WriteLine("2. Get User by ID");
            Console.WriteLine("3. Add New User");
            Console.WriteLine("4. Update User");
            Console.WriteLine("5. Delete User");
            Console.WriteLine("6. Exit");
            Console.Write("\nEnter your choice (1-6): ");
        }

        private void ListAllUsers()
        {
            Console.WriteLine("\n===== All Users =====");
            List<User> users = _userService.GetAllUsers();

            if (users.Count == 0)
            {
                Console.WriteLine("No users found in the database.");
                return;
            }

            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
        }

        private void GetUserById()
        {
            Console.Write("\nEnter user ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                User user = _userService.GetUserById(id);

                if (user != null)
                {
                    Console.WriteLine("\n===== User Details =====");
                    Console.WriteLine(user);
                }
                else
                {
                    Console.WriteLine($"User with ID {id} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        private void AddNewUser()
        {
            Console.WriteLine("\n===== Add New User =====");
            
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            
            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            
            Console.Write("Enter full name: ");
            string fullName = Console.ReadLine();

            User newUser = new User
            {
                Username = username,
                Email = email,
                FullName = fullName
            };

            if (_userService.RegisterUser(newUser, password))
            {
                Console.WriteLine("User added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add user.");
            }
        }

        private void UpdateExistingUser()
        {
            Console.Write("\nEnter ID of user to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                User existingUser = _userService.GetUserById(id);

                if (existingUser != null)
                {
                    Console.WriteLine("\n===== Update User =====");
                    Console.WriteLine($"Current details: {existingUser}");

                    Console.Write($"Enter new username (or press Enter to keep '{existingUser.Username}'): ");
                    string username = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(username))
                    {
                        existingUser.Username = username;
                    }

                    Console.Write("Enter new password (or press Enter to keep current): ");
                    string password = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        // If we're setting a new password, hash it
                        existingUser.Password = Utils.PasswordHasher.HashPassword(password);
                    }

                    Console.Write($"Enter new email (or press Enter to keep '{existingUser.Email}'): ");
                    string email = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        existingUser.Email = email;
                    }

                    Console.Write($"Enter new full name (or press Enter to keep '{existingUser.FullName}'): ");
                    string fullName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(fullName))
                    {
                        existingUser.FullName = fullName;
                    }

                    if (_userService.UpdateUser(existingUser))
                    {
                        Console.WriteLine("User updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update user.");
                    }
                }
                else
                {
                    Console.WriteLine($"User with ID {id} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        private void DeleteUser()
        {
            Console.Write("\nEnter ID of user to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write($"Are you sure you want to delete user with ID {id}? (y/n): ");
                string confirmation = Console.ReadLine().ToLower();

                if (confirmation == "y" || confirmation == "yes")
                {
                    if (_userService.DeleteUser(id))
                    {
                        Console.WriteLine("User deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to delete user.");
                    }
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }
    }
}