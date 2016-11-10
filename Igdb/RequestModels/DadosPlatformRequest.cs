namespace Igdb.RequestModels {
    public class DadosPlatformRequest {
        public string Fields {
            get {
                return "id,name";
            }
        }

        public int[] Ids { get; set; }
    }
}