using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class seedInitialStateAndVoterData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"), "Meca" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"));
        }
    }
}
