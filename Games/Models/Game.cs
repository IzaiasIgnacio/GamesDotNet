using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models {
    public class Game {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public decimal Nota { get; set; }
        public DateTime Lancamento { get; set; }
        public decimal Preco { get; set; }
        public decimal Metacritic { get; set; }
        public Boolean Completo { get; set; }
        public string Descricao { get; set; }
        public decimal Tamanho { get; set; }
        public enum formato { Fisíco, Digital };
        public string LinkGiantbomb { get; set; }
        public int Indice { get; set; }
        public Plataforma Plataforma { get; set; }
        public Loja Loja { get; set; }
        public Publisher Publisher { get; set; }
        public Developer Developer { get; set; }
        public Classificacao Classificacao { get; set; }
        public Situacao Situacao { get; set; }
    }
}