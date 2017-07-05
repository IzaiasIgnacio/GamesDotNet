using System.Runtime.Serialization;

namespace GamesApi.ResponseModels.Igdb {
    [DataContract(Name = "BuscaGameResponse")]
    public class BuscaGameIgdbResponse : BaseJsonResponse {
        [DataMember(Name = "cover")]
        public Cover Cover { get; set; }
    }
}