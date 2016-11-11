using Games.Models.Entity;
using Games.Models.Repository;
using Games.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Games.Controllers {
    public class BaseController : Controller {

        public List<platform> ListaPlatform;

        public ActionResult ExibirMenuPlatforms() {
            PlatformRepository platformRepository = new PlatformRepository();
            LayoutView view = new LayoutView();
            view.ListaPlatform = platformRepository.Listar();
            return PartialView("PlatformMenuView", view);
        }
    }

}