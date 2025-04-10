using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CozyHavenStay.Models;

namespace CozyHavenStayHotelApplication.Models
{
    [Table("Admins")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? User { get; set; }

        public string Status { get; set; } = "Active";
        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}