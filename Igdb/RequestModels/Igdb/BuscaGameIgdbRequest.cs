namespace GamesApi.RequestModels.Igdb {
    public class BuscaGameIgdbRequest {
        public string Fields {
            get {
                return "id,name,release_dates.platform,cover.cloudinary_id";
            }
        }

        public string Platforms {
            get {
                return "filter[release_dates.platform][in]=7,8,9,48,38,46,6";
            }
        }
        
        public int Limit { get { return 15; } }
        
        public string Search { get; set; }
    }
}
