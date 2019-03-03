using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.ViewModels
{
    public class CartViewModel : PapaViewModel
    {   
        public Product Product { get; set; }

        public Cart Cart { get; set; }
        
        public int Quantity { get; set; }
        
    }
}
