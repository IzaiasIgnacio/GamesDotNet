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
            DadosGameResponse response = igdb.DadosJogo(428).FirstOrDefault();

            Assert.IsNotNull(response.Id);
            Assert.IsNotNull(response.Name);
            Assert.IsNotNull(response.Developers);
            Assert.IsNotNull(response.Publishers);
            Assert.IsNotNull(response.Genres);
            Assert.IsNotNull(response.ReleaseDates);
            Assert.IsNotNull(response.Videos);
            Assert.IsNotNull(response.Cover.CloudinaryId);
        }

        [TestMethod]
        public void TesteBuscarDadosDeveloperPublisher() {
            IgdbService igdb = new IgdbService();
            DadosDeveloperPublisherResponse response = igdb.DadosDeveloperPublisher(250).FirstOrDefault();

            Assert.IsNotNull(response.Id);
            Assert.IsNotNull(response.Name);
        }
    }
}
