using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class RemoveVoterBeingNavigationPropertyFromCandidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Voter_VoterBeingId",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_VoterBeingId",
                table: "Candidate");

            migrationBuilder.AlterColumn<Guid>(
                name: "VoterBeingId",
                table: "Candidate",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "VoterBeingId",
                table: "Candidate",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

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
    }
}
