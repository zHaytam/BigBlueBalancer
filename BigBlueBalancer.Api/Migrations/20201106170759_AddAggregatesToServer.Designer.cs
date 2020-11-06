﻿// <auto-generated />
using System;
using BigBlueBalancer.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BigBlueBalancer.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201106170759_AddAggregatesToServer")]
    partial class AddAggregatesToServer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("BigBlueBalancer.Api.Entities.Server", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<double>("Load")
                        .HasColumnType("float");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Up")
                        .HasColumnType("bit");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("BigBlueBalancer.Api.Entities.ServerStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("ListenerCount")
                        .HasColumnType("int");

                    b.Property<int>("MeetingsCount")
                        .HasColumnType("int");

                    b.Property<int>("ParticipantCount")
                        .HasColumnType("int");

                    b.Property<short>("ServerId")
                        .HasColumnType("smallint");

                    b.Property<bool>("Up")
                        .HasColumnType("bit");

                    b.Property<int>("VideoCount")
                        .HasColumnType("int");

                    b.Property<int>("VoiceParticipantCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("ServerStats");
                });

            modelBuilder.Entity("BigBlueBalancer.Api.Entities.ServerStats", b =>
                {
                    b.HasOne("BigBlueBalancer.Api.Entities.Server", "Server")
                        .WithMany("Stats")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");
                });

            modelBuilder.Entity("BigBlueBalancer.Api.Entities.Server", b =>
                {
                    b.Navigation("Stats");
                });
#pragma warning restore 612, 618
        }
    }
}
