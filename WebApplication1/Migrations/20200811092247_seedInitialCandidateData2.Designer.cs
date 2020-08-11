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
    [Migration("20200811092247_seedInitialCandidateData2")]
    partial class seedInitialCandidateData2
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

                    b.Property<Guid?>("ElectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VoterBeingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isNeutralOpinion")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ElectionId");

                    b.HasIndex("VoterBeingId");

                    b.ToTable("Candidate");

                    b.HasData(
                        new
                        {
                            Id = new Guid("df5fdd35-6c00-4391-96bd-c194e598be78"),
                            VoterBeingId = new Guid("0d925194-fee5-4750-a53c-b36a47afeeab"),
                            isNeutralOpinion = false
                        },
                        new
                        {
                            Id = new Guid("239a05cc-ad00-43f6-ae75-54c58ccb9786"),
                            VoterBeingId = new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61"),
                            isNeutralOpinion = false
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.Election", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DurationInDays")
                        .HasColumnType("int");

                    b.Property<bool>("HasNeutral")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Election");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 1",
                            StartDate = new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 2",
                            StartDate = new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("6d1ac165-5488-4f86-84ad-47301d813802"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 3",
                            StartDate = new DateTime(2013, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 4",
                            StartDate = new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 5",
                            StartDate = new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 6",
                            StartDate = new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 7",
                            StartDate = new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 8",
                            StartDate = new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("b6ea83b1-3cbe-47ed-bf25-9abc8dac9644"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 9",
                            StartDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 10",
                            StartDate = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 11",
                            StartDate = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("b798676d-750e-4950-a527-5ccbc17004a4"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 12",
                            StartDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("33e5e889-f44b-461a-83c9-680f34f82e06"),
                            DurationInDays = 3,
                            HasNeutral = false,
                            Name = "Election Test 13",
                            StartDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("State");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fa9b72ee-dfcc-4353-b195-5c2855b1343f"),
                            Name = "Oran"
                        },
                        new
                        {
                            Id = new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"),
                            Name = "Meca"
                        },
                        new
                        {
                            Id = new Guid("3138e047-e80f-44a1-ae1d-96804784f807"),
                            Name = "Cairo"
                        },
                        new
                        {
                            Id = new Guid("33f88529-1a04-4d5e-84dc-135646948bc0"),
                            Name = "ElQuds"
                        },
                        new
                        {
                            Id = new Guid("918333ff-ac52-4424-a3d9-09bee4c39b91"),
                            Name = "Algiers"
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ElectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VoterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("ElectionId");

                    b.HasIndex("VoterId");

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

                    b.Property<Guid>("StateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Voter");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0d925194-fee5-4750-a53c-b36a47afeeab"),
                            FirstName = "Hamza",
                            LastName = "Dahmoun",
                            StateId = new Guid("fa9b72ee-dfcc-4353-b195-5c2855b1343f"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61"),
                            FirstName = "Ikram",
                            LastName = "Dahmoun",
                            StateId = new Guid("fa9b72ee-dfcc-4353-b195-5c2855b1343f"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("df4b1285-b216-488c-88b7-0de75528f2fc"),
                            FirstName = "Ahmed",
                            LastName = "Mohamed",
                            StateId = new Guid("fa9b72ee-dfcc-4353-b195-5c2855b1343f"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("c81388cf-be3e-42fc-b791-02eccce16095"),
                            FirstName = "Mohamed",
                            LastName = "Zitouni",
                            StateId = new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847"),
                            FirstName = "Yasser",
                            LastName = "Zitouni",
                            StateId = new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"),
                            FirstName = "Larbi",
                            LastName = "Fkaier",
                            StateId = new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("1d6d6442-25ac-4cf9-aa44-0d91622a4927"),
                            FirstName = "Djamel",
                            LastName = "Tahraoui",
                            StateId = new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef"),
                            FirstName = "Sidahmed",
                            LastName = "Dahmoun",
                            StateId = new Guid("3138e047-e80f-44a1-ae1d-96804784f807"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("6c9de511-db59-4be8-9315-d58700ce10be"),
                            FirstName = "Bilal",
                            LastName = "Dahmoun",
                            StateId = new Guid("3138e047-e80f-44a1-ae1d-96804784f807"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("cd285ce6-b6e9-4933-8e07-6d50e0251788"),
                            FirstName = "Maria",
                            LastName = "Hafsa",
                            StateId = new Guid("33f88529-1a04-4d5e-84dc-135646948bc0"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("d454ae67-913e-4f7b-82c1-4da539737199"),
                            FirstName = "Brahim",
                            LastName = "Roudjai",
                            StateId = new Guid("33f88529-1a04-4d5e-84dc-135646948bc0"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("18537eea-e8f8-49cd-8ddf-10646c9a9f21"),
                            FirstName = "Amine",
                            LastName = "Brahem",
                            StateId = new Guid("33f88529-1a04-4d5e-84dc-135646948bc0"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("d789a80a-493f-4248-90b0-35edab1c3c63"),
                            FirstName = "Azzedine",
                            LastName = "Brahmi",
                            StateId = new Guid("3138e047-e80f-44a1-ae1d-96804784f807"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("71ad22cf-168a-40be-9867-4a7ebe34c339"),
                            FirstName = "Mohamed",
                            LastName = "Chikhi",
                            StateId = new Guid("918333ff-ac52-4424-a3d9-09bee4c39b91"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("783118b1-111d-4c6e-b75e-dcd721ccfc2c"),
                            FirstName = "Mahmoud",
                            LastName = "Dahmoun",
                            StateId = new Guid("918333ff-ac52-4424-a3d9-09bee4c39b91"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("0642267b-8df3-405d-b596-e50c2cdeefde"),
                            FirstName = "Lakhdar",
                            LastName = "Zitouni",
                            StateId = new Guid("918333ff-ac52-4424-a3d9-09bee4c39b91"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.Candidate", b =>
                {
                    b.HasOne("WebApplication1.Models.Election", "Election")
                        .WithMany("Candidates")
                        .HasForeignKey("ElectionId");

                    b.HasOne("WebApplication1.Models.Voter", "VoterBeing")
                        .WithMany()
                        .HasForeignKey("VoterBeingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.Models.Vote", b =>
                {
                    b.HasOne("WebApplication1.Models.Candidate", "Candidate")
                        .WithMany("Votes")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Election", "Election")
                        .WithMany("Votes")
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Voter", "Voter")
                        .WithMany("Votes")
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.Models.Voter", b =>
                {
                    b.HasOne("WebApplication1.Models.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
