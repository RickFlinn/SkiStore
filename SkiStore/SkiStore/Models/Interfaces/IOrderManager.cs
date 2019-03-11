using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.Interfaces
{
    public interface IOrderManager
    {

        /// <summary>
        ///     Takes in the ID of a cart, builds a new Order, populates it with OrderEntries for each CartItem,
        ///     and returns the newly constructed Order. 
        /// </summary>
        /// <param name="cartID"> ID of the cart to convert into an Order </param>
        /// <returns> Newly created Order </returns>
        Task<Order> Checkout(int cartID);

        /// <summary>
        ///     Retrieves all Orders associated with the given username.
        /// </summary>
        /// <param name="userID"> Username of user we want to display past orders for </param>
        /// <returns> Enumerable collection of the orders that user has placed </returns>
        Task<IEnumerable<Order>> OrderHistory(string userID);

        /// <summary>
        ///     Gets an Order by its ID.
        /// </summary>
        /// <param name="orderID"> ID of desired Order </param>
        /// <returns> Order associated with that ID, or null. </returns>
        Task<Order> GetOrder(int orderID);
    }
}
