﻿using Games.Models.Entity;
using Igdb.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models.Repository {
    public class PlatformRepository : BaseRepository {

        public List<platform> Listar() {
            List<platform> listaPlatform = db.platform.OrderBy(platform => platform.ordem).ToList();
            return listaPlatform;
        }
        
        public platform GetPlatformByIgdb(int id_igdb) {
            platform plataforma = db.platform.Where(p => p.id_igdb == id_igdb).FirstOrDefault();
            if (plataforma != null) {
                return plataforma;
            }
            return null;
        }

        public int? GetIdBySigla(string sigla) {
            platform plataforma = db.platform.Where(p => p.sigla == sigla).FirstOrDefault();
            if (plataforma == null) {
                return null;
            }
            return plataforma.id;
        }

    }
}