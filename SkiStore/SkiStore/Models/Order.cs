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

        // @TODO: build out more functionality for payment processing / info

        public List<CartEntry> Items { get; set; }
        public Cart From { get; set; }
    }
}
