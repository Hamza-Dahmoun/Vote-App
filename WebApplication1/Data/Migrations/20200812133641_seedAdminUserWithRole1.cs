using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class seedAdminUserWithRole1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "6eb33ce9-8dd1-4eaf-b0f3-41c27ae9233f", "b885366d-7d91-4dac-ba45-0899f6cacceb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b885366d-7d91-4dac-ba45-0899f6cacceb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6eb33ce9-8dd1-4eaf-b0f3-41c27ae9233f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "6aa7fe70-fefc-443a-b905-9df6980ccef5", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "2bbcd4f1-3a1d-471e-adde-c8d67a9a2905", null, false, false, null, null, "MYUSER", "AQAAAAEAACcQAAAAEA75HDFdXwDn73vBeIay8by50rZfye/WRx2KCJzBlO00RXz5U8jbsrVk1H1izjeDUw==", null, false, "5a7f2e41-d67b-411a-bef4-46fc8fad6614", false, "myuser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", "2c5e174e-3b0e-446f-86af-483d56fd7210" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", "2c5e174e-3b0e-446f-86af-483d56fd7210" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b885366d-7d91-4dac-ba45-0899f6cacceb", "8c3f0c6b-4eab-4487-a3ae-6bce5e46ade4", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6eb33ce9-8dd1-4eaf-b0f3-41c27ae9233f", 0, "9f4f7380-a777-4c52-8a5c-b0ef470fec50", null, false, false, null, null, "MYUSER", "AQAAAAEAACcQAAAAEHBQhxIut0AuvYJ3hThIeCpuPlWxRlGBsIA4cZPlIPoX/gKJ18eM/ZeIPU2qmnp3qQ==", null, false, "511ce72f-7796-4b6c-9a2b-60674ddd86e2", false, "myuser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "6eb33ce9-8dd1-4eaf-b0f3-41c27ae9233f", "b885366d-7d91-4dac-ba45-0899f6cacceb" });
        }
    }
}
