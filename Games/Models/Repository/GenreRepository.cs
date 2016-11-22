using Games.Models.Entity;
using System.Linq;

namespace Games.Models.Repository {
    public class GenreRepository : BaseRepository {

        public int GetIdByIgdb(int id_igdb, string name) {
            genre genre = db.genre.Where(l => l.id_igdb == id_igdb).FirstOrDefault();
            if (genre == null) {
                genre = Adicionar(id_igdb, name);
            }

            return genre.id;
        }

        private genre Adicionar(int id_igdb, string nome) {
            genre genre = new genre {
                name = nome,
                id_igdb = id_igdb
            };
            db.genre.Add(genre);
            db.SaveChanges();

            return genre;
        }

    }

}