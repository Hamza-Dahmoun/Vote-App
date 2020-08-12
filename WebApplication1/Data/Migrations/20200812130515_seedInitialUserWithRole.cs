using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class seedInitialUserWithRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b885366d-7d91-4dac-ba45-0899f6cacceb",
                column: "ConcurrencyStamp",
                value: "1891dd4a-8e35-4eab-972f-bc098b1579ea");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6eb33ce9-8dd1-4eaf-b0f3-41c27ae9233f", 0, "d82be2c5-1aaf-447f-9d68-8b32cc9c85b9", null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEAv4XhTH0M6LJxT4vavONXSs9wZ36df7Y0YQqx9/RFPSzYDyrce7biVOxyMgUe0w9g==", null, false, "f50bd34c-3534-4813-91cd-5b7ec39c0926", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3e67f5a9-073d-4ad2-96f3-475739e242af", 0, "8875e977-6d41-40c8-81cd-fe74b3ea758e", null, false, false, null, null, null, null, null, false, "30232fe3-f32b-426f-821c-4191143fce77", false, "admin.user" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "6eb33ce9-8dd1-4eaf-b0f3-41c27ae9233f", "b885366d-7d91-4dac-ba45-0899f6cacceb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "6eb33ce9-8dd1-4eaf-b0f3-41c27ae9233f", "b885366d-7d91-4dac-ba45-0899f6cacceb" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3e67f5a9-073d-4ad2-96f3-475739e242af");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6eb33ce9-8dd1-4eaf-b0f3-41c27ae9233f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b885366d-7d91-4dac-ba45-0899f6cacceb",
                column: "ConcurrencyStamp",
                value: "8aed8dcd-91f3-40f2-8f77-8c33fa44c3c5");
        }
    }
}
