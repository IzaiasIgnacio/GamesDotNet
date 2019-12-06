using System.Collections.Generic;
using Igdb.ResponseModels;
using Igdb.RequestModels;
using Newtonsoft.Json;
using System.Linq;

namespace Igdb.Services {
    public class IgdbService {
        public List<BuscaGameResponse> BuscarJogo(string busca) {
            RequestService requestService = new RequestService();
            BuscaGameRequest request = new BuscaGameRequest {
                Search = busca
            };

            string resposta = requestService.RequestBuscaGame(request);

            List<BuscaGameResponse> response = JsonConvert.DeserializeObject<List<BuscaGameResponse>>(resposta);
                        
            return response;
        }
        
        public List<DadosGameResponse> DadosJogo(int id) {
            RequestService requestService = new RequestService();
            DadosGameRequest request = new DadosGameRequest {
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
            return null;
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
            DadosDeveloperPublisherRequest request = new DadosDeveloperPublisherRequest {
                Ids = devs
            };

            string resposta = requestService.RequestDadosDeveloperPublisherGame(request);

            List<DadosDeveloperPublisherResponse> response = JsonConvert.DeserializeObject<List<DadosDeveloperPublisherResponse>>(resposta);

            return response;
        }

        public List<DadosPlatformResponse> DadosPlatform(int[] plats) {
            return null;
            RequestService requestService = new RequestService();
            DadosPlatformRequest request = new DadosPlatformRequest {
                Ids = plats
            };

            string resposta = requestService.RequestDadosPlatform(request);

            List<DadosPlatformResponse> response = JsonConvert.DeserializeObject<List<DadosPlatformResponse>>(resposta);

            return response;
        }

        public List<DadosGenreResponse> DadosGenre(int[] genres) {
            return null;
            RequestService requestService = new RequestService();
            DadosGenreRequest request = new DadosGenreRequest {
                Ids = genres
            };

            string resposta = requestService.RequestDadosGenre(request);

            List<DadosGenreResponse> response = JsonConvert.DeserializeObject<List<DadosGenreResponse>>(resposta);

            return response;
        }
    }
}
