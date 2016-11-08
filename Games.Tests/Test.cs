using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Igdb.Services;
using Igdb.ResponseModels;

namespace Igdb.Test {
    [TestClass]
    public class Test {
        [TestMethod]
        public void TesteBuscarJogo() {
            IgdbService igdb = new IgdbService();
            igdb.BuscarJogo("dishonored");
        }
    }
}
