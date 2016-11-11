using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Igdb.Services;
using Igdb.ResponseModels;
using Igdb.RequestModels;
using Games.Models.Entity;
using Newtonsoft.Json;
using Games.Models.Repository;

namespace Igdb.Test {
    [TestClass]
    public class Test {
        [TestMethod]
        public void TesteSalvarJogo() {
            IgdbService igdb = new IgdbService();
            DadosGameResponse response = igdb.DadosJogo(428).FirstOrDefault();

            GameRepository gameRepository = new GameRepository();
            gameRepository.Adicionar(response);
        }

        [TestMethod]
        public void TesteCarregarSalvarJogo() {
            GameRepository gameRepository = new GameRepository();
            GameEntity game = gameRepository.BuscarDados(2);
            game.completo = 1;
            gameRepository.Salvar(game);

            game = gameRepository.BuscarDados(2);

            Assert.IsNotNull(game);
            Assert.AreEqual("Final Fantasy Tactics", game.name);
            Assert.AreEqual(1, game.completo);
        }

    }
}
