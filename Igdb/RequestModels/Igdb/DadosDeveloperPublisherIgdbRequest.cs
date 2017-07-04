using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace GamesApi.RequestModels.Igdb {
    public class DadosDeveloperPublisherIgdbRequest {
        public string Fields {
            get {
                return "id,name";
            }
        }
        
        public int[] Ids { get; set; }
    }
}
