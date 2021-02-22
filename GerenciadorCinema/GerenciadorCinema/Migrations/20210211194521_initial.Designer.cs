﻿// <auto-generated />
using System;
using GerenciadorCinema.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GerenciadorCinema.Migrations
{
    [DbContext(typeof(GerenciadorCinemaContext))]
    [Migration("20210211194521_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GerenciadorCinema.Models.Filme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Duracao")
                        .HasColumnType("decimal(65,30)");

                    b.Property<byte>("Imagem")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("filme");
                });

            modelBuilder.Entity("GerenciadorCinema.Models.Sala", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("QuantidadeAssentos")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sala");
                });

            modelBuilder.Entity("GerenciadorCinema.Models.Sessao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("FilmeId")
                        .HasColumnType("int");

                    b.Property<string>("Horario")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("SalaId")
                        .HasColumnType("int");

                    b.Property<int>("TipoAnimacao")
                        .HasColumnType("int");

                    b.Property<int>("TipoAudio")
                        .HasColumnType("int");

                    b.Property<float>("ValorIngresso")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FilmeId");

                    b.HasIndex("SalaId");

                    b.ToTable("Sessao");
                });

            modelBuilder.Entity("GerenciadorCinema.Models.Sessao", b =>
                {
                    b.HasOne("GerenciadorCinema.Models.Filme", "Filme")
                        .WithMany()
                        .HasForeignKey("FilmeId");

                    b.HasOne("GerenciadorCinema.Models.Sala", "Sala")
                        .WithMany()
                        .HasForeignKey("SalaId");
                });
#pragma warning restore 612, 618
        }
    }
}
