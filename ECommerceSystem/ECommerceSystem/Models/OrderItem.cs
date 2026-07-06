using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceSystem.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; } // system generated

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; } // foreign key

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; } // foreign key

        [Required]
        [Range(1, 999)]
        public int Quantity { get; set; } // user input

        // Navigation Properties
        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
