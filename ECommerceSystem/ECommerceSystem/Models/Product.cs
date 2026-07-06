using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceSystem.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; } // system generated

        [Required]
        [MaxLength(150)]
        public string ProductName { get; set; } // user input

        [MaxLength(1000)]
        public string? Description { get; set; } // user input

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; } // user input

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; } = 0; // default value

        [MaxLength(300)]
        public string? ImageUrl { get; set; } // user input

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; } // foreign key

        [Required]
        public DateTime CreatedAt { get; set; } // system generated

        public bool IsAvailable { get; set; } = true; // default value

        // Navigation Properties
        public Category Category { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
