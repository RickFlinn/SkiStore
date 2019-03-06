using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models
{
    public class Order
    {
        public int ID { get; set; }

        public string User { get; set; }

        public decimal Total { get; set; }

        public bool Paid { get; set; }

        public bool Delivered { get; set; }

        // Nav properties 
        public Cart Cart { get; set; }
        public int CartID { get; set; }
    }
}
