using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CozyHavenStayHotelApplication.Misc;

namespace CozyHavenStayHotelApplication.Models
{
    [Table("HotelOwners")]
    public class HotelOwner
    {
        [Key]
        public int OwnerId { get; set; }

        [Required]
        [NameValidation]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? User { get; set; }

        // Navigation
        //public ICollection<Hotel>? Hotels { get; set; }
        //public ICollection<Payment>? ReceivedPayments { get; set; }
    }
}
