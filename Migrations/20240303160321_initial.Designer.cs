﻿// <auto-generated />
using System;
using Contatos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace projeto.Migrations
{
    [DbContext(typeof(ClimaContexto))]
    [Migration("20240303160321_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("projeto.Models.Clima", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("TEXT");

                    b.Property<float>("Temperatura")
                        .HasColumnType("REAL");

                    b.Property<float>("Umidade")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Clima");
                });
#pragma warning restore 612, 618
        }
    }
}
