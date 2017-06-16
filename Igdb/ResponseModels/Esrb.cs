using System.Runtime.Serialization;

namespace Igdb.ResponseModels {
    [DataContract(Name = "Esrb")]
    public class Esrb {
        [DataMember(Name = "rating")]
        public int Rating { get; set; }
    }
}