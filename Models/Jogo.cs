using System;
namespace WorldWideBasketball.Models
{
    public class Jogo
    {
        public Jogo(int id, string data, string hora, string resultado, int equipa_casa_id, int equipa_vis_id)
        {
            this.Id = id;
            this.Data = data;
            this.Hora = hora;
            this.Resultado = resultado;
            this.Equipa_Casa = equipa_casa_id;
            this.Equipa_Vis = equipa_vis_id;
        }

        public int Id { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Resultado { get; set; }
        public int Equipa_Casa { get; set; }
        public int Equipa_Vis { get; set; }
    }
}
