using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkiStore.Models;
using SkiStore.Models.Interfaces;
using SkiStore.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace SkiStore.Controllers
{
    [Authorize(Policy = "WaivedAdult")]
    public class CartController : Controller
    {
        private readonly ICartEntryManager _cartEntries;
        private readonly ICartManager _cartMan;
        private readonly UserManager<SkiStoreUser> _userManager;

        public CartController(ICartEntryManager cartEntries, ICartManager cartMan, UserManager<SkiStoreUser> userManager)
        {
            _cartEntries = cartEntries;
            _userManager = userManager;
            _cartMan = cartMan;
        }

        
        /// <summary>
        ///     When accessed, sends the user a view displaying the user's current active cart.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            CartViewModel cvm = new CartViewModel();

            try
            {
                SkiStoreUser user = await _userManager.GetUserAsync(HttpContext.User);

                if (user == null)
                    return RedirectToAction("Login", "Account", "~/Cart/Index");


                cvm.Cart = await _cartMan.GetActiveCart(user.UserName);

                return View(cvm);
            }
            catch (Exception e)
            {
                ErrorViewModel errModel = new ErrorViewModel();
                errModel.ErrorMessage = e.Message;
                return RedirectToAction("Index", "Error", errModel);
            }
        }

        /// <summary>
        ///     Adds or updates an item to the user's cart. Takes in the product associated with the cart entry and the desired quantity.
        ///     
        ///     This endpoint is also used to "remove" items from the cart by setting its quantity to zero. 
        ///     
        ///     Redirects to the user's Cart overview when successful.
        /// </summary>
        /// <param name="cvm"> Cart ViewModel. Mostly used to access the ID </param>
        /// <returns> Redirects to Index action of Cart controller </returns>
        [HttpPost]
        public async Task<IActionResult> SaveToCart(Product product, int quantity = 1)
        {
            try
            {
                SkiStoreUser user = await _userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                    return RedirectToAction("Login", "Account", "~/Cart/Index");

                Cart activeCart = await _cartMan.GetActiveCart(user.UserName);

                CartEntry entry = await _cartEntries.GetItem(activeCart.ID, product.ID);


                if(entry == null)
                {
                    entry = new CartEntry()
                    {
                        ProductID = product.ID,
                        CartID = activeCart.ID
                    };
                }

                entry.Quantity = quantity;

                await _cartEntries.SaveItem(entry);

                return RedirectToAction("Index", "Cart");


            } catch (Exception e)
            {
                ErrorViewModel errModel = new ErrorViewModel();
                errModel.ErrorMessage = e.Message;
                return RedirectToAction("Index", "Error", errModel);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Checkout(int cartID)
        {

        }

    }
}
