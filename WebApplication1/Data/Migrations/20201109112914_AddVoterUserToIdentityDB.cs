using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class AddVoterUserToIdentityDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", "2c5e174e-3b0e-446f-86af-483d56fd7210" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2618884a-2189-48b5-a1ee-0077dcf92239",
                column: "ConcurrencyStamp",
                value: "5c38594d-f3a3-4d95-b3a7-bded9bf426e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "eaaa0456-b4ec-4bbe-ba0d-c0d182207bd4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78962b34-8b8c-4059-837a-25a8941654c5",
                column: "ConcurrencyStamp",
                value: "61ed46f1-4fae-4f63-bca6-b8ed461dc1c4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1788cddf-09e3-4ea6-ab17-133cf5b57310", 0, "ec2017f2-dbb5-45b6-8af6-86a17dde6b43", null, false, false, null, null, "MYADMIN", "AQAAAAEAACcQAAAAEON5Hvwfdft0U4NHxxhUOYjZ5lKT894veUNloV2hO6/MTBd9+I0vsfpxQMhZNqLUyw==", null, false, "6c1b260e-acd7-412b-a71a-ec2d23daf9dc", false, "myadmin" },
                    { "f304d35f-a3bd-4b63-bba3-3411331edc34", 0, "94324043-9c4d-4acb-a231-e1dec63e379c", null, false, false, null, null, "MYVOTER", "AQAAAAEAACcQAAAAEOKwZ/y8UYivlaSWOHQ0KdKbiqr/gEmfk/MxIDe4Eb1wPxVb5fRKb2r6MTr5AMYZOw==", null, false, "856751f7-d556-4976-9524-5bb4e5d46b4f", false, "myvoter" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1788cddf-09e3-4ea6-ab17-133cf5b57310", "2c5e174e-3b0e-446f-86af-483d56fd7210" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "f304d35f-a3bd-4b63-bba3-3411331edc34", "2618884a-2189-48b5-a1ee-0077dcf92239" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "1788cddf-09e3-4ea6-ab17-133cf5b57310", "2c5e174e-3b0e-446f-86af-483d56fd7210" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "f304d35f-a3bd-4b63-bba3-3411331edc34", "2618884a-2189-48b5-a1ee-0077dcf92239" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1788cddf-09e3-4ea6-ab17-133cf5b57310");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f304d35f-a3bd-4b63-bba3-3411331edc34");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2618884a-2189-48b5-a1ee-0077dcf92239",
                column: "ConcurrencyStamp",
                value: "3296c757-b367-4845-afca-5145aaaa83c8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "55157bf0-aef6-49cd-9eae-9c6c8a495b0e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78962b34-8b8c-4059-837a-25a8941654c5",
                column: "ConcurrencyStamp",
                value: "bf85c4e6-f2d9-4b5d-a649-b283fdc7f82c");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "30b8dd9a-eb35-47a5-96ad-599a5a4630b8", null, false, false, null, null, "MYUSER", "AQAAAAEAACcQAAAAEBEe9JNXl0ij51V38ctzZMUdfb6yvjy6xkKrwg+gI+I2BuplaCXEh/vMEVhIwUc+jA==", null, false, "d732ec18-6184-4e9f-a6ee-3e2b02bc650e", false, "myuser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", "2c5e174e-3b0e-446f-86af-483d56fd7210" });
        }
    }
}
