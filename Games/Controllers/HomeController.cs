﻿using Games.Models.Repository;
using Games.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Games.Controllers {
    public class HomeController : BaseController {

        public ActionResult Index() {
            GameListView view = new GameListView();
            GameRepository gameRepository = new GameRepository();
            view.ListaJogos = gameRepository.ListarJogos();

            return View(view);
        }

    }
}