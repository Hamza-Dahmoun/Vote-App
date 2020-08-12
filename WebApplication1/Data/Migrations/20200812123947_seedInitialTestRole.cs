using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class seedInitialTestRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b885366d-7d91-4dac-ba45-0899f6cacceb", "8aed8dcd-91f3-40f2-8f77-8c33fa44c3c5", "Testrole", "TESTROLE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b885366d-7d91-4dac-ba45-0899f6cacceb");
        }
    }
}
