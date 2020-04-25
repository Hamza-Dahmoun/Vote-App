using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Remove_Redundunt_Fields_From_Candidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_State_StateId",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_StateId",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Candidate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Candidate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Candidate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "Candidate",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Candidate",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_StateId",
                table: "Candidate",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_State_StateId",
                table: "Candidate",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
