using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiStore.Data;
using SkiStore.Models;
using SkiStore.Models.Interfaces;
using SkiStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Controllers
{
    [Authorize(Policy = "WaivedAdult")]
    public class CartController : Controller
    {
        public ICartManager _cartMan { get; }
        public UserManager<SkiStoreUser> _userManager { get; }

        public CartController(ICartManager cartMan, UserManager<SkiStoreUser> userManager)
        {
            _cartMan = cartMan;
            _userManager = userManager;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SkiStoreUser user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return RedirectToAction("Login", "Account", "~/Cart/Index");

            string userID = user.Id;
            CartViewModel cvm = new CartViewModel() { CartItems = await _cartMan.GetCartItems(userID) };

            return View(cvm);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(CartViewModel cvm)
        {
            SkiStoreUser user = await _userManager.GetUserAsync(HttpContext.User);
            await _cartMan.Add(user.Id, cvm.Item.ID, cvm.Quantity);

            return RedirectToAction("Index", "Cart");
        }

        [HttpPut]
        public async Task<IActionResult> Update(CartViewModel cvm)
        {
            SkiStoreUser user = await _userManager.GetUserAsync(HttpContext.User);
            await _cartMan.Update(user.Id, cvm.Item.ID, cvm.Quantity);

            return RedirectToAction("Index", "Cart");
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int productID)
        {
            SkiStoreUser user = await _userManager.GetUserAsync(HttpContext.User);
            CartItem ci = await _cartMan.GetItem(user.Id, productID);

            if(ci != null)
                await _cartMan.Remove(user.Id, productID);

            return RedirectToAction("Index", "Cart");
        }
    }
}
