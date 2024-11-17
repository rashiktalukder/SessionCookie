using Azure.Core;
using SessionCookie.API.DTOs;
using SessionCookie.API.Models;
using SessionCookie.API.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace SessionCookie.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> RegisterUserAsync(UserRegistrationRequest userRegistrationRequest)
        {
            if(await _userRepository.IsUserNameTakenAsync(userRegistrationRequest.UserName))
            {
                return false;
            }

            //use HashPassword func or(/) use BCrypt
            var hashedPassword = HashPassword(userRegistrationRequest.Password);

            var user = new User
            {
                UserName = userRegistrationRequest.UserName,
                Email = userRegistrationRequest.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegistrationRequest.Password, workFactor: 12)
            };

            await _userRepository.AddUserAsync(user);
            return true;
        }

        private string HashPassword(string password) 
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);

                // Convert the byte array to a hexadecimal string
                var stringBuilder = new StringBuilder();
                foreach (var item in hash)
                {
                    stringBuilder.Append(item.ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
    }
}
