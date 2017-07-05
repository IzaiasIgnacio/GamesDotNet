using System.Runtime.Serialization;

namespace GamesApi.ResponseModels.GiantBomb {
    [DataContract(Name = "Image")]
    public class Image {
        [DataMember(Name = "thumb_url")]
        public string thumb_url { get; set; }
        [DataMember(Name = "small_url")]
        public string small_url { get; set; }
        [DataMember(Name = "medium_url")]
        public string medium_url { get; set; }
        [DataMember(Name = "super_url")]
        public string super_url { get; set; }
    }
}