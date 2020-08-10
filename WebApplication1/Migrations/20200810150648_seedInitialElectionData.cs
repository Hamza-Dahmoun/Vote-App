using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class seedInitialElectionData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Election",
                columns: new[] { "Id", "DurationInDays", "HasNeutral", "Name", "StartDate" },
                values: new object[,]
                {
                    { new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), 3, false, "Election Test 1", new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"), 3, false, "Election Test 2", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6d1ac165-5488-4f86-84ad-47301d813802"), 3, false, "Election Test 3", new DateTime(2013, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), 3, false, "Election Test 4", new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), 3, false, "Election Test 5", new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), 3, false, "Election Test 6", new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), 3, false, "Election Test 7", new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"), 3, false, "Election Test 8", new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b6ea83b1-3cbe-47ed-bf25-9abc8dac9644"), 3, false, "Election Test 9", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"), 3, false, "Election Test 10", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), 3, false, "Election Test 11", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), 3, false, "Election Test 12", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33e5e889-f44b-461a-83c9-680f34f82e06"), 3, false, "Election Test 13", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("33e5e889-f44b-461a-83c9-680f34f82e06"));

            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"));

            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"));

            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"));

            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("6d1ac165-5488-4f86-84ad-47301d813802"));

            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"));

            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("b6ea83b1-3cbe-47ed-bf25-9abc8dac9644"));

            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("b798676d-750e-4950-a527-5ccbc17004a4"));

            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"));

            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"));

            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"));

            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"));

            migrationBuilder.DeleteData(
                table: "Election",
                keyColumn: "Id",
                keyValue: new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"));
        }
    }
}
