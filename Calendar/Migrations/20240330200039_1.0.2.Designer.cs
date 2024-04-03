﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Calendar.Migrations
{
    [DbContext(typeof(CalendarDb))]
    [Migration("20240330200039_1.0.2")]
    partial class _102
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("DayWeek", b =>
                {
                    b.Property<int>("DayWeekId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("date")
                        .HasColumnType("TEXT");

                    b.Property<int>("dayWeek")
                        .HasColumnType("INTEGER");

                    b.HasKey("DayWeekId");

                    b.ToTable("DayOfWeeks");
                });

            modelBuilder.Entity("Interval", b =>
                {
                    b.Property<int>("interval_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("date1")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("date2")
                        .HasColumnType("TEXT");

                    b.Property<int>("interval")
                        .HasColumnType("INTEGER");

                    b.HasKey("interval_id");

                    b.ToTable("Intervals");
                });

            modelBuilder.Entity("Leapnes", b =>
                {
                    b.Property<int>("Leapnes_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("leap")
                        .HasColumnType("INTEGER");

                    b.Property<int>("year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Leapnes_id");

                    b.ToTable("Leapness");
                });
#pragma warning restore 612, 618
        }
    }
}
