using System;
using System.Collections.Generic;

namespace WorldWideBasketball.Models
{
    public class Liga
    {
        public Liga(int id, string nome, string localizacao)
        {
            this.Id = id;
            this.Nome = nome;
            this.Localizacao = localizacao;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Localizacao { get; set; }

        public List<Equipa> Equipas { get; set; }

        public string getString()
        {
            return this.Id + ": " + this.Nome + ", " + this.Localizacao;
        }
    }
}
