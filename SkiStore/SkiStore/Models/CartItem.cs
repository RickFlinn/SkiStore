using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models
{
    public class CartItem
    {
        [Required]
        // Composite key
        public int ProductID { get; set; }
        public string UserID { get; set; }

        // Number of the item in user's cart
        public int Quantity { get; set; }

        // associated Product
        public Product Product { get; set; }
    }
}
