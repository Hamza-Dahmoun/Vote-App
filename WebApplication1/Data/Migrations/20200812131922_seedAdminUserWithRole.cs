using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class seedAdminUserWithRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3e67f5a9-073d-4ad2-96f3-475739e242af");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b885366d-7d91-4dac-ba45-0899f6cacceb",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8c3f0c6b-4eab-4487-a3ae-6bce5e46ade4", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6eb33ce9-8dd1-4eaf-b0f3-41c27ae9233f",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "9f4f7380-a777-4c52-8a5c-b0ef470fec50", "MYUSER", "AQAAAAEAACcQAAAAEHBQhxIut0AuvYJ3hThIeCpuPlWxRlGBsIA4cZPlIPoX/gKJ18eM/ZeIPU2qmnp3qQ==", "511ce72f-7796-4b6c-9a2b-60674ddd86e2", "myuser" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b885366d-7d91-4dac-ba45-0899f6cacceb",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1891dd4a-8e35-4eab-972f-bc098b1579ea", "Testrole", "TESTROLE" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6eb33ce9-8dd1-4eaf-b0f3-41c27ae9233f",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "d82be2c5-1aaf-447f-9d68-8b32cc9c85b9", "ADMIN", "AQAAAAEAACcQAAAAEAv4XhTH0M6LJxT4vavONXSs9wZ36df7Y0YQqx9/RFPSzYDyrce7biVOxyMgUe0w9g==", "f50bd34c-3534-4813-91cd-5b7ec39c0926", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3e67f5a9-073d-4ad2-96f3-475739e242af", 0, "8875e977-6d41-40c8-81cd-fe74b3ea758e", null, false, false, null, null, null, null, null, false, "30232fe3-f32b-426f-821c-4191143fce77", false, "admin.user" });
        }
    }
}
