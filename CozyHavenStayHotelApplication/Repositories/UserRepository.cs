using CozyHavenStayHotelApplication.Contexts;
using CozyHavenStayHotelApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStayHotelApplication.Repositories
{
    public class UserRepository : Repository<int, User>
    {
        public UserRepository(CozyHavenStayHotelContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users
                .Include(u => u.Guest)
                .Include(u => u.Admin)
                .Include(u => u.HotelOwner)
                .ToListAsync();
            //if (!users.Any())
            //    throw new Exception("No users found");
            return users;
        }

        public override async Task<User> GetById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Guest)
                .Include(u => u.Admin)
                .Include(u => u.HotelOwner)
                .FirstOrDefaultAsync(u => u.UserId == id);
            return user ?? throw new Exception("User not found");
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users
                .Include(u => u.Guest)
                .Include(u => u.Admin)
                .Include(u => u.HotelOwner)
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }
    }
}