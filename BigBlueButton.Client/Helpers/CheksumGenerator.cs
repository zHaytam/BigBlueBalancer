using System;
using System.Security.Cryptography;
using System.Text;

namespace BigBlueButton.Client.Helpers
{
    public static class CheksumGenerator
    {
        public static string Generate(string callName, string secret, string query = null)
        {
            using (var sha1 = SHA1.Create())
            {
                var result = sha1.ComputeHash(Encoding.ASCII.GetBytes($"{callName}{query}{secret}"));
                return BitConverter.ToString(result).Replace("-", "").ToLower();
            }
        }
    }
}