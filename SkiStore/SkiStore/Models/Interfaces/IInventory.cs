using SkiStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SkiStore.Interfaces
{
    public interface IInventory
    {
        //create
        Task CreateProduct(Product product);

        //read
        Task<Product> GetProduct(int id);

        // Get all of em
        IEnumerable<Product> GetAllProducts();

        // Create or update if the item exists
        Task SaveAsync(Product product);

        //update
        Task UpdateProduct(Product product);

        //delete
        Task DeleteProduct(int id);
    }
}
