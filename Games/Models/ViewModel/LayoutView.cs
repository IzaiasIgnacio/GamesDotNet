using Games.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Games.Models.ViewModel {

    public class LayoutView {
        public List<platform> listaPlatform;
        public List<int> ativos;
        private static LayoutView view;

        private LayoutView() { }

        public static LayoutView GetLayoutView() {
            if (view == null) {
                view = new LayoutView();
                view.ativos = new List<int>();
                view.listaPlatform = new List<platform>();
            }
            return view;
        }

        public static LayoutView init() {
            view = new LayoutView();
            view.ativos = new List<int>();
            view.listaPlatform = new List<platform>();

            return view;
        }
    }

}