using Games.Models.Repository;
using Games.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Games.Controllers {
    public class HomeController : BaseController {
        // GET: Home
        public ActionResult Index() {
            GameListView view = new GameListView();
            view.ListaPlatform = ListaPlatform;
            return View(view);
        }
    }
}