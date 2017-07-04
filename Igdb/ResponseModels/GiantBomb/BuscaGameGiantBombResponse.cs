using System.Runtime.Serialization;

namespace GamesApi.ResponseModels.GiantBomb {
    [DataContract(Name = "BuscaGameResponse")]
    public class BuscaGameGiantBombResponse : BaseJsonResponse {
        /*[DataMember(Name = "release_dates")]
        public List<ReleaseDate> ReleaseDates { get; set; }
        
        [DataMember(Name = "cover")]
        public Cover Cover { get; set; }*/
    }
}