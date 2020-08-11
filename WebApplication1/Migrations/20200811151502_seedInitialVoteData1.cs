using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class seedInitialVoteData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("7c6fadf7-089b-4836-a849-1a81f2c0b84f"),
                column: "Datetime",
                value: new DateTime(2012, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("7c6fadf7-089b-4836-a849-1a81f2c0b84f"),
                column: "Datetime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
