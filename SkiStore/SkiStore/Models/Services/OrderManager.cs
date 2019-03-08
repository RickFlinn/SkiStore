using Microsoft.EntityFrameworkCore;
using SkiStore.Data;
using SkiStore.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.Services
{
    public class OrderManager : IOrderManager
    {
        private readonly ICartManager _cartMan;
        private readonly ICartEntryManager _cartEntries;
        private readonly SkiStoreProductDbContext _context;

        public OrderManager(ICartManager cartMan, ICartEntryManager cartEntries, SkiStoreProductDbContext context)
        {
            _cartMan = cartMan;
            _cartEntries = cartEntries;
            _context = context;
        }


        /// <summary>
        ///     Takes in an ID corresponding to a user's cart. Marks that cart as "deactivated", then builds a new Order
        ///     using the items in that cart. 
        /// </summary>
        /// <param name="cartID"> ID of cart to check out </param>
        /// <returns> Order to be processed </returns>
        public async Task<Order> Checkout(int cartID)
        {
            Cart cart = await _cartMan.GetCart(cartID);

            if (cart == null) throw new ArgumentOutOfRangeException("That cart does not exist.");

            if (cart.CartEntries.Count < 1) throw new Exception("Given cart contains no items.");
            
            decimal total = 0;
            foreach(CartEntry entry in cart.CartEntries)
            {
                total += entry.Product.Price * entry.Product.Quantity;
            }

            Order newOrder = new Order()
            {
                Cart = cart,
                User = cart.User,
                Total = total
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return newOrder;
        }


        /// <summary>
        ///     Returns the order associated with the given ID, or null if it does not exist.
        /// </summary>
        /// <param name="orderID"> ID of the desired order </param>
        /// <returns> Order associated with that ID </returns>
        public async Task<Order> GetOrder(int orderID)
        {
            return await _context.Orders.Include(o => o.Cart)
                                        .ThenInclude(c => c.CartEntries)
                                        .ThenInclude(ce => ce.Product)
                                        .FirstOrDefaultAsync(o => o.ID==orderID);
        }

        /// <summary>
        ///     Takes in a username, and returns all orders associated with that username. 
        /// </summary>
        /// <param name="userID"> Username </param>
        /// <returns> All orders that user has placed </returns>
        public async Task<IEnumerable<Order>> OrderHistory(string userID)
        {
            return await _context.Orders.Where(o => o.User == userID)
                                        .Include(o => o.Cart)
                                        .ThenInclude(c => c.CartEntries)
                                        .ThenInclude(ce => ce.Product)
                                        .ToListAsync();
        }
    }
}
