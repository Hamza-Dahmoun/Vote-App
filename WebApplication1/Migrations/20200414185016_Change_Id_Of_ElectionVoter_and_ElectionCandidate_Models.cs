using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Change_Id_Of_ElectionVoter_and_ElectionCandidate_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectionVoter",
                table: "ElectionVoter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectionCandidate",
                table: "ElectionCandidate");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ElectionVoter",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ElectionCandidate",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectionVoter",
                table: "ElectionVoter",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectionCandidate",
                table: "ElectionCandidate",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionVoter_VoterId",
                table: "ElectionVoter",
                column: "VoterId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionCandidate_CandidateId",
                table: "ElectionCandidate",
                column: "CandidateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectionVoter",
                table: "ElectionVoter");

            migrationBuilder.DropIndex(
                name: "IX_ElectionVoter_VoterId",
                table: "ElectionVoter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectionCandidate",
                table: "ElectionCandidate");

            migrationBuilder.DropIndex(
                name: "IX_ElectionCandidate_CandidateId",
                table: "ElectionCandidate");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ElectionVoter");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ElectionCandidate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectionVoter",
                table: "ElectionVoter",
                columns: new[] { "VoterId", "ElectionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectionCandidate",
                table: "ElectionCandidate",
                columns: new[] { "CandidateId", "ElectionId" });
        }
    }
}
