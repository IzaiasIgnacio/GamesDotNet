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
        public GameResponse BuscarJogo(string busca) {
            RequestService requestService = new RequestService();
            GameRequest request = new GameRequest {
                Search = busca
            };

            string resposta = requestService.RequestGame(request);

            List<GameResponse> response = JsonConvert.DeserializeObject<List<GameResponse>>(resposta);
                        
            return null;
        }
    }
}
