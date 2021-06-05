using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WorldWideBasketball.Models;
namespace WorldWideBasketball.DataAccess
{
    public class LigasDAO

    {
        private Connection connection = new Connection();

        public List<Liga> getAllLigas()
        {
            List<Liga> lista = new List<Liga>();
            this.connection.open();
            string query = "select * from [Liga];";
            SqlDataReader dr = this.connection.executeReader(query);

            // Call Read before accessing data.
            while (dr.Read())
            {
                lista.Add(ReadSingleRow((IDataRecord)dr));
            }
            dr.Close();
            this.connection.close();
            return lista;
        }

        public Liga getLigaById(int id)
        {
            this.connection.open();
            string query = "select * from [Liga] where id='" + id + "';";
            SqlDataReader dr = this.connection.executeReader(query);
            Liga r = null;
            if (dr.Read())
            {
                r = ReadSingleRow((IDataRecord)dr);
            }
            dr.Close();
            this.connection.close();
            return r;
        }

        private Liga ReadSingleRow(IDataRecord record)
        {
            return new Liga(Int32.Parse(record[0].ToString()), record[1].ToString(), record[2].ToString());

        }
    }
}