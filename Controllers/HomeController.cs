using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorldWideBasketball.Models;
using WorldWideBasketball.DataAccess;

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

        public IActionResult Home()
        {
            LigasDAO ligasDAO = new LigasDAO();
            
            model.setObject(ligasDAO.getAllLigas());
            return View("Ligas", model);
        }

        public IActionResult Privacy()
        {
            return View(model);
        }

        public IActionResult Liga(int id)
        {
            LigasDAO ligasDAO = new LigasDAO();
            Liga liga = ligasDAO.getLigaById(id);
            if (liga != null)
            {
                EquipaDAO equipaDAO = new EquipaDAO();
                liga.Equipas = equipaDAO.getEquipaByLeage(id);

                model.setObject(liga);
                return View("Liga", model);
            }
            else
            {
                return View("Ligas", model);
            }
        }

        public IActionResult GetTeams()
        {
            EquipaDAO equipaDAO = new EquipaDAO();
            equipaDAO.getEquipaByName("Bulls");
            return View("Home", model);
        }


        public IActionResult Logout()
        {
            Console.WriteLine("LOGING OUT");
            model.setLogged(false);
            model.setStatus(4);
            return RedirectToAction("Index", "Account", model);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
