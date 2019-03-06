﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SkiStore.Models;
using SkiStore.Models.Interfaces;
using SkiStore.Models.Services;
using SkiStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<SkiStoreUser> _userManager;
        private readonly IOrderManager _orderMan;
        private readonly ICartManager _cartMan;
        private readonly IEmailSender _emailSender;

        public OrdersController( UserManager<SkiStoreUser> userManager, ICartManager cartMan,
                                 IOrderManager orderMan, IEmailSender emailSender )
        {
            _userManager = userManager;
            _orderMan = orderMan;
            _cartMan = cartMan;
            _emailSender = emailSender;
        }

        /// <summary>
        ///   Takes users to a Checkout page, where they can view the items they are being charged for, select shipping options,
        ///   and enter payment details.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            SkiStoreUser user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return RedirectToAction("Login", "Account", "~/Cart/Index");

            Cart cart = await _cartMan.GetActiveCart(user.UserName);

            Order order = await _orderMan.Checkout(cart.ID);

            CheckoutViewModel cvm = new CheckoutViewModel() { Order = order };

            return View(cvm);
        }

        /// <summary>
        ///     Takes in a user's payment details for the given Order. If the payment succeeds, sends the user a confirmation
        ///      email, deactivates(empties) the user's cart, and redirects them to a confirmation page.
        ///     If the payment did not succeed, redirects them to the previous checkout/payment page. 
        /// </summary>
        /// <param name="order"> Order to be checked out </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Confirm(Order order)
        {

            SkiStoreUser user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return RedirectToAction("Login", "Account", "~/Cart/Index");

            
            //future TODO: logic for payment
            bool isPaid = true;

            if (!isPaid)
            {
                CheckoutViewModel cvm = new CheckoutViewModel()
                {
                    Order = await _orderMan.GetOrder(order.ID),
                    AlertMessage = "Payment could not be processed."
                };
                return View("Orders/Checkout", cvm);
            }

            string message = $"Thanks for your business, {user.FirstName}! Your order number is {order.ID}.";

            await _emailSender.SendEmailAsync(user.Email, "Your Order", message);

            Cart cart = await _cartMan.GetActiveCart(user.UserName);
            await _cartMan.Deactivate(cart.ID);

            return View("Confirmed");
        }
        
    }
}