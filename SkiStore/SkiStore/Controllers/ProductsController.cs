using Microsoft.AspNetCore.Mvc;
using SkiStore.Interfaces;
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
        public IActionResult Index()
        {
            ProductsViewModel pvm = new ProductsViewModel();
            pvm.Products = _productMan.GetAllProducts();
            return View(pvm);
        }

    }
}
