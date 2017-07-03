using FluentMetacritic;
using Games.Models.Entity;
using Games.Models.Repository;
using Games.Models.Validacao;
using Games.Models.ViewModel;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Igdb.ResponseModels;
using Igdb.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using static Games.Models.ViewModel.GameListView;

namespace Games.Controllers {
    public class JqueryController : BaseController {

        public ActionResult MenuPlataformas(int? plataforma) {
            if (plataforma.HasValue) {
                layoutView.ativos = new List<int>();
                layoutView.ativos.Add(plataforma.Value);
            }
            return PartialView("PlatformMenuView", layoutView);
        }

        public ActionResult Menu() {
            return PartialView("MenuView", layoutView);
        }

        public ActionResult ListaJogosIndex(string status) {
            string tipo = null;
            GameRepository gameRepository = new GameRepository();
            GameListView view = new GameListView();

            switch (status) {
                case "wishlist":
                    view.ListaJogos = gameRepository.ListarJogosWishlist(layoutView.ativos);
                break;
                case "watchlist":
                    view.ListaJogos = gameRepository.ListarJogos(layoutView.ativos, 3);
                break;
                default:
                    view.ListaJogos = gameRepository.ListarJogos(layoutView.ativos, (int)Enum.Parse(typeof(GameListView.status), status));
                break;
            }

            if (tipo == null) {
                switch (status) {
                    case "colecao":
                        tipo = "GameGridView";
                    break;
                    case "wishlist":
                        tipo = "GameListView";
                    break;
                    case "watchlist":
                        tipo = "GameListView";
                    break;
                    case "plus":
                        tipo = "GameGridView";
                    break;
                }
            }
            return PartialView(tipo, view);
        }

        [HttpPost]
        public ActionResult BuscarJogoIgdbJquery(string search) {
            IgdbService igdb = new IgdbService();
            GameResultView view = new GameResultView();

            view.ListaJogos = igdb.BuscarJogo(search);

            return PartialView("GameResultView", view);
        }

        [HttpPost]
        public ActionResult BuscarJogoEntityJquery(string search) {
            GameRepository gameRepository = new GameRepository();
            BuscaResultView view = new BuscaResultView();

            view.ListaJogosEntity = gameRepository.BuscarJogos(search);

            return PartialView("BuscaResultView", view);
        }

        [HttpPost]
        public ActionResult ExibirFormGame() {
            return PartialView("FormGameView", GameDataView.init());
        }
        
        [HttpPost]
        public JsonResult SalvarNovoJogoJquery(GameDataView dados) {
            ValidacaoGameMessage validacao = ValidacaoGameService.Validar(dados);
            if (validacao.Valido) {
                GameRepository gameRepository = new GameRepository();
                gameRepository.Adicionar(dados);
            }
            return Json(validacao);
        }

        [HttpPost]
        public JsonResult AlterarJogoJquery(GameDataView dados) {
            ValidacaoGameMessage validacao = ValidacaoGameService.Validar(dados);
            if (validacao.Valido) {
                GameRepository gameRepository = new GameRepository();
                gameRepository.Alterar(dados);
            }
            return Json(validacao);
        }

        [HttpPost]
        public void AtualizarOrdemWishlistJquery(int game, int posicao) {
            GameRepository gameRepository = new GameRepository();
            gameRepository.AtualizarOrdemWishlist(game, posicao);
        }

        [HttpPost]
        public void ExcluirJogoJquery(int id) {
            GameRepository gameRepository = new GameRepository();
            GameEntity game = gameRepository.BuscarDados(id);

            gameRepository.Excluir(game);
        }

        [HttpPost]
        public ActionResult RemoverPlataformaJquery(int index) {
            GameDataView view = GameDataView.GetGameDataView();
            view.Platforms.RemoveAt(index);
            return PartialView("PlatformStatusView", view);
        }

        [HttpPost]
        public ActionResult AdicionarPlataformaJquery() {
            GameDataView view = GameDataView.GetGameDataView();
            view.Platforms.Add(new game_platform());
            return PartialView("PlatformStatusView", view);
        }

