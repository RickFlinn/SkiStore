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
        private readonly ICartEntryManager _cartEntries;
        private readonly ICartManager _cartMan;
        private readonly UserManager<SkiStoreUser> _userManager;

        public CartController(ICartEntryManager cartEntries, ICartManager cartMan, UserManager<SkiStoreUser> userManager)
        {
            _cartEntries = cartEntries;
            _userManager = userManager;
            _cartMan = cartMan;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            CartViewModel cvm = new CartViewModel();

            try
            {
                SkiStoreUser user = await _userManager.GetUserAsync(HttpContext.User);

                if (user == null)
                    return RedirectToAction("Login", "Account", "~/Cart/Index");

                string userID = user.Id;

                cvm.Cart = await _cartMan.GetActiveCart(userID);

            }
            catch (Exception e)
            {
                cvm.ErrorMessage = e.Message;
                cvm.Cart = new Cart();


            }
            return View(cvm);
        }

        [HttpPost]
        public async Task<IActionResult> SaveToCart(CartViewModel cvm)
        {
            try
            {
                await _cartEntries.SaveItem(cvm.ItemEntry);

                return RedirectToAction("Index", "Cart");
            }
            catch (Exception e)
            {
                if(cvm == null)
                    return RedirectToAction("Index", "Cart");

                cvm.ErrorMessage = e.Message;

                return RedirectToAction("Index", "Cart", cvm);
            }
            
        }

    }
}
