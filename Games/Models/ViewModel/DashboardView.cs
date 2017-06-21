using Games.Models.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Games.Models.ViewModel {

    public class DashboardReportView {
        public int totalJogos;
        public string titulo;
        public Dictionary<string, int> dados;

        public DashboardReportView(string titulo, Dictionary<string, int> dados, int totalJogos) {
            this.titulo = titulo;
            this.dados = dados;
            this.totalJogos = totalJogos;
        }

        public string porcentagem(int total, int valor) {
            return (((Double)valor / (Double)total)).ToString("0.00%", CultureInfo.InvariantCulture);
        }

        public string texto(string texto) {
            int tamanho = 100;
            if (texto.Length > tamanho) {
                texto = texto.Substring(0, tamanho);
            }
            return texto;
        }
    }

    public class DashboardSummaryView {
        public string valor;
        public string descricao;
        public string icone;

        public DashboardSummaryView(string valor, string descricao, string icone) {
            this.valor = valor;
            this.descricao = descricao;

            switch (icone) {
                case "jogos":
                    this.icone = "<i class=\"fa fa-gamepad fa-inverse fa-2x\"></i>";
                break;
                case "completos":
                    this.icone = "<i class=\"fa fa-check fa-inverse fa-2x\"></i>"; ;
                break;
                case "preco":
                    this.icone = "<i class=\"fa fa-money fa-2x\"></i>"; ;
                break;
                case "fisicos":
                    this.icone = "<span class=\"glyphicon glyphicon-cd\" aria-hidden=\"true\"></span>";
                break;
                case "digitais":
                    this.icone = "<i class=\"fa fa-cloud fa-inverse fa-2x\"></i>";
                break;
            }
        }
    }

    public class DashboardView {
        private GameRepository gameRepository;
        private int totalJogos;
        private int totalJogosCompletos;
        private int totalJogosFisicos;
        private int totalJogosDigitais;
        private decimal? totalPreco;
        private Dictionary<string, int> totalJogosStatus;
        private Dictionary<string, int> totalJogosPlataforma;
        private Dictionary<string, int> totalJogosGenero;
        private Dictionary<string, int> totalJogosDeveloper;
        private Dictionary<string, int> totalJogosPublisher;
        private Dictionary<string, int> totalJogosAno;
        private Dictionary<string, int> totalJogosFaixaPreco;
        private Dictionary<string, int> totalJogosFaixaMetacritic;
        private Dictionary<string, int> totalJogosLoja;

        public DashboardView() {
            gameRepository = new GameRepository();
            totalJogos = gameRepository.GetTotalJogos();
            totalPreco = gameRepository.GetTotalPreco();
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
            totalJogosFaixaMetacritic = gameRepository.GetTotalJogosFaixaMetacritic();
            totalJogosLoja = gameRepository.GetTotalJogosLoja();
        }

        public DashboardReportView GetDashboardReportView(string titulo, Dictionary<string, int> dados, int total) {
            return new DashboardReportView(titulo, dados, total);
        }

        public DashboardSummaryView GetDashboardSummaryView(string valor, string descricao,string icone) {
            return new DashboardSummaryView(valor, descricao, icone);
        }

        public int TotalJogos {
            get {
                return totalJogos;
            }
        }

        public decimal? TotalPreco {
            get {
                return totalPreco;
            }
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

        public Dictionary<string, int> TotalJogosFaixaPreco {
            get {
                return totalJogosFaixaPreco;
            }
        }

        public Dictionary<string, int> TotalJogosFaixaMetacritic {
            get {
                return totalJogosFaixaMetacritic;
            }
        }

        public Dictionary<string, int> TotalJogosLoja {
            get {
                return totalJogosLoja;
            }
        }

    }

}