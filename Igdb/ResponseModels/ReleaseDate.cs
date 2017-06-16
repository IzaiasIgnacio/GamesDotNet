using System.Runtime.Serialization;

namespace Igdb.ResponseModels {
    [DataContract(Name = "ReleaseDate")]
    public class ReleaseDate {
        [DataMember(Name = "platform")]
        public int Platform { get; set; }

        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "region")]
        public int Region { get; set; }

        [DataMember(Name = "human")]
        public string Human { get; set; }
    }
}