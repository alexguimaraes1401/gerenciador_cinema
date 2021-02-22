using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCinema.Models
{
    public class Result
    {
        public bool OK { get; set; }
        public bool Fail { get; set; }
        public string Msg { get; set;}
        public string Erro { get; set; }
        public object Data { get; set; }
        public object Validation { get; set; }
    }
}
