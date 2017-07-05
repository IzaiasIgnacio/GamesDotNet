using System.Runtime.Serialization;

namespace GamesApi.ResponseModels.GiantBomb {
    [DataContract(Name = "BuscaGameGiantBombResponse")]
    public class BuscaGameGiantBombResponse : BaseJsonResponse {
        [DataMember(Name = "image")]
        public Image Image { get; set; }
    }
}