using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Games.Models.Validacao {
    [DataContract(Name = "ValidacaoGameMessage")]
    public class ValidacaoGameMessage {
        [DataMember(Name = "valido")]
        public bool Valido { get; set; }

        [DataMember(Name = "mensagem")]
        public List<string> Mensagem { get; set; }
    }
}