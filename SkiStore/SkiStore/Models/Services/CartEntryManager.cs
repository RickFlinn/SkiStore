using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkiStore.Data;
using SkiStore.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.Services
{
    public class CartEntryManager : ICartEntryManager
    {
        private readonly SkiStoreProductDbContext _context;

        public CartEntryManager(SkiStoreProductDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Adds given cart entry to the database.
        /// </summary>
        /// <param name="cartEntry"> Cart entry to create </param>
        /// <returns></returns>
        public async Task SaveItem(CartEntry cartEntry)
        {
            CartEntry entry = await _context.CartEntries.Where(ce => ce.ProductID == cartEntry.ProductID
                                                                  && ce.CartID == cartEntry.CartID)
                                             .FirstOrDefaultAsync();

            if (entry == null)
                _context.Add(cartEntry);
            else
                _context.Update(cartEntry);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     Gets all of the item entries in the cart with the given ID.
        /// </summary>
        /// <param name="cartID"> ID of Cart </param>
        /// <returns> All items stored in that cart </returns>
        public async Task<IEnumerable<CartEntry>> GetCartItems(int cartID)
        {
            Cart cart = await _context.Carts.Where(c => c.ID == cartID)
                                            .Include(c => c.CartEntries)
                                            .FirstOrDefaultAsync();

            if(cart != null)
                return cart.CartEntries;
            else
                return null;
                                            
        }

        /// <summary>
        ///     Returns the cart entry with the given product and cart ID.
        /// </summary>
        /// <param name="cartID"> Cart containing the desired entry </param>
        /// <param name="productID"> Product inside the cart </param>
        /// <returns> Cart entry with given product and cart ID </returns>
        public async Task<CartEntry> GetItem(int cartID, int productID)
        {
            return await _context.CartEntries.Where(ce => ce.CartID == cartID 
                                                       && ce.ProductID == productID)
                                             .Include(ce => ce.Product)
                                             .FirstOrDefaultAsync();
        }


        // ************************************************************
        // We may want to avoid using the remove method; it may be better to simply set the quantity to zero and not display any 
        //  cart entries with a quantity of 0. 

        /// <summary>
        ///     Removes the given CartEntry from the database, if it exists.
        /// </summary>
        /// <param name="cartEntry"> CartEntry to r </param>
        /// <returns> </returns>
        public async Task Remove(CartEntry cartEntry)
        {
            CartEntry entry = await _context.CartEntries.Where(ce => ce.CartID == cartEntry.CartID
                                                                  && ce.ProductID == cartEntry.ProductID)
                                                        .FirstOrDefaultAsync();
            if(entry != null)
            {
                _context.Remove(cartEntry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
