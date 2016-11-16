using Games.Models.Entity;
using Igdb.ResponseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Games.Models.ViewModel {

    public class GameListView {
        public List<GameEntity> ListaJogos { get; set; }
        public string CoverUrl = "https://res.cloudinary.com/igdb/image/upload/t_cover_small_2x/";
    }

    public class GameResultView {
        public List<BuscaGameResponse> ListaJogos { get; set; }
    }

    public class GameDataView {
        public enum formato { Fisíco, Digital };
        GamesEntities db = new GamesEntities();

        [Key]
        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Informe o título")]
        public string Titulo { get; set; }

        [Display(Name = "Nota")]
        public decimal? Nota { get; set; }

        [Display(Name = "Preço")]
        public decimal? Preco { get; set; }

        [Display(Name = "Metacritic")]
        public int? Metacritic { get; set; }

        [Display(Name = "Completo")]
        public Boolean Completo { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Tamanho")]
        public decimal? Tamanho { get; set; }
        
        [Display(Name = "Formato")]
        public formato? Formato { get; set; }
        
        public List<platform> ListaPlatform {
            get {
                return db.platform.OrderBy(p=>p.ordem).ToList();
            }
        }

        public List<status> ListaStatus {
            get {
                return db.status.ToList();
            }
        }

        public List<region> ListaRegion {
            get {
                return db.region.ToList();
            }
        }

        public List<rating> ListaRating {
            get {
                return db.rating.ToList();
            }
        }

        public List<game_platform> platforms;
        public List<game_platform> Platforms {
            get {
                if (platforms == null) {
                    platforms = new List<game_platform>();
                }
                return platforms;
            }
            set {
                platforms = value;
            }
        }

        [Display(Name = "Loja")]
        public store Loja { get; set; }
        public List<store> ListaLoja {
            get {
                return db.store.ToList();
            }
        }

        [Display(Name = "Publisher(s)")]
        public List<developerPublisher> ListaPublisher { get; set; }

        [Display(Name = "Developer(s)")]
        public List<developerPublisher> ListaDeveloper { get; set; }
        
        public string CloudnaryId { get; set; }
    }

}