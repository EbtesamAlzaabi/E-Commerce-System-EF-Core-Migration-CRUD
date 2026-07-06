using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceSystem.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; } // system generated

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; } // foreign key

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; } // foreign key

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; } // user input

        [MaxLength(1000)]
        public string? Comment { get; set; } // user input

        [Required]
        public DateTime ReviewDate { get; set; } // system generated

        // Navigation Properties
        public User User { get; set; }

        public Product Product { get; set; }
    }
}
