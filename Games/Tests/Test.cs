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

namespace Games.Test {
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
        public void TesteCarregarExcluirJogo(){
            GameRepository gameRepository = new GameRepository();
            GameEntity game = gameRepository.BuscarDados(2);

            gameRepository.Excluir(game);
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

        /*public void gerar_planilha(string aba, List<string> colunas, List<GameView> jogos, Workbook arquivo) {
            var tamanho = jogos.Count;
            var num = colunas.Count;

            Worksheet planilha = (Worksheet)arquivo.Worksheets.get_Item(arquivo.Worksheets.Count);
            Range formatRange;
            formatRange = planilha.get_Range("a1");
            formatRange.EntireRow.Font.Bold = true;
            formatRange.EntireRow.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGray);
            formatRange.EntireRow.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;

            planilha.Name = aba;
            for (int i = 1; i <= num; i++) {
                planilha.Cells[1, i] = colunas[i - 1];
            }
            planilha.Columns.AutoFit();

            for (int i = 2; i <= tamanho + 1; i++) {
                int index = i - 2;
                formatRange = planilha.get_Range("a" + i);
                if (i % 2 == 0) {
                    formatRange.EntireRow.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                }
                formatRange.EntireRow.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;

                planilha.Cells[i, 1] = jogos[index].Name;
                if (colunas.Count > 1) {
                    planilha.Cells[i, 2] = jogos[index].ReleaseDate;
                }
            }
            Marshal.ReleaseComObject(planilha);
        }

        [TestMethod]
        public void TesteExcel() {
            //http://csharp.net-informations.com/excel/worksheet.htm
            var lista = new List<GameView>();
            lista.Add(new GameView { Name = "teste", ReleaseDate = new DateTime(2017, 05, 05) });
            lista.Add(new GameView { Name = "teste", ReleaseDate = new DateTime(2017, 05, 05) });
            lista.Add(new GameView { Name = "teste", ReleaseDate = new DateTime(2017, 05, 05) });
            lista.Add(new GameView { Name = "teste", ReleaseDate = new DateTime(2017, 05, 05) });
            lista.Add(new GameView { Name = "teste", ReleaseDate = new DateTime(2017, 05, 05) });
            var tamanho = lista.Count;

            Application app = new Application();
            object missing = System.Reflection.Missing.Value;

            Workbook arquivo = app.Workbooks.Add();

            gerar_planilha("Wishlist", new List<string> { "Título", "Lançamento" }, lista, arquivo);
            arquivo.Worksheets.Add(After: arquivo.Sheets[arquivo.Sheets.Count]);
            gerar_planilha("Watchlist", new List<string> { "Título", "Lançamento" }, lista, arquivo);
            arquivo.Worksheets.Add(After: arquivo.Sheets[arquivo.Sheets.Count]);
            gerar_planilha("PS4", new List<string> { "Título" }, lista, arquivo);

            ((Worksheet)app.ActiveWorkbook.Sheets[1]).Select();

            app.DisplayAlerts = false;
            arquivo.SaveAs("f:\\new\\csharp-Excel.xls", XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing, XlSaveAsAccessMode.xlExclusive);
            arquivo.Close(true, missing, missing);
            app.Quit();

            Marshal.ReleaseComObject(arquivo);
            Marshal.ReleaseComObject(app);
        }*/

    }
}
