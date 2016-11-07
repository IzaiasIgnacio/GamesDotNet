using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Igdb.Models {
    public class ReleaseDate {
        [DataMember(Name = "platform")]
        public Platform Platform { get; set; }

        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "region")]
        public int Region { get; set; }
    }
}