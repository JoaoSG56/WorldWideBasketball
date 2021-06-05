using System;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WorldWideBasketball.Models;

namespace WorldWideBasketball.DataAccess
{
	public class UserDAO
	{

        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        private void connectionString()
        {
            con.ConnectionString = "data source=127.0.0.1,1433; Database=WWB; User ID=SA;Password=MyPassword.1;";
            //con.ConnectionString = "Data Source=DESKTOP-8CRGERR;Initial Catalog=WWB;Integrated Security=True";
        }


        public bool isValid(Account account)
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
                return true;
            }
            else
            {
                Console.WriteLine("Wrong Mail/Password");
                dr.Close();
                con.Close();
                return false;
            }
        }

        public int Insert(Account account)
        {
            if (account.Email == null || account.Email == "" || !account.Email.Contains("@"))
            {
                return -1;
            }

            if (account.Password != account.ConfirmPassword)
            {
                return 2;
            }

            connectionString();
            con.Open();

            com.Connection = con;
            com.CommandText = "select * from [Utilizador] where Email='" + account.Email + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                Console.WriteLine("User já Existente");
                dr.Close();
                con.Close();
                return 7;
            }
            else
            {
                dr.Close();
                Console.WriteLine("Registering ...");
                com.CommandText = "Insert into [Utilizador] VALUES('" + account.Username + "', '" + account.Password + "', '" + account.Data + "', '" + account.Email + "', '" + account.Name + "');";
                try
                {
                    com.ExecuteNonQuery();
                    Console.WriteLine("Sucesso");
                    con.Close();
                    return 1;
                }
                catch (SqlException)
                {
                    Console.WriteLine("Erro no Registo");
                    con.Close();
                    return 6;
                }

            }
        }
	}
}

