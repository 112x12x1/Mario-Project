using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MarioGameSystem.Helpers
{
    public static class UserSession
    {
        public static int UserID { get; set; }
        public static string Username { get; set; } = string.Empty;
        public static int RoleID { get; set; }
        public static List<string> Permissions { get; set; } = new List<string>();
    }

    public static class SecurityManager
    {
        public static string GenerateSalt()
        {
            byte[] bytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return Convert.ToBase64String(bytes);
        }

        public static string HashPassword(string pwd, string salt)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(pwd + salt);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}