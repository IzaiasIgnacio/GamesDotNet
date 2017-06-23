using Games.Models.Entity;
using Games.Models.Repository;
using Igdb.ResponseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Games.Models.ViewModel {

    public class BasegameView {
        public string CloudnaryUrl;
        public string BigCoverUrl;
        public string BigCoverUrl2x;
        public string MicroCoverUrl;
        public string MicroCoverUrl2x;
        public string SmallCoverUrl;
        public string Imagesfolder;
        public GameRepository gameRepository;

        public BasegameView() {
            gameRepository = new GameRepository();
            CloudnaryUrl = "https://images.igdb.com/igdb/image/upload/t_";
            BigCoverUrl = CloudnaryUrl + "cover_big/";
            BigCoverUrl2x = CloudnaryUrl + "cover_big_2x/";
            MicroCoverUrl = CloudnaryUrl + "micro/";
            MicroCoverUrl2x = CloudnaryUrl + "micro_2x/";
            SmallCoverUrl = CloudnaryUrl + "cover_small_2x/";
            Imagesfolder = AppDomain.CurrentDomain.BaseDirectory + "images\\";
        }
    }

    public class DadosGameView : BasegameView {
        public DadosGameView() {
            ListaDeveloper = new List<developerPublisher>();
            ListaPublisher = new List<developerPublisher>();
            ListaGenre = new List<genre>();
            Platforms = new List<game_platform>();
        }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string CloudnaryId { get; set; }
        public List<developerPublisher> ListaDeveloper { get; set; }
        public List<developerPublisher> ListaPublisher { get; set; }
        public List<genre> ListaGenre { get; set; }
        public decimal? Nota { get; set; }
        public Boolean Completo { get; set; }
        public string GetCompleto() {
            if (Completo) {
                return "Sim";
            }
            return "Não";
        }
        public List<game_platform> Platforms { get; set; }
        public string Descricao { get; set; }
        public string GetFormato(int? formato) {
            if (formato == null) {
                return "";
            }

            if (formato == 1) {
                return "Físico";
            }

            return "Digital";
        }
    }

    public class GameListView : BasegameView {
        public enum status { colecao = 1, wishlist = 2, watchlist = 3, plus = 4 }
        public status Status { get; set; }
        public class GameView {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CloudnaryId { get; set; }
            public DateTime? ReleaseDate { get; set; }
            public int? Ordem { get; set; }
            public List<string> Plataformas { get; set; }
        }

        private List<GameView> listaJogos;
        public List<GameView> ListaJogos {
            get {
                if (listaJogos == null) {
                    listaJogos = new List<GameView>();
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

    public class BuscaResultView : BasegameView {
        public List<GameEntity> ListaJogosEntity { get; set; }
    }

    public class GameDataView : BasegameView {
        private static GameDataView view;

        public static GameDataView GetGameDataView() {
            if (view == null) {
                view = new GameDataView();
            }
            return view;
        }

        public static GameDataView init() {
            view = new GameDataView();

            return view;
        }

        PlatformRepository platformRepository = new PlatformRepository();
        
        [Key]
        public int? Id { get; set; }

        public int? id_igdb { get; set; }

        [Display(Name = "Título")]
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

        public string Imagem { get; set; }
        [Display(Name = "Cloudnary Id")]
        public string CloudnaryId { get; set; }

        public enum formato { Fisíco=1, Digital=2 };
        [Display(Name = "Formato")]
        public formato? Formato { get { return new formato(); } }
        public IList<SelectListItem> SelectFormato {
            get {
                return EnumHelper.GetSelectList(typeof(formato));
            }
        }

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
        
        public List<store> ListaLoja {
            get {
                return gameRepository.Listar<store>();
            }
        }

        public enum tipoDeveloperPublisher { Developer = 1, Publisher = 2 };
        public tipoDeveloperPublisher TipoDeveloperPublisher { get; set; }

        public void InitListas() {
            listaPublisher = new List<developerPublisher>();
            listaDeveloper = new List<developerPublisher>();
            listaGenre = new List<genre>();
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