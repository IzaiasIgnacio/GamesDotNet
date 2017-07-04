using Games.Models.Entity;
using GamesApi.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models.Repository {
    public class StoreRepository : BaseRepository {
        
        public int GetIdByName(string name) {
            store loja = db.store.Where(l => l.name == name).FirstOrDefault();
            if (loja == null) {
                loja = Adicionar(name);
            }

            return loja.id;
        }

        private store Adicionar(string nome) {
            store loja = new store{
                name = nome
            };
            db.store.Add(loja);
            db.SaveChanges();

            return loja;
        }

    }
}