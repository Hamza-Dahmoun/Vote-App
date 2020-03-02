﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Models;

namespace WebApplication1.Migrations
{
    [DbContext(typeof(VoteDBContext))]
    [Migration("20200302133854_updateStructureLevel")]
    partial class updateStructureLevel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication1.Models.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("StructureId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StructureId");

                    b.ToTable("Candidate");
                });

            modelBuilder.Entity("WebApplication1.Models.Structure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.ToTable("Structure");
                });

            modelBuilder.Entity("WebApplication1.Models.StructureLevel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StructureLevel");
                });

            modelBuilder.Entity("WebApplication1.Models.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("VoterID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CandidateID");

                    b.HasIndex("VoterID")
                        .IsUnique();

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("WebApplication1.Models.Voter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("StructureId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StructureId");

                    b.ToTable("Voter");
                });

            modelBuilder.Entity("WebApplication1.Models.Candidate", b =>
                {
                    b.HasOne("WebApplication1.Models.Structure", "Structure")
                        .WithMany()
                        .HasForeignKey("StructureId");
                });

            modelBuilder.Entity("WebApplication1.Models.Structure", b =>
                {
                    b.HasOne("WebApplication1.Models.StructureLevel", "Level")
                        .WithMany("Structures")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.Models.Vote", b =>
                {
                    b.HasOne("WebApplication1.Models.Candidate", "Candidate")
                        .WithMany("Votes")
                        .HasForeignKey("CandidateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Voter", "Voter")
                        .WithOne("Vote")
                        .HasForeignKey("WebApplication1.Models.Vote", "VoterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.Models.Voter", b =>
                {
                    b.HasOne("WebApplication1.Models.Structure", "Structure")
                        .WithMany()
                        .HasForeignKey("StructureId");
                });
#pragma warning restore 612, 618
        }
    }
}
