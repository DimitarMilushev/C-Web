﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Panda.Data;

namespace Panda.Data.Migrations
{
    [DbContext(typeof(PandaDbContext))]
    [Migration("20181217145626_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Panda.Models.Package", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime?>("EstimatedDeliveryDate");

                    b.Property<string>("RecipientId");

                    b.Property<string>("ShippingAddress");

                    b.Property<int>("Status");

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("Panda.Models.Receipt", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Fee");

                    b.Property<DateTime>("IssuedOn");

                    b.Property<string>("PackageId");

                    b.Property<string>("RecipientId");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("RecipientId");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("Panda.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Panda.Models.Package", b =>
                {
                    b.HasOne("Panda.Models.User", "Recipient")
                        .WithMany("Packages")
                        .HasForeignKey("RecipientId");
                });

            modelBuilder.Entity("Panda.Models.Receipt", b =>
                {
                    b.HasOne("Panda.Models.Package", "Package")
                        .WithMany()
                        .HasForeignKey("PackageId");

                    b.HasOne("Panda.Models.User", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId");
                });
#pragma warning restore 612, 618
        }
    }
}