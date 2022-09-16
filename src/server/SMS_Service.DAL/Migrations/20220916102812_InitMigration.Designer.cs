﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SMS_Service.DAL;

#nullable disable

namespace SMS_Service.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220916102812_InitMigration")]
    partial class InitMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SMS_Service.DAL.Entities.Receiver", b =>
                {
                    b.Property<Guid>("SmsId")
                        .HasColumnType("uuid");

                    b.Property<string>("ReceiverNumber")
                        .HasColumnType("text");

                    b.HasKey("SmsId", "ReceiverNumber");

                    b.ToTable("SmsToReceivers");
                });

            modelBuilder.Entity("SMS_Service.DAL.Entities.SMS", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SMS");
                });

            modelBuilder.Entity("SMS_Service.DAL.Entities.Receiver", b =>
                {
                    b.HasOne("SMS_Service.DAL.Entities.SMS", "SMS")
                        .WithMany("Receivers")
                        .HasForeignKey("SmsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SMS");
                });

            modelBuilder.Entity("SMS_Service.DAL.Entities.SMS", b =>
                {
                    b.Navigation("Receivers");
                });
#pragma warning restore 612, 618
        }
    }
}
