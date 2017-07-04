using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GamesApi.ResponseModels.Igdb {
    [DataContract(Name = "DadosGameResponse")]
    public class DadosGameSeriesResponse : BaseJsonResponse {
        [DataMember(Name = "games")]
        public List<int> Games { get; set; }
    }
}
