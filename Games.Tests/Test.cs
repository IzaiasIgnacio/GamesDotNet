﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Igdb.Services;
using Igdb.ResponseModels;
using System.Net;

namespace Igdb.Test {
    [TestClass]
    public class Test {
        [TestMethod]
        public void TesteBuscarJogo() {
            IgdbService igdb = new IgdbService();
            List<BuscaGameResponse> response = igdb.BuscarJogo("infamous");

            Assert.IsNotNull(response[0].Id);
            Assert.IsNotNull(response[0].Cover.CloudinaryId);
            Assert.IsNotNull(response[0].Name);
            Assert.IsNotNull(response[0].ReleaseDates[0].Platform);
        }

        [TestMethod]
        public void TesteSalvarImagem() {
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://res.cloudinary.com/igdb/image/upload/t_cover_small_2x/tdmbpbzh0gdsp6rwtnjp", "a.jpg");
        }

        [TestMethod]
        public void TesteBuscarDadosJogo() {
            IgdbService igdb = new IgdbService();
            DadosGameResponse response = igdb.DadosJogo(533).FirstOrDefault();
            
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
        public void TesteBuscarDadosJogosSerie() {
            IgdbService igdb = new IgdbService();
            DadosGameResponse response = igdb.DadosJogo(20044).FirstOrDefault();
            List<DadosGameResponse> listaGames = new List<DadosGameResponse>();

            if (response.Cover == null) {    
                DadosGameSeriesResponse resposta = igdb.BuscaGameSeries(response.Collection).FirstOrDefault();
                foreach (int game in resposta.Games) {
                    DadosGameResponse dados = igdb.DadosJogo(game).FirstOrDefault();
                    if (igdb.ValidarGamePlataforma(dados)) {
                        listaGames.Add(igdb.DadosJogo(game).FirstOrDefault());
                    }
                }
            }
            Assert.IsNotNull(listaGames);
            Assert.IsNotNull(response.Collection);
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
            int[] plats = { 7, 6, 9, 48, 38, 8, 9, 46 };
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
