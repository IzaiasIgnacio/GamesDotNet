using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Igdb.ResponseModels {
    public class Cover {
        [DataMember(Name = "cloudinary_id")]
        public int CloudinaryId { get; set; }
    }
}