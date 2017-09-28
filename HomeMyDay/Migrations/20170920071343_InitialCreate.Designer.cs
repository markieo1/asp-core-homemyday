﻿// <auto-generated />
using HomeMyDay.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HomeMyDay.Migrations
{
    [DbContext(typeof(HomeMyDayDbContext))]
    [Migration("20170920071343_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomeMyDay.Models.Accommodation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Continent");

                    b.Property<string>("Country");

                    b.Property<int>("MaxPersons");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Accommodations");
                });

            modelBuilder.Entity("HomeMyDay.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccommodationId");

                    b.Property<DateTime>("DepartureDate");

                    b.Property<int>("NrPersons");

                    b.Property<DateTime>("ReturnDate");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("HomeMyDay.Models.Holiday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccommodationId");

                    b.Property<int?>("Bed");

                    b.Property<string>("Category");

                    b.Property<decimal>("Price");

                    b.Property<bool>("Recommended");

                    b.Property<int?>("Room");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("HomeMyDay.Models.Booking", b =>
                {
                    b.HasOne("HomeMyDay.Models.Accommodation", "Accommodation")
                        .WithMany()
                        .HasForeignKey("AccommodationId");
                });

            modelBuilder.Entity("HomeMyDay.Models.Holiday", b =>
                {
                    b.HasOne("HomeMyDay.Models.Accommodation", "Accommodation")
                        .WithMany()
                        .HasForeignKey("AccommodationId");
                });
#pragma warning restore 612, 618
        }
    }
}
