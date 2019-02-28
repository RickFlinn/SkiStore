using SkiStore.Data;
using SkiStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.Services
{
    public class Producterator : IInventory
    {

        public SkiStoreProductDbContext _context { get; }

        public Producterator(SkiStoreProductDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Creates a database entry for the given product. 
        /// </summary>
        /// <param name="product"> Product to add to database </param>
        /// <returns></returns>
        public async Task CreateProduct(Product product)
        {
            Product exists = await _context.Products.FindAsync(product.ID);
            if(exists == null)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
            }
        }


        /// <summary>
        ///     Deletes a product from the DB, if it 
        /// </summary>
        /// <param name="id">ID of product to delete</param>
        /// <returns></returns>
        public async Task DeleteProduct(int id)
        {
            Product doomed = await _context.Products.FindAsync(id);
            if (doomed != null)
            {
                _context.Products.Remove(doomed);
                await _context.SaveChangesAsync();
            }
        }

        
        /// <summary>
        ///     Get all products stored in the database
        /// </summary>
        /// <returns> List of all products in the database </returns>
        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> allProducts = _context.Products.ToList();
            return allProducts;
        }


        /// <summary>
        ///     Gets a specific product by its ID 
        /// </summary>
        /// <param name="id"> Product ID </param>
        /// <returns> Product with corresponding ID </returns>
        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        /// <summary>
        ///     Updates a product if it exists in the DB
        /// </summary>
        /// <param name="product"> Product to update </param>
        /// <returns></returns>        
        public async Task UpdateProduct(Product product)
        {
            Product exists = await _context.Products.FindAsync(product.ID);
            if(exists != null)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }
        
        /// <summary>
        ///     Adds the given product to the database if it doesn't exist, or updates the product entry if it does.
        /// </summary>
        /// <param name="product"> Product to create or update </param>
        /// <returns></returns>
        public async Task SaveAsync(Product product)
        {
            Product exists = await _context.Products.FindAsync(product.ID);
            if(exists == null)
            {
                _context.Add(product);
            } else
            {
                _context.Update(product);
            }
            await _context.SaveChangesAsync();
        }
    }
}
