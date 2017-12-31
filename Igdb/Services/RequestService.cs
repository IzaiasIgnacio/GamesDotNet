using Igdb.RequestModels;
using System.IO;
using System.Net;

namespace Igdb.Services {
    public class RequestService {

        public string requestUrl = "https://api-2445582011268.apicast.io";

        public string DoRequest(string url) {
            string resposta = string.Empty;
            HttpWebRequest get = (HttpWebRequest)WebRequest.Create(url);
            get.AutomaticDecompression = DecompressionMethods.GZip;
            get.Accept = "application/json";
            get.Headers["user-key"] = "fe07e6780755d0352df34380835b942f";
            
            using (HttpWebResponse response = (HttpWebResponse)get.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                resposta = reader.ReadToEnd();
            }

            return resposta;
        }

        public string RequestBuscaGame(BuscaGameRequest request) {
            string url = requestUrl+"/games/?fields="+request.Fields+"&filter="+request.Platforms+"&limit="+request.Limit+"&search="+request.Search;

            return DoRequest(url);
        }

        public string RequestDadosGame(DadosGameRequest request) {
            string resposta = string.Empty;
            string url = requestUrl+"/games/"+request.Id+"?fields="+request.Fields;

            return DoRequest(url);
        }

        public string RequestDadosDeveloperPublisherGame(DadosDeveloperPublisherRequest request) {
            string resposta = string.Empty;
            string url = requestUrl + "/companies/" + string.Join(",",request.Ids)+"?fields=" + request.Fields;

            return DoRequest(url);
        }

        public string RequestDadosPlatform(DadosPlatformRequest request) {
            string resposta = string.Empty;
            string url = requestUrl + "/platforms/" + string.Join(",", request.Ids) + "?fields=" + request.Fields;

            return DoRequest(url);
        }

        public string RequestDadosGenre(DadosGenreRequest request) {
            string resposta = string.Empty;
            string url = requestUrl + "/genres/" + string.Join(",", request.Ids) + "?fields=" + request.Fields;

            return DoRequest(url);
        }

        public string RequestGameSeries(DadosGameSeriesRequest request) {
            string url = requestUrl + "/collections/" + request.Id + "?fields=" + request.Fields;

            return DoRequest(url);
        }
    }
}
