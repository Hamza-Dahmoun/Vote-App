using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class add_vote_belongs_to_election_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ElectionId",
                table: "Vote",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Vote_ElectionId",
                table: "Vote",
                column: "ElectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Election_ElectionId",
                table: "Vote",
                column: "ElectionId",
                principalTable: "Election",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Election_ElectionId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_ElectionId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Vote");
        }
    }
}
