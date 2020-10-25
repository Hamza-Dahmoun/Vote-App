using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class makeVoterBeingIdNullableForCandidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Voter_VoterBeingId",
                table: "Candidate");

            migrationBuilder.AlterColumn<Guid>(
                name: "VoterBeingId",
                table: "Candidate",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "VoterBeingId",
                table: "Candidate",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Voter_VoterBeingId",
                table: "Candidate",
                column: "VoterBeingId",
                principalTable: "Voter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
