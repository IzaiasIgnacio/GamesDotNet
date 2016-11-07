using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Igdb.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Igdb.Test {
    public class Test {
        [TestMethod]
        public void TesteBuscarJogo() {
            IgdbService igdb = new IgdbService();
            igdb.BuscarJogo("dishonored");
        }
        
    }
}
