using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class seedInitialStateAndVoterData3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Voter",
                columns: new[] { "Id", "FirstName", "LastName", "StateId", "UserId" },
                values: new object[,]
                {
                    { new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61"), "Ikram", "Dahmoun", new Guid("fa9b72ee-dfcc-4353-b195-5c2855b1343f"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("df4b1285-b216-488c-88b7-0de75528f2fc"), "Ahmed", "Mohamed", new Guid("fa9b72ee-dfcc-4353-b195-5c2855b1343f"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c81388cf-be3e-42fc-b791-02eccce16095"), "Mohamed", "Zitouni", new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847"), "Yasser", "Zitouni", new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"), "Larbi", "Fkaier", new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("1d6d6442-25ac-4cf9-aa44-0d91622a4927"), "Djamel", "Tahraoui", new Guid("32065802-7f25-47bc-8987-dd4fdb2829c4"), new Guid("00000000-0000-0000-0000-000000000000") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("1d6d6442-25ac-4cf9-aa44-0d91622a4927"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("c81388cf-be3e-42fc-b791-02eccce16095"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("df4b1285-b216-488c-88b7-0de75528f2fc"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"));
        }
    }
}
