using GerenciadorCinema.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace GerenciadorCinema.Data
{
    public class GerenciadorCinemaContext : DbContext 
    {

        public DbSet<SalaEntity> Sala { get; set; }
        public DbSet<FilmeEntity> Filme { get; set; }
        public DbSet<SessaoEntity> Sessao { get; set; }

        public GerenciadorCinemaContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {

        }
    }
}
