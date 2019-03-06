using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.Interfaces
{
    public interface ICartManager
    {
        Task<Cart> GetCart(int cartID);

        Task<Cart> GetActiveCart(string userID);

        Task<IEnumerable<Cart>> GetAllCarts();

        Task<IEnumerable<Cart>> GetUsersCarts(string userID);

        Task Deactivate(int cartID);
        
        
    }
}
