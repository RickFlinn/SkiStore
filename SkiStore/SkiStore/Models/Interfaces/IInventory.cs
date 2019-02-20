using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Interfaces
{
    interface IInventory
    {
        //create
        Task CreateProduct(Product product);

        //read
        Task<Product> GetProduct(int id);

        IEnumerable<Product> GetAllProducts();

        //update
        Task UpdateProduct(Product product);

        
    }
}
