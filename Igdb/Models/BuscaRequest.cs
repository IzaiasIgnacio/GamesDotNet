using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Igdb.Models {
    public class BuscaRequest {
        [DataMember(Name = "fields")]
        public string Fields
        {
            get
            {
                return "id,name,summary,storyline,developers,publishers,genres,release_dates,videos,cover.cloudinary_id";
            }
        }

        [DataMember(Name = "limit")]
        public int Limit { get { return 10; } }

        [DataMember(Name = "order")]
        public string Order { get { return "release_dates.date:desc"; } }

        [DataMember(Name = "search")]
        public string Search { get; set; }
    }
}
