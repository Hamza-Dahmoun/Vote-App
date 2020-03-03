﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class addLevelValuetoStructureLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelValue",
                table: "StructureViewModel",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "LevelValue",
            //    table: "StructureLevel",
            //    nullable: false,
            //    defaultValue: 0);
        }

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropColumn(
        //        name: "LevelValue",
        //        table: "StructureViewModel");

        //    migrationBuilder.DropColumn(
        //        name: "LevelValue",
        //        table: "StructureLevel");
        //}
    }
}
