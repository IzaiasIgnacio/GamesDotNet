﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Igdb.Services;
using Igdb.ResponseModels;
using Games.Models.Entity;
using Games.Models.Repository;
using Games.Models.ViewModel;
using Games.Controllers;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using System.Net;
using static Games.Models.ViewModel.GameListView;
using System.Data.Entity;
using FluentMetacritic;
using System.Threading.Tasks;

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
                platform plataforma = pr.GetPlatformByIgdb(lancamento.Platform);
                if (plataforma != null) {
                    DateTime data = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(Convert.ToDouble(Convert.ToDouble(lancamento.Date)));
                    gameDataView.Platforms.Add(new game_platform {
                        id_platform = plataforma.id,
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
        public void TesteCarregarExcluirJogo() {
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

        [TestMethod]
        public void TesteSalvarImagens() {
            GameDataView dadosGame = new GameDataView();
            dadosGame.CloudnaryId = "zfuftanzlr4yvkq7dedr";
            dadosGame.Imagesfolder = "I:\\Documents\\Visual Studio 2015\\Projects\\Games\\Games\\images";
            var game = new GameEntity();
            game.id = 0;
            game.cloudnary_id = "zfuftanzlr4yvkq7dedr";
            WebClient webClient = new WebClient();
            try {
                webClient.DownloadFile(dadosGame.BigCoverUrl + dadosGame.CloudnaryId + ".jpg", dadosGame.Imagesfolder + game.id + "_BigCover_" + game.cloudnary_id + ".jpg");
                webClient.DownloadFile(dadosGame.BigCoverUrl2x + dadosGame.CloudnaryId + ".jpg", dadosGame.Imagesfolder + game.id + "_BigCover2x_" + game.cloudnary_id + ".jpg");
                webClient.DownloadFile(dadosGame.SmallCoverUrl + dadosGame.CloudnaryId + ".jpg", dadosGame.Imagesfolder + game.id + "_SmallCover_" + game.cloudnary_id + ".jpg");
                webClient.DownloadFile(dadosGame.MicroCoverUrl2x + dadosGame.CloudnaryId + ".jpg", dadosGame.Imagesfolder + game.id + "_MicroCover2x_" + game.cloudnary_id + ".jpg");
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        static string[] Scopes = { SheetsService.Scope.Spreadsheets };

        [TestMethod]
        public void TesteSheet() {
            #region setup
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read)) {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/games.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Sheets API service.
            SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer {
                HttpClientInitializer = credential,
                ApplicationName = "Google-SheetsSample/0.1",
            });

            string id = "1k7Reqz1ZqGXwr8lTy5Y5r6bX53hxWv4kJSWTs3ptAuc";

            List<GameView> lista_mock = new List<GameView>();
            lista_mock.Add(new GameView { Name = "teste" });
            lista_mock.Add(new GameView { Name = "teste" });
            lista_mock.Add(new GameView { Name = "teste" });
            lista_mock.Add(new GameView { Name = "teste" });
            lista_mock.Add(new GameView { Name = "teste teste teste teste teste teste teste" });

            GameRepository game = new GameRepository();
            PlatformRepository plataforma = new PlatformRepository();
            #endregion

            #region limpar, preencher e formatar abas
            SpreadsheetsResource.GetRequest get = sheetsService.Spreadsheets.Get(id);
            Spreadsheet planilhas = get.Execute();

            SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum valueInputOption = (SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum)2;

            foreach (Sheet planilha in planilhas.Sheets) {
                var aba = planilha.Properties.Title;

                ClearValuesRequest clearRequest = new ClearValuesRequest();
                SpreadsheetsResource.ValuesResource.ClearRequest request = sheetsService.Spreadsheets.Values.Clear(clearRequest, id, aba+"!A1:Z1000");
                ClearValuesResponse response = request.Execute();

                List<GameView> lista = new List<GameView>();
                List<int> plat = new List<int>();
                List<object> cabecalho = null;

                switch (aba) {
                    case "Wishlist":
                        lista = game.ListarJogosWishlist(new List<int> { 0 });
                        cabecalho = new List<object>() { "", "Título", "Lançamento", "Plataformas" };
                    break;
                    case "Watchlist":
                        lista = game.ListarJogos(new List<int> { 0 }, 3);
                        cabecalho = new List<object>() { "", "Título", "Lançamento", "Plataformas" };
                    break;
                    default:
                        int? plataformas = plataforma.GetIdBySigla(aba);
                        plat = new List<int> { plataformas.Value };
                        lista = game.ListarJogos(plat, 1);
                        cabecalho = new List<object>() { "", "Título" };
                    break;
                }
                
                string range = aba+"!A1:D"+lista.Count+1;

                List<IList<object>> dados = new List<IList<object>>();
                dados.Add(cabecalho);

                foreach (GameView jogo in lista) {
                    if (cabecalho.Count == 2) {
                        dados.Add(new List<object>() { "=IMAGE(\"https://images.igdb.com/igdb/image/upload/t_micro/" + jogo.CloudnaryId+".jpg\")", jogo.Name });
                    }
                    else {
                        string data = null;
                        if (jogo.ReleaseDate != null) {
                            data = jogo.ReleaseDate.Value.ToShortDateString();
                        }
                        dados.Add(new List<object>() { "=IMAGE(\"https://images.igdb.com/igdb/image/upload/t_micro/" + jogo.CloudnaryId+".jpg\")", jogo.Name, data, String.Join(", ",jogo.Plataformas) });
                    }
                }

                ValueRange valueRange = new ValueRange();
                valueRange.Values = dados;

                SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = sheetsService.Spreadsheets.Values.Update(valueRange, id, range);
                updateRequest.ValueInputOption = valueInputOption;
                
                UpdateValuesResponse resposta = updateRequest.Execute();
                
                Request alignLeftRequest = new Request();
                alignLeftRequest.RepeatCell = new RepeatCellRequest();
                alignLeftRequest.RepeatCell.Fields = "userEnteredFormat(HorizontalAlignment)";
                alignLeftRequest.RepeatCell.Range = new GridRange { SheetId = planilha.Properties.SheetId, StartColumnIndex = 2, EndColumnIndex = 3 };
                alignLeftRequest.RepeatCell.Cell = new CellData { UserEnteredFormat = new CellFormat { HorizontalAlignment = "LEFT" } };

                Request alignCenterRequest = new Request();
                alignCenterRequest.RepeatCell = new RepeatCellRequest();
                alignCenterRequest.RepeatCell.Fields = "userEnteredFormat(HorizontalAlignment)";
                alignCenterRequest.RepeatCell.Range = new GridRange { SheetId = planilha.Properties.SheetId, StartColumnIndex = 0, EndColumnIndex = 1 };
                alignCenterRequest.RepeatCell.Cell = new CellData { UserEnteredFormat = new CellFormat { HorizontalAlignment = "Center" } };

                Request resizeRequest = new Request();
                resizeRequest.AutoResizeDimensions = new AutoResizeDimensionsRequest();
                resizeRequest.AutoResizeDimensions.Dimensions = new DimensionRange { SheetId = planilha.Properties.SheetId, Dimension = "COLUMNS", StartIndex = 1, EndIndex = cabecalho.Count };
                
                BatchUpdateSpreadsheetRequest batch = new BatchUpdateSpreadsheetRequest();
                batch.Requests = new List<Request>();
                batch.Requests.Add(alignLeftRequest);
                batch.Requests.Add(alignCenterRequest);
                batch.Requests.Add(resizeRequest);

                SpreadsheetsResource.BatchUpdateRequest u = sheetsService.Spreadsheets.BatchUpdate(batch, id);
                BatchUpdateSpreadsheetResponse responseResize = u.Execute();
            }
            #endregion

            #region deletar aba
            // A list of updates to apply to the spreadsheet.
            // Requests will be applied in the order they are specified.
            // If any request is not valid, no requests will be applied.
            /*List<Request> requests = new List<Request>();  // TODO: Update placeholder value.
            var a = new DeleteSheetRequest();
            a.SheetId = 0;

            var t = new Request();
            t.DeleteSheet = a;

            requests.Add(t);

            // TODO: Assign values to desired properties of `requestBody`:
            BatchUpdateSpreadsheetRequest requestBody = new BatchUpdateSpreadsheetRequest();
            requestBody.Requests = requests;

            SpreadsheetsResource.BatchUpdateRequest req = sheetsService.Spreadsheets.BatchUpdate(requestBody, id);

            // To execute asynchronously in an async method, replace `request.Execute()` as shown:
            BatchUpdateSpreadsheetResponse response = req.Execute();*/
            #endregion

            #region criar planilha
            // TODO: Assign values to desired properties of `requestBody`:
            /*Spreadsheet requestBody3 = new Spreadsheet();
            var propriedades = new SpreadsheetProperties();
            propriedades.Title = "Games";
            requestBody3.Properties = propriedades;

            SpreadsheetsResource.CreateRequest request3 = sheetsService.Spreadsheets.Create(requestBody3);

            // To execute asynchronously in an async method, replace `request.Execute()` as shown:
            Spreadsheet response3 = request3.Execute();
            // Data.Spreadsheet response = await request.ExecuteAsync();

            // TODO: Change code below to process the `response` object:
            Console.WriteLine(response3);*/
            #endregion
        }

        [TestMethod]
        public void TesteDatas() {
            GamesEntities db = new GamesEntities();
            IgdbService igdb = new IgdbService();
            List<game_platform> games = db.game_platform.Where(gp => gp.release_date == null).Where(gp => gp.id_status == 1).ToList();
            foreach (game_platform g in games) {
                GameEntity jogo = db.game.Find(g.id_game);
                try {
                    platform plataforma = db.platform.Find(g.id_platform);
                    DadosGameResponse dados = igdb.DadosJogo(jogo.id_igdb.Value).FirstOrDefault();
                    ReleaseDate lancamento;
                    if (g.id_region == null) {
                        lancamento = dados.ReleaseDates.Where(r => r.Region == 8).Where(r => r.Platform == plataforma.id_igdb).FirstOrDefault();
                        if (lancamento == null) {
                            lancamento = dados.ReleaseDates.Where(r => r.Region == 2).Where(r => r.Platform == plataforma.id_igdb).FirstOrDefault();
                        }
                        if (lancamento != null) {
                            g.id_region = lancamento.Region;
                        }
                    }
                    else {
                        lancamento = dados.ReleaseDates.Where(r => r.Region == g.id_region).Where(r => r.Platform == plataforma.id_igdb).FirstOrDefault();
                        if (lancamento == null) {
                            lancamento = dados.ReleaseDates.Where(r => r.Region == 8).Where(r => r.Platform == plataforma.id_igdb).FirstOrDefault();
                        }
                        if (lancamento == null) {
                            lancamento = dados.ReleaseDates.Where(r => r.Region == 2).Where(r => r.Platform == plataforma.id_igdb).FirstOrDefault();
                        }
                        if (lancamento == null) {
                            if (plataforma.id != 7) {
                                lancamento = dados.ReleaseDates.Where(r => r.Region == 8).Where(r => r.Platform == 45).FirstOrDefault();
                                if (lancamento == null) {
                                    lancamento = dados.ReleaseDates.Where(r => r.Region == 2).Where(r => r.Platform == 45).FirstOrDefault();
                                }
                            }
                        }
                    }
                    if (lancamento != null) {
                        if (lancamento.Date != null) {
                            DateTime data = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(Convert.ToDouble(Convert.ToDouble(lancamento.Date)));
                            g.release_date = data;
                            db.Entry(g).State = EntityState.Modified;
                        }
                    }
                }
                catch (Exception ex) {
                    Console.Write(jogo.name+" "+ex.Message);
                }
            }
            db.SaveChanges();
        }

        [TestMethod]
        public async Task TesteMetacritic() {
            GamesEntities db = new GamesEntities();
            List<game_platform> games = db.game_platform.Where(gp => gp.metacritic == null).ToList();
            foreach (game_platform g in games) {
                GameEntity jogo = db.game.Find(g.id_game);
                platform plataforma = db.platform.Find(g.id_platform);
                string sigla;
                switch (plataforma.sigla) {
                    case "PS1":
                        sigla = "PS";
                    break;
                    case "PSVITA":
                        sigla = "VITA";
                    break;
                    default:
                        sigla = plataforma.sigla;
                    break;
                }
                try {
                    var metacritic = await Metacritic.SearchFor().Games().UsingTextAsync(jogo.name);
                    var resultado = metacritic.Where(m => m.Platform == sigla).Where(m => m.Name == jogo.name).FirstOrDefault();
                    if (resultado != null) {
                        g.metacritic = resultado.Score;
                        db.Entry(g).State = EntityState.Modified;
                    }
                }
                catch (Exception ex) {

                }
            }
            db.SaveChanges();
        }

    }
}
