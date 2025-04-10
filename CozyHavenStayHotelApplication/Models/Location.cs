using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStay.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}
