using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Remove_All_Election_Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Election_ElectionId",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Voter_Candidate_CandidateId",
                table: "Voter");

            migrationBuilder.DropForeignKey(
                name: "FK_Voter_Election_ElectionId",
                table: "Voter");

            migrationBuilder.DropForeignKey(
                name: "FK_Voter_Voter_VoterId",
                table: "Voter");

            migrationBuilder.DropIndex(
                name: "IX_Voter_CandidateId",
                table: "Voter");

            migrationBuilder.DropIndex(
                name: "IX_Voter_ElectionId",
                table: "Voter");

            migrationBuilder.DropIndex(
                name: "IX_Voter_VoterId",
                table: "Voter");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_ElectionId",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Voter");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Voter");

            migrationBuilder.DropColumn(
                name: "VoterId",
                table: "Voter");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Candidate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CandidateId",
                table: "Voter",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ElectionId",
                table: "Voter",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VoterId",
                table: "Voter",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ElectionId",
                table: "Candidate",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voter_CandidateId",
                table: "Voter",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Voter_ElectionId",
                table: "Voter",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Voter_VoterId",
                table: "Voter",
                column: "VoterId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_ElectionId",
                table: "Candidate",
                column: "ElectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Election_ElectionId",
                table: "Candidate",
                column: "ElectionId",
                principalTable: "Election",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voter_Candidate_CandidateId",
                table: "Voter",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voter_Election_ElectionId",
                table: "Voter",
                column: "ElectionId",
                principalTable: "Election",
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
    }
}
