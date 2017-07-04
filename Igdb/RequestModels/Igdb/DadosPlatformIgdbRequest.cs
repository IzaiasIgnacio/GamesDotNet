namespace GamesApi.RequestModels.Igdb {
    public class DadosPlatformIgdbRequest {
        public string Fields {
            get {
                return "id,name";
            }
        }

        public int[] Ids { get; set; }
    }
}