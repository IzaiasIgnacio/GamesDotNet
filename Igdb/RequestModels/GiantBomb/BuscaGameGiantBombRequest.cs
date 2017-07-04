namespace GamesApi.RequestModels.GiantBomb {
    public class BuscaGameGiantBombRequest {
        public string Fields {
            get {
                return "id,name,deck,platforms,image";
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
