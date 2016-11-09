using Igdb.RequestModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Igdb.Services {
    public class RequestService {
        public string DoRequest(string url) {
            string resposta = string.Empty;
            HttpWebRequest get = (HttpWebRequest)WebRequest.Create(url);
            get.AutomaticDecompression = DecompressionMethods.GZip;
            get.Accept = "application/json";
            get.Headers["X-Mashape-Key"] = "UHp7vqCOoumshnITXnFRvjeVRX6Bp1Euzr2jsnSUkkS2pxGYC2";
            using (HttpWebResponse response = (HttpWebResponse)get.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                resposta = reader.ReadToEnd();
            }

            return resposta;
        }

        public string RequestBuscaGame(BuscaGameRequest request) {
            string url = @"https://igdbcom-internet-game-database-v1.p.mashape.com/games/?fields="+request.Fields+"&limit="+request.Limit+
                "&order="+request.Order+"&search="+request.Search;

            return DoRequest(url);
        }

        public string RequestDadosGame(DadosGameRequest request) {
            string resposta = string.Empty;
            string url = @"https://igdbcom-internet-game-database-v1.p.mashape.com/games/"+request.Id+"?fields="+request.Fields;

            return DoRequest(url);
        }

        public string RequestDadosDeveloperPublisherGame(DadosDeveloperPublisherRequest request) {
            string resposta = string.Empty;
            string url = @"https://igdbcom-internet-game-database-v1.p.mashape.com/companies/"+string.Join(",",request.Ids)+"?fields=" + request.Fields;

            return DoRequest(url);
        }

        public string RequestDadosPlatform(DadosPlatformRequest request) {
            string resposta = string.Empty;
            string url = @"https://igdbcom-internet-game-database-v1.p.mashape.com/platforms/" + string.Join(",", request.Ids) + "?fields=" + request.Fields;

            return DoRequest(url);
        }
    }
}
