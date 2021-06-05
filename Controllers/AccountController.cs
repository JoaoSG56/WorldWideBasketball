using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WorldWideBasketball.Models;
using WorldWideBasketball.DataAccess;

namespace WorldWideBasketball.Controllers

{
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        private LogingInfo model = new LogingInfo(false);
        
        // GET: Account
        [HttpGet]
        public IActionResult Login()
        {
            return View(model);
        }

        public IActionResult Register()
        {

            return View(model);
        }

        public IActionResult Index()
        {
            return View(model);   
        }

        public IActionResult Privacy()
        {
            return View(model);
        }

        //public IActionResult Logout()
        //{
        //    model.set(false);
        //    return RedirectToAction("Home", "Index", model);

        //}

        void connectionString()
        {
            //con.ConnectionString = "data source=127.0.0.1,1433; Database=WWB; User ID=;Password=MyPassword.1;";
            con.ConnectionString = "Data Source=DESKTOP-8CRGERR;Initial Catalog=WWB;Integrated Security=True";
        }

        [HttpPost]
        public ActionResult Verify(Account account)
        {

            connectionString();
            con.Open();

            com.Connection = con;
            com.CommandText = "select * from [Utilizador] where Email='" + account.Email + "' and Password='" + account.Password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                Console.WriteLine("CorreCCCto");
                dr.Close();
                con.Close();
                model.setLogged(true);
                model.setStatus(3);
                return RedirectToAction("Home", "Home",model);
            }
            else
            {
                Console.WriteLine("Wrong Mail/Password");
                dr.Close();
                con.Close();
                model.setStatus(5);
                return View("Login",model);
            }


        }

        [HttpPost]
        public ActionResult RegisterUser(Account account)
        {

            UserDAO user = new UserDAO();

            switch (user.Insert(account))
            {
                case -1:
                    model.setStatus(-1);
                    return View("Register", model);
                case 1:
                    model.setStatus(1);
                    return View("Login",model);
                case 2:
                    model.setStatus(2);
                    return View("Register", model);
                case 6:
                    model.setStatus(6);
                    return View("Register", model);
                case 7:
                    model.setStatus(7);
                    return View("Register",model);
                default:
                    model.setStatus(0);
                    return View("Register", model);
            }


            //if(account.Email == null || account.Email == "" || !account.Email.Contains("@"))
            //{
            //    model.setStatus(-1);
            //    return View("Register", model);
            //}

            //if(account.Password != account.ConfirmPassword)
            //{
            //    model.setStatus(2);
            //    return View("Register", model);
            //}

            //connectionString();
            //con.Open();

            //com.Connection = con;
            //com.CommandText = "select * from [Utilizador] where Email='" + account.Email + "'";
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    Console.WriteLine("User já Existente");
            //    dr.Close();
            //    con.Close();
            //    model.setStatus(7);
            //    return View("Register",model);
            //}
            //else
            //{
            //    dr.Close();
            //    Console.WriteLine("Registering ...");
            //    com.CommandText = "Insert into [Utilizador] VALUES('" + account.Username + "', '" + account.Password + "', '" + account.Data + "', '" + account.Email + "', '" + account.Name + "');";
            //    try
            //    {
            //        com.ExecuteNonQuery();
            //        Console.WriteLine("Sucesso");
            //        con.Close();
            //        model.setStatus(1);
            //        return View("Login",model);
            //    }
            //    catch (SqlException)
            //    {
            //        Console.WriteLine("Erro no Registo");
            //        con.Close();
            //        model.setStatus(6);
            //        return View("Register",model);
            //    }

            //}
        }
    }
}