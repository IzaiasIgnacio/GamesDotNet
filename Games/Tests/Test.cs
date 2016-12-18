using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Igdb.Services;
using Igdb.ResponseModels;
using Igdb.RequestModels;
using Games.Models.Entity;
using Newtonsoft.Json;
using Games.Models.Repository;
using Games.Models.ViewModel;
using System.Net;
using Games.Controllers;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Igdb.Test {
    [TestClass]
    public class Test {
        public void TesteSalvarJogoView() {
            IgdbService igdb = new IgdbService();
            DadosGameResponse response = igdb.DadosJogo(428).FirstOrDefault();
            PlatformRepository pr = new PlatformRepository();
            List<DadosDeveloperPublisherResponse> devs = igdb.DadosDeveloperPublisher(response.Developers.ToArray());
            List<DadosDeveloperPublisherResponse> pubs = igdb.DadosDeveloperPublisher(response.Publishers.ToArray());

            GameDataView gameDataView = GameDataView.init();
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

            GameRepository gameRepository = new GameRepository();
            gameRepository.Adicionar(gameDataView);
        }

        [TestMethod]
        public void TesteGerarListaDeveloper() {
            IgdbService igdb = new IgdbService();
            DadosGameResponse response = igdb.DadosJogo(428).FirstOrDefault();

            List<DadosDeveloperPublisherResponse> devs = igdb.DadosDeveloperPublisher(response.Developers.ToArray());
            List<DadosDeveloperPublisherResponse> pubs = igdb.DadosDeveloperPublisher(response.Publishers.ToArray());

            GameDataView gameDataView = GameDataView.init();
            
            foreach(DadosDeveloperPublisherResponse dev in devs){
                gameDataView.ListaDeveloper.Add(new developerPublisher {
                    name = dev.Name
                });
            }

            foreach (DadosDeveloperPublisherResponse pub in pubs) {
                gameDataView.ListaPublisher.Add(new developerPublisher {
                    name = pub.Name
                });
            }

            Assert.IsNotNull(gameDataView.ListaDeveloper);
            Assert.IsNotNull(gameDataView.ListaPublisher);
        }

        [TestMethod]
        public void TesteCarregarSalvarJogo() {
            GameRepository gameRepository = new GameRepository();
            GameEntity game = gameRepository.BuscarDados(2);
            game.completo = true;
            //gameRepository.Salvar(game);

            game = gameRepository.BuscarDados(2);

            Assert.IsNotNull(game);
            Assert.AreEqual("Final Fantasy Tactics", game.name);
            Assert.AreEqual(1, game.completo);
        }
        
        [TestMethod]
        public void AtualizarTudoIgdb() {
            GamesEntities db = new GamesEntities();
            IgdbService igdb = new IgdbService();
            DeveloperPublisherRepository developerPublisherRepository = new DeveloperPublisherRepository();
            GenreRepository genreRepository = new GenreRepository();

            var lista = db.game;
            foreach (var game in lista) {
                if (game.id_igdb != null) {
                    DadosGameResponse dados = igdb.DadosJogo((int)game.id_igdb).FirstOrDefault();
                    game.summary = dados.Summary;
                    if (dados.Developers != null) {
                        foreach (var dev in dados.Developers) {
                            var id_dev = developerPublisherRepository.GetIdByIgdb(dev, igdb.DadosDeveloperPublisher(new int[] { dev }).FirstOrDefault().Name);
                            var devs = db.game_developerPublisher.Where(g => g.id_game == game.id && g.id_developerPublisher == id_dev && g.tipo == 1).Any();
                            if (!devs) {
                                game.game_developerPublisher.Add(new game_developerPublisher { id_developerPublisher = id_dev, id_game = game.id, tipo = 1 });
                            }
                        }
                    }
                    if (dados.Publishers != null) {
                        foreach (var pub in dados.Publishers) {
                            var id_pub = developerPublisherRepository.GetIdByIgdb(pub, igdb.DadosDeveloperPublisher(new int[] { pub }).FirstOrDefault().Name);
                            var pubs = db.game_developerPublisher.Where(g => g.id_game == game.id && g.id_developerPublisher == id_pub && g.tipo == 2).Any();
                            if (!pubs) {
                                game.game_developerPublisher.Add(new game_developerPublisher { id_developerPublisher = id_pub, id_game = game.id, tipo = 2 });
                            }
                        }
                    }
                    if (dados.Genres != null) {
                        foreach (var genre in dados.Genres) {
                            var id_genre = genreRepository.GetIdByIgdb(genre, igdb.DadosGenre(new int[] { genre }).FirstOrDefault().Name);
                            var genres = db.game_genre.Where(g => g.id_game == game.id && g.id_genre == id_genre).Any();
                            if (!genres) {
                                game.game_genre.Add(new game_genre { id_genre = id_genre, id_game = game.id });
                            }
                        }
                    }
                }
            }
            db.SaveChanges();
        }

        [TestMethod]
        public void TesteListarJogos() {
            List<int> plataformas = new List<int> { 1, 2 };
            int status = 2;
            GamesEntities db = new GamesEntities();
            List<GameEntity> ListaJogos;
            int[] plats = plataformas.ToArray();
            ListaJogos = (from game in db.game
                          join game_platform in db.game_platform on game.id equals game_platform.id_game
                          where plats.Contains(game_platform.id_platform)
                          where game_platform.id_status == status
                          select game).OrderBy(game => game.name).ToList();

            //DateTime a = ListaJogos[0].game_platform.First().release_date;
            Assert.IsNotNull(ListaJogos);
        }

        [TestMethod]
        public void TesteValidarGameView() {
            GameDataView view = new GameDataView();
            JqueryController j = new JqueryController();
            var json = j.AlterarJogoJquery(view);
        }

    }
}
