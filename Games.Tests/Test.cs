using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Igdb.Services;
using Igdb.ResponseModels;
using Igdb.RequestModels;
using Newtonsoft.Json;

namespace Igdb.Test {
    [TestClass]
    public class Test {
        [TestMethod]
        public void TesteBuscarJogo() {
            IgdbService igdb = new IgdbService();
            List<BuscaGameResponse> response = igdb.BuscarJogo("dishonored");

            Assert.IsNotNull(response[0].Id);
            Assert.IsNotNull(response[0].Cover.CloudinaryId);
            Assert.IsNotNull(response[0].Name);
            Assert.IsNotNull(response[0].ReleaseDates[0].Platform);
        }

        [TestMethod]
        public void TesteBuscarDadosJogo() {
            IgdbService igdb = new IgdbService();
            List<DadosGameResponse> response = igdb.DadosJogo(428);

            Assert.IsNotNull(response[0].Id);
            Assert.IsNotNull(response[0].Name);
            Assert.IsNotNull(response[0].Developers);
            Assert.IsNotNull(response[0].Publishers);
            Assert.IsNotNull(response[0].Genres);
            Assert.IsNotNull(response[0].ReleaseDates);
            Assert.IsNotNull(response[0].Videos);
            Assert.IsNotNull(response[0].Cover.CloudinaryId);
        }
    }
}
