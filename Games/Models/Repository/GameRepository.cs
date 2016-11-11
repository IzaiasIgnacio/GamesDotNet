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