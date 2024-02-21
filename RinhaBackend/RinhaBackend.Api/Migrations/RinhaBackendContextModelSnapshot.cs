﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RinhaBackend.Api.Database.Context;

#nullable disable

namespace RinhaBackend.Api.Migrations
{
    [DbContext(typeof(RinhaBackendContext))]
    partial class RinhaBackendContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RinhaBackend.Api.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Limite")
                        .HasColumnType("double precision");

                    b.Property<double>("SaldoInicial")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Clientes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Limite = 100000.0,
                            SaldoInicial = 0.0
                        },
                        new
                        {
                            Id = 2,
                            Limite = 80000.0,
                            SaldoInicial = 0.0
                        },
                        new
                        {
                            Id = 3,
                            Limite = 1000000.0,
                            SaldoInicial = 0.0
                        },
                        new
                        {
                            Id = 4,
                            Limite = 10000000.0,
                            SaldoInicial = 0.0
                        },
                        new
                        {
                            Id = 5,
                            Limite = 500000.0,
                            SaldoInicial = 0.0
                        });
                });

            modelBuilder.Entity("RinhaBackend.Api.Models.Transacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<char>("Tipo")
                        .HasMaxLength(1)
                        .HasColumnType("character(1)")
                        .IsFixedLength();

                    b.Property<double>("Valor")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Transacoes");
                });

            modelBuilder.Entity("RinhaBackend.Api.Models.Transacao", b =>
                {
                    b.HasOne("RinhaBackend.Api.Models.Cliente", "Cliente")
                        .WithMany("Transacoes")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("RinhaBackend.Api.Models.Cliente", b =>
                {
                    b.Navigation("Transacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
