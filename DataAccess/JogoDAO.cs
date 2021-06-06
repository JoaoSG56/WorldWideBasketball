using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WorldWideBasketball.Models;
namespace WorldWideBasketball.DataAccess
{
    public class JogoDAO

    {
        private Connection connection = new Connection();


        public Dictionary<int, List<Jogo>> getAllJogos()
        {
            Dictionary<int, List<Jogo>> dict = new Dictionary<int, List<Jogo>>();

            this.connection.open();
            string query = "select * from [Jogo];";
            SqlDataReader dr = this.connection.executeReader(query);

            // Call Read before accessing data.
            while (dr.Read())
            {
                Jogo j = ReadSingleRow((IDataRecord)dr);
                if (!dict.ContainsKey(j.Id))
                {
                    List<Jogo> list = new List<Jogo>();
                    list.Add(j);
                    dict.Add(j.Id, list);
                }
                else
                {
                    dict[j.Id].Add(j);
                }
            }
            dr.Close();
            this.connection.close();
            return dict;
        }

        public List<Jogo> getAllJogosByTeamId(int id)
        {
            this.connection.open();
            string query = "select * from [Jogo] where equipa_casa='" + id + "'OR equipa_visitante= '" + id + "';";
            SqlDataReader dr = this.connection.executeReader(query);
            List<Jogo> r = new List<Jogo>();
            while (dr.Read())
            {
                Jogo j = ReadSingleRow((IDataRecord)dr);
                r.Add(j);
            }
            dr.Close();
            this.connection.close();
            return r;
        }

        private Jogo ReadSingleRow(IDataRecord record)
        {
            return new Jogo(Int32.Parse(record[0].ToString()), record[1].ToString(), record[2].ToString(),record[3].ToString(), Int32.Parse(record[4].ToString()), Int32.Parse(record[5].ToString()));

        }
    }
}