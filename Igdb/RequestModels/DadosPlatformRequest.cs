namespace Igdb.RequestModels {
    public class DadosPlatformRequest {
        private string fields;
        private int id;

        public string Fields
        {
            get
            {
                return "id,name";
            }
        }

        public int[] Ids { get; set; }
    }
}