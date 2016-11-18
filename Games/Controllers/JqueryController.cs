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
    public class JqueryController : Controller {

        public ActionResult MenuPlataformas() {
            GameListView view = new GameListView();
            GameRepository gameRepository = new GameRepository();
            view.ListaJogos = gameRepository.ListarJogos();

            return View(view);
        }

        [HttpPost]
        public ActionResult BuscarJogoJquery(string search) {
            IgdbService igdb = new IgdbService();
            GameResultView view = new GameResultView();
            view.ListaJogos = igdb.BuscarJogo(search);

            return PartialView("GameResultView", view);
        }

        [HttpPost]
        public void AtualizarJogoJquery(int id_local, int id_igdb) {
            IgdbService igdb = new IgdbService();
            DadosGameResponse response = igdb.DadosJogo(id_igdb).FirstOrDefault();

            GameRepository gameRepository = new GameRepository();
            GameEntity game = gameRepository.BuscarDados(id_local);

            game.id_igdb = response.Id;
            game.name = response.Name;
            game.nota = null;
            game.preco = null;
            game.metacritic = null;
            game.completo = 0;
            game.summary = response.Summary;
            game.formato = null;
            game.tamanho = null;
            game.store = null;
            game.cloudnary_id = response.Cover.CloudinaryId;

            gameRepository.Salvar(game);
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

            GameDataView gameDataView = new GameDataView();
            gameDataView.Titulo = response.Name;
            gameDataView.Descricao = response.Summary;
            gameDataView.CloudnaryId = response.Cover.CloudinaryId;

            foreach (DadosDeveloperPublisherResponse dev in devs) {
                gameDataView.ListaDeveloper.Add(new developerPublisher {
                    name = dev.Name
                });
            }

            foreach (DadosDeveloperPublisherResponse pub in pubs) {
                gameDataView.ListaPublisher.Add(new developerPublisher {
                    name = pub.Name
                });
            }

            foreach (ReleaseDate lancamento in response.ReleaseDates) {
                int? plataforma = pr.getIdByIgdb(lancamento.Platform);
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

    }
}