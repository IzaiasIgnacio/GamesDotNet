using Games.Models.Entity;
using Games.Models.Repository;
using Igdb.ResponseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Games.Models.ViewModel {

    public class DashboardView {
        private GameRepository gameRepository;
        private int totalJogos;
        private int totalJogosCompletos;
        private int totalJogosFisicos;
        private int totalJogosDigitais;
        private Dictionary<string, int> totalJogosColecaoPlataforma;
        private Dictionary<string, int> totalJogosWishlistPlataforma;
        private Dictionary<string, int> totalJogosWatchlistPlataforma;
        private Dictionary<string, int> totalJogosStatus;
        private Dictionary<string, int> totalJogosPlataforma;
        private Dictionary<string, int> totalJogosGenero;
        private Dictionary<string, int> totalJogosDeveloper;
        private Dictionary<string, int> totalJogosPublisher;
        private Dictionary<string, int> totalJogosAno;
        private Dictionary<string, int> totalJogosFaixaPreco;
        private Dictionary<string, int> totalJogosLoja;

        public DashboardView() {
            gameRepository = new GameRepository();
            totalJogos = gameRepository.GetTotalJogos();
            totalJogosCompletos = gameRepository.GetTotalJogosCompletos();
            totalJogosFisicos = gameRepository.GetTotalJogosFormato(1);
            totalJogosDigitais = gameRepository.GetTotalJogosFormato(2);
            totalJogosStatus = gameRepository.GetTotalJogosStatus();
            totalJogosPlataforma = gameRepository.GetTotalJogosPlataforma();
            totalJogosGenero = gameRepository.GetTotalJogosGenero();
            totalJogosDeveloper = gameRepository.GetTotalJogosDeveloper();
            totalJogosPublisher = gameRepository.GetTotalJogosPublisher();
            totalJogosAno = gameRepository.GetTotalJogosAno();
            totalJogosFaixaPreco = gameRepository.GetTotalJogosFaixaPreco();
            totalJogosLoja = gameRepository.GetTotalJogosLoja();
        }
        
        public int TotalJogos {
            get {
                return totalJogos;
            }
        }

        public string porcentagem(int total, int valor) {
            return (((Double)valor / (Double)total)).ToString("0.00%", CultureInfo.InvariantCulture);
        }

        public Dictionary<string, int> TotalJogosStatus {
            get {
                return totalJogosStatus;
            }
        }

        public Dictionary<string, int> TotalJogosPlataforma {
            get {
                return totalJogosPlataforma;
            }
        }

        public Dictionary<string, int> TotalJogosGenero {
            get {
                return totalJogosGenero;
            }
        }

        public Dictionary<string, int> TotalJogosDeveloper {
            get {
                return totalJogosDeveloper;
            }
        }

        public Dictionary<string, int> TotalJogosPublisher {
            get {
                return totalJogosPublisher;
            }

        }

        public Dictionary<string, int> TotalJogosAno {
            get {
                return totalJogosAno;
            }
        }

        public int TotalJogosCompletos {
            get {
                return totalJogosCompletos;
            }
        }

        public int TotalJogosFisicos {
            get {
                return totalJogosFisicos;
            }
        }

        public int TotalJogosDigitais {
            get {
                return totalJogosDigitais;
            }
        }

        public Dictionary<string, int> TotalJogosFaixaPreco
        {
            get {
                return totalJogosFaixaPreco;
            }
        }

        public Dictionary<string, int> TotalJogosLoja {
            get {
                return totalJogosLoja;
            }
        }
        
    }

}