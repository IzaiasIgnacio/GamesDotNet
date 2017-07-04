namespace GamesApi.RequestModels.Igdb {
    public class DadosGenreIgdbRequest {
        public string Fields {
            get {
                return "id,name";
            }
        }

        public int[] Ids { get; set; }
    }
}