using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class seedInitialVoteData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("7c6fadf7-089b-4836-a849-1a81f2c0b84f"),
                column: "VoterId",
                value: new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("7c6fadf7-089b-4836-a849-1a81f2c0b84f"),
                column: "VoterId",
                value: new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61"));
        }
    }
}
