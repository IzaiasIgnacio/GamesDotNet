using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace GamesApi.RequestModels.Igdb {
    public class DadosGameSeriesIgdbRequest {
        public string Fields {
            get {
                return "name,games";
            }
        }
        
        public int Id { get; set; }
    }
}
