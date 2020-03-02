using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class AddVoteDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StructureLevel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructureLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Structure",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    levelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Structure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Structure_StructureLevel_levelId",
                        column: x => x.levelId,
                        principalTable: "StructureLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    StructureId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidate_Structure_StructureId",
                        column: x => x.StructureId,
                        principalTable: "Structure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Voter",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    StructureId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voter_Structure_StructureId",
                        column: x => x.StructureId,
                        principalTable: "Structure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    datetime = table.Column<DateTime>(nullable: false),
                    VoterID = table.Column<Guid>(nullable: false),
                    CandidateID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vote_Candidate_CandidateID",
                        column: x => x.CandidateID,
                        principalTable: "Candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vote_Voter_VoterID",
                        column: x => x.VoterID,
                        principalTable: "Voter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_StructureId",
                table: "Candidate",
                column: "StructureId");

            migrationBuilder.CreateIndex(
                name: "IX_Structure_levelId",
                table: "Structure",
                column: "levelId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_CandidateID",
                table: "Vote",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_VoterID",
                table: "Vote",
                column: "VoterID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voter_StructureId",
                table: "Voter",
                column: "StructureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "Voter");

            migrationBuilder.DropTable(
                name: "Structure");

            migrationBuilder.DropTable(
                name: "StructureLevel");
        }
    }
}
