using System.Runtime.Serialization;

namespace GamesApi.ResponseModels.Igdb {
    [DataContract(Name = "Cover")]
    public class Cover {
        [DataMember(Name = "cloudinary_id")]
        public string CloudinaryId { get; set; }
    }
}