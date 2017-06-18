using Games.Models.Entity;
using Games.Models.Repository;
using System.Collections.Generic;

namespace Games.Models.ViewModel {

    public class LayoutView {
        public List<platform> listaPlatform;
        public List<int> ativos;
        public int total_colecao;
        public int total_wishlist;
        public int total_watchlist;
        public int total_plus;
        public List<Totais> totais;
        private static LayoutView view;

        private LayoutView() { }

        public static LayoutView GetLayoutView() {
            if (view == null) {
                init();
            }
            return view;
        }

        public class Totais {
            public string plataforma;
            public string status;
            public int total;
        }

        public static LayoutView init() {
            GameRepository gr = new GameRepository();
            PlatformRepository platformRepository = new PlatformRepository();

            view = new LayoutView();
            view.ativos = new List<int>();
            view.totais = new List<Totais>();

            Dictionary<string, int> total = gr.GetTotalJogosStatus();
            List<game_platform> jogos = gr.GetListaGeral();
            view.listaPlatform = platformRepository.Listar();
            
            view.total_colecao = total["Coleção"];
            view.total_wishlist = total["Wishlist"];
            view.total_watchlist = total["Watchlist"];
            view.total_plus = total["Plus"];
            
            foreach (platform plat in view.listaPlatform) {
                view.totais.Add(new Totais {
                        plataforma = plat.sigla,
                        status = "colecao",
                        total = jogos.FindAll(j => (j.id_status == 1) && (j.platform.sigla == plat.sigla)).Count
                    });
                view.totais.Add(new Totais {
                    plataforma = plat.sigla,
                    status = "wishlist",
                    total = jogos.FindAll(j => (j.id_status == 2) && (j.platform.sigla == plat.sigla)).Count
                });
                view.totais.Add(new Totais {
                    plataforma = plat.sigla,
                    status = "watchlist",
                    total = jogos.FindAll(j => (j.id_status == 3) && (j.platform.sigla == plat.sigla)).Count
                });
                view.totais.Add(new Totais {
                    plataforma = plat.sigla,
                    status = "plus",
                    total = jogos.FindAll(j => (j.id_status == 4) && (j.platform.sigla == plat.sigla)).Count
                });
            }

            return view;
        }
    }

}