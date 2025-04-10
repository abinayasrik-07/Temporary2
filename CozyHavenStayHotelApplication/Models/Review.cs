using CozyHavenStayHotelApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStay.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        [ForeignKey("Guest")]
        public int GuestId { get; set; }
        public Guest? Guest { get; set; }
    }
}
