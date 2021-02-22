using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCinema.Models
{
    public class SessaoEntity
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Horario { get; set; }
        public decimal ValorIngresso { get; set; }
        public int TipoAnimacao { get; set; }
        public int TipoAudio { get; set; }
        public FilmeEntity Filme { get; set; }
        public SalaEntity Sala { get; set; }
    }
}
