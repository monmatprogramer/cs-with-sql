using System;

namespace UserManagementApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }  // Note: In a production app, store hashed passwords
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Username: {Username}, Email: {Email}, FullName: {FullName}, CreatedAt: {CreatedAt}";
        }
    }
}