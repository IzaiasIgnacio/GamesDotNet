using Games.Models.Entity;
using Games.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using static Games.Models.ViewModel.GameListView;

namespace Games.Models.Repository {
    public class GameRepository : BaseRepository {
        GameEntity game;
        StoreRepository loja;
        DeveloperPublisherRepository developerPublisher;
        GenreRepository genre;
        GameDataView dadosGame;

        public void Adicionar(GameDataView dados) {
            dadosGame = dados;
            game = new GameEntity();
            loja = new StoreRepository();
            developerPublisher = new DeveloperPublisherRepository();
            genre = new GenreRepository();
            
            SetDadosgame();

            db.game.Add(game);

            SetPlataformasGame();

            SetDevelopersGame();

            SetPublishersGame();

            SetGenresGame();

            db.SaveChanges();

            SaveImagemGame();
        }
        
        public void Alterar(GameDataView dados) {
            dadosGame = dados;
            
            game = db.game.Find(dadosGame.Id);
            loja = new StoreRepository();
            developerPublisher = new DeveloperPublisherRepository();
            genre = new GenreRepository();

            SetDadosgame();

            db.Entry(game).State = EntityState.Modified;

            SetPlataformasGame();

            RemovePlataformasGame();

            SetDevelopersGame();

            SetPublishersGame();

            SetGenresGame();

            SaveImagemGame();

            db.SaveChanges();
        }
        
        #region dados
        private void SetDadosgame() {
            game.id_igdb = dadosGame.id_igdb;
            game.name = dadosGame.Titulo;
            game.nota = dadosGame.Nota;
            game.completo = dadosGame.Completo;
            game.summary = dadosGame.Descricao;
        }

        private void SetPlataformasGame() {
            foreach (game_platform plataforma in dadosGame.Platforms) {
                plataforma.id_game = game.id;
                if (plataforma.store.name != null) {
                    plataforma.id_store = loja.GetIdByName(plataforma.store.name);
                    plataforma.store = db.store.Find(plataforma.id_store);
                }
                else {
                    plataforma.store = null;
                }
                if (plataforma.id == 0) {
                    db.game_platform.Add(plataforma);
                }
                else {
                    db.Entry(plataforma).State = EntityState.Modified;
                }
            }
        }
        
        private void RemovePlataformasGame() {
            int[] plats = dadosGame.Platforms.Select(p => p.id).ToArray();
            List<game_platform> excluirPlataformas = db.game_platform.
                Where(p => !plats.Contains(p.id)).
                Where(p => p.id_game == game.id).
                ToList();

            foreach (game_platform gp in excluirPlataformas) {
                db.game_platform.Remove(gp);
            }
        }

        private void SetDevelopersGame() {
            foreach (developerPublisher dev in dadosGame.ListaDeveloper) {
                if (dev.id == 0) {
                    dev.id = developerPublisher.GetIdByIgdb(dev.id_igdb, dev.name);
                    db.game_developerPublisher.Add(new game_developerPublisher {
                        id_developerPublisher = dev.id,
                        id_game = game.id,
                        tipo = (int)GameDataView.tipoDeveloperPublisher.Developer
                    });
                }
            }
        }

        private void RemoveDevelopersGame() {
            int[] devs = dadosGame.ListaDeveloper.Select(d => d.id).ToArray();
            List<game_developerPublisher> excluirDevelopers = db.game_developerPublisher.
                Where(d => !devs.Contains(d.id)).
                Where(d => d.id_game == game.id).
                Where(d => d.tipo == (int)GameDataView.tipoDeveloperPublisher.Developer).
                ToList();

            foreach (game_developerPublisher gd in excluirDevelopers) {
                db.game_developerPublisher.Remove(gd);
            }
        }

        private void SetPublishersGame() {
            foreach (developerPublisher pub in dadosGame.ListaPublisher) {
                if (pub.id == 0) {
                    pub.id = developerPublisher.GetIdByIgdb(pub.id_igdb, pub.name);
                    db.game_developerPublisher.Add(new game_developerPublisher {
                        id_developerPublisher = pub.id,
                        id_game = game.id,
                        tipo = (int)GameDataView.tipoDeveloperPublisher.Publisher
                    });
                }
            }
        }

        private void RemovePublishersGame() {
            int[] pubs = dadosGame.ListaPublisher.Select(d => d.id).ToArray();
            List<game_developerPublisher> excluirPublishers = db.game_developerPublisher.
                Where(d => !pubs.Contains(d.id)).
                Where(d => d.id_game == game.id).
                Where(d => d.tipo == (int)GameDataView.tipoDeveloperPublisher.Publisher).
                ToList();

            foreach (game_developerPublisher gpub in excluirPublishers) {
                db.game_developerPublisher.Remove(gpub);
            }
        }

        private void SetGenresGame() {
            foreach (genre genero in dadosGame.ListaGenre) {
                if (genero.id == 0) {
                    genero.id = genre.GetIdByIgdb(genero.id_igdb, genero.name);
                    db.game_genre.Add(new game_genre {
                        id_game = game.id,
                        id_genre = genero.id
                    });
                }
            }
        }

        private void RemoveGenresGame() {
            int[] genres = dadosGame.ListaGenre.Select(d => d.id).ToArray();
            List<game_genre> excluirGenres = db.game_genre.
                Where(d => !genres.Contains(d.id)).
                Where(d => d.id_game == game.id).
                ToList();

            foreach (game_genre gg in excluirGenres) {
                db.game_genre.Remove(gg);
            }
        }

