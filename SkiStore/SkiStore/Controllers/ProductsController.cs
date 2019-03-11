using Microsoft.AspNetCore.Mvc;
using SkiStore.Interfaces;
using SkiStore.Models;
using SkiStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Controllers
{
    public class ProductsController : Controller
    {
        public IInventory _productMan { get; }

        public ProductsController(IInventory productMan)
        {
            _productMan = productMan;
        }


        [HttpGet]
        public IActionResult Index(string alertMsg = null)
        {
            ProductsViewModel pvm = new ProductsViewModel();
            pvm.Products = _productMan.GetAllProducts();
            if(alertMsg != null)
                pvm.AlertMessage = alertMsg;
            return View(pvm);
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            try
            {
                Product product = await _productMan.GetProduct(id);
                if(product != null)
                {
                    return View(new ProductsViewModel() { Product = product });
                } else
                {
                    return RedirectToAction("Index", "Products");
                }
            }
            catch (Exception e)
            {
                ErrorViewModel evm = new ErrorViewModel { ErrorMessage = e.Message };
                return RedirectToAction("Index", "Error", evm);
            }
        }
        

    }
}
