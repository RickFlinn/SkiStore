using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SkiStore.Models;
using SkiStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<SkiStoreUser> _userManager { get; }
        private SignInManager<SkiStoreUser> _signInManager { get; set; }
        private IEmailSender _emailSender;


        public AccountController(UserManager<SkiStoreUser> userManager, SignInManager<SkiStoreUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     Returns the register/signup view. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register() => View();

        /// <summary>
        ///     Takes in user registration information and creates a new User in the Identity user database, with name, date of birth, and waiver claims.
        /// </summary>
        /// <param name="regVModel"> View model containing user registration data </param>
        /// <returns> Home Index view if successful, or redirects to Register view </returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regVModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    regVModel.ErrorMessage = "Ya done goofed. How hard is it to fill in all the forms, huh?";
                    return View(regVModel);
                }

                if (!regVModel.AgreedToWaiver)
                {
                    regVModel.ErrorMessage = "Please read and agree to all terms and conditions.";
                    return View(regVModel);
                }

                SkiStoreUser newUser = new SkiStoreUser();
                newUser.FirstName = regVModel.FirstName;
                newUser.LastName = regVModel.LastName;
                newUser.Email = regVModel.Email;
                newUser.PhoneNumber = regVModel.PhoneNumber;
                newUser.UserName = regVModel.UserName;
                newUser.AgreedToWaiver = regVModel.AgreedToWaiver.ToString();
                newUser.DateOfBirth = regVModel.DateOfBirth;

                IdentityResult userCreate = await _userManager.CreateAsync(newUser, regVModel.Password);

                if (userCreate.Succeeded)
                {
                    Claim claimToTheName = new Claim("FullName", $"{newUser.FirstName} {newUser.LastName}");
                    Claim dobClaim = new Claim("DateOfBirth",
                                               new DateTime(newUser.DateOfBirth.Year,
                                                            newUser.DateOfBirth.Month,
                                                            newUser.DateOfBirth.Day
                                                            ).ToString("u"),
                                               ClaimValueTypes.DateTime);

                    Claim waiverClaim = new Claim("AgreedToWaiver", newUser.AgreedToWaiver);

                    await _userManager.AddClaimsAsync(newUser, new Claim[] { claimToTheName, dobClaim, waiverClaim });


                    // Send the user an email
                    StringBuilder sb = new StringBuilder();

                    sb.Append($"<p>Hello {newUser.FirstName} {newUser.LastName},<br>");
                    sb.AppendLine("Thank you for joining SkiStore!</p>");

                    // Get the user's email
                    await _emailSender.SendEmailAsync(regVModel.Email, "Thank you for Registering!", sb.ToString());
                    var ourUser = await _userManager.FindByEmailAsync(regVModel.Email);
                    string id = ourUser.Id;

                    await _signInManager.SignInAsync(newUser, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string errs = "";
                    foreach (var error in userCreate.Errors)
                    {
                        errs += $"{error.Description}. ";
                    }
                    regVModel.ErrorMessage = errs;
                    return View(regVModel);

                }
            }
            catch (Exception e)
            {
                regVModel.ErrorMessage = e.Message;
                return View(regVModel);
                //RedirectToAction("Index", "Error", new ErrorViewModel(e.Message));
            }
        }


        /// <summary>
        ///     Directs users to the Login page. Accepts a return URL to take the user back to once they've logged in. 
        /// </summary>
        /// <param name="returnUrl"> URL to return to after successful login attempt </param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl });

        /// <summary>
        ///     Attempts to log in user with specified login info. If the attempt succeeds, returns the user to the view where they attempted to log
        ///     in from, or the Home view if a return URL was not supplied. 
        ///     If the user's login attempt fails, redirects to the login page with an appropriate alert message. 
        /// </summary>
        /// <param name="lvm"> ViewModel containing entered username and password </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            LoginViewModel lvm = new LoginViewModel()
            {
                UserName = userName,
                Password = password,
                ReturnUrl = returnUrl
            };

            try
            {
                
                if (ModelState.IsValid)
                {
                    Microsoft.AspNetCore.Identity.SignInResult
                        result = await _signInManager.PasswordSignInAsync(userName, password, false, false);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                            return LocalRedirect(returnUrl);

                        else
                            return RedirectToAction("Index", "Home");

                    } else
                    {
                        
                        if (result.IsNotAllowed)
                        {
                            lvm.AlertMessage = "User is not allowed to sign in.";
                        }
                        else if (result.IsLockedOut)
                        {
                            lvm.AlertMessage = "User locked out.";
                        }
                        else if (result.RequiresTwoFactor)
                        {
                            lvm.AlertMessage = "Two-factor verification required.";
                        }
                        else
                        {
                            lvm.AlertMessage = "The given username or password was incorrect.";
                        }
                    }
                }
                else
                {
                    lvm.AlertMessage = "You must enter both a username and a password, NIMWIT.";
                }
                
            }
            catch (Exception e)
            {
                lvm.AlertMessage = e.Message;
                
            }

            return View(lvm);
        }

        /// <summary>
        ///     Logs out the current user. Redirects to Home view, or return url if supplied 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            if (!string.IsNullOrEmpty(returnUrl))
                return LocalRedirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }

    }
}
