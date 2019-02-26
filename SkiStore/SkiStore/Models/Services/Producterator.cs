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

        // Adds the given Product into the database. 
        public async Task CreateProduct(Product product)
        {
            Product exists = await _context.Products.FindAsync(product.ID);
            if(exists == null)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
            }
        }

        // Deletes a product from the DB, if it exists
        public async Task DeleteProduct(int id)
        {
            Product doomed = await _context.Products.FindAsync(id);
            if (doomed != null)
            {
                _context.Products.Remove(doomed);
                await _context.SaveChangesAsync();
            }
        }

        // Returns all products
        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> allProducts = _context.Products.ToList();
            return allProducts;
        }

        // Gets a specific product by its ID
        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        // Updates a product if it exists in the DB
        public async Task UpdateProduct(Product product)
        {
            Product exists = await _context.Products.FindAsync(product.ID);
            if(exists != null)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        // Adds the given product to the database if it doesn't exist, or updates the product entry if it does.
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
