using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorldWideBasketball.Models;
using WorldWideBasketball.DataAccess;
using MapIntegration.Models;
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

        public IActionResult Ligas(string id)
        {
            LigasDAO ligasDAO = new LigasDAO();
            if (id == null)
            {

                model.setObject(ligasDAO.getAllLigas());
                return View("Ligas", model);
            }
            else
            {
                Console.WriteLine(id);
                List<Liga> ligas = ligasDAO.getLigasByCountry(id);
                if (ligas.Any())
                {
                    model.setObject(ligasDAO.getLigasByCountry(id));
                    return View("Ligas", model);
                }
                else
                    return Home();
            }
        }
        


        public IActionResult Home()
        {

            LocationLists modelaux = new LocationLists();
            var locations = new List<Locations>()
            {
                new Locations(1, "Bhubaneswar","Bhubaneswar, Odisha", "20.296059", "85.824539"),
                new Locations(2, "Hyderabad","Hyderabad, Telengana", "17.387140", "78.491684"),
                new Locations(3, "Bengaluru","Bengaluru, Karnataka", "12.972442", "77.580643")
            };
            modelaux.LocationList = locations;
            model.setObject(modelaux);
            return View("Home", model);
        }

        public IActionResult Privacy()
        {
            if (!model.getLogged()) return Error();
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


        public IActionResult LigaEquipas(int id)
        {
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
   
            Dictionary<string, Object> values = new Dictionary<string, Object>();

            values["ligas"] = (Object) new LigasDAO().getLigasLike(key);
            values["equipas"] = (Object)new EquipaDAO().getEquipasLike(key);

            model.setObject(values);
            return View("Search", model);
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
