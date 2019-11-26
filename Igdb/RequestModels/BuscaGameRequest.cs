using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Igdb.RequestModels {
    public class BuscaGameRequest {
        public string Fields {
            get {
                return "id,name,release_dates.platform,cover";
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
