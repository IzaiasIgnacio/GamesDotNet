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
            StoreRepository loja = new StoreRepository();
            game.name = dadosGame.Titulo;
            game.nota = dadosGame.Nota;
            game.preco = dadosGame.Preco;
            game.metacritic = dadosGame.Metacritic;
            game.completo = dadosGame.Completo;
            game.summary = dadosGame.Descricao;
            game.formato = (int)dadosGame.Formato;
            game.tamanho = dadosGame.Tamanho;
            game.id_store = loja.GetIdByName(dadosGame.Loja);
            game.cloudnary_id = dadosGame.CloudnaryId;

            db.game.Add(game);
            db.SaveChanges();
            
            foreach (game_platform plataforma in dadosGame.Platforms) {
                plataforma.id_game = game.id;
                db.game_platform.Add(plataforma);
            }

            db.SaveChanges();
        }
        public void Salvar(GameEntity game) {
            db.SaveChanges();
        }

        public GameEntity BuscarDados(int id) {
            GameEntity game = db.game.Find(id);
            return game;
        }

        public List<GameEntity> ListarJogos(List<int> plataformas) {
            List<GameEntity> ListaJogos;
            int[] plats = plataformas.ToArray();
            ListaJogos = (from game in db.game
                           join game_platform in db.game_platform on game.id equals game_platform.id_game
                           where plats.Contains(game_platform.id_platform)
                           select game).OrderBy(game => game.name).ToList();
            return ListaJogos;
        }

    }
}