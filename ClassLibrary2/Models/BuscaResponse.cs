using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Igdb.Models {
    public class BuscaResponse : BaseJsonResponse {
        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        [DataMember(Name = "storyline")]
        public string Storyline { get; set; }

        [DataMember(Name = "developers")]
        public List<Developer> Developers { get; set; }

        [DataMember(Name = "publishers")]
        public List<Publisher> Publishers { get; set; }

        [DataMember(Name = "genres")]
        public List<Genre> Genres { get; set; }

        [DataMember(Name = "release_dates")]
        public List<ReleaseDate> ReleaseDates { get; set; }

        [DataMember(Name = "videos")]
        public List<Video> Videos { get; set; }

        [DataMember(Name = "cover")]
        public Cover Cover { get; set; }
    }
}
