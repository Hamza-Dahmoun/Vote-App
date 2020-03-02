using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class UseUpperCaseinVoteandStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Structure_StructureLevel_levelId",
                table: "Structure");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "StructureLevel");

            migrationBuilder.RenameColumn(
                name: "datetime",
                table: "Vote",
                newName: "Datetime");

            migrationBuilder.RenameColumn(
                name: "levelId",
                table: "Structure",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Structure_levelId",
                table: "Structure",
                newName: "IX_Structure_LevelId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Structure",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Structure_StructureLevel_LevelId",
                table: "Structure",
                column: "LevelId",
                principalTable: "StructureLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Structure_StructureLevel_LevelId",
                table: "Structure");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Structure");

            migrationBuilder.RenameColumn(
                name: "Datetime",
                table: "Vote",
                newName: "datetime");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Structure",
                newName: "levelId");

            migrationBuilder.RenameIndex(
                name: "IX_Structure_LevelId",
                table: "Structure",
                newName: "IX_Structure_levelId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StructureLevel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Structure_StructureLevel_levelId",
                table: "Structure",
                column: "levelId",
                principalTable: "StructureLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
