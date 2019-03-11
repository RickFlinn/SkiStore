using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.Interfaces
{
    public interface ICartEntryManager
    {

        /// <summary>
        ///     Get all items in a user's cart.
        /// </summary>
        /// <param name="userID"> User's ID </param>
        /// <returns> All items in the user's Cart </returns>
        Task<IEnumerable<CartEntry>> GetCartItems(int cartID);

        /// <summary>
        ///     Get a cart item entry with the given ID
        /// </summary>
        /// <param name="cartItemID"> ID of cart item </param>
        /// <returns> Cart Item </returns>
        Task<CartEntry> GetItem(int cartID, int productID);

        
        /// <summary>
        ///     Updates the given CartEntry, or creates it if it does not already exist in the database.
        /// </summary>
        /// <param name="cartEntry"> CartEntry to create or update </param>
        /// <returns></returns>
        Task SaveItem(CartEntry cartEntry);

       
        /// <summary>
        ///     Remove the given cart item from a user's cart. 
        /// </summary>
        /// <param name="cartItemID"></param>
        /// <returns></returns>
        Task Remove(CartEntry cartEntry);

        
    }
}
