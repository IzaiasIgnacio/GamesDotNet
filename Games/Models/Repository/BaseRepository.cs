﻿using Games.Models.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Games.Models.Repository {
    public class BaseRepository {
        protected GamesEntities db;

        public BaseRepository() {
            db = new GamesEntities();
        }

        public List<T> Listar<T>() where T : class {
            return db.Set<T>().ToList();
        }

    }
}