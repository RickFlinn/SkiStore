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
    public class CartMan : ICartManager
    {
        private readonly SkiStoreProductDbContext _context;

        public CartMan(SkiStoreProductDbContext context)
        {
            _context = context;
        }

        public async Task<Order> Checkout(int cartID)
        {
            // TODO: build order checkout logic when user goes to check out a cart. Copy all of the cart entries to the 
            //  user's order, then return the order 
            return new Order();
        }

        /// <summary>
        ///     Marks the cart with the given ID as "InActive". 
        /// </summary>
        /// <param name="cartID"> ID of cart to deactivate </param>
        /// <returns></returns>
        public async Task Deactivate(int cartID)
        {
            Cart doomedCart = await _context.Carts.FindAsync(cartID);
            if(doomedCart != null)
            {
                doomedCart.Active = false;
                _context.Carts.Update(doomedCart);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        ///     Gets the current active cart for a user. 
        /// </summary>
        /// <param name="userID"> Username of currently signed in user </param>
        /// <returns> Active cart associated with that user </returns>
        public async Task<Cart> GetActiveCart(string userID)
        {
            Cart activeCart = await _context.Carts.Where(c => c.User == userID && c.Active)
                                                   .Include(c => c.CartEntries)
                                                     .ThenInclude(ce => ce.Product)
                                                   .FirstOrDefaultAsync();
            if(activeCart == null)
            {
                activeCart = new Cart() { Active = true, User = userID };
                _context.Carts.Add(activeCart);
                await _context.SaveChangesAsync();
            }

            return activeCart;
        }

        /// <summary>
        ///     Returns a List of all Carts in the database.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Cart>> GetAllCarts()
        {
            return await _context.Carts.ToListAsync();
        }

        /// <summary>
        ///     Gets a Cart by its ID.
        /// </summary>
        /// <param name="cartID"> ID of the Cart to retrieve </param>
        /// <returns> Cart with the given ID </returns>
        public async Task<Cart> GetCart(int cartID)
        {
            return await _context.Carts.Where(c => c.ID == cartID)
                                        .Include(c => c.CartEntries)
                                        .FirstOrDefaultAsync();
        }

        /// <summary>
        ///     Returns all of the carts that have ever been associated with a user.
        /// </summary>
        /// <param name="userID"> User's username </param>
        /// <returns> List of all carts associated with that user </returns>
        public async Task<IEnumerable<Cart>> GetUsersCarts(string userID)
        {
            return await _context.Carts.Where(c => c.User == userID)
                                        .Include(c => c.CartEntries)
                                        .ToListAsync();
        }
    }
}
