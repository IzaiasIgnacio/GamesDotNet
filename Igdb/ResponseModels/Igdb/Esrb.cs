using System.Runtime.Serialization;

namespace GamesApi.ResponseModels.Igdb {
    [DataContract(Name = "Esrb")]
    public class Esrb {
        [DataMember(Name = "rating")]
        public int Rating { get; set; }
    }
}