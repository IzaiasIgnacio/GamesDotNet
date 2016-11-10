namespace Igdb.RequestModels {
    public class DadosGenreRequest {
        public string Fields {
            get {
                return "id,name";
            }
        }

        public int[] Ids { get; set; }
    }
}