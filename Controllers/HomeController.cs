using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorldWideBasketball.Models;

namespace WorldWideBasketball.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private LogingInfo model = new LogingInfo(true);

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    LogingInfo model = new LogingInfo(false);
        //    return View(model);
        //}

        public IActionResult Home()
        {
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View(model);
        }

        //public IActionResult Login()
        //{
        //    Console.WriteLine("VAI PARA ACCOUNT\n\n");
        //    return View("Login");
        //}

        public IActionResult Logout()
        {
            Console.WriteLine("LOGING OUT");
            model.setLogged(false);
            model.setStatus(4);
            return RedirectToAction("Index", "Account",model);

        }

        //public IActionResult Register()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
