using AutoMapper;
using CozyHavenStayHotelApplication.Interfaces;
using CozyHavenStayHotelApplication.Models;
using CozyHavenStayHotelApplication.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace CozyHavenStayHotelApplication.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<int, Admin> _adminRepository;
        private readonly IRepository<int, User> _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AdminService(IRepository<int, Admin> adminRepository,
                            IRepository<int, User> userRepository,
                            IMapper mapper,
                            ITokenService tokenService)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<GetAdminResponse>> GetAllAdmins()
        {
            var admins = await _adminRepository.GetAll();
            if (!admins.Any())
                throw new Exception("No admins found");

            return admins.Select(admin =>
            {
                var dto = _mapper.Map<GetAdminResponse>(admin);
                dto.Email = admin.User?.Email ?? string.Empty;
                return dto;
            });
        }

        public async Task<GetAdminResponse> GetAdminById(int id)
        {
            var admin = await _adminRepository.GetById(id);
            if (admin == null)
                throw new Exception("Admin not found");

            var dto = _mapper.Map<GetAdminResponse>(admin);
            dto.Email = admin.User?.Email ?? string.Empty;
            return dto;
        }

        public async Task<GetAdminResponse> UpdateAdmin(UpdateAdminRequest request)
        {
            var admin = await _adminRepository.GetById(request.AdminId);
            if (admin == null)
                throw new Exception("Admin not found");

            admin.FullName = request.FullName ?? admin.FullName;
            admin.PhoneNumber = request.PhoneNumber ?? admin.PhoneNumber;

            var result = await _adminRepository.Update(request.AdminId, admin);
            var dto = _mapper.Map<GetAdminResponse>(result);
            dto.Email = admin.User?.Email ?? string.Empty;
            return dto;
        }

        public async Task<bool> DeleteAdmin(int id)
        {
            var admin = await _adminRepository.GetById(id);
            if (admin == null)
                throw new Exception("Admin not found");

            await _adminRepository.Delete(id);
            await _userRepository.Delete(admin.UserId);
            return true;
        }

        public async Task<AdminLoginResponse> RegisterAdmin(CreateAdminRequest request)
        {
            if ((await _userRepository.GetAll()).Any(u => u.Email == request.Email))
                throw new Exception("Email already registered");

            using var hmac = new HMACSHA512();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            var user = new User
            {
                Email = request.Email,
                Password = passwordHash,
                HashKey = hmac.Key
            };

            var userResult = await _userRepository.Add(user);
            if (userResult == null)
                throw new Exception("Failed to create user");

            var admin = new Admin
            {
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                UserId = userResult.UserId
            };

            var adminResult = await _adminRepository.Add(admin);
            if (adminResult == null)
            {
                await _userRepository.Delete(userResult.UserId); // rollback
                throw new Exception("Failed to create admin");
            }

            var token = await _tokenService.GenerateToken(userResult.UserId, userResult.Email);

            return new AdminLoginResponse
            {
                AdminId = adminResult.AdminId,
                FullName = adminResult.FullName,
                Email = userResult.Email
            };
        }

        public async Task<IEnumerable<GetAdminResponse>> GetAdminsByFilter(AdminRequest request)
        {
            var admins = (await _adminRepository.GetAll()).ToList();

            if (!admins.Any())
                throw new Exception("No admins found");

            if (request.Filter?.FullName != null)
                admins = admins.Where(a => a.FullName.Contains(request.Filter.FullName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (request.Filter?.PhoneNumber != null)
                admins = admins.Where(a => a.PhoneNumber.Contains(request.Filter.PhoneNumber)).ToList();

            admins = request.SortBy switch
            {
                -1 => admins.OrderByDescending(a => a.AdminId).ToList(),
                -2 => admins.OrderByDescending(a => a.FullName).ToList(),
                1 => admins.OrderBy(a => a.AdminId).ToList(),
                2 => admins.OrderBy(a => a.FullName).ToList(),
                _ => admins
            };

            if (request.Pagination != null)
            {
                admins = admins
                    .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
                    .Take(request.Pagination.PageSize)
                    .ToList();
            }

            return _mapper.Map<IEnumerable<GetAdminResponse>>(admins);
        }

    }
}
