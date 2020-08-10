using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class seedInitialStateAndVoterData4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("3138e047-e80f-44a1-ae1d-96804784f807"), "Cairo" });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("33f88529-1a04-4d5e-84dc-135646948bc0"), "ElQuds" });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("918333ff-ac52-4424-a3d9-09bee4c39b91"), "Algiers" });

            migrationBuilder.InsertData(
                table: "Voter",
                columns: new[] { "Id", "FirstName", "LastName", "StateId", "UserId" },
                values: new object[,]
                {
                    { new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef"), "Sidahmed", "Dahmoun", new Guid("3138e047-e80f-44a1-ae1d-96804784f807"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6c9de511-db59-4be8-9315-d58700ce10be"), "Bilal", "Dahmoun", new Guid("3138e047-e80f-44a1-ae1d-96804784f807"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d789a80a-493f-4248-90b0-35edab1c3c63"), "Azzedine", "Brahmi", new Guid("3138e047-e80f-44a1-ae1d-96804784f807"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("cd285ce6-b6e9-4933-8e07-6d50e0251788"), "Maria", "Hafsa", new Guid("33f88529-1a04-4d5e-84dc-135646948bc0"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d454ae67-913e-4f7b-82c1-4da539737199"), "Brahim", "Roudjai", new Guid("33f88529-1a04-4d5e-84dc-135646948bc0"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("18537eea-e8f8-49cd-8ddf-10646c9a9f21"), "Amine", "Brahem", new Guid("33f88529-1a04-4d5e-84dc-135646948bc0"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("71ad22cf-168a-40be-9867-4a7ebe34c339"), "Mohamed", "Chikhi", new Guid("918333ff-ac52-4424-a3d9-09bee4c39b91"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("783118b1-111d-4c6e-b75e-dcd721ccfc2c"), "Mahmoud", "Dahmoun", new Guid("918333ff-ac52-4424-a3d9-09bee4c39b91"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0642267b-8df3-405d-b596-e50c2cdeefde"), "Lakhdar", "Zitouni", new Guid("918333ff-ac52-4424-a3d9-09bee4c39b91"), new Guid("00000000-0000-0000-0000-000000000000") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("0642267b-8df3-405d-b596-e50c2cdeefde"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("18537eea-e8f8-49cd-8ddf-10646c9a9f21"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("6c9de511-db59-4be8-9315-d58700ce10be"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("71ad22cf-168a-40be-9867-4a7ebe34c339"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("783118b1-111d-4c6e-b75e-dcd721ccfc2c"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("cd285ce6-b6e9-4933-8e07-6d50e0251788"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("d454ae67-913e-4f7b-82c1-4da539737199"));

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("d789a80a-493f-4248-90b0-35edab1c3c63"));

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: new Guid("3138e047-e80f-44a1-ae1d-96804784f807"));

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: new Guid("33f88529-1a04-4d5e-84dc-135646948bc0"));

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: new Guid("918333ff-ac52-4424-a3d9-09bee4c39b91"));
        }
    }
}
