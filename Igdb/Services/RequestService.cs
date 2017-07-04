using GamesApi.RequestModels.Igdb;
using GamesApi.RequestModels.GiantBomb;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace GamesApi.Services {
    public class RequestService {

        private WebHeaderCollection headers = null;
        private string key;

        public RequestService(string api) {
            headers = new WebHeaderCollection();
            switch (api) {
                case "igdb":
                    headers["X-Mashape-Key"] = "UHp7vqCOoumshnITXnFRvjeVRX6Bp1Euzr2jsnSUkkS2pxGYC2";
                break;
                case "giantbomb":
                    key = "78f0ed0d0ea29e0b2a68464eeeff31fec85c3dda";
                break;
            }
        }

        public string DoRequest(string url) {
            string resposta = string.Empty;
            HttpWebRequest get = (HttpWebRequest)WebRequest.Create(url);
            get.AutomaticDecompression = DecompressionMethods.GZip;
            get.Accept = "application/json";
            get.Headers = headers;
            get.UserAgent = "Battou";
            using (HttpWebResponse response = (HttpWebResponse)get.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                resposta = reader.ReadToEnd();
            }

            return resposta;
        }

        public string RequestBuscaGameGiantBomb(BuscaGameGiantBombRequest request) {
            string url = "http://www.giantbomb.com/api/search/?api_key="+key+"&format=json&query=" + request.Search + "&resources=game&field_list="
                + request.Fields;

            return DoRequest(url);
        }

        #region Igdb
        public string RequestBuscaGameIgdb(BuscaGameIgdbRequest request) {
            string url = @"https://igdbcom-internet-game-database-v1.p.mashape.com/games/?fields="+request.Fields+"&filter="+request.Platforms+
                "&limit="+request.Limit+"&search="+request.Search;

            return DoRequest(url);
        }

        public string RequestDadosGameIgdb(DadosGameIgdbRequest request) {
            string resposta = string.Empty;
            string url = @"https://igdbcom-internet-game-database-v1.p.mashape.com/games/"+request.Id+"?fields="+request.Fields;

            return DoRequest(url);
        }

        public string RequestDadosDeveloperPublisherGameIgdb(DadosDeveloperPublisherIgdbRequest request) {
            string resposta = string.Empty;
            string url = @"https://igdbcom-internet-game-database-v1.p.mashape.com/companies/"+string.Join(",",request.Ids)+"?fields=" + request.Fields;

            return DoRequest(url);
        }

        public string RequestDadosPlatformIgdb(DadosPlatformIgdbRequest request) {
            string resposta = string.Empty;
            string url = @"https://igdbcom-internet-game-database-v1.p.mashape.com/platforms/" + string.Join(",", request.Ids) + "?fields=" + request.Fields;

            return DoRequest(url);
        }

        public string RequestDadosGenreIgdb(DadosGenreIgdbRequest request) {
            string resposta = string.Empty;
            string url = @"https://igdbcom-internet-game-database-v1.p.mashape.com/genres/" + string.Join(",", request.Ids) + "?fields=" + request.Fields;

            return DoRequest(url);
        }

        public string RequestGameSeriesIgdb(DadosGameSeriesIgdbRequest request) {
            string url = @"https://igdbcom-internet-game-database-v1.p.mashape.com/collections/" + request.Id + "?fields=" + request.Fields;

            return DoRequest(url);
        }
        #endregion
    }
}
