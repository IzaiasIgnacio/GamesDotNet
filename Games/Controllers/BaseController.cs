using Games.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Games.Controllers {
    public class BaseController : Controller {
        protected LayoutView layoutView;
        public BaseController() {
            layoutView = LayoutView.GetLayoutView();
        }
    }
}