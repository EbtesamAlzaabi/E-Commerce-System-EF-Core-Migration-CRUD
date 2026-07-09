using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceSystem.Models
{

    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; } // system generated

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; } // foreign key

        [Required]
        public DateTime OrderDate { get; set; } // system generated

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; } // calculated

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Pending"; // default value

        [Required]
        [MaxLength(300)]
        public string ShippingAddress { get; set; } // user input

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } // from list

        // Navigation Properties
        public virtual User User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }  = new List<OrderItem>();
    }
}
