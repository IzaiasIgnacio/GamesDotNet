using Igdb.RequestModels;
using System.IO;
using System.Net;
using System.Text;

namespace Igdb.Services {
    public class RequestService {

        public string requestUrl = "http://localhost:8080/ICIGames/legado";

        public string DoRequest(string url) {
            string resposta = string.Empty;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest get = (HttpWebRequest)WebRequest.Create(url);
            get.AutomaticDecompression = DecompressionMethods.GZip;
            get.Accept = "application/json";
            //get.Headers["user-key"] = "013620f2a5966f7277677d89841e6262";

            //get.Method = "POST";
            //var postData = "fields id,name,release_dates.platform,cover;limit 15;search infamous;where release_dates.platform = (7, 8, 9, 48, 38, 46, 6);";
            //var data = Encoding.ASCII.GetBytes(postData);
            //get.ContentLength = data.Length;
            //using (var stream = get.GetRequestStream()) {
            //    stream.Write(data, 0, data.Length);
            //}

            using (HttpWebResponse response = (HttpWebResponse)get.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                resposta = reader.ReadToEnd();
            }

            return resposta;
        }

        public string RequestBuscaGame(BuscaGameRequest request) {
            //string url = requestUrl+"/games/?fields="+request.Fields+"&filter="+request.Platforms+"&limit="+request.Limit+"&search="+request.Search;
            //string url = requestUrl + "/games/?fields=" + request.Fields + "&search=" + request.Search;
            string url = requestUrl + "/buscar_jogos/" + request.Search;

            return DoRequest(url);
        }

        public string RequestDadosGame(DadosGameRequest request) {
            string resposta = string.Empty;
            //string url = requestUrl+"/games/"+request.Id+"?fields="+request.Fields;
            string url = requestUrl + "/buscar_dados_jogo/" + request.Id;

            return DoRequest(url);
        }

        public string RequestDadosDeveloperPublisherGame(DadosDeveloperPublisherRequest request) {
            string resposta = string.Empty;
            string url = requestUrl + "/buscar_dados_empresas/" + string.Join(",",request.Ids);

            return DoRequest(url);
        }

        public string RequestDadosPlatform(DadosPlatformRequest request) {
            return null;
            string resposta = string.Empty;
            string url = requestUrl + "/platforms/" + string.Join(",", request.Ids) + "?fields=" + request.Fields;

            return DoRequest(url);
        }

        public string RequestDadosGenre(DadosGenreRequest request) {
            string resposta = string.Empty;
            string url = requestUrl + "/buscar_dados_generos/" + string.Join(",", request.Ids);

            return DoRequest(url);
        }

        public string RequestGameSeries(DadosGameSeriesRequest request) {
            return null;
            string url = requestUrl + "/collections/" + request.Id + "?fields=" + request.Fields;

            return DoRequest(url);
        }
    }
}
