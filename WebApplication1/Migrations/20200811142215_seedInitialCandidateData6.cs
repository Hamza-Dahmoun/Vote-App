using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class seedInitialCandidateData6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("239a05cc-ad00-43f6-ae75-54c58ccb9786"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("4ba8ee63-f5f4-41bc-b4ed-f4ba9d11431d"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("5f274499-e11a-4955-a044-0b2a8b106e57"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("5fa3828d-5ab3-44a3-9c9b-8826de4c4dca"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("8784eb52-3c3f-427b-b325-958f854dbfbb"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("bf04bf97-e955-4692-b211-3363b802e6a9"));

            migrationBuilder.InsertData(
                table: "Candidate",
                columns: new[] { "Id", "ElectionId", "VoterBeingId", "isNeutralOpinion" },
                values: new object[,]
                {
                    { new Guid("beb19f42-63b1-45ec-bf80-a8a7cd381a65"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61"), false },
                    { new Guid("1ba784f8-e0ef-491d-a451-e3ffc06dbf3e"), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847"), false },
                    { new Guid("73d29d97-35c5-4194-9683-2df0a0b6b390"), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"), false },
                    { new Guid("d06219da-1725-494a-9f7c-2653d9087ab6"), new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"), new Guid("c81388cf-be3e-42fc-b791-02eccce16095"), false },
                    { new Guid("fba05cfd-4d29-4c03-ad68-c58b094d5e87"), new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847"), false },
                    { new Guid("919cfb2c-580a-4079-90f5-f3ae967b672b"), new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"), false },
                    { new Guid("56e347ac-7a53-4080-95d7-b4e87ecdefb2"), new Guid("b6ea83b1-3cbe-47ed-bf25-9abc8dac9644"), new Guid("c81388cf-be3e-42fc-b791-02eccce16095"), false },
                    { new Guid("56618227-52a4-4a21-9819-b3f48fb68bbf"), new Guid("b6ea83b1-3cbe-47ed-bf25-9abc8dac9644"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847"), false },
                    { new Guid("caa965c1-d725-4ed2-929b-1827c12e707b"), new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"), false },
                    { new Guid("5d5adec0-00e7-4b6e-b0b4-05851cdda9d8"), new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"), new Guid("1d6d6442-25ac-4cf9-aa44-0d91622a4927"), false },
                    { new Guid("b4627729-5bb8-4f5c-8f6b-7cb0e06ad406"), new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef"), false },
                    { new Guid("ccf8b96e-1297-4839-8d41-c35a117dc8e6"), new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be"), false },
                    { new Guid("2fe358a7-f2d4-4d48-97b2-e58764539830"), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("cd285ce6-b6e9-4933-8e07-6d50e0251788"), false },
                    { new Guid("5d58bff5-14d1-403f-b6c4-47779dcc8622"), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("d454ae67-913e-4f7b-82c1-4da539737199"), false },
                    { new Guid("958e6e3b-4ddf-4f09-9a3a-1d6fa746ecaa"), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("18537eea-e8f8-49cd-8ddf-10646c9a9f21"), false },
                    { new Guid("9a38e1d1-b5ad-4959-940b-a8c212f04996"), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("d789a80a-493f-4248-90b0-35edab1c3c63"), false },
                    { new Guid("2020db60-310d-42ec-b6d3-107942583f6f"), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("71ad22cf-168a-40be-9867-4a7ebe34c339"), false },
                    { new Guid("87ce555e-9ae7-4127-8fa5-af423df9dc9a"), new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"), false },
                    { new Guid("1634d34b-0e45-4731-a019-28b0582bd6a5"), new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), new Guid("1d6d6442-25ac-4cf9-aa44-0d91622a4927"), false },
                    { new Guid("128c6749-994a-44e6-b445-f6a3e26d2a03"), new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef"), false },
                    { new Guid("c151f1a8-26e5-4b31-9234-fd8405374235"), new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be"), false },
                    { new Guid("3be3ad44-211b-4206-8fde-353ec7b8e526"), new Guid("33e5e889-f44b-461a-83c9-680f34f82e06"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"), false },
                    { new Guid("27c6ba1f-0170-4fbf-ad91-32f022975cbf"), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("c81388cf-be3e-42fc-b791-02eccce16095"), false },
                    { new Guid("0500016d-c3c1-41dd-9013-f450cf5e8110"), new Guid("33e5e889-f44b-461a-83c9-680f34f82e06"), new Guid("1d6d6442-25ac-4cf9-aa44-0d91622a4927"), false },
                    { new Guid("ea0efdaa-d579-44ec-a223-38f70bc2f570"), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("df4b1285-b216-488c-88b7-0de75528f2fc"), false },
                    { new Guid("8b2947e3-f56f-4035-afcb-e04fd497063a"), new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be"), false },
                    { new Guid("317760ab-9fcd-42ec-85de-0631d3b7356c"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("df4b1285-b216-488c-88b7-0de75528f2fc"), false },
                    { new Guid("aaf78762-1576-4f60-8593-b0de7303a1c9"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("c81388cf-be3e-42fc-b791-02eccce16095"), false },
                    { new Guid("bc2007e6-51b6-49b0-992f-fdc977d3ae1c"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847"), false },
                    { new Guid("36e9af62-c6b2-439f-b850-ee7084cfde42"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"), false },
                    { new Guid("2ba62cf5-85a5-415c-913e-f313fb0c143c"), new Guid("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"), new Guid("c81388cf-be3e-42fc-b791-02eccce16095"), false },
                    { new Guid("a8926c2a-01da-49e9-955b-5ab53fbfa7fe"), new Guid("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847"), false },
                    { new Guid("338557b1-43f2-4ab4-9a8c-1bd30cc5712d"), new Guid("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"), false },
                    { new Guid("9d97bf6e-e986-45f5-ba6b-38a6e50ba6ec"), new Guid("6d1ac165-5488-4f86-84ad-47301d813802"), new Guid("c81388cf-be3e-42fc-b791-02eccce16095"), false },
                    { new Guid("ea2534cd-0a2b-4eb6-b3bd-2d752b2903b1"), new Guid("6d1ac165-5488-4f86-84ad-47301d813802"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847"), false },
                    { new Guid("eb789035-af8e-41f4-a8b4-c63839ea76b4"), new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"), false },
                    { new Guid("25748fa5-1d69-441f-9deb-2f927c5e95f8"), new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), new Guid("1d6d6442-25ac-4cf9-aa44-0d91622a4927"), false },
                    { new Guid("06d98284-629c-493d-aa06-46a5008e1696"), new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef"), false },
                    { new Guid("42acb95f-d02c-4c62-b3c3-c3c7dc59b192"), new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be"), false },
                    { new Guid("974c2553-a3ab-4a63-bdaf-1018630f8f59"), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("cd285ce6-b6e9-4933-8e07-6d50e0251788"), false },
                    { new Guid("f1c7d2f6-e23e-4398-a1db-794d398c0ea3"), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("d454ae67-913e-4f7b-82c1-4da539737199"), false },
                    { new Guid("e373f311-ffc4-4a88-8360-1762d638930b"), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("18537eea-e8f8-49cd-8ddf-10646c9a9f21"), false },
                    { new Guid("4225bbeb-5321-4ed0-9163-cb2ebea3eb67"), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("d789a80a-493f-4248-90b0-35edab1c3c63"), false },
                    { new Guid("df494a48-38ac-4c53-8df6-7bc26aa760ff"), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("71ad22cf-168a-40be-9867-4a7ebe34c339"), false },
                    { new Guid("875c1b24-8287-40f2-9686-421544795564"), new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"), false },
                    { new Guid("cf67c200-e5f1-4017-937d-cd3cb2ac7a78"), new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), new Guid("1d6d6442-25ac-4cf9-aa44-0d91622a4927"), false },
                    { new Guid("43fe0843-3607-4f14-af03-6195bf5f8760"), new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef"), false },
                    { new Guid("2e372dbe-2927-4767-a2bb-3558069beb0e"), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61"), false },
                    { new Guid("75ecc207-2d6d-4abb-86ba-f26c6129d93c"), new Guid("33e5e889-f44b-461a-83c9-680f34f82e06"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef"), false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("0500016d-c3c1-41dd-9013-f450cf5e8110"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("06d98284-629c-493d-aa06-46a5008e1696"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("128c6749-994a-44e6-b445-f6a3e26d2a03"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("1634d34b-0e45-4731-a019-28b0582bd6a5"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("1ba784f8-e0ef-491d-a451-e3ffc06dbf3e"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("2020db60-310d-42ec-b6d3-107942583f6f"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("25748fa5-1d69-441f-9deb-2f927c5e95f8"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("27c6ba1f-0170-4fbf-ad91-32f022975cbf"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("2ba62cf5-85a5-415c-913e-f313fb0c143c"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("2e372dbe-2927-4767-a2bb-3558069beb0e"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("2fe358a7-f2d4-4d48-97b2-e58764539830"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("317760ab-9fcd-42ec-85de-0631d3b7356c"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("338557b1-43f2-4ab4-9a8c-1bd30cc5712d"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("36e9af62-c6b2-439f-b850-ee7084cfde42"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("3be3ad44-211b-4206-8fde-353ec7b8e526"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("4225bbeb-5321-4ed0-9163-cb2ebea3eb67"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("42acb95f-d02c-4c62-b3c3-c3c7dc59b192"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("43fe0843-3607-4f14-af03-6195bf5f8760"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("56618227-52a4-4a21-9819-b3f48fb68bbf"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("56e347ac-7a53-4080-95d7-b4e87ecdefb2"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("5d58bff5-14d1-403f-b6c4-47779dcc8622"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("5d5adec0-00e7-4b6e-b0b4-05851cdda9d8"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("73d29d97-35c5-4194-9683-2df0a0b6b390"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("75ecc207-2d6d-4abb-86ba-f26c6129d93c"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("875c1b24-8287-40f2-9686-421544795564"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("87ce555e-9ae7-4127-8fa5-af423df9dc9a"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("8b2947e3-f56f-4035-afcb-e04fd497063a"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("919cfb2c-580a-4079-90f5-f3ae967b672b"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("958e6e3b-4ddf-4f09-9a3a-1d6fa746ecaa"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("974c2553-a3ab-4a63-bdaf-1018630f8f59"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("9a38e1d1-b5ad-4959-940b-a8c212f04996"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("9d97bf6e-e986-45f5-ba6b-38a6e50ba6ec"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("a8926c2a-01da-49e9-955b-5ab53fbfa7fe"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("aaf78762-1576-4f60-8593-b0de7303a1c9"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("b4627729-5bb8-4f5c-8f6b-7cb0e06ad406"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("bc2007e6-51b6-49b0-992f-fdc977d3ae1c"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("beb19f42-63b1-45ec-bf80-a8a7cd381a65"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("c151f1a8-26e5-4b31-9234-fd8405374235"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("caa965c1-d725-4ed2-929b-1827c12e707b"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("ccf8b96e-1297-4839-8d41-c35a117dc8e6"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("cf67c200-e5f1-4017-937d-cd3cb2ac7a78"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("d06219da-1725-494a-9f7c-2653d9087ab6"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("df494a48-38ac-4c53-8df6-7bc26aa760ff"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("e373f311-ffc4-4a88-8360-1762d638930b"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("ea0efdaa-d579-44ec-a223-38f70bc2f570"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("ea2534cd-0a2b-4eb6-b3bd-2d752b2903b1"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("eb789035-af8e-41f4-a8b4-c63839ea76b4"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("f1c7d2f6-e23e-4398-a1db-794d398c0ea3"));

            migrationBuilder.DeleteData(
                table: "Candidate",
                keyColumn: "Id",
                keyValue: new Guid("fba05cfd-4d29-4c03-ad68-c58b094d5e87"));

            migrationBuilder.InsertData(
                table: "Candidate",
                columns: new[] { "Id", "ElectionId", "VoterBeingId", "isNeutralOpinion" },
                values: new object[,]
                {
                    { new Guid("8784eb52-3c3f-427b-b325-958f854dbfbb"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("0d925194-fee5-4750-a53c-b36a47afeeab"), false },
                    { new Guid("239a05cc-ad00-43f6-ae75-54c58ccb9786"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61"), false },
                    { new Guid("bf04bf97-e955-4692-b211-3363b802e6a9"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("df4b1285-b216-488c-88b7-0de75528f2fc"), false },
                    { new Guid("5f274499-e11a-4955-a044-0b2a8b106e57"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("c81388cf-be3e-42fc-b791-02eccce16095"), false },
                    { new Guid("5fa3828d-5ab3-44a3-9c9b-8826de4c4dca"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847"), false },
                    { new Guid("4ba8ee63-f5f4-41bc-b4ed-f4ba9d11431d"), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9"), false }
                });
        }
    }
}
