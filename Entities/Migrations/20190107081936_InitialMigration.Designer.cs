﻿// <auto-generated />
using System;
using Entities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entities.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190107081936_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.Flight", b =>
                {
                    b.Property<Guid>("FlightId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AircraftType")
                        .IsRequired();

                    b.Property<DateTime>("ArrivalTime");

                    b.Property<DateTime>("DepartureTime");

                    b.Property<string>("FromLocation")
                        .IsRequired();

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("ToLocation")
                        .IsRequired();

                    b.HasKey("FlightId");

                    b.ToTable("Flights");
                });
#pragma warning restore 612, 618
        }
    }
}
