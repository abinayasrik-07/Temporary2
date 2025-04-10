using System.ComponentModel.DataAnnotations;

namespace CozyHavenStay.Models
{
    public class StarRating
    {
        [Key]
        public int StarRatingId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Description { get; set; }

        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}
