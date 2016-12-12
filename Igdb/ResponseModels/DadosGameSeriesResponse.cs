using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Igdb.ResponseModels {
    [DataContract(Name = "DadosGameResponse")]
    public class DadosGameSeriesResponse : BaseJsonResponse {
        [DataMember(Name = "games")]
        public List<int> Games { get; set; }
    }
}
