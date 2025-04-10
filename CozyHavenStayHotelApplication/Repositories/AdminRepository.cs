using CozyHavenStayHotelApplication.Contexts;
using CozyHavenStayHotelApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStayHotelApplication.Repositories
{
    public class AdminRepository : Repository<int, Admin>
    {
        public AdminRepository(CozyHavenStayHotelContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Admin>> GetAll()
        {
            var admins = await _context.Admins
                .Include(a => a.User)
                .ToListAsync();

            if (!admins.Any())
                throw new Exception("No admins found");

            return admins;
        }

        public override async Task<Admin> GetById(int id)
        {
            var admin = await _context.Admins
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AdminId == id);

            return admin ?? throw new Exception("Admin not found");
        }
    }
}