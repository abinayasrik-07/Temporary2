using CozyHavenStayHotelApplication.Contexts;
using CozyHavenStayHotelApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStayHotelApplication.Repositories
{
    public class GuestRepository : Repository<int, Guest>
    {
        public GuestRepository(CozyHavenStayHotelContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Guest>> GetAll()
        {
            var guests = await _context.Guests
                .Include(g => g.User)
                .ToListAsync();

            if (!guests.Any())
                throw new Exception("No guests found");

            return guests;
        }

        public override async Task<Guest> GetById(int id)
        {
            var guest = await _context.Guests
                .Include(g => g.User)
                .FirstOrDefaultAsync(g => g.GuestId == id);

            return guest ?? throw new Exception("Guest not found");
        }
    }
}