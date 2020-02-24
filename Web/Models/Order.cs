

using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a count more than {1}")]
        public int Count { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a price greater than {1}")]
        public decimal Price { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        public string Stautus { get; set; }
    }
}
