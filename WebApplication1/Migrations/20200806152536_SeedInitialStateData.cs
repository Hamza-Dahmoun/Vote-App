using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class SeedInitialStateData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("fa9b72ee-dfcc-4353-b195-5c2855b1343f"), "Oran" },
                    { new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"), "Meca" },
                    { new Guid("3138e047-e80f-44a1-ae1d-96804784f807"), "Cairo" },
                    { new Guid("33f88529-1a04-4d5e-84dc-135646948bc0"), "ElQuds" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: new Guid("3138e047-e80f-44a1-ae1d-96804784f807"));

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"));

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: new Guid("33f88529-1a04-4d5e-84dc-135646948bc0"));

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: new Guid("fa9b72ee-dfcc-4353-b195-5c2855b1343f"));
        }
    }
}
