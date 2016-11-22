using Games.Models.Entity;
using Games.Models.Repository;
using Igdb.ResponseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Games.Models.ViewModel {

    public class BasegameView {
        public string CloudnaryUrl;
        public string BigCoverUrl;
        public string BigCoverUrl2x;
        public string MicroCoverUrl;
        public string SmallCoverUrl;
        public string Imagesfolder;
        public GameRepository gameRepository;

        public BasegameView() {
            gameRepository = new GameRepository();
            CloudnaryUrl = "https://res.cloudinary.com/igdb/image/upload/t_";
            BigCoverUrl = CloudnaryUrl + "cover_big/";
            BigCoverUrl2x = CloudnaryUrl + "cover_big_2x/";
            MicroCoverUrl = CloudnaryUrl + "micro/";
            SmallCoverUrl = CloudnaryUrl + "cover_small_2x/";
            Imagesfolder = AppDomain.CurrentDomain.BaseDirectory + "Content\\images\\";
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
        PlatformRepository platformRepository = new PlatformRepository();
        
        [Key]
        public int Id { get; set; }

        public int id_igdb { get; set; }

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

        public string CloudnaryId { get; set; }

        public enum formato { Fisíco=1, Digital=2 };
        [Display(Name = "Formato")]
        public formato? Formato { get; set; }

        public List<platform> ListaPlatform {
            get {
                return platformRepository.Listar();
            }
        }
        public List<status> ListaStatus {
            get {
                return gameRepository.Listar<status>();
            }
        }
        public List<region> ListaRegion {
            get {
                return gameRepository.Listar<region>();
            }
        }
        public List<rating> ListaRating {
            get {
                return gameRepository.Listar<rating>();
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
        public string Loja { get; set; }
        public List<store> ListaLoja {
            get {
                return gameRepository.Listar<store>();
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

        private List<genre> listaGenre;
        [Display(Name = "Gênero(s)")]
        public List<genre> ListaGenre {
            get {
                if (listaGenre == null) {
                    listaGenre = new List<genre>();
                }
                return listaGenre;
            }
            set {
                listaGenre = value;
            }
        }

    }

}