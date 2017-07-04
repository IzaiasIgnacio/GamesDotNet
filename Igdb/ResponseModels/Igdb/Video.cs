using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace GamesApi.ResponseModels.Igdb {
    [DataContract(Name = "Video")]
    public class Video {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "video_id")]
        public string VideoId { get; set; }
    }
}