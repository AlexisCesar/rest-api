﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restful_API.Data;

#nullable disable

namespace Restful_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220623163112_InitialCreation")]
    partial class InitialCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("Entidades.Models.Funcionario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<double>("Salario")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Funcionario");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Funcionario");
                });

            modelBuilder.Entity("Entidades.Models.FuncionarioCLT", b =>
                {
                    b.HasBaseType("Entidades.Models.Funcionario");

                    b.HasDiscriminator().HasValue("FuncionarioCLT");
                });

            modelBuilder.Entity("Entidades.Models.FuncionarioPJ", b =>
                {
                    b.HasBaseType("Entidades.Models.Funcionario");

                    b.HasDiscriminator().HasValue("FuncionarioPJ");
                });
#pragma warning restore 612, 618
        }
    }
}