using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class seedInitialCandidateData4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Candidate",
                columns: new[] { "Id", "ElectionId", "VoterBeingId", "isNeutralOpinion" },
                values: new object[] { new Guid("8784eb52-3c3f-427b-b325-958f854dbfbb"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("0d925194-fee5-4750-a53c-b36a47afeeab"), false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("8784eb52-3c3f-427b-b325-958f854dbfbb"));
        }
    }
}
