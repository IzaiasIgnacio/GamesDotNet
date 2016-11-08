using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Igdb.RequestModels {
    public class GameRequest {

        private string fields;
        private int limit;
        private string order;
        private string search;

        public string Fields
        {
            get
            {
                return "id,name,summary,storyline,developers,publishers,genres,release_dates,videos,cover.cloudinary_id";
            }
        }
        
        public int Limit { get { return 10; } }
        
        public string Order { get { return "release_dates.date:desc"; } }
        
        public string Search { get; set; }
    }
}
