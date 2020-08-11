using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class seedInitialCandidateData5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Candidate",
                columns: new[] { "Id", "ElectionId", "VoterBeingId", "isNeutralOpinion" },
                values: new object[,]
                {
                    { new Guid("bf04bf97-e955-4692-b211-3363b802e6a9"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("df4b1285-b216-488c-88b7-0de75528f2fc"), false },
                    { new Guid("5f274499-e11a-4955-a044-0b2a8b106e57"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("c81388cf-be3e-42fc-b791-02eccce16095"), false },
                    { new Guid("5fa3828d-5ab3-44a3-9c9b-8826de4c4dca"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847"), false },
                    { new Guid("4ba8ee63-f5f4-41bc-b4ed-f4ba9d11431d"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"), false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("4ba8ee63-f5f4-41bc-b4ed-f4ba9d11431d"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("5f274499-e11a-4955-a044-0b2a8b106e57"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("5fa3828d-5ab3-44a3-9c9b-8826de4c4dca"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("bf04bf97-e955-4692-b211-3363b802e6a9"));
        }
    }
}
