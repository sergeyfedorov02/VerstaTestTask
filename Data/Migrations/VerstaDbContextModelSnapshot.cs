﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VerstaTestTask.Data;

#nullable disable

namespace VerstaTestTask.Data.Migrations
{
    [DbContext(typeof(VerstaDbContext))]
    partial class VerstaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.7");

            modelBuilder.Entity("VerstaTestTask.Models.City", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("CityRegion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities", t =>
                        {
                            t.HasTrigger("Cities_Trigger");
                        });
                });

            modelBuilder.Entity("VerstaTestTask.Models.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CargoWeight")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("RecipientAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<long>("RecipientCityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SenderAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<long>("SenderCityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RecipientCityId");

                    b.HasIndex("SenderCityId");

                    b.ToTable("Orders", t =>
                        {
                            t.HasTrigger("Orders_Trigger");
                        });
                });

            modelBuilder.Entity("VerstaTestTask.Models.Order", b =>
                {
                    b.HasOne("VerstaTestTask.Models.City", "RecipientCity")
                        .WithMany()
                        .HasForeignKey("RecipientCityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VerstaTestTask.Models.City", "SenderCity")
                        .WithMany()
                        .HasForeignKey("SenderCityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecipientCity");

                    b.Navigation("SenderCity");
                });
#pragma warning restore 612, 618
        }
    }
}
