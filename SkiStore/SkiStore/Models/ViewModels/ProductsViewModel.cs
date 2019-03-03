using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.ViewModels
{
    public class ProductsViewModel : PapaViewModel
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public IEnumerable<Product> Products { get; set; }

        


        


    }
}
