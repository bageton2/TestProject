
namespace Web.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public ProductDTO Product { get; set; }

        public StatusDTO Status { get; set; }
    }
}
