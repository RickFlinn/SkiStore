using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        
        public Product Item { get; set; }
        public int Quantity { get; set; }

        public string AlertMessage { get; set; }
        public string AlertSummary { get; set; }
    }
}
