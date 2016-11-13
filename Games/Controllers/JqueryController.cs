using Games.Models.Entity;
using Games.Models.Repository;
using Games.Models.ViewModel;
using Igdb.ResponseModels;
using Igdb.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Games.Controllers {
    public class JqueryController : Controller {

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
            game.release_date = null;
            game.preco = null;
            game.metacritic = null;
            game.completo = 0;
            game.summary = response.Summary;
            game.formato = null;
            game.tamanho = null;
            game.store = null;
            game.rating = null;
            game.cloudnary_id = response.Cover.CloudinaryId;

            gameRepository.Salvar(game);
        }

        [HttpPost]
        public ActionResult AdicionarPlataformaJquery() {
            return PartialView("PlatformStatusView", new GameDataView());
        }

    }
}