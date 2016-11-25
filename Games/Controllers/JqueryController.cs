using Games.Models.Entity;
using Games.Models.Repository;
using Games.Models.ViewModel;
using Igdb.ResponseModels;
using Igdb.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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

        public ActionResult ListaJogosIndex() {
            GameRepository gameRepository = new GameRepository();
            GameListView view = new GameListView();
            view.ListaJogos = gameRepository.ListarJogos(layoutView.ativos);
            return PartialView("GameGridView", view);
        }

        [HttpPost]
        public ActionResult BuscarJogoJquery(string search) {
            IgdbService igdb = new IgdbService();
            GameResultView view = new GameResultView();
            view.ListaJogos = igdb.BuscarJogo(search);

            return PartialView("GameResultView", view);
        }

        [HttpPost]
        public void SalvarNovoJogoJquery(GameDataView dados) {
            GameRepository gameRepository = new GameRepository();
            gameRepository.Adicionar(dados);
        }

        [HttpPost]
        public void AlterarJogoJquery(GameDataView dados) {
            GameRepository gameRepository = new GameRepository();
            gameRepository.Alterar(dados);
        }
        
        [HttpPost]
        public ActionResult AdicionarPlataformaJquery() {
            return PartialView("PlatformStatusView", new GameDataView());
        }

        [HttpPost]
        public ActionResult PreencherDadosGameIgdbJquery(int id_igdb) {
            IgdbService igdb = new IgdbService();
            DadosGameResponse response = igdb.DadosJogo(id_igdb).FirstOrDefault();

            PlatformRepository pr = new PlatformRepository();
            List<DadosDeveloperPublisherResponse> devs = igdb.DadosDeveloperPublisher(response.Developers.ToArray());
            List<DadosDeveloperPublisherResponse> pubs = igdb.DadosDeveloperPublisher(response.Publishers.ToArray());
            List<DadosGenreResponse> genres = igdb.DadosGenre(response.Genres.ToArray());

            GameDataView gameDataView = new GameDataView();
            gameDataView.id_igdb = id_igdb;            
            gameDataView.Titulo = response.Name;
            gameDataView.Descricao = response.Summary;

            if (response.Cover != null) {
                gameDataView.CloudnaryId = response.Cover.CloudinaryId;
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

            foreach (ReleaseDate lancamento in response.ReleaseDates) {
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
            
            return PartialView("DadosGameVIew", gameDataView);
        }

        [HttpPost]
        public ActionResult PreencherDadosGameEntityJquery(int id) {
            GameRepository gameRepository = new GameRepository();
            GameEntity game = gameRepository.BuscarDados(id);

            List<game_developerPublisher> devs = game.game_developerPublisher.Where(d => d.tipo == (int)GameDataView.tipoDeveloperPublisher.Developer).ToList();
            List<game_developerPublisher> pubs = game.game_developerPublisher.Where(p => p.tipo == (int)GameDataView.tipoDeveloperPublisher.Publisher).ToList();
            List<game_genre> genres = game.game_genre.ToList();

            GameDataView gameDataView = new GameDataView();
            gameDataView.Id = game.id;
            gameDataView.id_igdb = game.id_igdb;
            gameDataView.Titulo = game.name;
            gameDataView.Descricao = game.summary;
            gameDataView.Formato = (GameDataView.formato)game.formato;
            gameDataView.Metacritic = game.metacritic;
            gameDataView.Nota = game.nota;
            gameDataView.Preco = game.preco;
            gameDataView.Tamanho = game.tamanho;
            gameDataView.Loja = game.store.name;

            if (game.cloudnary_id != null) {
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
                    id_platform = lancamento.id_platform,
                    id_status = lancamento.id_status,
                    release_date = lancamento.release_date,
                    id_region = lancamento.id_region,
                    id_rating = lancamento.id_rating
                });
            }

            return PartialView("DadosGameVIew", gameDataView);
        }

    }
}