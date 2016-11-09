using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Igdb.ResponseModels;
using System.Net;
using System.IO;
using Igdb.RequestModels;
using Newtonsoft.Json;

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
    }
}
