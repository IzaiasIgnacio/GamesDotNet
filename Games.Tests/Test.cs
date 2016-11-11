using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Igdb.Services;
using Igdb.ResponseModels;
using Igdb.RequestModels;
using Newtonsoft.Json;
using Games.Models.Repository;

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
            int[] devs = { 250, 400 };
            List<DadosDeveloperPublisherResponse> response = igdb.DadosDeveloperPublisher(devs);

            Assert.IsNotNull(response[0].Id);
            Assert.IsNotNull(response[0].Name);
        }

        [TestMethod]
        public void TesteBuscarDadosPlatform() {
            IgdbService igdb = new IgdbService();
            int[] plats = { 52, 4 };
            List<DadosPlatformResponse> response = igdb.DadosPlatform(plats);

            Assert.IsNotNull(response[0].Id);
            Assert.IsNotNull(response[0].Name);
        }

        [TestMethod]
        public void TesteBuscarDadosGenre() {
            IgdbService igdb = new IgdbService();
            int[] genres = { 12, 15 };
            List<DadosGenreResponse> response = igdb.DadosGenre(genres);

            Assert.IsNotNull(response[0].Id);
            Assert.IsNotNull(response[0].Name);
        }
    }
}
