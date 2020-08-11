using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class seedInitialCandidateData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Candidate",
                columns: new[] { "Id", "ElectionId", "VoterBeingId", "isNeutralOpinion" },
                values: new object[] { new Guid("239a05cc-ad00-43f6-ae75-54c58ccb9786"), null, new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61"), false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("239a05cc-ad00-43f6-ae75-54c58ccb9786"));
        }
    }
}
