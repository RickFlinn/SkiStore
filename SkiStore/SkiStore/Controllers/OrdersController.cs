using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SkiStore.Models;
using SkiStore.Models.Interfaces;
using SkiStore.Models.Services;
using SkiStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SkiStore.Controllers
{
    [Authorize(Policy = "WaivedAdult")]
    public class OrdersController : Controller
    {
        private readonly UserManager<SkiStoreUser> _userManager;
        private readonly IOrderManager _orderMan;
        private readonly ICartManager _cartMan;
        private readonly IEmailSender _emailSender;
        private readonly IChargeCreditCards _biller;

        public OrdersController( UserManager<SkiStoreUser> userManager, ICartManager cartMan,
                                 IOrderManager orderMan, IEmailSender emailSender, IChargeCreditCards biller)
        {
            _userManager = userManager;
            _orderMan = orderMan;
            _cartMan = cartMan;
            _emailSender = emailSender;
            _biller = biller;
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

            return View("Payment", cvm);
        }

        /// <summary>
        ///     Takes in a user's payment details for the given Order. If the payment succeeds, sends the user a confirmation
        ///      email, deactivates(empties) the user's cart, and redirects them to a confirmation page.
        ///     If the payment did not succeed, redirects them to the previous checkout/payment page. 
        /// </summary>
        /// <param name="order"> Order to be checked out </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Confirm(Order order, string cardNumber, string expMonth, string expYear, string secCode)
        {
            SkiStoreUser user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return RedirectToAction("Login", "Account", "~/Cart/Index");

            order = await _orderMan.GetOrder(order.ID);

            //string testCardNumber = "4111111111111111"; 
            //string testExpDate = "1028";
            //string testCode = "901";
            string expDate = expMonth + expYear;

            Tuple<bool, string> chargeResponse = _biller.ChargeCard(cardNumber, expDate, secCode, order.Cart.CartEntries);

            // If the transaction fails, redirect the user to the previous billing view, with any available information on the failed transaction.
            if (chargeResponse.Item1 == false)
            {
                CheckoutViewModel cvm = new CheckoutViewModel()
                {
                    Order = order,
                    AlertMessage = chargeResponse.Item2
                };
                return View("Payment", cvm);
            }

            StringBuilder message = new StringBuilder();
            message.AppendLine($"Thanks for your business, {user.FirstName}! Your order number is {order.ID}.");
            message.AppendLine("<table><tr><th>Product</th><th>Quantity</th><th>Price</th></tr>");
            foreach (CartEntry entry in order.Cart.CartEntries)
            {
                message.AppendLine($"<tr><td>{entry.Product.Name}</td><td>{entry.Quantity}</td><td>{entry.Product.Price}</td></tr>");
            }
            message.Append("</table>");
            message.AppendLine($"Order Total: ${order.Total}");

            await _emailSender.SendEmailAsync(user.Email, "Your Order", message.ToString());
            
            Cart cart = await _cartMan.GetActiveCart(user.UserName);
            await _cartMan.Deactivate(cart.ID);

            return View("Confirmed", order);
        }
        
    }
}
