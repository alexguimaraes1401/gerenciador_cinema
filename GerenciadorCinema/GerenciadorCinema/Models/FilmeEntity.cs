﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCinema.Models
{
    public class FilmeEntity
    {
        public int? Id { get; set; }
        public string Titulo { get; set;}
        public string Descricao { get; set; }
        public string Duracao { get; set; }
        public string Imagem { get; set; }

    }
}
