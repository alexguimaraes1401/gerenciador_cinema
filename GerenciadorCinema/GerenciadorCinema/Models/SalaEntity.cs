using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCinema.Models
{
    public class SalaEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeAssentos { get; set; }
    }
}
