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
        public void AtualizarImagens() {
            GameRepository gameRepository = new GameRepository();
            List<GameEntity> lista1 = gameRepository.ListarJogos(new List<int> { 1, 2, 3, 4, 5, 6, 7 }, 1);
            List<GameEntity> lista2 = gameRepository.ListarJogos(new List<int> { 1, 2, 3, 4, 5, 6, 7 }, 2);
            List<GameEntity> lista3 = gameRepository.ListarJogos(new List<int> { 1, 2, 3, 4, 5, 6, 7 }, 3);
            List<GameEntity> lista4 = gameRepository.ListarJogos(new List<int> { 1, 2, 3, 4, 5, 6, 7 }, 4);

            List<GameEntity> lista = new List<GameEntity>();
            lista.AddRange(lista1);
            lista.AddRange(lista2);
            lista.AddRange(lista3);
            lista.AddRange(lista4);

            string CloudnaryUrl = "https://res.cloudinary.com/igdb/image/upload/t_";
            string BigCoverUrl = CloudnaryUrl + "cover_big/";
            string BigCoverUrl2x = CloudnaryUrl + "cover_big_2x/";
            string MicroCoverUrl = CloudnaryUrl + "micro/";
            string MicroCoverUrl2x = CloudnaryUrl + "micro_2x/";
            string SmallCoverUrl = CloudnaryUrl + "cover_small_2x/";
            //string Imagesfolder = "I:\\Documents\\Visual Studio 2015\\Projects\\Games\\Games\\images\\";
            string Imagesfolder = "F:\\new\\Games\\images\\";

            WebClient webClient = new WebClient();

            foreach (GameEntity game in lista) {
                webClient.DownloadFile(BigCoverUrl + game.cloudnary_id, Imagesfolder + game.id + "_BigCover_" + game.cloudnary_id + ".jpg");
                webClient.DownloadFile(BigCoverUrl2x + game.cloudnary_id, Imagesfolder + game.id + "_BigCover2x_" + game.cloudnary_id + ".jpg");
                webClient.DownloadFile(SmallCoverUrl + game.cloudnary_id, Imagesfolder + game.id + "_SmallCover_" + game.cloudnary_id + ".jpg");
                webClient.DownloadFile(MicroCoverUrl2x + game.cloudnary_id, Imagesfolder + game.id + "_MicroCover2x_" + game.cloudnary_id + ".jpg");
            }
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

    }
}
