using Games.Models.Entity;
using Igdb.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models.Repository {
    public class PlatformRepository : BaseRepository {

        public List<platform> Listar() {
            List<platform> listaPlatform = db.platform.ToList();
            return listaPlatform;
        }

    }
}