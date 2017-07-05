using System.Collections.Generic;
using GamesApi.ResponseModels;
using GamesApi.RequestModels.GiantBomb;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;
using GamesApi.ResponseModels.GiantBomb;

namespace GamesApi.Services {
    public class GiantBombService {
        public List<BuscaGameGiantBombResponse> BuscarJogo(string busca) {
            RequestService requestService = new RequestService("giantbomb");
            BuscaGameGiantBombRequest request = new BuscaGameGiantBombRequest {
                Search = busca
            };

            string resposta = requestService.RequestBuscaGameGiantBomb(request);
            resposta = JObject.Parse(resposta).SelectToken("results").ToString();

            List<BuscaGameGiantBombResponse> response = JsonConvert.DeserializeObject<List<BuscaGameGiantBombResponse>>(resposta);
                        
            return response;
        }
        
        /*public List<DadosGameResponse> DadosJogo(int id) {
            RequestService requestService = new RequestService();
            DadosGameIgdbRequest request = new DadosGameIgdbRequest {
                Id = id
            };

            string resposta = requestService.RequestDadosGame(request);

            List<DadosGameResponse> response = JsonConvert.DeserializeObject<List<DadosGameResponse>>(resposta);

            return response;
        }

        public bool ValidarGamePlataforma(DadosGameResponse dados) {
            if (dados.ReleaseDates == null) {
                return false;
            }
            int[] plats = { 7, 8, 9, 48, 38, 46, 6 };
            return dados.ReleaseDates.Where(release => plats.Contains(release.Platform)).Any();
        }

        public List<DadosGameSeriesResponse> BuscaGameSeries(int id) {
            RequestService requestService = new RequestService();
            DadosGameSeriesRequest request = new DadosGameSeriesRequest {
                Id = id
            };

            string resposta = requestService.RequestGameSeries(request);

            List<DadosGameSeriesResponse> response = JsonConvert.DeserializeObject<List<DadosGameSeriesResponse>>(resposta);

            return response;
        }

        public List<DadosDeveloperPublisherResponse> DadosDeveloperPublisher(int[] devs) {
            RequestService requestService = new RequestService();
            DadosDeveloperPublisherIgdbRequest request = new DadosDeveloperPublisherIgdbRequest {
                Ids = devs
            };

            string resposta = requestService.RequestDadosDeveloperPublisherGame(request);

            List<DadosDeveloperPublisherResponse> response = JsonConvert.DeserializeObject<List<DadosDeveloperPublisherResponse>>(resposta);

            return response;
        }

        public List<DadosPlatformResponse> DadosPlatform(int[] plats) {
            RequestService requestService = new RequestService();
            DadosPlatformRequest request = new DadosPlatformRequest {
                Ids = plats
            };

            string resposta = requestService.RequestDadosPlatform(request);

            List<DadosPlatformResponse> response = JsonConvert.DeserializeObject<List<DadosPlatformResponse>>(resposta);

            return response;
        }

        public List<DadosGenreResponse> DadosGenre(int[] genres) {
            RequestService requestService = new RequestService();
            DadosGenreRequest request = new DadosGenreRequest {
                Ids = genres
            };

            string resposta = requestService.RequestDadosGenre(request);

            List<DadosGenreResponse> response = JsonConvert.DeserializeObject<List<DadosGenreResponse>>(resposta);

            return response;
        }*/
    }
}
