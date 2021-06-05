using System;
using System.Collections.Generic;
using WorldWideBasketball.Models;
using System.Data.SqlClient;
using System.Data;

namespace WorldWideBasketball.DataAccess
{
    public class EquipaDAO
    {
        private Connection connection = new Connection();

        public List<Equipa> getEquipaByName(string name)
        {
            string query = "select * from [Equipa] where Nome Like '%" + name + "%';";
            return getAllTeamsByQuery(query);
        }

        public List<Equipa> getEquipaByLeage(int leagueID)
        {
            string query = "select * from [Equipa] where Liga_Id='"+ leagueID + "';";
            return getAllTeamsByQuery(query);   
        }



        private List<Equipa> getAllTeamsByQuery(string query)
        {
            List<Equipa> lista = new List<Equipa>();
            this.connection.open();
            SqlDataReader dr = this.connection.executeReader(query);

            while (dr.Read())
            {
                lista.Add(ReadSingleRow((IDataRecord)dr));
            }
            dr.Close();
            this.connection.close();
            return lista;
        }

        private Equipa ReadSingleRow(IDataRecord record)
        {
            return new Equipa(Int32.Parse(record[0].ToString()), record[1].ToString(), record[2].ToString(),Int32.Parse(record[3].ToString()));
        }
    }
}
