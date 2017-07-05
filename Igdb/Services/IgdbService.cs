using System.Collections.Generic;
using GamesApi.ResponseModels;
using GamesApi.RequestModels.Igdb;
using Newtonsoft.Json;
using System.Linq;
using GamesApi.ResponseModels.Igdb;

namespace GamesApi.Services {
    public class IgdbService {
        public List<BuscaGameIgdbResponse> BuscarJogo(string busca) {
            RequestService requestService = new RequestService("igdb");
            BuscaGameIgdbRequest request = new BuscaGameIgdbRequest {
                Search = busca
            };

            string resposta = requestService.RequestBuscaGameIgdb(request);

            List<BuscaGameIgdbResponse> response = JsonConvert.DeserializeObject<List<BuscaGameIgdbResponse>>(resposta);
                        
            return response;
        }
        
        public List<DadosGameResponse> DadosJogo(int id) {
            RequestService requestService = new RequestService("igdb");
            DadosGameIgdbRequest request = new DadosGameIgdbRequest {
                Id = id
            };

            string resposta = requestService.RequestDadosGameIgdb(request);

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
            RequestService requestService = new RequestService("igdb");
            DadosGameSeriesIgdbRequest request = new DadosGameSeriesIgdbRequest {
                Id = id
            };

            string resposta = requestService.RequestGameSeriesIgdb(request);

            List<DadosGameSeriesResponse> response = JsonConvert.DeserializeObject<List<DadosGameSeriesResponse>>(resposta);

            return response;
        }

        public List<DadosDeveloperPublisherResponse> DadosDeveloperPublisher(int[] devs) {
            RequestService requestService = new RequestService("igdb");
            DadosDeveloperPublisherIgdbRequest request = new DadosDeveloperPublisherIgdbRequest {
                Ids = devs
            };

            string resposta = requestService.RequestDadosDeveloperPublisherGameIgdb(request);

            List<DadosDeveloperPublisherResponse> response = JsonConvert.DeserializeObject<List<DadosDeveloperPublisherResponse>>(resposta);

            return response;
        }

        public List<DadosPlatformResponse> DadosPlatform(int[] plats) {
            RequestService requestService = new RequestService("igdb");
            DadosPlatformIgdbRequest request = new DadosPlatformIgdbRequest {
                Ids = plats
            };

            string resposta = requestService.RequestDadosPlatformIgdb(request);

            List<DadosPlatformResponse> response = JsonConvert.DeserializeObject<List<DadosPlatformResponse>>(resposta);

            return response;
        }

        public List<DadosGenreResponse> DadosGenre(int[] genres) {
            RequestService requestService = new RequestService("igdb");
            DadosGenreIgdbRequest request = new DadosGenreIgdbRequest {
                Ids = genres
            };

            string resposta = requestService.RequestDadosGenreIgdb(request);

            List<DadosGenreResponse> response = JsonConvert.DeserializeObject<List<DadosGenreResponse>>(resposta);

            return response;
        }
    }
}
