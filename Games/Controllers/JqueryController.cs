using Games.Models.Entity;
using Games.Models.Repository;
using Games.Models.Validacao;
using Games.Models.ViewModel;
using Igdb.ResponseModels;
using Igdb.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Games.Controllers {
    public class JqueryController : BaseController {

        public ActionResult MenuPlataformas(int? plataforma) {
            PlatformRepository platformRepository = new PlatformRepository();
            layoutView.listaPlatform = platformRepository.Listar();
            if (plataforma.HasValue) {
                if (layoutView.ativos.IndexOf(plataforma.Value) != -1) {
                    layoutView.ativos.Remove(plataforma.Value);
                }
                else {
                    layoutView.ativos.Add(plataforma.Value);
                }
            }
            return PartialView("PlatformMenuView", layoutView);
        }

        public ActionResult ListaJogosIndex(string status) {
            string tipo = null;
            GameRepository gameRepository = new GameRepository();
            GameListView view = new GameListView();
            view.ListaJogos = gameRepository.ListarJogos(layoutView.ativos, (int)Enum.Parse(typeof(GameListView.status), status));

            if (tipo == null) {
                switch (status) {
                    case "colecao":
                        tipo = "GameGridView";
                    break;
                    case "wishlist":
                        tipo = "GameListView";
                    break;
                    case "watchlist":
                        tipo = "GameListView";
                    break;
                    case "plus":
                        tipo = "GameGridView";
                    break;
                }
            }
            return PartialView(tipo, view);
        }

        [HttpPost]
        public ActionResult BuscarJogoJquery(string search) {
            IgdbService igdb = new IgdbService();
            GameResultView view = new GameResultView();
            view.ListaJogos = igdb.BuscarJogo(search);

            return PartialView("GameResultView", view);
        }

        [HttpPost]
        public ActionResult ExibirFormGame() {
            return PartialView("DadosGameView", GameDataView.init());
        }
        
        [HttpPost]
        public JsonResult SalvarNovoJogoJquery(GameDataView dados) {
            ValidacaoGameMessage validacao = ValidacaoGameService.Validar(dados);
            if (validacao.Valido) {
                GameRepository gameRepository = new GameRepository();
                gameRepository.Adicionar(dados);
            }
            return Json(validacao);
        }

        [HttpPost]
        public JsonResult AlterarJogoJquery(GameDataView dados) {
            ValidacaoGameMessage validacao = ValidacaoGameService.Validar(dados);
            if (validacao.Valido) {
                GameRepository gameRepository = new GameRepository();
                gameRepository.Alterar(dados);
            }
            return Json(validacao);
        }

        [HttpPost]
        public void RemoverPlataformaJquery(int index) {
            GameDataView view = GameDataView.GetGameDataView();
            view.Platforms.RemoveAt(index);
        }

        [HttpPost]
        public ActionResult AdicionarPlataformaJquery() {
            GameDataView view = GameDataView.GetGameDataView();
            view.Platforms.Add(new game_platform());
            return PartialView("PlatformStatusView", view);
        }

        [HttpPost]
        public ActionResult PreencherDadosGameIgdbJquery(int id_igdb, int Id = 0) {
            IgdbService igdb = new IgdbService();
            DadosGameResponse response = igdb.DadosJogo(id_igdb).FirstOrDefault();
            List<DadosDeveloperPublisherResponse> devs = new List<DadosDeveloperPublisherResponse>();
            List<DadosDeveloperPublisherResponse> pubs = new List<DadosDeveloperPublisherResponse>();
            List<DadosGenreResponse> genres = new List<DadosGenreResponse>();
            List<ReleaseDate> lancamentos = new List<ReleaseDate>();

            PlatformRepository pr = new PlatformRepository();

            if (response.Developers != null)
                devs = igdb.DadosDeveloperPublisher(response.Developers.ToArray());
            if (response.Developers != null)
                pubs = igdb.DadosDeveloperPublisher(response.Publishers.ToArray());
            if (response.Genres != null)
                genres = igdb.DadosGenre(response.Genres.ToArray());
            if (response.ReleaseDates != null)
                lancamentos = response.ReleaseDates;

            GameDataView gameDataView = GameDataView.GetGameDataView();
            gameDataView.Id = Id;
            gameDataView.id_igdb = id_igdb;            
            gameDataView.Titulo = response.Name;
            gameDataView.Descricao = response.Summary;

            if (response.Cover != null) {
                gameDataView.Imagem = gameDataView.BigCoverUrl+response.Cover.CloudinaryId;
                gameDataView.CloudnaryId = response.Cover.CloudinaryId;
            }
            else {
                gameDataView.Imagem = "/Content/ps.png";
            }

            foreach (DadosDeveloperPublisherResponse dev in devs) {
                gameDataView.ListaDeveloper.Add(new developerPublisher {
                    name = dev.Name,
                    id_igdb = dev.Id
                });
            }

            foreach (DadosDeveloperPublisherResponse pub in pubs) {
                gameDataView.ListaPublisher.Add(new developerPublisher {
                    name = pub.Name,
                    id_igdb = pub.Id
                });
            }

            foreach (DadosGenreResponse genre in genres) {
                gameDataView.ListaGenre.Add(new genre {
                    id_igdb = genre.Id,
                    name = genre.Name
                });
            }

            foreach (ReleaseDate lancamento in lancamentos) {
                int? plataforma = pr.GetIdByIgdb(lancamento.Platform);
                if (plataforma != null) {
                    DateTime data = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(Convert.ToDouble(Convert.ToDouble(lancamento.Date)));
                    gameDataView.Platforms.Add(new game_platform {
                        id_platform = plataforma.Value,
                        release_date = data,
                        id_region = lancamento.Region
                    });
                }
            }
            
            return PartialView("DadosGameView", gameDataView);
        }

        [HttpPost]
        public ActionResult PreencherDadosGameEntityJquery(int id) {
            GameRepository gameRepository = new GameRepository();
            GameEntity game = gameRepository.BuscarDados(id);

            List<game_developerPublisher> devs = game.game_developerPublisher.Where(d => d.tipo == (int)GameDataView.tipoDeveloperPublisher.Developer).ToList();
            List<game_developerPublisher> pubs = game.game_developerPublisher.Where(p => p.tipo == (int)GameDataView.tipoDeveloperPublisher.Publisher).ToList();
            List<game_genre> genres = game.game_genre.ToList();

            GameDataView gameDataView = GameDataView.init();
            gameDataView.Id = game.id;
            gameDataView.id_igdb = game.id_igdb;
            gameDataView.Titulo = game.name;
            gameDataView.Descricao = game.summary;
            gameDataView.Nota = game.nota;
            gameDataView.Completo = game.completo;
            
            string arquivo = gameDataView.Imagesfolder + game.id + "_BigCover_" + game.cloudnary_id + ".jpg";
            if (System.IO.File.Exists(arquivo)) {
                gameDataView.Imagem = "/images/" + game.id + "_BigCover_" + game.cloudnary_id + ".jpg";
                gameDataView.CloudnaryId = game.cloudnary_id;
            }

            foreach (game_developerPublisher dev in devs) {
                gameDataView.ListaDeveloper.Add(new developerPublisher {
                    id = dev.id,
                    name = dev.developerPublisher.name
                });
            }

            foreach (game_developerPublisher pub in pubs) {
                gameDataView.ListaPublisher.Add(new developerPublisher {
                    id = pub.id,
                    name = pub.developerPublisher.name
                });
            }

            foreach (game_genre genre in genres) {
                gameDataView.ListaGenre.Add(new genre {
                    id = genre.id,
                    name = genre.genre.name
                });
            }

            foreach (game_platform lancamento in game.game_platform) {
                gameDataView.Platforms.Add(new game_platform {
                    id = lancamento.id,
                    release_date = lancamento.release_date,
                    metacritic = lancamento.metacritic,
                    preco = lancamento.preco,
                    formato = lancamento.formato,
                    tamanho = lancamento.tamanho,
                    id_platform = lancamento.id_platform,
                    id_status = lancamento.id_status,
                    id_region = lancamento.id_region,
                    id_rating = lancamento.id_rating,
                    id_store = lancamento.id_store,
                    store = lancamento.store
                });
            }
            
            return PartialView("DadosGameView", gameDataView);
        }

    }
}