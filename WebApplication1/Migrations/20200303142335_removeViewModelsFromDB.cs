using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class removeViewModelsFromDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateViewModel");

            migrationBuilder.DropTable(
                name: "PersonViewModel");

            migrationBuilder.DropTable(
                name: "StructureViewModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CandidateViewModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StructureLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StructureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VotesCount = table.Column<int>(type: "int", nullable: false),
                    hasVoted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonViewModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StructureLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StructureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hasVoted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StructureViewModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelValue = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructureViewModel", x => x.Id);
                });
        }
    }
}
