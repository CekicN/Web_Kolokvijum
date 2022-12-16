﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace Novafascikla4.Migrations
{
    [DbContext(typeof(AerodromContext))]
    partial class AerodromContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Aerodrom", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KapacitetLetelica")
                        .HasColumnType("int");

                    b.Property<int>("KapacitetPutnika")
                        .HasColumnType("int");

                    b.Property<string>("Lokacija")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Aerodromi");
                });

            modelBuilder.Entity("Models.Letelica", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BrMotora")
                        .HasColumnType("int");

                    b.Property<int>("KapacitetPutnika")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Posada")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Letelice");
                });

            modelBuilder.Entity("Models.Letovi", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BrojPutnika")
                        .HasColumnType("int");

                    b.Property<int?>("LetelicaID")
                        .HasColumnType("int");

                    b.Property<int?>("TackaAID")
                        .HasColumnType("int");

                    b.Property<int?>("TackaBID")
                        .HasColumnType("int");

                    b.Property<DateTime>("VremePoletanja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VremeSletanja")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("LetelicaID");

                    b.HasIndex("TackaAID");

                    b.HasIndex("TackaBID");

                    b.ToTable("Letovi");
                });

            modelBuilder.Entity("Models.Letovi", b =>
                {
                    b.HasOne("Models.Letelica", "Letelica")
                        .WithMany("Let")
                        .HasForeignKey("LetelicaID");

                    b.HasOne("Models.Aerodrom", "TackaA")
                        .WithMany("LetoviA")
                        .HasForeignKey("TackaAID");

                    b.HasOne("Models.Aerodrom", "TackaB")
                        .WithMany("LetoviB")
                        .HasForeignKey("TackaBID");

                    b.Navigation("Letelica");

                    b.Navigation("TackaA");

                    b.Navigation("TackaB");
                });

            modelBuilder.Entity("Models.Aerodrom", b =>
                {
                    b.Navigation("LetoviA");

                    b.Navigation("LetoviB");
                });

            modelBuilder.Entity("Models.Letelica", b =>
                {
                    b.Navigation("Let");
                });
#pragma warning restore 612, 618
        }
    }
}
