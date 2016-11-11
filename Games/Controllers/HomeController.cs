using Games.Models.Repository;
using Games.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Games.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {
            GameListView view = new GameListView();
            PlatformRepository platformRepository = new PlatformRepository();
            view.ListaPlatform = platformRepository.Listar();
            return View(view);
        }
    }
}