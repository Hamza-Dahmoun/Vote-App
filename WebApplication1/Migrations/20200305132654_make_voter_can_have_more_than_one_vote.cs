using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class make_voter_can_have_more_than_one_vote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Voter_VoterID",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_VoterID",
                table: "Vote");

            migrationBuilder.RenameColumn(
                name: "VoterID",
                table: "Vote",
                newName: "VoterId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_VoterId",
                table: "Vote",
                column: "VoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Voter_VoterId",
                table: "Vote",
                column: "VoterId",
                principalTable: "Voter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Voter_VoterId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_VoterId",
                table: "Vote");

            migrationBuilder.RenameColumn(
                name: "VoterId",
                table: "Vote",
                newName: "VoterID");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_VoterID",
                table: "Vote",
                column: "VoterID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Voter_VoterID",
                table: "Vote",
                column: "VoterID",
                principalTable: "Voter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
