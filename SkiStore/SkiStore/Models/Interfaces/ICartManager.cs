using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.Interfaces
{
    public interface ICartManager
    {

        // Get all items in a user's cart.
        Task<IEnumerable<CartItem>> GetCartItems(string userID);

        // Gets a specific cart item.
        Task<CartItem> GetItem(string userID, int productID);

        // Add the given quantity of the given item to a user's cart.
        Task Add(string userID, int productID, int quantity);

        // Remove an item from the user's cart.
        Task Remove(string userID, int cartItemID);

        // Alter the quantity of an item in the user's cart.
        Task Update(string userID, int cartItemID, int newQuantity);
    }
}
