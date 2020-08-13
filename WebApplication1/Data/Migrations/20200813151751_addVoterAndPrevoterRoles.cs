using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class addVoterAndPrevoterRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "55157bf0-aef6-49cd-9eae-9c6c8a495b0e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2618884a-2189-48b5-a1ee-0077dcf92239", "3296c757-b367-4845-afca-5145aaaa83c8", "Voter", "VOTER" },
                    { "78962b34-8b8c-4059-837a-25a8941654c5", "bf85c4e6-f2d9-4b5d-a649-b283fdc7f82c", "PreVoter", "PREVOTER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30b8dd9a-eb35-47a5-96ad-599a5a4630b8", "AQAAAAEAACcQAAAAEBEe9JNXl0ij51V38ctzZMUdfb6yvjy6xkKrwg+gI+I2BuplaCXEh/vMEVhIwUc+jA==", "d732ec18-6184-4e9f-a6ee-3e2b02bc650e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2618884a-2189-48b5-a1ee-0077dcf92239");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78962b34-8b8c-4059-837a-25a8941654c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "6aa7fe70-fefc-443a-b905-9df6980ccef5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bbcd4f1-3a1d-471e-adde-c8d67a9a2905", "AQAAAAEAACcQAAAAEA75HDFdXwDn73vBeIay8by50rZfye/WRx2KCJzBlO00RXz5U8jbsrVk1H1izjeDUw==", "5a7f2e41-d67b-411a-bef4-46fc8fad6614" });
        }
    }
}
