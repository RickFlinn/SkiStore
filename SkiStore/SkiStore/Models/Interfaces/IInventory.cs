using SkiStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SkiStore.Interfaces
{
    public interface IInventory
    {
        /// <summary>
        ///     Create the given product.
        /// </summary>
        /// <param name="product"> Product to create </param>
        /// <returns></returns>
        Task CreateProduct(Product product);

        /// <summary>
        ///     Retrive the product with the given ID.
        /// </summary>
        /// <param name="id">ID of product to retrieve </param>
        /// <returns> Product with that ID </returns>
        Task<Product> GetProduct(int id);

        /// <summary>
        ///     Retrieve all stored products.
        /// </summary>
        /// <returns> IEnumerable of all products </returns>
        IEnumerable<Product> GetAllProducts();

        /// <summary>
        ///     Create or update the given product, if it exists. 
        /// </summary>
        /// <param name="product"> Product to create or update </param>
        /// <returns></returns>
        Task SaveAsync(Product product);

        /// <summary>
        ///     Updates the given Product entry, if it exists.
        /// </summary>
        /// <param name="product"> Product </param>
        /// <returns></returns>
        Task UpdateProduct(Product product);

        /// <summary>
        ///     Delete the Product entry with the given ID, if it exists.
        /// </summary>
        /// <param name="id">ID of product to delete </param>
        /// <returns></returns>
        Task DeleteProduct(int id);
    }
}
