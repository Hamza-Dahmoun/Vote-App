using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class updateVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Candidate_CandidateID",
                table: "Vote");

            migrationBuilder.RenameColumn(
                name: "CandidateID",
                table: "Vote",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_CandidateID",
                table: "Vote",
                newName: "IX_Vote_CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Candidate_CandidateId",
                table: "Vote",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Candidate_CandidateId",
                table: "Vote");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "Vote",
                newName: "CandidateID");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_CandidateId",
                table: "Vote",
                newName: "IX_Vote_CandidateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Candidate_CandidateID",
                table: "Vote",
                column: "CandidateID",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
