using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models.Excecao {
    public class ValidacaoGameException : Exception {
        private string message;

        public ValidacaoGameException(string message) : base(message) {
            this.message = message;
        }

        public override string Message {
            get {
                return message;
            }

        }
    }
}