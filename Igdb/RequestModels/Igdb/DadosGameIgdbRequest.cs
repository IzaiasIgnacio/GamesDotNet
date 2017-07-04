using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace GamesApi.RequestModels.Igdb {
    public class DadosGameIgdbRequest {
        public string Fields {
            get {
                return "id,name,summary,storyline,developers,publishers,genres,release_dates,videos,cover.cloudinary_id,collection";
            }
        }
        
        public int Id { get; set; }
    }
}
