using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Igdb.RequestModels {
    public class DadosGameSeriesRequest {
        public string Fields {
            get {
                return "name,games";
            }
        }
        
        public int Id { get; set; }
    }
}
