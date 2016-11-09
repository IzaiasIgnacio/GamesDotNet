using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Igdb.RequestModels {
    public class DadosDeveloperPublisherRequest {

        private string fields;
        private int id;

        public string Fields {
            get {
                return "id,name";
            }
        }
        
        public int[] Ids { get; set; }
    }
}
