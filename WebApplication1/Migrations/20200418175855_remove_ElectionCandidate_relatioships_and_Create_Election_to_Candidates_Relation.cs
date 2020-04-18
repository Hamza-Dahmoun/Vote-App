using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class remove_ElectionCandidate_relatioships_and_Create_Election_to_Candidates_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ElectionId",
                table: "Candidate",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Election_ElectionId",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_ElectionId",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Candidate");
        }
    }
}
