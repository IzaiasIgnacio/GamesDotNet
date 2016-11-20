using Games.Models.Repository;
using Games.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Games.Controllers {
    public class HomeController : Controller {

        public ActionResult Index() {
            LayoutView layoutView = LayoutView.init();
            layoutView.ativos.Add(4);
            return View();
        }
        
    }
}