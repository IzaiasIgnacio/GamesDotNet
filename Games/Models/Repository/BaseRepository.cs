﻿using Games.Models.Entity;
using Igdb.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models.Repository {
    public class BaseRepository {
        protected GamesEntities db;

        public BaseRepository() {
            db = new GamesEntities();
        }

    }
}