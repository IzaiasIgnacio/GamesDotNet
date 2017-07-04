using System.Runtime.Serialization;

namespace GamesApi.ResponseModels {
    [DataContract(Name = "BaseJsonResponse")]
    public class BaseJsonResponse {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
