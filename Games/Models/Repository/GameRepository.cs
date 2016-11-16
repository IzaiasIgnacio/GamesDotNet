using Games.Models.Entity;
using Igdb.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models.Repository {
    public class GameRepository : BaseRepository {

        public void Adicionar(DadosGameResponse dadosGame) {
            GameEntity game = new GameEntity();
            game.name = dadosGame.Name;
            game.nota = null;
            game.preco = null;
            game.metacritic = null;
            game.completo = 0;
            game.summary = dadosGame.Summary;
            game.formato = null;
            game.tamanho = null;
            game.store = null;
            game.cloudnary_id = dadosGame.Cover.CloudinaryId;

            db.game.Add(game);
            db.SaveChanges();
        }
        public void Salvar(GameEntity game) {
            db.SaveChanges();
        }

        public GameEntity BuscarDados(int id) {
            GameEntity game = db.game.Find(id);
            return game;
        }

        public List<GameEntity> ListarJogos() {
            List<GameEntity> ListaJogos = db.game.ToList();
            return ListaJogos;
        }

    }
}