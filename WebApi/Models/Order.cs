using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Products")]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey("Statuses")]
        public int StatusId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Product Product { get; set; }

        public virtual Status Status { get; set; }
    }
}
