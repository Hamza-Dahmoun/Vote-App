using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class use_State_Insteadof_Structure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Structure_StructureId",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Voter_Structure_StructureId",
                table: "Voter");

            migrationBuilder.DropTable(
                name: "Structure");

            migrationBuilder.DropTable(
                name: "StructureLevel");

            migrationBuilder.DropIndex(
                name: "IX_Voter_StructureId",
                table: "Voter");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_StructureId",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "StructureId",
                table: "Voter");

            migrationBuilder.DropColumn(
                name: "StructureId",
                table: "Candidate");

            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "Voter",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "Candidate",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voter_StateId",
                table: "Voter",
                column: "StateId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Voter_State_StateId",
                table: "Voter",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_State_StateId",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Voter_State_StateId",
                table: "Voter");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropIndex(
                name: "IX_Voter_StateId",
                table: "Voter");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_StateId",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Voter");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Candidate");

            migrationBuilder.AddColumn<Guid>(
                name: "StructureId",
                table: "Voter",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StructureId",
                table: "Candidate",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StructureLevel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelValue = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructureLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Structure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Structure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Structure_StructureLevel_LevelId",
                        column: x => x.LevelId,
                        principalTable: "StructureLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voter_StructureId",
                table: "Voter",
                column: "StructureId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_StructureId",
                table: "Candidate",
                column: "StructureId");

            migrationBuilder.CreateIndex(
                name: "IX_Structure_LevelId",
                table: "Structure",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Structure_StructureId",
                table: "Candidate",
                column: "StructureId",
                principalTable: "Structure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voter_Structure_StructureId",
                table: "Voter",
                column: "StructureId",
                principalTable: "Structure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
