using Games.Models.Entity;
using Games.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Games.Models.Repository {
    public class GameRepository : BaseRepository {

        public void Adicionar(GameDataView dadosGame) {
            GameEntity game = new GameEntity();
            StoreRepository loja = new StoreRepository();
            DeveloperPublisherRepository developerPublisher = new DeveloperPublisherRepository();
            GenreRepository genre = new GenreRepository();
            game.id_igdb = dadosGame.id_igdb;
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

            foreach (developerPublisher dev in dadosGame.ListaDeveloper) {
                dev.id = developerPublisher.GetIdByIgdb(dev.id_igdb, dev.name);
                db.game_developerPublisher.Add(new game_developerPublisher {
                    id_developerPublisher = dev.id,
                    id_game = game.id,
                    tipo = "Developer"
                });
            }

            foreach (developerPublisher pub in dadosGame.ListaPublisher) {
                pub.id = developerPublisher.GetIdByIgdb(pub.id_igdb, pub.name);
                db.game_developerPublisher.Add(new game_developerPublisher {
                    id_developerPublisher = pub.id,
                    id_game = game.id,
                    tipo = "Publisher"
                });
            }

            foreach (genre genero in dadosGame.ListaGenre) {
                genero.id = genre.GetIdByIgdb(genero.id_igdb, genero.name);
                db.game_genre.Add(new game_genre {
                    id_game = game.id,
                    id_genre = genero.id
                });
            }

            db.SaveChanges();

            if (game.cloudnary_id != null) {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(dadosGame.BigCoverUrl+dadosGame.CloudnaryId, AppDomain.CurrentDomain.BaseDirectory + "Content\\images\\" + game.id+"_BigCover_"+game.cloudnary_id+".jpg");
                webClient.DownloadFile(dadosGame.BigCoverUrl2x + dadosGame.CloudnaryId, AppDomain.CurrentDomain.BaseDirectory + "Content\\images\\" + game.id + "_BigCover2x_" + game.cloudnary_id + ".jpg");
                webClient.DownloadFile(dadosGame.SmallCoverUrl + dadosGame.CloudnaryId, AppDomain.CurrentDomain.BaseDirectory + "Content\\images\\" + game.id + "_SmallCover_" + game.cloudnary_id + ".jpg");
            }
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