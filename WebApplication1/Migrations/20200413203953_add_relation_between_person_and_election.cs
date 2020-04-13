using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class add_relation_between_person_and_election : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CandidateId",
                table: "Voter",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VoterId",
                table: "Voter",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voter_CandidateId",
                table: "Voter",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Voter_VoterId",
                table: "Voter",
                column: "VoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voter_Candidate_CandidateId",
                table: "Voter",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voter_Voter_VoterId",
                table: "Voter",
                column: "VoterId",
                principalTable: "Voter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voter_Candidate_CandidateId",
                table: "Voter");

            migrationBuilder.DropForeignKey(
                name: "FK_Voter_Voter_VoterId",
                table: "Voter");

            migrationBuilder.DropIndex(
                name: "IX_Voter_CandidateId",
                table: "Voter");

            migrationBuilder.DropIndex(
                name: "IX_Voter_VoterId",
                table: "Voter");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Voter");

            migrationBuilder.DropColumn(
                name: "VoterId",
                table: "Voter");
        }
    }
}
