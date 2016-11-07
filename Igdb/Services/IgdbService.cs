using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Igdb.Models;

namespace Igdb.Services {
    public class IgdbService {
        public BuscaResponse BuscarJogo(string busca) {
            BuscaRequest request = new BuscaRequest();
            request.Search = busca;
            return null;
        }
    }
}
