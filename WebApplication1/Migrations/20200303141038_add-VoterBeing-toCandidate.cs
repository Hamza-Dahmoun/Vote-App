using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class addVoterBeingtoCandidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VoterBeingId",
                table: "Candidate",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CandidateViewModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    StructureName = table.Column<string>(nullable: true),
                    StructureLevel = table.Column<string>(nullable: true),
                    VotesCount = table.Column<int>(nullable: false),
                    hasVoted = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_VoterBeingId",
                table: "Candidate",
                column: "VoterBeingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Voter_VoterBeingId",
                table: "Candidate",
                column: "VoterBeingId",
                principalTable: "Voter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Voter_VoterBeingId",
                table: "Candidate");

            migrationBuilder.DropTable(
                name: "CandidateViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_VoterBeingId",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "VoterBeingId",
                table: "Candidate");
        }
    }
}
