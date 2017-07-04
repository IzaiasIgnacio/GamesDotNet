using Games.Models.Entity;
using GamesApi.ResponseModels;
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

        public List<T> Listar<T>() where T : class {
            return db.Set<T>().ToList();
        }

    }
}