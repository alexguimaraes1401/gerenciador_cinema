using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCinema.Models.View
{
    public class SessaoViewEntity
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public string Horario { get; set; }
        public string ValorIngresso { get; set; }
        public string TipoAnimacao { get; set; }
        public string TipoAudio { get; set; }
        public string Filme {get;set;}
        public string Sala { get;set; }
    }
}
