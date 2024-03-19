using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.ViewModel.Service
{
    internal class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert mật khẩu từ string sang byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Chuyển đổi byte array thành một chuỗi hex
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Compute hash of entered password
                byte[] enteredPasswordHash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));

                // Convert stored hash from hex string to byte array
                byte[] storedHashBytes = new byte[storedHash.Length / 2];
                for (int i = 0; i < storedHashBytes.Length; i++)
                {
                    storedHashBytes[i] = Convert.ToByte(storedHash.Substring(i * 2, 2), 16);
                }

                // Compare computed hash with stored hash
                if (enteredPasswordHash.Length != storedHashBytes.Length)
                {
                    return false; // Hash lengths don't match
                }
                for (int i = 0; i < enteredPasswordHash.Length; i++)
                {
                    if (enteredPasswordHash[i] != storedHashBytes[i])
                    {
                        return false; // Passwords don't match
                    }
                }
            }

            return true; // Passwords match
        }



    }
}
