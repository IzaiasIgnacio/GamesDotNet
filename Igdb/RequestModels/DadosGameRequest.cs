using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Igdb.RequestModels {
    public class DadosGameRequest {
        public string Fields {
            get {
                return "id,name,summary,storyline,developers,publishers,genres,release_dates,videos,cover.cloudinary_id";
            }
        }
        
        public int Id { get; set; }
    }
}
