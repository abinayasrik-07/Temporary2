using CozyHavenStayHotelApplication.Contexts;
using CozyHavenStayHotelApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStayHotelApplication.Repositories
{
    public class HotelOwnerRepository : Repository<int, HotelOwner>
    {
        public HotelOwnerRepository(CozyHavenStayHotelContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<HotelOwner>> GetAll()
        {
            var owners = await _context.HotelOwners
                .Include(h => h.User)
                .ToListAsync();

            if (!owners.Any())
                throw new Exception("No hotel owners found");

            return owners;
        }

        public override async Task<HotelOwner> GetById(int id)
        {
            var owner = await _context.HotelOwners
                .Include(h => h.User)
                .FirstOrDefaultAsync(h => h.OwnerId == id);

            return owner ?? throw new Exception("Hotel owner not found");
        }
    }
}