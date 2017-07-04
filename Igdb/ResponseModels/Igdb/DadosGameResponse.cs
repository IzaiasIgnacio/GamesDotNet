using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GamesApi.ResponseModels.Igdb {
    [DataContract(Name = "DadosGameResponse")]
    public class DadosGameResponse : BaseJsonResponse {
        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        [DataMember(Name = "storyline")]
        public string Storyline { get; set; }

        [DataMember(Name = "developers")]
        public List<int> Developers { get; set; }

        [DataMember(Name = "publishers")]
        public List<int> Publishers { get; set; }

        [DataMember(Name = "genres")]
        public List<int> Genres { get; set; }

        [DataMember(Name = "release_dates")]
        public List<ReleaseDate> ReleaseDates { get; set; }

        [DataMember(Name = "videos")]
        public List<Video> Videos { get; set; }

        [DataMember(Name = "cover")]
        public Cover Cover { get; set; }

        [DataMember(Name = "collection")]
        public int Collection { get; set; }

        [DataMember(Name = "game_engines")]
        public List<GameEngine> GameEngines { get; set; }

        [DataMember(Name = "time_to_beat")]
        public TimeToBeat TimeToBeat { get; set; }

        [DataMember(Name = "esrb")]
        public Esrb Esrb { get; set; }

        [DataMember(Name = "pegi")]
        public Pegi Pegi { get; set; }
    }
}
