using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500)]
        public string Description { get; set; }

        // Navigation Property: A Category can have many Products.
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

