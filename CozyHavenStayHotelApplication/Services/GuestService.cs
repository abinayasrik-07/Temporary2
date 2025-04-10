using AutoMapper;
using CozyHavenStayHotelApplication.Interfaces;
using CozyHavenStayHotelApplication.Models;
using CozyHavenStayHotelApplication.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace CozyHavenStayHotelApplication.Services
{
    public class GuestService : IGuestService
    {
        private readonly IRepository<int, Guest> _guestRepository;
        private readonly IRepository<int, User> _userRepository;
        private readonly IMapper _mapper;

        public GuestService(
            IRepository<int, Guest> guestRepository,
            IRepository<int, User> userRepository,
            IMapper mapper)
        {
            _guestRepository = guestRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CreateGuestResponse> RegisterGuest(CreateGuestRequest request)
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
                throw new Exception("Failed to create user account");

            var guest = new Guest
            {
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                Nationality = request.Nationality,
                DateOfBirth = request.DateOfBirth,
                UserId = userResult.UserId
            };

            var guestResult = await _guestRepository.Add(guest);
            if (guestResult == null)
            {
                await _userRepository.Delete(userResult.UserId); // rollback
                throw new Exception("Failed to create guest profile");
            }

            return new CreateGuestResponse
            {
                GuestId = guestResult.GuestId,
                Email = userResult.Email
            };
        }

        public async Task<IEnumerable<GetGuestResponse>> GetAllGuests()
        {
            var guests = await _guestRepository.GetAll();
            if (!guests.Any())
                throw new Exception("No guests found");

            return MapGuestResponses(guests);
        }

        public async Task<GetGuestResponse> GetGuestById(int guestId)
        {
            var guest = await _guestRepository.GetById(guestId);
            if (guest == null)
                throw new Exception("Guest not found");

            return MapGuestResponse(guest);
        }

        public async Task<GetGuestResponse> UpdateGuestDetails(UpdateGuestRequest request)
        {
            var guest = await _guestRepository.GetById(request.GuestId);
            if (guest == null)
                throw new Exception("Guest not found");

            guest.FullName = request.FullName ?? guest.FullName;
            guest.PhoneNumber = request.PhoneNumber ?? guest.PhoneNumber;
            guest.Gender = request.Gender ?? guest.Gender;
            guest.Nationality = request.Nationality ?? guest.Nationality;
            guest.DateOfBirth = request.DateOfBirth ?? guest.DateOfBirth;

            var updatedGuest = await _guestRepository.Update(request.GuestId, guest);
            if (updatedGuest == null)
                throw new Exception("Failed to update guest");

            return MapGuestResponse(updatedGuest);
        }

        public async Task<bool> DeleteGuest(int guestId)
        {
            var guest = await _guestRepository.GetById(guestId);
            if (guest == null)
                throw new Exception("Guest not found");

            await _guestRepository.Delete(guestId);
            await _userRepository.Delete(guest.UserId);

            return true;
        }

        public async Task<IEnumerable<GetGuestResponse>> GetGuestsByFilter(GuestRequest request)
        {
            var guests = (await _guestRepository.GetAll()).ToList();

            if (!guests.Any())
                throw new Exception("No guests found");

            if (request.Filter?.Name != null)
                guests = guests.Where(g => g.FullName.Contains(request.Filter.Name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (request.Filter?.Phone != null)
                guests = guests.Where(g => g.PhoneNumber.Contains(request.Filter.Phone)).ToList();

            if (request.Filter?.Nationality != null)
                guests = guests.Where(g => g.Nationality.Equals(request.Filter.Nationality, StringComparison.OrdinalIgnoreCase)).ToList();

            if (request.SortBy != null)
                guests = SortGuests((int)request.SortBy, guests);

            if (request.Pagination != null)
                guests = PaginateGuests(request.Pagination, guests);

            return MapGuestResponses(guests);
        }

        private List<Guest> SortGuests(int sortBy, List<Guest> guests)
        {
            return sortBy switch
            {
                -2 => guests.OrderByDescending(g => g.FullName).ToList(),
                -1 => guests.OrderByDescending(g => g.GuestId).ToList(),
                1 => guests.OrderBy(g => g.GuestId).ToList(),
                2 => guests.OrderBy(g => g.FullName).ToList(),
                _ => guests
            };
        }

        private List<Guest> PaginateGuests(Pagination pagination, List<Guest> guests)
        {
            if (guests.Count <= pagination.PageSize)
                return guests;

            return guests
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();
        }

        private List<GetGuestResponse> MapGuestResponses(IEnumerable<Guest> guests)
        {
            var response = _mapper.Map<List<GetGuestResponse>>(guests);
            foreach (var res in response)
            {
                var original = guests.First(g => g.GuestId == res.GuestId);
                res.Email = original.User?.Email ?? "";
                res.Age = original.DateOfBirth.HasValue ? CalculateAge(original.DateOfBirth.Value) : null;
            }
            return response;
        }

        private GetGuestResponse MapGuestResponse(Guest guest)
        {
            var res = _mapper.Map<GetGuestResponse>(guest);
            res.Email = guest.User?.Email ?? "";
            res.Age = guest.DateOfBirth.HasValue ? CalculateAge(guest.DateOfBirth.Value) : null;
            return res;
        }

        private int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
