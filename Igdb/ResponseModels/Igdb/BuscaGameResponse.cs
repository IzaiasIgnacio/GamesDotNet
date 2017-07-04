using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GamesApi.ResponseModels.Igdb {
    [DataContract(Name = "BuscaGameResponse")]
    public class BuscaGameResponse : BaseJsonResponse {
        [DataMember(Name = "release_dates")]
        public List<ReleaseDate> ReleaseDates { get; set; }
        
        [DataMember(Name = "cover")]
        public Cover Cover { get; set; }
    }
}