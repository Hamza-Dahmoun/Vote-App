using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Adding_Election_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ElectionId",
                table: "Voter",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ElectionId",
                table: "Candidate",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Election",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    DurationInDays = table.Column<int>(nullable: false),
                    HasNeutral = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Election", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voter_ElectionId",
                table: "Voter",
                column: "ElectionId");

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
                name: "FK_Voter_Election_ElectionId",
                table: "Voter",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Voter_Election_ElectionId",
                table: "Voter");

            migrationBuilder.DropTable(
                name: "Election");

            migrationBuilder.DropIndex(
                name: "IX_Voter_ElectionId",
                table: "Voter");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_ElectionId",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Voter");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Candidate");
        }
    }
}
