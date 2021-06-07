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

        public IActionResult Ligas()
        {
            if (model == null) Console.WriteLine("AAAAAAAA");
            if (!model.getLogged()) return RedirectToAction("Error", "Account", model);
            Console.WriteLine(model.getLogged());
            LigasDAO ligasDAO = new LigasDAO();
            model.setObject(ligasDAO.getAllLigas());
            return View("Ligas", model);
        }

        public IActionResult Home()
        {
            if (!model.getLogged()) return Error();
            return View("Home", model);
        }

        public IActionResult Privacy()
        {
            if (!model.getLogged()) return Error();
            return View(model);
        }

        public IActionResult LigaJogos(int id)
        {
            if (!model.getLogged()) return RedirectToAction("Error", "Account", model);
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


        public IActionResult LigaEquipas(int id)
        {
            if (!model.getLogged()) return RedirectToAction("Error", "Account", model);
            List<Equipa> lista = new EquipaDAO().getEquipasByLeage(id);
            if (lista != null)
            {
                Liga l = new LigasDAO().getLigaById(id);

                model.setObject((l, lista));

                return View("LigaEquipas", model);
            }
            return View("Ligas", model);

        }

        public IActionResult Equipa(int id)
        {
            if (!model.getLogged()) return RedirectToAction("Error", "Account", model);
            Equipa equipa = new EquipaDAO().getEquipaById(id);
            Estatistica estatistica = new EstatisticaDAO().getEstatisticaByID(id);

            if (equipa != null && estatistica != null)
            {
                model.setObject((equipa, estatistica));
                return View("Equipa", model);
            }
            return View("Home",model);

        }

        [HttpPost]
        public IActionResult Search(string key)
        {
            if (!model.getLogged()) return RedirectToAction("Error", "Account", model);
            Dictionary<string, Object> values = new Dictionary<string, Object>();

            values["ligas"] = (Object) new LigasDAO().getLigasLike(key);
            values["equipas"] = (Object)new EquipaDAO().getEquipasLike(key);

            model.setObject(values);
            return View("Search", model);
        }


        public IActionResult Logout()
        {
            if (!model.getLogged()) return RedirectToAction("Error", "Account", model);
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
