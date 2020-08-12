using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class seedInitialVoteData3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("7c6fadf7-089b-4836-a849-1a81f2c0b84f"));

            migrationBuilder.InsertData(
                table: "Vote",
                columns: new[] { "Id", "CandidateId", "Datetime", "ElectionId", "VoterId" },
                values: new object[,]
                {
                    { new Guid("36ee9f08-6a2e-46ee-87a0-5c41eada7e1e"), new Guid("8bc92480-c3d1-440d-86c4-a6a4ed89255a"), new DateTime(2011, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("783118b1-111d-4c6e-b75e-dcd721ccfc2c") },
                    { new Guid("f703c6d5-df3d-4685-9e9c-30a3b12251d7"), new Guid("08828a2a-b8aa-4c7e-be3a-7961b8e40daf"), new DateTime(2021, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be") },
                    { new Guid("e41222a9-0379-4043-b16b-bf7c6fa22f92"), new Guid("5d1858eb-06f1-40a4-ad71-04f5f2ec37bf"), new DateTime(2021, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("0642267b-8df3-405d-b596-e50c2cdeefde") },
                    { new Guid("eab4cbf7-5a40-4705-a44f-6b2c66ef3e6f"), new Guid("3a884c97-5e90-4e3d-87c0-5d96745e54a9"), new DateTime(2020, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"), new Guid("d789a80a-493f-4248-90b0-35edab1c3c63") },
                    { new Guid("ea724645-1765-4184-9abf-e6fc092111b6"), new Guid("ba5a0188-6983-40d9-9364-9113509e97ee"), new DateTime(2019, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b6ea83b1-3cbe-47ed-bf25-9abc8dac9644"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef") },
                    { new Guid("5456d7e8-31e0-478d-96e3-19ee84db18a1"), new Guid("6c44ea03-afa1-4546-94fa-0cf086293fc2"), new DateTime(2018, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef") },
                    { new Guid("e3778148-e2b9-4c4c-a8a7-0a1ccbb7581d"), new Guid("c0e68551-3e5e-4268-89e9-b6310de3956b"), new DateTime(2018, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be") },
                    { new Guid("9e54fa6d-dd10-4f8b-9b2d-e116afe7ae1e"), new Guid("910fb488-8da3-4183-a92a-17d046d59553"), new DateTime(2017, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847") },
                    { new Guid("1db62365-1956-464f-93da-17be78eb72b6"), new Guid("a49e5a59-c2b5-451f-a620-425adec7c44d"), new DateTime(2017, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847") },
                    { new Guid("dfb69202-05e0-4500-8654-bdfba9115213"), new Guid("a5452366-7898-47cd-a5bb-0e4a95ed319b"), new DateTime(2016, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), new Guid("d454ae67-913e-4f7b-82c1-4da539737199") },
                    { new Guid("f91effc8-32f9-4df8-af49-532e6a3fac6c"), new Guid("2548f0bc-8005-42cc-a938-dd9c19b29216"), new DateTime(2022, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be") },
                    { new Guid("e911bd84-df40-4cb6-abf2-7ad2be76a7ba"), new Guid("950a79f2-bed6-40e1-8cb6-6d89ab553188"), new DateTime(2016, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), new Guid("0642267b-8df3-405d-b596-e50c2cdeefde") },
                    { new Guid("23eb7fa2-767b-4db5-9b97-567614641e6e"), new Guid("473f81e3-aa8b-457a-9d83-cffb1d277dec"), new DateTime(2015, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be") },
                    { new Guid("d44c1d90-2f6b-4bfc-97f9-cce6b627d0f6"), new Guid("575d00a6-b8e1-4b71-ba9e-4d26490d4d84"), new DateTime(2015, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("c81388cf-be3e-42fc-b791-02eccce16095") },
                    { new Guid("cdf05f25-4010-44a4-9ce3-1f9e3ed5e9fb"), new Guid("556e2593-197d-4c40-a1a7-159917816196"), new DateTime(2014, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), new Guid("cd285ce6-b6e9-4933-8e07-6d50e0251788") },
                    { new Guid("71a4aaad-57cf-430d-9b63-afaf0fced0ec"), new Guid("e194ac0c-a0d0-48ec-a3a6-25b9f6a07587"), new DateTime(2014, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847") },
                    { new Guid("30f074fd-87e4-4b85-af55-f0f833f3f90f"), new Guid("69da0af1-b243-439b-a227-95cec2ed4350"), new DateTime(2013, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6d1ac165-5488-4f86-84ad-47301d813802"), new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61") },
                    { new Guid("eccbaa29-04b0-43f6-a2d2-cbb49a5b72e9"), new Guid("43b303b1-630e-4dd6-811b-63f88c06e60d"), new DateTime(2012, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"), new Guid("1d6d6442-25ac-4cf9-aa44-0d91622a4927") },
                    { new Guid("006699e1-f7d3-4c31-9fba-1e800c9df7ce"), new Guid("0f2088c2-06d8-4913-9986-c44473b50f2b"), new DateTime(2011, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61") },
                    { new Guid("5247a247-0bcd-4a42-977d-bcc0e5fa4980"), new Guid("b58d1bf6-a210-40e1-a745-6bade04019a7"), new DateTime(2011, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("c81388cf-be3e-42fc-b791-02eccce16095") },
                    { new Guid("15cf520b-b01b-4587-ba67-2c418c19fc91"), new Guid("87945fcb-6b98-4a17-99d6-0611606fb203"), new DateTime(2011, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("c81388cf-be3e-42fc-b791-02eccce16095") },
                    { new Guid("945073a8-900c-4f7b-b0ba-e090833d170c"), new Guid("62055ca5-e962-4fff-b32d-48d7c71f9f23"), new DateTime(2015, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("df4b1285-b216-488c-88b7-0de75528f2fc") },
                    { new Guid("df0a5fda-6d72-41d2-aa3d-84585e34a206"), new Guid("224cdcc9-ae8e-4668-b88a-ceb9e3927594"), new DateTime(2023, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33e5e889-f44b-461a-83c9-680f34f82e06"), new Guid("0d925194-fee5-4750-a53c-b36a47afeeab") },
                    { new Guid("98745388-580d-4ad9-b86b-cdc233bb36ee"), new Guid("b2b4e50f-ad7d-4736-8d9d-5067b7a4f173"), new DateTime(2022, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), new Guid("783118b1-111d-4c6e-b75e-dcd721ccfc2c") },
                    { new Guid("c8c4cbcd-6e14-49fb-bc06-a0d36e0138d6"), new Guid("87945fcb-6b98-4a17-99d6-0611606fb203"), new DateTime(2011, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9") },
                    { new Guid("96ca05c2-ff5a-4cc8-991f-8ee3ec63d5b5"), new Guid("2548f0bc-8005-42cc-a938-dd9c19b29216"), new DateTime(2022, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61") },
                    { new Guid("bc8ec1c0-61ae-4b05-9b10-d634846d340a"), new Guid("08828a2a-b8aa-4c7e-be3a-7961b8e40daf"), new DateTime(2021, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("71ad22cf-168a-40be-9867-4a7ebe34c339") },
                    { new Guid("5a85227c-49fc-4788-a4aa-6f54a11d20be"), new Guid("5d1858eb-06f1-40a4-ad71-04f5f2ec37bf"), new DateTime(2021, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("df4b1285-b216-488c-88b7-0de75528f2fc") },
                    { new Guid("6c55a82c-72f2-4edf-a3a3-2c4c7579ab1d"), new Guid("3a884c97-5e90-4e3d-87c0-5d96745e54a9"), new DateTime(2020, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"), new Guid("783118b1-111d-4c6e-b75e-dcd721ccfc2c") },
                    { new Guid("bfa10354-a4aa-4938-a4b0-d665b5819f5e"), new Guid("ba5a0188-6983-40d9-9364-9113509e97ee"), new DateTime(2019, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b6ea83b1-3cbe-47ed-bf25-9abc8dac9644"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef") },
                    { new Guid("98d446d2-d5d9-406a-afc9-8e0dcbe700fc"), new Guid("6c44ea03-afa1-4546-94fa-0cf086293fc2"), new DateTime(2018, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"), new Guid("d454ae67-913e-4f7b-82c1-4da539737199") },
                    { new Guid("14a79102-e54f-435d-90eb-8035c2d799ce"), new Guid("c0e68551-3e5e-4268-89e9-b6310de3956b"), new DateTime(2018, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef") },
                    { new Guid("a5f8d5b7-7909-46f8-8cdc-f560da208636"), new Guid("910fb488-8da3-4183-a92a-17d046d59553"), new DateTime(2017, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("d789a80a-493f-4248-90b0-35edab1c3c63") },
                    { new Guid("ce388d9b-ec9c-4dda-9a2b-a4ccea22ae24"), new Guid("a49e5a59-c2b5-451f-a620-425adec7c44d"), new DateTime(2017, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be") },
                    { new Guid("38658e94-887c-4204-a1fd-748f2f350316"), new Guid("be668d55-1706-4f8b-98eb-c0af8a7341fe"), new DateTime(2023, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33e5e889-f44b-461a-83c9-680f34f82e06"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be") },
                    { new Guid("0dae9b5b-5400-4876-94bc-c5852e003fd6"), new Guid("a5452366-7898-47cd-a5bb-0e4a95ed319b"), new DateTime(2016, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61") },
                    { new Guid("be260190-421a-41b7-8bb4-11da2893c7cb"), new Guid("62055ca5-e962-4fff-b32d-48d7c71f9f23"), new DateTime(2015, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef") },
                    { new Guid("7e42dd44-725d-48db-94e9-1a37bbe035ae"), new Guid("473f81e3-aa8b-457a-9d83-cffb1d277dec"), new DateTime(2015, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61") },
                    { new Guid("a8a776d8-cb34-48cf-ace6-468add23bef8"), new Guid("575d00a6-b8e1-4b71-ba9e-4d26490d4d84"), new DateTime(2015, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef") },
                    { new Guid("0f5c9d34-7139-4ca6-b313-b3ac327991cd"), new Guid("556e2593-197d-4c40-a1a7-159917816196"), new DateTime(2014, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be") },
                    { new Guid("44b051c8-16d4-49cf-b4f7-d633a35e7a09"), new Guid("e194ac0c-a0d0-48ec-a3a6-25b9f6a07587"), new DateTime(2014, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847") },
                    { new Guid("eba857fa-ce45-4de9-a248-75c4a06937ea"), new Guid("69da0af1-b243-439b-a227-95cec2ed4350"), new DateTime(2013, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6d1ac165-5488-4f86-84ad-47301d813802"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef") },
                    { new Guid("8311ab82-b877-48cd-86ce-b1c36d8cfcd7"), new Guid("43b303b1-630e-4dd6-811b-63f88c06e60d"), new DateTime(2012, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"), new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61") },
                    { new Guid("4d530b58-cc2f-4dc3-b6ae-3a74f1566798"), new Guid("0f2088c2-06d8-4913-9986-c44473b50f2b"), new DateTime(2011, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("df4b1285-b216-488c-88b7-0de75528f2fc") },
                    { new Guid("3819ecca-827b-4553-a198-660a528eedf8"), new Guid("b58d1bf6-a210-40e1-a745-6bade04019a7"), new DateTime(2011, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("71ad22cf-168a-40be-9867-4a7ebe34c339") },
                    { new Guid("0f67792f-0b6d-40b3-9bab-37db57ea9821"), new Guid("950a79f2-bed6-40e1-8cb6-6d89ab553188"), new DateTime(2016, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847") },
                    { new Guid("02029116-35e6-4860-897b-abf35aade507"), new Guid("b2b4e50f-ad7d-4736-8d9d-5067b7a4f173"), new DateTime(2022, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef") },
                    { new Guid("25b1721e-a7c2-4e56-aa8d-c747c81897e9"), new Guid("15ca3802-01d6-4f7d-beef-fb99a6485606"), new DateTime(2023, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33e5e889-f44b-461a-83c9-680f34f82e06"), new Guid("cd285ce6-b6e9-4933-8e07-6d50e0251788") },
                    { new Guid("656377ff-1e3c-4753-8c6a-35bd48a6a799"), new Guid("e3bdc2bb-b5d0-4b0b-b187-3e43454f5f34"), new DateTime(2022, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef") },
                    { new Guid("0a0a0c7e-a626-4e67-a3ae-09def01656fb"), new Guid("950a79f2-bed6-40e1-8cb6-6d89ab553188"), new DateTime(2016, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), new Guid("c81388cf-be3e-42fc-b791-02eccce16095") },
                    { new Guid("9cc35050-e8be-407c-b659-2c31618a933c"), new Guid("1590c51e-7122-48e3-bcd1-12a43e170ede"), new DateTime(2016, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), new Guid("d454ae67-913e-4f7b-82c1-4da539737199") },
                    { new Guid("bd42eff3-3764-4258-9ff0-6885528d759e"), new Guid("701bd96b-02e4-47af-92f4-d1e49951a2dc"), new DateTime(2015, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("0642267b-8df3-405d-b596-e50c2cdeefde") },
                    { new Guid("bf00dbe4-cb16-4e78-bdd1-f772bf3ab5c0"), new Guid("62055ca5-e962-4fff-b32d-48d7c71f9f23"), new DateTime(2015, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("71ad22cf-168a-40be-9867-4a7ebe34c339") },
                    { new Guid("ff30a50c-4d16-4285-92f0-6d354e2c46e3"), new Guid("473f81e3-aa8b-457a-9d83-cffb1d277dec"), new DateTime(2015, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("df4b1285-b216-488c-88b7-0de75528f2fc") },
                    { new Guid("a90dc979-1564-4afc-957b-7b52808b13b2"), new Guid("575d00a6-b8e1-4b71-ba9e-4d26490d4d84"), new DateTime(2015, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("cd285ce6-b6e9-4933-8e07-6d50e0251788") },
                    { new Guid("0e677ef4-877a-4e1b-8b3d-c4161ad59573"), new Guid("ea0eefce-3b8e-4a33-9af9-524ba5c678ee"), new DateTime(2015, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("40556dbd-b5ae-47af-89eb-32deee130dd9"), new Guid("cd285ce6-b6e9-4933-8e07-6d50e0251788") },
                    { new Guid("fe0bba6d-5313-4ef4-a82f-a03bce1490b6"), new Guid("2bdb3acd-e9b7-40a0-b2ea-d958f59c3b6c"), new DateTime(2014, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), new Guid("df4b1285-b216-488c-88b7-0de75528f2fc") },
                    { new Guid("5d5ac6a8-f532-450e-bcdf-1033e509f6af"), new Guid("556e2593-197d-4c40-a1a7-159917816196"), new DateTime(2014, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), new Guid("d454ae67-913e-4f7b-82c1-4da539737199") },
                    { new Guid("b5c9ec2c-7454-4ed1-8337-af2e253914fa"), new Guid("a5452366-7898-47cd-a5bb-0e4a95ed319b"), new DateTime(2016, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), new Guid("df4b1285-b216-488c-88b7-0de75528f2fc") },
                    { new Guid("818ffa39-73db-46ef-94fd-ef4a7398a0b0"), new Guid("e194ac0c-a0d0-48ec-a3a6-25b9f6a07587"), new DateTime(2014, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), new Guid("71ad22cf-168a-40be-9867-4a7ebe34c339") },
                    { new Guid("55ee050f-4d87-4767-a4ef-2a35e3eb48c0"), new Guid("69da0af1-b243-439b-a227-95cec2ed4350"), new DateTime(2013, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6d1ac165-5488-4f86-84ad-47301d813802"), new Guid("d454ae67-913e-4f7b-82c1-4da539737199") },
                    { new Guid("43a2f571-3b74-48b6-a7cf-dccb1d5c4d73"), new Guid("cb6e54e2-77a0-4f31-96f4-4ef31fee2d24"), new DateTime(2013, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6d1ac165-5488-4f86-84ad-47301d813802"), new Guid("cd285ce6-b6e9-4933-8e07-6d50e0251788") },
                    { new Guid("5280803f-064c-4521-9090-602391203c36"), new Guid("470ac0b5-151c-43a2-bbd8-af80f38d9435"), new DateTime(2012, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"), new Guid("d789a80a-493f-4248-90b0-35edab1c3c63") },
                    { new Guid("cb0d8440-09a3-4b0b-9d9a-a83c904d28db"), new Guid("43b303b1-630e-4dd6-811b-63f88c06e60d"), new DateTime(2012, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"), new Guid("1d6d6442-25ac-4cf9-aa44-0d91622a4927") },
                    { new Guid("3a4890fc-b4b8-48c8-817b-032e40733cc7"), new Guid("2c9b8405-39bc-4bc8-b539-a473122eb007"), new DateTime(2012, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef") },
                    { new Guid("b3366d5b-81b0-4642-8b18-a1f02edd1bb0"), new Guid("0f2088c2-06d8-4913-9986-c44473b50f2b"), new DateTime(2011, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847") },
                    { new Guid("39320365-ffb5-4b7f-9cf2-636792171d50"), new Guid("b58d1bf6-a210-40e1-a745-6bade04019a7"), new DateTime(2011, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("df4b1285-b216-488c-88b7-0de75528f2fc") },
                    { new Guid("4f14d76a-e8f4-424f-b644-628ad7ea4789"), new Guid("87945fcb-6b98-4a17-99d6-0611606fb203"), new DateTime(2011, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61") },
                    { new Guid("14c2a76e-8ae3-41e7-a6f3-063378d7f48d"), new Guid("0d201c31-b90f-4721-98ce-03f7806a1d2d"), new DateTime(2011, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9") },
                    { new Guid("456395da-1f42-4805-8956-22b3415ef3bf"), new Guid("c877feb9-c8a2-42de-80c0-196410a60196"), new DateTime(2014, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("76daa454-e061-46ac-ba1e-4c09fdcd418e"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be") },
                    { new Guid("a84e9896-61af-4d10-95a0-35fab153e3ea"), new Guid("be668d55-1706-4f8b-98eb-c0af8a7341fe"), new DateTime(2023, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33e5e889-f44b-461a-83c9-680f34f82e06"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847") },
                    { new Guid("88a5aa50-35a5-46b1-87b6-938c37743f20"), new Guid("fda62639-faf3-4178-90fe-7da3484c48af"), new DateTime(2016, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), new Guid("783118b1-111d-4c6e-b75e-dcd721ccfc2c") },
                    { new Guid("003f2db0-9ea2-441e-a210-1ebf8624d119"), new Guid("a49e5a59-c2b5-451f-a620-425adec7c44d"), new DateTime(2017, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("4ff2b64f-17fe-4621-9310-eeb59a9af847") },
                    { new Guid("5ef28023-87d9-42c3-a516-b497fb6b7d54"), new Guid("b2b4e50f-ad7d-4736-8d9d-5067b7a4f173"), new DateTime(2022, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), new Guid("18537eea-e8f8-49cd-8ddf-10646c9a9f21") },
                    { new Guid("41020320-e8c3-4b72-9623-b9fbac10ed0d"), new Guid("2548f0bc-8005-42cc-a938-dd9c19b29216"), new DateTime(2022, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), new Guid("18537eea-e8f8-49cd-8ddf-10646c9a9f21") },
                    { new Guid("04b06cd3-e4c7-4458-8c7f-b0c3c9bb5b38"), new Guid("73da9cde-8eed-4743-b369-8cb2280e4278"), new DateTime(2022, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b798676d-750e-4950-a527-5ccbc17004a4"), new Guid("783118b1-111d-4c6e-b75e-dcd721ccfc2c") },
                    { new Guid("407a9cbc-9645-4114-abbc-17ea795167fa"), new Guid("83c9cd1b-4212-426b-985f-1770676c0be9"), new DateTime(2021, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("18537eea-e8f8-49cd-8ddf-10646c9a9f21") },
                    { new Guid("e46c62ec-3851-4d58-8319-ddf5c3b9aeec"), new Guid("0668f926-7d1f-4f3d-b0a0-3dbc90357eb7"), new DateTime(2021, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("d789a80a-493f-4248-90b0-35edab1c3c63") },
                    { new Guid("2331dece-90f6-4bd7-9906-d89020500426"), new Guid("08828a2a-b8aa-4c7e-be3a-7961b8e40daf"), new DateTime(2021, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("783118b1-111d-4c6e-b75e-dcd721ccfc2c") },
                    { new Guid("cb50c637-6fea-4dba-ab71-fc5188529b40"), new Guid("5d1858eb-06f1-40a4-ad71-04f5f2ec37bf"), new DateTime(2021, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("0d925194-fee5-4750-a53c-b36a47afeeab") },
                    { new Guid("fce56560-9e1d-48a5-b187-371ac09c2474"), new Guid("dd50e713-110f-4ea5-91a2-9144d95cc1fa"), new DateTime(2021, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbcd22cb-dc43-4ea2-854c-acb61564699c"), new Guid("d454ae67-913e-4f7b-82c1-4da539737199") },
                    { new Guid("68cf5aa5-e4d1-4145-830f-b7e18e8e3431"), new Guid("ea67fb4e-329b-4340-9a71-2fb944461ca5"), new DateTime(2020, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"), new Guid("62ae0555-718d-4623-a12a-7ae4a2e26aef") },
                    { new Guid("e7550fda-85a5-40ae-9b5f-f92a0a0e1251"), new Guid("7f2811bc-48df-478d-83ad-649a7e2a8195"), new DateTime(2017, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("cd285ce6-b6e9-4933-8e07-6d50e0251788") },
                    { new Guid("b888eead-142f-4bae-a112-d01309834c92"), new Guid("0bc40cad-00fe-4ca8-a789-91b59f0c5a2f"), new DateTime(2020, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"), new Guid("1d6d6442-25ac-4cf9-aa44-0d91622a4927") },
                    { new Guid("264d9788-70f3-491f-9060-d47266f0f87b"), new Guid("e139e024-45d8-4e24-83fb-1b394bd458cb"), new DateTime(2020, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"), new Guid("71ad22cf-168a-40be-9867-4a7ebe34c339") },
                    { new Guid("ed596e4f-483f-4aac-b64b-73ff72d70ed3"), new Guid("ba5a0188-6983-40d9-9364-9113509e97ee"), new DateTime(2019, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b6ea83b1-3cbe-47ed-bf25-9abc8dac9644"), new Guid("0642267b-8df3-405d-b596-e50c2cdeefde") },
                    { new Guid("6ddc6066-a16c-4bc1-98c8-1731ce829c84"), new Guid("80cd8f07-8c6e-4279-b62e-9883aedf0d56"), new DateTime(2019, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b6ea83b1-3cbe-47ed-bf25-9abc8dac9644"), new Guid("d454ae67-913e-4f7b-82c1-4da539737199") },
                    { new Guid("c808d0f3-bb45-4f47-9301-b57daefff788"), new Guid("6c44ea03-afa1-4546-94fa-0cf086293fc2"), new DateTime(2018, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"), new Guid("0d925194-fee5-4750-a53c-b36a47afeeab") },
                    { new Guid("9597de35-91e3-4687-8462-ddbf771fbc1c"), new Guid("c0e68551-3e5e-4268-89e9-b6310de3956b"), new DateTime(2018, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"), new Guid("1d6d6442-25ac-4cf9-aa44-0d91622a4927") },
                    { new Guid("245a1edc-a691-437f-ba17-e9bece210180"), new Guid("342a2bbc-d9ec-41d8-b45d-91ffa59975b2"), new DateTime(2018, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f8899970-5057-4a23-833c-dc75ee84c8d2"), new Guid("cd285ce6-b6e9-4933-8e07-6d50e0251788") },
                    { new Guid("3e0a8ac7-ea3a-4fa9-a4c9-6645e90fe39d"), new Guid("943633be-4f99-44d1-b9cf-04434f03fc6d"), new DateTime(2017, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("18537eea-e8f8-49cd-8ddf-10646c9a9f21") },
                    { new Guid("e538cd70-c0a0-4d99-8c41-387c9eb85ead"), new Guid("85c45467-32c4-4dae-8023-06df1df955b4"), new DateTime(2017, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be") },
                    { new Guid("a2fb9c59-9a62-40b0-ab87-d82a323d831c"), new Guid("910fb488-8da3-4183-a92a-17d046d59553"), new DateTime(2017, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), new Guid("18537eea-e8f8-49cd-8ddf-10646c9a9f21") },
                    { new Guid("2274d833-785e-49d7-bb13-0df3aa309f1b"), new Guid("3a884c97-5e90-4e3d-87c0-5d96745e54a9"), new DateTime(2020, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"), new Guid("8dd6ebe1-e2e4-4555-ac61-9887cebebf61") },
                    { new Guid("d6c9a4ff-e41d-41e6-b0e6-4532f87fd557"), new Guid("be668d55-1706-4f8b-98eb-c0af8a7341fe"), new DateTime(2023, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33e5e889-f44b-461a-83c9-680f34f82e06"), new Guid("6c9de511-db59-4be8-9315-d58700ce10be") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("003f2db0-9ea2-441e-a210-1ebf8624d119"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("006699e1-f7d3-4c31-9fba-1e800c9df7ce"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("02029116-35e6-4860-897b-abf35aade507"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("04b06cd3-e4c7-4458-8c7f-b0c3c9bb5b38"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("0a0a0c7e-a626-4e67-a3ae-09def01656fb"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("0dae9b5b-5400-4876-94bc-c5852e003fd6"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("0e677ef4-877a-4e1b-8b3d-c4161ad59573"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("0f5c9d34-7139-4ca6-b313-b3ac327991cd"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("0f67792f-0b6d-40b3-9bab-37db57ea9821"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("14a79102-e54f-435d-90eb-8035c2d799ce"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("14c2a76e-8ae3-41e7-a6f3-063378d7f48d"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("15cf520b-b01b-4587-ba67-2c418c19fc91"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("1db62365-1956-464f-93da-17be78eb72b6"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("2274d833-785e-49d7-bb13-0df3aa309f1b"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("2331dece-90f6-4bd7-9906-d89020500426"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("23eb7fa2-767b-4db5-9b97-567614641e6e"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("245a1edc-a691-437f-ba17-e9bece210180"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("25b1721e-a7c2-4e56-aa8d-c747c81897e9"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("264d9788-70f3-491f-9060-d47266f0f87b"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("30f074fd-87e4-4b85-af55-f0f833f3f90f"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("36ee9f08-6a2e-46ee-87a0-5c41eada7e1e"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("3819ecca-827b-4553-a198-660a528eedf8"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("38658e94-887c-4204-a1fd-748f2f350316"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("39320365-ffb5-4b7f-9cf2-636792171d50"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("3a4890fc-b4b8-48c8-817b-032e40733cc7"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("3e0a8ac7-ea3a-4fa9-a4c9-6645e90fe39d"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("407a9cbc-9645-4114-abbc-17ea795167fa"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("41020320-e8c3-4b72-9623-b9fbac10ed0d"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("43a2f571-3b74-48b6-a7cf-dccb1d5c4d73"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("44b051c8-16d4-49cf-b4f7-d633a35e7a09"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("456395da-1f42-4805-8956-22b3415ef3bf"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("4d530b58-cc2f-4dc3-b6ae-3a74f1566798"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("4f14d76a-e8f4-424f-b644-628ad7ea4789"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("5247a247-0bcd-4a42-977d-bcc0e5fa4980"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("5280803f-064c-4521-9090-602391203c36"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("5456d7e8-31e0-478d-96e3-19ee84db18a1"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("55ee050f-4d87-4767-a4ef-2a35e3eb48c0"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("5a85227c-49fc-4788-a4aa-6f54a11d20be"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("5d5ac6a8-f532-450e-bcdf-1033e509f6af"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("5ef28023-87d9-42c3-a516-b497fb6b7d54"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("656377ff-1e3c-4753-8c6a-35bd48a6a799"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("68cf5aa5-e4d1-4145-830f-b7e18e8e3431"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("6c55a82c-72f2-4edf-a3a3-2c4c7579ab1d"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("6ddc6066-a16c-4bc1-98c8-1731ce829c84"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("71a4aaad-57cf-430d-9b63-afaf0fced0ec"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("7e42dd44-725d-48db-94e9-1a37bbe035ae"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("818ffa39-73db-46ef-94fd-ef4a7398a0b0"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("8311ab82-b877-48cd-86ce-b1c36d8cfcd7"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("88a5aa50-35a5-46b1-87b6-938c37743f20"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("945073a8-900c-4f7b-b0ba-e090833d170c"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("9597de35-91e3-4687-8462-ddbf771fbc1c"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("96ca05c2-ff5a-4cc8-991f-8ee3ec63d5b5"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("98745388-580d-4ad9-b86b-cdc233bb36ee"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("98d446d2-d5d9-406a-afc9-8e0dcbe700fc"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("9cc35050-e8be-407c-b659-2c31618a933c"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("9e54fa6d-dd10-4f8b-9b2d-e116afe7ae1e"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("a2fb9c59-9a62-40b0-ab87-d82a323d831c"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("a5f8d5b7-7909-46f8-8cdc-f560da208636"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("a84e9896-61af-4d10-95a0-35fab153e3ea"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("a8a776d8-cb34-48cf-ace6-468add23bef8"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("a90dc979-1564-4afc-957b-7b52808b13b2"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("b3366d5b-81b0-4642-8b18-a1f02edd1bb0"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("b5c9ec2c-7454-4ed1-8337-af2e253914fa"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("b888eead-142f-4bae-a112-d01309834c92"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("bc8ec1c0-61ae-4b05-9b10-d634846d340a"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("bd42eff3-3764-4258-9ff0-6885528d759e"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("be260190-421a-41b7-8bb4-11da2893c7cb"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("bf00dbe4-cb16-4e78-bdd1-f772bf3ab5c0"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("bfa10354-a4aa-4938-a4b0-d665b5819f5e"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("c808d0f3-bb45-4f47-9301-b57daefff788"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("c8c4cbcd-6e14-49fb-bc06-a0d36e0138d6"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("cb0d8440-09a3-4b0b-9d9a-a83c904d28db"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("cb50c637-6fea-4dba-ab71-fc5188529b40"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("cdf05f25-4010-44a4-9ce3-1f9e3ed5e9fb"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("ce388d9b-ec9c-4dda-9a2b-a4ccea22ae24"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("d44c1d90-2f6b-4bfc-97f9-cce6b627d0f6"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("d6c9a4ff-e41d-41e6-b0e6-4532f87fd557"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("df0a5fda-6d72-41d2-aa3d-84585e34a206"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("dfb69202-05e0-4500-8654-bdfba9115213"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("e3778148-e2b9-4c4c-a8a7-0a1ccbb7581d"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("e41222a9-0379-4043-b16b-bf7c6fa22f92"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("e46c62ec-3851-4d58-8319-ddf5c3b9aeec"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("e538cd70-c0a0-4d99-8c41-387c9eb85ead"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("e7550fda-85a5-40ae-9b5f-f92a0a0e1251"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("e911bd84-df40-4cb6-abf2-7ad2be76a7ba"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("ea724645-1765-4184-9abf-e6fc092111b6"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("eab4cbf7-5a40-4705-a44f-6b2c66ef3e6f"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("eba857fa-ce45-4de9-a248-75c4a06937ea"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("eccbaa29-04b0-43f6-a2d2-cbb49a5b72e9"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("ed596e4f-483f-4aac-b64b-73ff72d70ed3"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("f703c6d5-df3d-4685-9e9c-30a3b12251d7"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("f91effc8-32f9-4df8-af49-532e6a3fac6c"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("fce56560-9e1d-48a5-b187-371ac09c2474"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("fe0bba6d-5313-4ef4-a82f-a03bce1490b6"));

            migrationBuilder.DeleteData(
                table: "Vote",
                keyColumn: "Id",
                keyValue: new Guid("ff30a50c-4d16-4285-92f0-6d354e2c46e3"));

            migrationBuilder.InsertData(
                table: "Vote",
                columns: new[] { "Id", "CandidateId", "Datetime", "ElectionId", "VoterId" },
                values: new object[] { new Guid("7c6fadf7-089b-4836-a849-1a81f2c0b84f"), new Guid("8bc92480-c3d1-440d-86c4-a6a4ed89255a"), new DateTime(2012, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"), new Guid("e61f81ab-a991-4298-b668-c973a5b75dc9") });
        }
    }
}
