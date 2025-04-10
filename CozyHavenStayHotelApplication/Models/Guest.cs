using CozyHavenStayHotelApplication.Misc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStayHotelApplication.Models
{
    [Table("Guests")]
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }

        [Required]
        [NameValidation]
        public string FullName { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        // Payment and Identity Info (could be separate tables)
        //public string? PaymentMethods { get; set; } // JSON or separate table
        //public string? IdentityProofs { get; set; } // JSON or separate table

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? User { get; set; }

        // Navigation
        //public ICollection<Booking>? Bookings { get; set; }
        //public ICollection<Payment>? Payments { get; set; }
    }
}