using Games.Models.Entity;
using Games.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models.Repository {
    public class GameRepository : BaseRepository {

        public void Adicionar(GameDataView dadosGame) {
            GameEntity game = new GameEntity();
            game.name = dadosGame.Titulo;
            game.nota = null;
            game.preco = null;
            game.metacritic = null;
            game.completo = 0;
            game.summary = dadosGame.Descricao;
            game.formato = null;
            game.tamanho = null;
            game.store = null;
            game.cloudnary_id = dadosGame.CloudnaryId;

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