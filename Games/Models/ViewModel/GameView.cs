using Games.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Games.Models.ViewModel {

    public class GameListView {
        public List<GameEntity> ListaJogos { get; set; }
    }

    public class GameDataView {
        [Key]
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
        public List<platform> ListaPlatform { get; set; }
        public List<store> ListaLoja { get; set; }
        public List<developerPublisher> ListaPublisher { get; set; }
        public List<developerPublisher> ListaDeveloper { get; set; }
        public List<rating> ListaClassificacao { get; set; }
        //public List<situacao> ListaSituacao { get; set; }
    }

}