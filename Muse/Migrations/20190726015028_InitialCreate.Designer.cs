﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Muse.Models;

namespace Muse.Migrations
{
    [DbContext(typeof(MuseContext))]
    [Migration("20190726015028_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Muse.Models.Musing", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Aspirations");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Entry")
                        .IsRequired();

                    b.Property<int>("Id");

                    b.Property<int>("SUDS");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int?>("UserId");

                    b.HasKey("UserID");

                    b.HasIndex("UserId");

                    b.ToTable("Musing");
                });

            modelBuilder.Entity("Muse.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Muse.Models.Musing", b =>
                {
                    b.HasOne("Muse.Models.User", "User")
                        .WithMany("Musings")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
