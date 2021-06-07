using System;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WorldWideBasketball.Models;

namespace WorldWideBasketball.DataAccess
{
	public class UserDAO
	{

        private Connection connection = new Connection();

        public bool isValid(Account account)
        {
            this.connection.open();
            string query = "select * from [Utilizador] where Email='" + account.Email + "' and Password='" + account.Password + "'";
            SqlDataReader dr = this.connection.executeReader(query);

            if (dr.Read())
            {
                dr.Close();
                this.connection.close();
                return true;
            }
            else
            {
                dr.Close();
                this.connection.close();
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

            this.connection.open();

            string query = "select * from [Utilizador] where Email='" + account.Email + "'";
            SqlDataReader dr = this.connection.executeReader(query);
            if (dr.Read())
            {
                Console.WriteLine("User já Existente");
                dr.Close();
                this.connection.close();
                return 7;
            }
            else
            {
                dr.Close();
                Console.WriteLine("Registering ...");
                query = "Insert into [Utilizador] VALUES('" + account.Username + "', '" + account.Password + "', '" + account.Data + "', '" + account.Email + "', '" + account.Name + "');";
                try
                {
                    this.connection.executeQuery(query);
                    Console.WriteLine("Sucesso");
                    this.connection.close();
                    return 1;
                }
                catch (SqlException)
                {
                    Console.WriteLine("Erro no Registo");
                    this.connection.close();
                    return 6;
                }

            }
        }
	}
}

