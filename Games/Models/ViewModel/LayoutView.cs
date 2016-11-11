using Games.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Games.Models.ViewModel {
    public class LayoutView {
        public List<platform> ListaPlatform { get; set; }
    }
}