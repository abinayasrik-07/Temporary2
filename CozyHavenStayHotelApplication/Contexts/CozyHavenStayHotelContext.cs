using CozyHavenStay.Models;
using CozyHavenStayHotelApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CozyHavenStayHotelApplication.Contexts
{
    public class CozyHavenStayHotelContext : DbContext
    {
        public CozyHavenStayHotelContext(DbContextOptions<CozyHavenStayHotelContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<HotelOwner> HotelOwners { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<StarRating> StarRatings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Admin)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>(a => a.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Guest)
                .WithOne(g => g.User)
                .HasForeignKey<Guest>(g => g.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.HotelOwner)
                .WithOne(h => h.User)
                .HasForeignKey<HotelOwner>(h => h.UserId);
        }
    }
}
