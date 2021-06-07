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

        public IActionResult LigaJogos(int id)
        {
            LigasDAO ligasDAO = new LigasDAO();
            Liga liga = ligasDAO.getLigaById(id);
            if (liga != null)
            {
                EquipaDAO equipaDAO = new EquipaDAO();
                liga.Equipas = equipaDAO.getEquipasByLeage(id);

                JogoDAO jogoDAO = new JogoDAO();
                Dictionary<int, List<Jogo>> dict = new Dictionary<int, List<Jogo>>();
                foreach(Equipa a in liga.Equipas)
                {
                    List<Jogo> jogos;
                    jogos = jogoDAO.getAllJogosByTeamId(a.Id);
                    dict.Add(a.Id, jogos);
                }

                liga.JogosEquipa = dict;

                model.setObject(liga);
                return View("LigaJogos", model);
            }
            else
            {
                return View("Ligas", model);
            }
        }

        public IActionResult LigaEquipas(int idLiga)
        {
            Console.WriteLine("[HomeController]: " + idLiga);

            List<Equipa> lista = new EquipaDAO().getEquipasByLeage(idLiga);
            if(lista != null)
            {
                Liga l = new LigasDAO().getLigaById(idLiga);
                if (l == null)
                    Console.WriteLine("MERDA");
                else
                    Console.WriteLine(l.Nome);
                model.setObject((l,lista));
                
                return View("LigaEquipas", model);
            }
            return View("Ligas",model);
            
        }

        public IActionResult GetTeams()
        {
            EquipaDAO equipaDAO = new EquipaDAO();
            equipaDAO.getEquipasByName("Bulls");
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
