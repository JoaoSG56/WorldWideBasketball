using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WorldWideBasketball.Models;

namespace WorldWideBasketball.Controllers

{
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;


        // GET: Account
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        void connectionString()
        {
            con.ConnectionString = "data source=127.0.0.1,1433; Database=WWB; User ID=SA;Password=MyPassword.1;";
        }

        [HttpPost]
        public ActionResult Verify(Account account)
        {

            connectionString();
            con.Open();

            com.Connection = con;
            com.CommandText = "select * from Utilizador where Email='" + account.Email + "' and Password='" + account.Password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                Console.WriteLine("CorreCCCto");
                dr.Close();
                con.Close();
                return View("Home_Logged");
            }
            else
            {
                Console.WriteLine("Wrong Mail/Password");
                dr.Close();
                con.Close();
                return View("Register");
            }


        }

        [HttpPost]
        public ActionResult RegisterUser(Account account)
        {
            connectionString();
            con.Open();

            com.Connection = con;
            com.CommandText = "select * from Utilizador where Email='" + account.Email + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                Console.WriteLine("User já Existente");
                dr.Close();
                con.Close();
                return View();
            }
            else
            {
                dr.Close();
                Console.WriteLine("Registering ...");
                com.CommandText = "Insert into Utilizador VALUES('" + account.Username + "', '" + account.Password + "', '" + account.Data + "', '" + account.Email + "', '" + account.Name + "');";
                try
                {
                    com.ExecuteNonQuery();
                    Console.WriteLine("Sucesso");
                    con.Close();
                    return View("Login");
                }
                catch (SqlException)
                {
                    Console.WriteLine("Erro no Registo");
                    con.Close();
                    return View();
                }

            }
        }
    }
}