using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models
{
    public class CartEntry
    {
        

        // ---- Foreign Composite Keys ---- //
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int CartID { get; set; }
        

        // Number of the item in user's cart
        [Required]
        public int Quantity { get; set; }

        // ---- Navigation Properties ---- // 

        // associated Product
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
