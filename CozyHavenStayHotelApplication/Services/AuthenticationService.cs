using CozyHavenStayHotelApplication.Interfaces;
using CozyHavenStayHotelApplication.Models;
using CozyHavenStayHotelApplication.Models.DTOs;
using CozyHavenStayHotelApplication.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace CozyHavenStayHotelApplication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<int, User> _userRepository;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IRepository<int, User> userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<object> Login(LoginRequest loginRequest)
        {
            var users = await _userRepository.GetAll();
            var user = users.FirstOrDefault(u => u.Email == loginRequest.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            using var hmac = new HMACSHA512(user.HashKey);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequest.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.Password[i])
                    throw new UnauthorizedAccessException("Invalid email or password");
            }

            var token = await _tokenService.GenerateToken(user.UserId, user.Email);

            if (user.Guest != null)
            {
                return new LoginResponse
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Token = token
                };
            }

            if (user.Admin != null)
            {
                return new AdminLoginResponse
                {
                    AdminId = user.Admin.AdminId,
                    FullName = user.Admin.FullName,
                    Email = user.Email,
                    Token = token
                };
            }
            throw new UnauthorizedAccessException("Account type not supported or not registered.");
        }
    }
}
