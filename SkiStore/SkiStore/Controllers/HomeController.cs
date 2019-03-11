using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        /// <summary>
        ///     Return view of home page.
        /// </summary>
        /// <returns> Home index view </returns>
        [HttpGet]
        public IActionResult Index() => View();


    }
}
