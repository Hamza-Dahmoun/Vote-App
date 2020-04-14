using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Remove_Election_Voter_Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Election_Voter_VoterId",
                table: "Election");

            migrationBuilder.DropForeignKey(
                name: "FK_Voter_Election_ElectionId",
                table: "Voter");

            migrationBuilder.DropIndex(
                name: "IX_Voter_ElectionId",
                table: "Voter");

            migrationBuilder.DropIndex(
                name: "IX_Election_VoterId",
                table: "Election");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Voter");

            migrationBuilder.DropColumn(
                name: "VoterId",
                table: "Election");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ElectionId",
                table: "Voter",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VoterId",
                table: "Election",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voter_ElectionId",
                table: "Voter",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Election_VoterId",
                table: "Election",
                column: "VoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Election_Voter_VoterId",
                table: "Election",
                column: "VoterId",
                principalTable: "Voter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voter_Election_ElectionId",
                table: "Voter",
                column: "ElectionId",
                principalTable: "Election",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
