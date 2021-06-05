using System;
namespace WorldWideBasketball.Models
{
    public class Equipa
    {
        public Equipa(int id, string name, string localizacao, int liga_id)
        {
            this.Id = id;
            this.Name = name;
            this.Localizacao = localizacao;
            this.Liga_ID = liga_id;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Localizacao { get; set; }

        public int Liga_ID { get; set; }
    }
}
