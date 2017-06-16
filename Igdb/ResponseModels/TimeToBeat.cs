using System.Runtime.Serialization;

namespace Igdb.ResponseModels {
    [DataContract(Name = "TimeToBeat")]
    public class TimeToBeat {
        [DataMember(Name = "hastly")]
        public int Hastly { get; set; }

        [DataMember(Name = "normally")]
        public int Normally { get; set; }

        [DataMember(Name = "completely")]
        public int Completely { get; set; }
    }
}