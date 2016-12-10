using Games.Models.Entity;
using Games.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models.Validacao {
    public static class ValidacaoGameService {
        public static ValidacaoGameMessage Validar(GameDataView game) {
            ValidacaoGameMessage resposta = new ValidacaoGameMessage { Valido = true };
            resposta.Mensagem = new List<string>();

            if (string.IsNullOrWhiteSpace(game.Titulo)) {
                resposta.Valido = false;
                resposta.Mensagem.Add("Título não informado");
            }

            if (game.Platforms.Count == 0) {
                resposta.Valido = false;
                resposta.Mensagem.Add("Nenhuma Plataforma informada");
            }
            else {
                foreach (game_platform gp in game.Platforms) {
                    if (gp.id_platform == 0) {
                        resposta.Valido = false;
                        resposta.Mensagem.Add("Plataforma não definida");
                    }
                    if (gp.id_status == 0) {
                        resposta.Valido = false;
                        resposta.Mensagem.Add("Status não definido");
                    }
                }
            }

            return resposta;
        }
    }
}