using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkiStore.Models;
using SkiStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkiStore.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<SkiStoreUser> _userManager { get; }
        private SignInManager<SkiStoreUser> _signInManager { get; set; }


        public AccountController(UserManager<SkiStoreUser> userManager, SignInManager<SkiStoreUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regVModel)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    regVModel.ErrorMessage = "Ya done goofed. How hard is it to fill in all the forms, huh?";
                    return View(regVModel);
                }

                if(!regVModel.AgreedToWaiver)
                {
                    regVModel.ErrorMessage = "Please read and agree to all terms and conditions.";
                    return View(regVModel);
                }

                SkiStoreUser newUser = new SkiStoreUser();
                    newUser.FirstName =   regVModel.FirstName;
                    newUser.LastName =    regVModel.LastName;
                    newUser.Email =       regVModel.Email;
                    newUser.PhoneNumber = regVModel.PhoneNumber;
                    newUser.UserName =    regVModel.UserName;
                    newUser.AgreedToWaiver = regVModel.AgreedToWaiver.ToString();
                    newUser.DateOfBirth = regVModel.DateOfBirth;

                IdentityResult userCreate = await _userManager.CreateAsync(newUser, regVModel.Password);

                if (userCreate.Succeeded)
                {
                    Claim claimToTheName = new Claim("FullName", $"{newUser.LastName}, {newUser.FirstName}");
                    Claim dobClaim = new Claim("DateOfBirth",
                                               new DateTime(newUser.DateOfBirth.Year,
                                                            newUser.DateOfBirth.Month,
                                                            newUser.DateOfBirth.Day
                                                            ).ToString("u"),
                                               ClaimValueTypes.DateTime);
                    
                    Claim waiverClaim = new Claim("AgreedToWaiver", newUser.AgreedToWaiver);

                    await _userManager.AddClaimsAsync(newUser, new Claim[] { claimToTheName, dobClaim, waiverClaim });


                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    
                    return RedirectToAction("Index", "Home");
                } else
                {
                    string errs = "";
                    foreach(var error in userCreate.Errors)
                    {
                        errs += $"{error.Description}. ";
                    }
                    regVModel.ErrorMessage = errs;
                    return View(regVModel);
                    
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
                //RedirectToAction("Index", "Error", new ErrorViewModel(e.Message));
            }
        }


        [HttpGet]
        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Microsoft.AspNetCore.Identity.SignInResult 
                        result = await _signInManager.PasswordSignInAsync(lvm.UserName, lvm.Password, false, false);

                    if(result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(lvm.ReturnUrl))
                            return LocalRedirect(lvm.ReturnUrl);

                        else
                            return RedirectToAction("Index", "Home");

                    } else
                    {
                        lvm.AlertMessage = "The given username or password was incorrect.";
                        return View(lvm);
                    }
                } else
                {
                    lvm.AlertMessage = "You must enter both a username and a password, NIMWIT.";
                    return View(lvm);
                }
            }
            catch (Exception e)
            {
                lvm.AlertMessage = e.Message;
                return View(lvm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOut(string returnUrl)
        {
            await _signInManager.SignOutAsync();

            if (!string.IsNullOrEmpty(returnUrl))
                return LocalRedirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }
        
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