        private void SaveImagemGame() {
            if (dadosGame.CloudnaryId != null) {
                game.cloudnary_id = dadosGame.CloudnaryId;
                WebClient webClient = new WebClient();
                webClient.DownloadFile(dadosGame.BigCoverUrl + dadosGame.CloudnaryId, dadosGame.Imagesfolder + game.id + "_BigCover_" + game.cloudnary_id + ".jpg");
                webClient.DownloadFile(dadosGame.BigCoverUrl2x + dadosGame.CloudnaryId, dadosGame.Imagesfolder + game.id + "_BigCover2x_" + game.cloudnary_id + ".jpg");
                webClient.DownloadFile(dadosGame.SmallCoverUrl + dadosGame.CloudnaryId, dadosGame.Imagesfolder + game.id + "_SmallCover_" + game.cloudnary_id + ".jpg");
                webClient.DownloadFile(dadosGame.MicroCoverUrl2x + dadosGame.CloudnaryId, dadosGame.Imagesfolder + game.id + "_MicroCover2x_" + game.cloudnary_id + ".jpg");
            }
        }
        #endregion

        public GameEntity BuscarDados(int id) {
            GameEntity game = db.game.Find(id);
            return game;
        }

        public List<GameEntity> ListarJogos(List<int> plataformas, int status = 1) {
            List<GameEntity> ListaJogos;
            int[] plats = plataformas.ToArray();
            ListaJogos = (from game in db.game
                           join game_platform in db.game_platform on game.id equals game_platform.id_game
                           where plats.Contains(game_platform.id_platform)
                           where game_platform.id_status == status
                           select game).GroupBy(game=>game.name).Select(game=>game.FirstOrDefault()).ToList();
            return ListaJogos;
        }

        public int GetTotalJogos() {
            return db.game.Count();
        }
        
        public Dictionary<string, int> GetTotalJogosStatus() {
            Dictionary<string, int> total = new Dictionary<string, int>();
            var query = db.game_platform.GroupBy(gp => gp.status.name).Select(g => new { status = g.Key, total = g.Count() });
            foreach (var gp in query) {
                total.Add(gp.status,  gp.total);
            }
            return total;
        }

        public Dictionary<string, int> GetTotalJogosPlataforma() {
            Dictionary<string, int> total = new Dictionary<string, int>();
            var query = db.game_platform.GroupBy(gp => gp.platform.name).Select(g => new { plataforma = g.Key, total = g.Count() });
            foreach (var gp in query) {
                total.Add(gp.plataforma, gp.total);
            }
            return total;
        }

        public Dictionary<string, int> GetTotalJogosPublisher() {
            Dictionary<string, int> total = new Dictionary<string, int>();
            var query = db.game_developerPublisher.Where(gp=>gp.tipo == 2).GroupBy(gp => gp.developerPublisher.name).Select(g => new { publisher = g.Key, total = g.Count() });
            foreach (var gp in query) {
                total.Add(gp.publisher, gp.total);
            }
            return total;
        }

        public Dictionary<string, int> GetTotalJogosCompletos() {
            Dictionary<string, int> total = new Dictionary<string, int>();
            var query = db.game.Where(gp=>gp.completo == true).GroupBy(gp => gp.name).Select(g => new { status = g.Key, total = g.Count() });
            foreach (var gp in query) {
                total.Add(gp.status, gp.total);
            }
            return total;
        }

        public Dictionary<string, int> GetTotalJogosLoja() {
            Dictionary<string, int> total = new Dictionary<string, int>();
            var query = db.game_platform.GroupBy(gp => gp.store.name).Select(g => new { loja = g.Key, total = g.Count() });
            foreach (var gp in query) {
                total.Add(gp.loja, gp.total);
            }
            return total;
        }

        public Dictionary<string, int> GetTotalJogosFaixaPreco() {
            /*Dictionary<string, int> total = new Dictionary<string, int>();
            var query = db.GroupBy(gp => gp.name).Select(g => new { status = g.Key, total = g.Count() });
            foreach (var gp in query) {
                total.Add(gp, gp.total);
            }*/
            return null;
        }

        public Dictionary<string, int> GetTotalJogosAno() {
            /*Dictionary<string, int> total = new Dictionary<string, int>();
            var query = db.GroupBy(gp => gp.name).Select(g => new { status = g.Key, total = g.Count() });
            foreach (var gp in query) {
                total.Add(gp, gp.total);
            }*/
            return null;
        }

        public Dictionary<string, int> GetTotalJogosDeveloper() {
            Dictionary<string, int> total = new Dictionary<string, int>();
            var query = db.game_developerPublisher.Where(gp => gp.tipo == 1).GroupBy(gp => gp.developerPublisher.name).Select(g => new { publisher = g.Key, total = g.Count() });
            foreach (var gp in query) {
                total.Add(gp.publisher, gp.total);
            }
            return total;
        }

        public Dictionary<string, int> GetTotalJogosGenero() {
            Dictionary<string, int> total = new Dictionary<string, int>();
            var query = db.game_genre.GroupBy(gp => gp.genre.name).Select(g => new { genero = g.Key, total = g.Count() });
            foreach (var gp in query) {
                total.Add(gp.genero, gp.total);
            }
            return total;
        }

        public Dictionary<string, int> GetTotalJogosFormato() {
            return null;
        }

    }
}