        [HttpPost]
        public async Task<ActionResult> PreencherDadosGameIgdbJquery(int id_igdb, int Id = 0) {
            IgdbService igdb = new IgdbService();
            DadosGameResponse response = igdb.DadosJogo(id_igdb).FirstOrDefault();
            List<DadosDeveloperPublisherResponse> devs = new List<DadosDeveloperPublisherResponse>();
            List<DadosDeveloperPublisherResponse> pubs = new List<DadosDeveloperPublisherResponse>();
            List<DadosGenreResponse> genres = new List<DadosGenreResponse>();
            List<ReleaseDate> lancamentos = new List<ReleaseDate>();

            PlatformRepository pr = new PlatformRepository();

            if (response.Developers != null)
                devs = igdb.DadosDeveloperPublisher(response.Developers.ToArray());
            if (response.Developers != null)
                pubs = igdb.DadosDeveloperPublisher(response.Publishers.ToArray());
            if (response.Genres != null)
                genres = igdb.DadosGenre(response.Genres.ToArray());
            if (response.ReleaseDates != null)
                lancamentos = response.ReleaseDates;

            GameDataView gameDataView = GameDataView.GetGameDataView();
            gameDataView.Id = Id;
            gameDataView.id_igdb = id_igdb;
            gameDataView.Titulo = response.Name;
            gameDataView.Descricao = response.Summary;

            if (response.Cover != null) {
                gameDataView.Imagem = gameDataView.BigCoverUrl + response.Cover.CloudinaryId + ".jpg";
                gameDataView.CloudnaryId = response.Cover.CloudinaryId;
            }
            else {
                gameDataView.Imagem = "/Content/ps.png";
            }

            gameDataView.InitListas();

            foreach (DadosDeveloperPublisherResponse dev in devs) {
                gameDataView.ListaDeveloper.Add(new developerPublisher {
                    name = dev.Name,
                    id_igdb = dev.Id
                });
            }

            foreach (DadosDeveloperPublisherResponse pub in pubs) {
                gameDataView.ListaPublisher.Add(new developerPublisher {
                    name = pub.Name,
                    id_igdb = pub.Id
                });
            }

            foreach (DadosGenreResponse genre in genres) {
                gameDataView.ListaGenre.Add(new genre {
                    id_igdb = genre.Id,
                    name = genre.Name
                });
            }

            try {
                var buscaMetacritic = await Metacritic.SearchFor().Games().UsingTextAsync(response.Name);

                foreach (ReleaseDate lancamento in lancamentos) {
                    platform plataforma = pr.GetPlatformByIgdb(lancamento.Platform);
                    int? meta = null;
                    if (plataforma != null) {
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

                        var resultado = buscaMetacritic.Where(m => m.Platform == sigla).Where(m => m.Name.ToLowerInvariant() == response.Name.ToLowerInvariant()).FirstOrDefault();
                        if (resultado != null) {
                            meta = resultado.Score;
                        }

                        DateTime data = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(Convert.ToDouble(Convert.ToDouble(lancamento.Date)));
                        gameDataView.Platforms.Add(new game_platform {
                            id_platform = plataforma.id,
                            release_date = data,
                            metacritic = meta,
                            id_region = lancamento.Region
                        });
                    }
                }
            }
            catch (Exception ex) {

            }

            return PartialView("FormGameView", gameDataView);
        }

        [HttpPost]
        public ActionResult PreencherDadosGameEntityJquery(int id) {
            GameRepository gameRepository = new GameRepository();
            DeveloperPublisherRepository devPubRepository = new DeveloperPublisherRepository();
            GameEntity game = gameRepository.BuscarDados(id);

            List<game_developerPublisher> devs = game.game_developerPublisher.Where(d => d.tipo == (int)GameDataView.tipoDeveloperPublisher.Developer).ToList();
            List<game_developerPublisher> pubs = game.game_developerPublisher.Where(p => p.tipo == (int)GameDataView.tipoDeveloperPublisher.Publisher).ToList();
            List<game_genre> genres = game.game_genre.ToList();

            GameDataView gameDataView = GameDataView.init();
            gameDataView.Id = game.id;
            gameDataView.id_igdb = game.id_igdb;
            gameDataView.Titulo = game.name;
            gameDataView.Descricao = game.summary;
            gameDataView.Nota = game.nota;
            gameDataView.Completo = game.completo;
            
            string arquivo = gameDataView.Imagesfolder + game.id + "_BigCover_" + game.cloudnary_id + ".jpg";
            if (System.IO.File.Exists(arquivo)) {
                gameDataView.Imagem = "~/images/" + game.id + "_BigCover_" + game.cloudnary_id + ".jpg";
                gameDataView.CloudnaryId = game.cloudnary_id;
            }

            foreach (game_developerPublisher dev in devs) {
                gameDataView.ListaDeveloper.Add(new developerPublisher {
                    id = dev.id,
                    id_igdb = devPubRepository.GetIgdbById(dev.id_developerPublisher),
                    name = dev.developerPublisher.name
                });
            }

            foreach (game_developerPublisher pub in pubs) {
                gameDataView.ListaPublisher.Add(new developerPublisher {
                    id = pub.id,
                    id_igdb = devPubRepository.GetIgdbById(pub.id_developerPublisher),
                    name = pub.developerPublisher.name
                });
            }

            foreach (game_genre genre in genres) {
                gameDataView.ListaGenre.Add(new genre {
                    id = genre.id,
                    name = genre.genre.name
                });
            }

            foreach (game_platform lancamento in game.game_platform) {
                gameDataView.Platforms.Add(new game_platform {
                    id = lancamento.id,
                    release_date = lancamento.release_date,
                    metacritic = lancamento.metacritic,
                    preco = lancamento.preco,
                    formato = lancamento.formato,
                    tamanho = lancamento.tamanho,
                    id_platform = lancamento.id_platform,
                    id_status = lancamento.id_status,
                    id_region = lancamento.id_region,
                    id_rating = lancamento.id_rating,
                    id_store = lancamento.id_store,
                    store = lancamento.store
                });
            }
            
            return PartialView("FormGameView", gameDataView);
        }

