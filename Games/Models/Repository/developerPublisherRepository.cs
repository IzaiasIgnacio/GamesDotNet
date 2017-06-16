using Games.Models.Entity;
using System.Linq;

namespace Games.Models.Repository {
    public class DeveloperPublisherRepository : BaseRepository {

        public int GetIgdbById(int id) {
            developerPublisher developerPublisher = db.developerPublisher.Where(l => l.id == id).FirstOrDefault();

            return developerPublisher.id_igdb;
        }

        public int GetIdByIgdb(int id_igdb, string name) {
            developerPublisher developerPublisher = db.developerPublisher.Where(l => l.id_igdb == id_igdb).FirstOrDefault();
            if (developerPublisher == null) {
                developerPublisher = Adicionar(id_igdb, name);
            }

            return developerPublisher.id;
        }

        private developerPublisher Adicionar(int id_igdb, string nome) {
            developerPublisher developerPublisher = new developerPublisher {
                name = nome,
                id_igdb = id_igdb
            };
            db.developerPublisher.Add(developerPublisher);
            db.SaveChanges();

            return developerPublisher;
        }
        
    }

}