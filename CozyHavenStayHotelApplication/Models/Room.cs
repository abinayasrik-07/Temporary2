using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStay.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        public string RoomType { get; set; } = string.Empty; // e.g., Deluxe, Suite

        public decimal PricePerNight { get; set; }

        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        public Hotel? Hotel { get; set; }
    }
}

