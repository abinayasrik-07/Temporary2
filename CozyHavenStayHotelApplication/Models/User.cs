using CozyHavenStayHotelApplication.Misc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStayHotelApplication.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [EmailValidation]
        public string Email { get; set; } = string.Empty;
        public byte[] Password { get; set; }
        public byte[] HashKey { get; set; }

        // Navigation Properties
        public Admin? Admin { get; set; }
        public Guest? Guest { get; set; }
        public HotelOwner? HotelOwner { get; set; }
    }
}