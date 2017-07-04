using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace GamesApi.ResponseModels.Igdb {
    [DataContract(Name = "Cover")]
    public class Cover {
        [DataMember(Name = "cloudinary_id")]
        public string CloudinaryId { get; set; }
    }
}