        [HttpPost]
        public ActionResult ExibirDadosGameJquery(int id, bool full = false) {
            GameRepository gameRepository = new GameRepository();
            GameEntity game = gameRepository.BuscarDados(id);

            List<game_developerPublisher> devs = game.game_developerPublisher.Where(d => d.tipo == (int)GameDataView.tipoDeveloperPublisher.Developer).ToList();
            List<game_developerPublisher> pubs = game.game_developerPublisher.Where(p => p.tipo == (int)GameDataView.tipoDeveloperPublisher.Publisher).ToList();
            List<game_genre> genres = game.game_genre.ToList();

            DadosGameView dadosGameView = new DadosGameView();
            dadosGameView.Id = id;
            dadosGameView.Titulo = game.name;
            dadosGameView.CloudnaryId = game.cloudnary_id;
            dadosGameView.Nota = game.nota;
            dadosGameView.Completo = game.completo;
            dadosGameView.Descricao = game.summary;

            foreach (game_developerPublisher dev in devs) {
                dadosGameView.ListaDeveloper.Add(new developerPublisher {
                    name = dev.developerPublisher.name
                });
            }

            foreach (game_developerPublisher pub in pubs) {
                dadosGameView.ListaPublisher.Add(new developerPublisher {
                    name = pub.developerPublisher.name
                });
            }

            foreach (game_genre genre in genres) {
                dadosGameView.ListaGenre.Add(new genre {
                    name = genre.genre.name
                });
            }

            foreach (game_platform lancamento in game.game_platform) {
                dadosGameView.Platforms.Add(new game_platform {
                    release_date = lancamento.release_date,
                    metacritic = lancamento.metacritic,
                    preco = lancamento.preco,
                    formato = lancamento.formato,
                    tamanho = lancamento.tamanho,
                    status = lancamento.status,
                    region = lancamento.region,
                    rating = lancamento.rating,
                    store = lancamento.store,
                    platform = lancamento.platform
                });
            }

            string destino = "DadosGameView";
            if (full) {
                destino = "DadosGameFullView";
            }
            return PartialView(destino, dadosGameView);
        }

        [HttpPost]
        public void Teste() {
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
                    Console.Write(jogo.name + " " + ex.Message);
                }
            }
            db.SaveChanges();
        }

        [HttpPost]
        public void ExportarJogosJquery() {
            string[] Scopes = { SheetsService.Scope.Spreadsheets };

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
            
            GameRepository game = new GameRepository();
            PlatformRepository plataforma = new PlatformRepository();

            SpreadsheetsResource.GetRequest get = sheetsService.Spreadsheets.Get(id);
            Spreadsheet planilhas = get.Execute();

            SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum valueInputOption = (SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum)2;

            foreach (Sheet planilha in planilhas.Sheets) {
                var aba = planilha.Properties.Title;

                ClearValuesRequest clearRequest = new ClearValuesRequest();
                SpreadsheetsResource.ValuesResource.ClearRequest request = sheetsService.Spreadsheets.Values.Clear(clearRequest, id, aba + "!A1:Z1000");
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

                string range = aba + "!A1:D" + lista.Count + 1;

                List<IList<object>> dados = new List<IList<object>>();
                dados.Add(cabecalho);

                foreach (GameView jogo in lista) {
                    if (cabecalho.Count == 2) {
                        dados.Add(new List<object>() { "=IMAGE(\"https://images.igdb.com/igdb/image/upload/t_micro/" + jogo.CloudnaryId + ".jpg\")", jogo.Name });
                    }
                    else {
                        string data = null;
                        if (jogo.ReleaseDate != null) {
                            data = jogo.ReleaseDate.Value.ToShortDateString();
                        }
                        dados.Add(new List<object>() { "=IMAGE(\"https://images.igdb.com/igdb/image/upload/t_micro/" + jogo.CloudnaryId + ".jpg\")", jogo.Name, data, String.Join(", ", jogo.Plataformas) });
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
        }

    }
}