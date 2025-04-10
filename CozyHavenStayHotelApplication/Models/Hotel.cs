using CozyHavenStayHotelApplication.Misc;
using CozyHavenStayHotelApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStay.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [Required]
        public string HotelName { get; set; } = string.Empty;
        
        [ForeignKey("HotelOwner")]
        public int UserId { get; set; }
        public HotelOwner? HotelOwner { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location? Location { get; set; }

        [ForeignKey("StarRating")]
        public int StarRatingId { get; set; }
        public StarRating? StarRating { get; set; }

        // Optional 
        [EmailValidation]
        public string Email { get; set; } = string.Empty;

        public string Amenities { get; set; } = string.Empty; 

        public ICollection<Admin> Admins { get; set; } = new List<Admin>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
