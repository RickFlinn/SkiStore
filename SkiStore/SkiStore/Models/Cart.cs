using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models
{
    public class Cart
    {
        // Primary Key
        public int ID { get; set; }

        // Associated User
        [Required]
        public string User { get; set;}

        // Indicates whether this cart is active (true) or has been checked out or dumped (false)
        public bool Active { get; set; }

        //---- Navigation properties ---- //

        // Item entries in this Cart - Products and quantity of those products
        public List<CartEntry> CartEntries { get; set; }
    }
}
