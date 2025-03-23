using System;
using System.Configuration;

namespace UserManagementApp.Data
{
    public static class DatabaseConfig
    {
        public static string ConnectionString
        {
            get
            {
                // In a real application, you would store this in App.config or appsettings.json
                // For this example, we'll hardcode it, but you should use configuration files
                string server = "LAPTOP-F8UMHM18\\SQLEXPRESS";     // SQL Server instance name
                string database = "UserRegistrationDB";  // Your database name
                string userId = "sa";            // SQL Server username
                string password = "monmat_1995@2024"; // SQL Server password

                // SQL Server Authentication
                //return $"Server={server};Database={database};User Id={userId};Password={password};";
                 return @"Server=LAPTOP-F8UMHM18\SQLEXPRESS;Database=UserRegistrationDB;Trusted_Connection=True;TrustServerCertificate=True;";
                // For Windows Authentication, use:
                // return $"Server={server};Database={database};Trusted_Connection=True;";
            }
        }
    }
}