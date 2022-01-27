﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniProj_HongSun.Models;

namespace MiniProj_HongSun.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220127023017_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MiniProj_HongSun.Models.Laptop", b =>
                {
                    b.Property<int>("LaptopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 2)
                        .HasAnnotation("SqlServer:IdentitySeed", 2)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Brand")
                        .HasColumnType("int");

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ScreenSize")
                        .HasColumnType("int");

                    b.HasKey("LaptopId");

                    b.ToTable("Laptops");
                });

            modelBuilder.Entity("MiniProj_HongSun.Models.Mobilephone", b =>
                {
                    b.Property<int>("MobilePhoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 2)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("PhoneBrand")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MobilePhoneId");

                    b.ToTable("Mobilephones");
                });

            modelBuilder.Entity("MiniProj_HongSun.Models.Rate", b =>
                {
                    b.Property<int>("RateLocation")
                        .HasColumnType("int");

                    b.Property<double>("RateValue")
                        .HasColumnType("float");

                    b.HasKey("RateLocation");

                    b.ToTable("Rates");
                });
#pragma warning restore 612, 618
        }
    }
}
