using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.ViewModels
{
    public class CheckoutViewModel : PapaViewModel
    {
        
        public Order Order { get; set; }

        // TODO: Add necessary data models for payment details when payment is implemented 
        public string CardNumber { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
    }
}
