using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class seedInitialStateAndVoterData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voter_State_StateId",
                table: "Voter");

            migrationBuilder.AlterColumn<Guid>(
                name: "StateId",
                table: "Voter",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("fa9b72ee-dfcc-4353-b195-5c2855b1343f"), "Oran" });

            migrationBuilder.InsertData(
                table: "Voter",
                columns: new[] { "Id", "FirstName", "LastName", "StateId", "UserId" },
                values: new object[] { new Guid("0d925194-fee5-4750-a53c-b36a47afeeab"), "Hamza", "Dahmoun", new Guid("fa9b72ee-dfcc-4353-b195-5c2855b1343f"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.AddForeignKey(
                name: "FK_Voter_State_StateId",
                table: "Voter",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voter_State_StateId",
                table: "Voter");

            migrationBuilder.DeleteData(
                table: "Voter",
                keyColumn: "Id",
                keyValue: new Guid("0d925194-fee5-4750-a53c-b36a47afeeab"));

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: new Guid("fa9b72ee-dfcc-4353-b195-5c2855b1343f"));

            migrationBuilder.AlterColumn<Guid>(
                name: "StateId",
                table: "Voter",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Voter_State_StateId",
                table: "Voter",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
