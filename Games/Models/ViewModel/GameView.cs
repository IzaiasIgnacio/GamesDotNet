using Games.Models.Entity;
using Games.Models.Repository;
using Igdb.ResponseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Games.Models.ViewModel {

    public class BasegameView {
        public string CloudnaryUrl;
        public string BigCoverUrl;
        public string MicroCoverUrl;
        public string SmallCoverUrl;
        public GamesEntities db;
        public GameRepository gameRepository;

        public BasegameView() {
            db = new GamesEntities();
            gameRepository = new GameRepository();
            CloudnaryUrl = "https://res.cloudinary.com/igdb/image/upload/t_";
            BigCoverUrl = CloudnaryUrl + "cover_big/";
            MicroCoverUrl = CloudnaryUrl + "micro/";
            SmallCoverUrl = CloudnaryUrl + "cover_small_2x/";
        }
    }

    public class GameListView : BasegameView {
        private List<GameEntity> listaJogos;
        public List<GameEntity> ListaJogos {
            get {
                if (listaJogos == null) {
                    listaJogos = new List<GameEntity>();
                }
                return listaJogos;
            }
            set {
                listaJogos = value;
            }
        }
    }

    public class GameResultView : BasegameView {
        public List<BuscaGameResponse> ListaJogos { get; set; }
    }

    public class GameDataView : BasegameView {
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

        public enum formato { Fisíco, Digital };
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

        private List<game_platform> platforms;
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
        
        private List<developerPublisher> listaPublisher;
        [Display(Name = "Publisher(s)")]
        public List<developerPublisher> ListaPublisher {
            get {
                if (listaPublisher == null) {
                    listaPublisher = new List<developerPublisher>();
                }
                return listaPublisher;
            }
            set {
                listaPublisher = value;
            }
        }

        private List<developerPublisher> listaDeveloper;
        [Display(Name = "Developer(s)")]
        public List<developerPublisher> ListaDeveloper {
            get {
                if (listaDeveloper == null) {
                    listaDeveloper = new List<developerPublisher>();
                }
                return listaDeveloper;
            }
            set {
                listaDeveloper = value;
            }
        }
        
        public string CloudnaryId { get; set; }
    }

}