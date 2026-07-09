using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceSystem.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; } // system generated

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; } // user input

        [MaxLength(500)]
        public string? Description { get; set; } // user input

        [MaxLength(300)]
        public string? ImageUrl { get; set; } // user input

        // Navigation Property
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
