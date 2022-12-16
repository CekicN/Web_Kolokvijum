﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace Jun22022.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Kompanija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BrojDanaZaIsporuku")
                        .HasColumnType("int");

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProsecnaZarada")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Kompanija");
                });

            modelBuilder.Entity("Models.Roba", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("CenaDo")
                        .HasColumnType("int");

                    b.Property<int>("CenaOd")
                        .HasColumnType("int");

                    b.Property<int?>("KompanijaID")
                        .HasColumnType("int");

                    b.Property<int>("Tezina")
                        .HasColumnType("int");

                    b.Property<int>("Zapremina")
                        .HasColumnType("int");

                    b.Property<DateTime>("datumIsporuke")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("datumPrijema")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("KompanijaID");

                    b.ToTable("Roba");
                });

            modelBuilder.Entity("Models.Vozilo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.Property<int?>("KompanijaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("KompanijaID");

                    b.ToTable("Vozilo");
                });

            modelBuilder.Entity("Models.Roba", b =>
                {
                    b.HasOne("Models.Kompanija", "Kompanija")
                        .WithMany("Robe")
                        .HasForeignKey("KompanijaID");

                    b.Navigation("Kompanija");
                });

            modelBuilder.Entity("Models.Vozilo", b =>
                {
                    b.HasOne("Models.Kompanija", "Kompanija")
                        .WithMany("Vozila")
                        .HasForeignKey("KompanijaID");

                    b.Navigation("Kompanija");
                });

            modelBuilder.Entity("Models.Kompanija", b =>
                {
                    b.Navigation("Robe");

                    b.Navigation("Vozila");
                });
#pragma warning restore 612, 618
        }
    }
}