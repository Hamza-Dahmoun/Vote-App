using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Models
{
    public class VoteDBContext:DbContext
    {
        public VoteDBContext() : base()
        {

        }
        public VoteDBContext(DbContextOptions<VoteDBContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //I OVERRIDED THIS METHOD SO I CAN SEED MINIMUM DATA TO THE DATABASE FOR BETTER USER EXPERIENCE
            //FOR PEOPLE WHO GET THE SOURCE CODE OF THE APP AND ANT TO TRY IT

            //N.B: I used hard coded GUID instead of Guid.NewGuid() to avoid inserting new data everytime we run the app, in this way the rows are identified by the hard coded GUID

            //  seeding data to State table
            State Oran = new State { Id = Guid.Parse("fa9b72ee-dfcc-4353-b195-5c2855b1343f"), Name = "Oran" };
            State Meca = new State { Id = Guid.Parse("32065802-7f25-47bc-8987-dd4fdb2829c4"), Name = "Meca" };
            State Cairo = new State { Id = Guid.Parse("3138e047-e80f-44a1-ae1d-96804784f807"), Name = "Cairo" };
            State ElQuds = new State { Id = Guid.Parse("33f88529-1a04-4d5e-84dc-135646948bc0"), Name = "ElQuds" };
            State Algiers = new State { Id = Guid.Parse("918333ff-ac52-4424-a3d9-09bee4c39b91"), Name = "Algiers" };
            modelBuilder.Entity<State>().HasData(Oran);
            modelBuilder.Entity<State>().HasData(Meca);
            modelBuilder.Entity<State>().HasData(Cairo);
            modelBuilder.Entity<State>().HasData(ElQuds);
            modelBuilder.Entity<State>().HasData(Algiers);

            //  seeding data to Voter table
            Voter voter1 = new Voter { Id = Guid.Parse("0d925194-fee5-4750-a53c-b36a47afeeab"), FirstName = "Hamza", LastName = "Dahmoun", StateId = Oran.Id };
            Voter voter2 = new Voter { Id = Guid.Parse("8dd6ebe1-e2e4-4555-ac61-9887cebebf61"), FirstName = "Ikram", LastName = "Dahmoun", StateId = Oran.Id };
            Voter voter3 = new Voter { Id = Guid.Parse("df4b1285-b216-488c-88b7-0de75528f2fc"), FirstName = "Ahmed", LastName = "Mohamed", StateId = Oran.Id };
            Voter voter4 = new Voter { Id = Guid.Parse("c81388cf-be3e-42fc-b791-02eccce16095"), FirstName = "Mohamed", LastName = "Zitouni", StateId = Meca.Id };
            Voter voter5 = new Voter { Id = Guid.Parse("4ff2b64f-17fe-4621-9310-eeb59a9af847"), FirstName = "Yasser", LastName = "Zitouni", StateId = Meca.Id };
            Voter voter6 = new Voter { Id = Guid.Parse("e61f81ab-a991-4298-b668-c973a5b75dc9"), FirstName = "Larbi", LastName = "Fkaier", StateId = Meca.Id };
            Voter voter7 = new Voter { Id = Guid.Parse("1d6d6442-25ac-4cf9-aa44-0d91622a4927"), FirstName = "Djamel", LastName = "Tahraoui", StateId = Meca.Id };
            Voter voter8 = new Voter { Id = Guid.Parse("62ae0555-718d-4623-a12a-7ae4a2e26aef"), FirstName = "Sidahmed", LastName = "Dahmoun", StateId = Cairo.Id };
            Voter voter9 = new Voter { Id = Guid.Parse("6c9de511-db59-4be8-9315-d58700ce10be"), FirstName = "Bilal", LastName = "Dahmoun", StateId = Cairo.Id };
            Voter voter10 = new Voter { Id = Guid.Parse("cd285ce6-b6e9-4933-8e07-6d50e0251788"), FirstName = "Maria", LastName = "Hafsa", StateId = ElQuds.Id };
            Voter voter11 = new Voter { Id = Guid.Parse("d454ae67-913e-4f7b-82c1-4da539737199"), FirstName = "Brahim", LastName = "Roudjai", StateId = ElQuds.Id };
            Voter voter12 = new Voter { Id = Guid.Parse("18537eea-e8f8-49cd-8ddf-10646c9a9f21"), FirstName = "Amine", LastName = "Brahem", StateId = ElQuds.Id };
            Voter voter13 = new Voter { Id = Guid.Parse("d789a80a-493f-4248-90b0-35edab1c3c63"), FirstName = "Azzedine", LastName = "Brahmi", StateId = Cairo.Id };
            Voter voter14 = new Voter { Id = Guid.Parse("71ad22cf-168a-40be-9867-4a7ebe34c339"), FirstName = "Mohamed", LastName = "Chikhi", StateId = Algiers.Id };
            Voter voter15 = new Voter { Id = Guid.Parse("783118b1-111d-4c6e-b75e-dcd721ccfc2c"), FirstName = "Mahmoud", LastName = "Dahmoun", StateId = Algiers.Id };
            Voter voter16 = new Voter { Id = Guid.Parse("0642267b-8df3-405d-b596-e50c2cdeefde"), FirstName = "Lakhdar", LastName = "Zitouni", StateId = Algiers.Id };
            modelBuilder.Entity<Voter>(v =>
            {
                v.HasData(voter1);
            });
            modelBuilder.Entity<Voter>().HasData(voter2);
            modelBuilder.Entity<Voter>().HasData(voter3);
            modelBuilder.Entity<Voter>().HasData(voter4);
            modelBuilder.Entity<Voter>().HasData(voter5);
            modelBuilder.Entity<Voter>().HasData(voter6);
            modelBuilder.Entity<Voter>().HasData(voter7);
            modelBuilder.Entity<Voter>().HasData(voter8);
            modelBuilder.Entity<Voter>().HasData(voter9);
            modelBuilder.Entity<Voter>().HasData(voter10);
            modelBuilder.Entity<Voter>().HasData(voter11);
            modelBuilder.Entity<Voter>().HasData(voter12);
            modelBuilder.Entity<Voter>().HasData(voter13);
            modelBuilder.Entity<Voter>().HasData(voter14);
            modelBuilder.Entity<Voter>().HasData(voter15);
            modelBuilder.Entity<Voter>().HasData(voter16);







            


            //  seeding data to Election table
            Election Election1 = new Election { Id = Guid.Parse("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), Name = "Election Test 1", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2011") };
            Election Election2 = new Election { Id = Guid.Parse("c71e21c7-3c87-4aea-bf8d-2be8edc8722a"), Name = "Election Test 2", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2012") };
            Election Election3 = new Election { Id = Guid.Parse("6d1ac165-5488-4f86-84ad-47301d813802"), Name = "Election Test 3", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2013") };
            Election Election4 = new Election { Id = Guid.Parse("76daa454-e061-46ac-ba1e-4c09fdcd418e"), Name = "Election Test 4", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2014") };
            Election Election5 = new Election { Id = Guid.Parse("40556dbd-b5ae-47af-89eb-32deee130dd9"), Name = "Election Test 5", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2015") };
            Election Election6 = new Election { Id = Guid.Parse("42c5a090-b3bd-4b0b-9de4-48f987d8346e"), Name = "Election Test 6", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2016") };
            Election Election7 = new Election { Id = Guid.Parse("442530b2-3e0a-4fc1-995d-1da1c6cb55cc"), Name = "Election Test 7", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2017") };
            Election Election8 = new Election { Id = Guid.Parse("f8899970-5057-4a23-833c-dc75ee84c8d2"), Name = "Election Test 8", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2018") };
            Election Election9 = new Election { Id = Guid.Parse("b6ea83b1-3cbe-47ed-bf25-9abc8dac9644"), Name = "Election Test 9", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2019") };
            Election Election10 = new Election { Id = Guid.Parse("cfd1f2a9-3984-42a0-8ec1-26fd4eef0ead"), Name = "Election Test 10", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2020") };
            Election Election11 = new Election { Id = Guid.Parse("bbcd22cb-dc43-4ea2-854c-acb61564699c"), Name = "Election Test 11", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2021") };
            Election Election12 = new Election { Id = Guid.Parse("b798676d-750e-4950-a527-5ccbc17004a4"), Name = "Election Test 12", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2022") };
            Election Election13 = new Election { Id = Guid.Parse("33e5e889-f44b-461a-83c9-680f34f82e06"), Name = "Election Test 13", DurationInDays = 3, HasNeutral = false, StartDate = DateTime.Parse("01/01/2023") };
            modelBuilder.Entity<Election>().HasData(Election1);
            modelBuilder.Entity<Election>().HasData(Election2);
            modelBuilder.Entity<Election>().HasData(Election3);
            modelBuilder.Entity<Election>().HasData(Election4);
            modelBuilder.Entity<Election>().HasData(Election5);
            modelBuilder.Entity<Election>().HasData(Election6);
            modelBuilder.Entity<Election>().HasData(Election7);
            modelBuilder.Entity<Election>().HasData(Election8);
            modelBuilder.Entity<Election>().HasData(Election9);
            modelBuilder.Entity<Election>().HasData(Election10);
            modelBuilder.Entity<Election>().HasData(Election11);
            modelBuilder.Entity<Election>().HasData(Election12);
            modelBuilder.Entity<Election>().HasData(Election13);



            // seeding data to Candidate table            
            // Election1 candidates
            Candidate Candidate2 = new Candidate { Id = Guid.Parse("8bc92480-c3d1-440d-86c4-a6a4ed89255a"), isNeutralOpinion = false, VoterBeingId = voter2.Id, ElectionId = Election1.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate2);
            Candidate Candidate3 = new Candidate { Id = Guid.Parse("0d201c31-b90f-4721-98ce-03f7806a1d2d"), isNeutralOpinion = false, VoterBeingId = voter3.Id, ElectionId = Election1.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate3);
            Candidate Candidate4 = new Candidate { Id = Guid.Parse("87945fcb-6b98-4a17-99d6-0611606fb203"), isNeutralOpinion = false, VoterBeingId = voter4.Id, ElectionId = Election1.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate4);
            Candidate Candidate5 = new Candidate { Id = Guid.Parse("b58d1bf6-a210-40e1-a745-6bade04019a7"), isNeutralOpinion = false, VoterBeingId = voter5.Id, ElectionId = Election1.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate5);
            Candidate Candidate6 = new Candidate { Id = Guid.Parse("0f2088c2-06d8-4913-9986-c44473b50f2b"), isNeutralOpinion = false, VoterBeingId = voter6.Id, ElectionId = Election1.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate6);

            // Election2 candidates
            Candidate Candidate7 = new Candidate { Id = Guid.Parse("2c9b8405-39bc-4bc8-b539-a473122eb007"), isNeutralOpinion = false, VoterBeingId = voter4.Id, ElectionId = Election2.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate7);
            Candidate Candidate8 = new Candidate { Id = Guid.Parse("43b303b1-630e-4dd6-811b-63f88c06e60d"), isNeutralOpinion = false, VoterBeingId = voter5.Id, ElectionId = Election2.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate8);
            Candidate Candidate9 = new Candidate { Id = Guid.Parse("470ac0b5-151c-43a2-bbd8-af80f38d9435"), isNeutralOpinion = false, VoterBeingId = voter6.Id, ElectionId = Election2.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate9);

            // Election3 candidates
            Candidate Candidate10 = new Candidate { Id = Guid.Parse("cb6e54e2-77a0-4f31-96f4-4ef31fee2d24"), isNeutralOpinion = false, VoterBeingId = voter4.Id, ElectionId = Election3.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate10);
            Candidate Candidate11 = new Candidate { Id = Guid.Parse("69da0af1-b243-439b-a227-95cec2ed4350"), isNeutralOpinion = false, VoterBeingId = voter5.Id, ElectionId = Election3.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate11);

            // Election4 candidates
            Candidate Candidate12 = new Candidate { Id = Guid.Parse("c877feb9-c8a2-42de-80c0-196410a60196"), isNeutralOpinion = false, VoterBeingId = voter6.Id, ElectionId = Election4.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate12);
            Candidate Candidate13 = new Candidate { Id = Guid.Parse("e194ac0c-a0d0-48ec-a3a6-25b9f6a07587"), isNeutralOpinion = false, VoterBeingId = voter7.Id, ElectionId = Election4.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate13);
            Candidate Candidate14 = new Candidate { Id = Guid.Parse("556e2593-197d-4c40-a1a7-159917816196"), isNeutralOpinion = false, VoterBeingId = voter8.Id, ElectionId = Election4.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate14);
            Candidate Candidate15 = new Candidate { Id = Guid.Parse("2bdb3acd-e9b7-40a0-b2ea-d958f59c3b6c"), isNeutralOpinion = false, VoterBeingId = voter9.Id, ElectionId = Election4.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate15);

            // Election5 candidates
            Candidate Candidate16 = new Candidate { Id = Guid.Parse("ea0eefce-3b8e-4a33-9af9-524ba5c678ee"), isNeutralOpinion = false, VoterBeingId = voter10.Id, ElectionId = Election5.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate16);
            Candidate Candidate17 = new Candidate { Id = Guid.Parse("575d00a6-b8e1-4b71-ba9e-4d26490d4d84"), isNeutralOpinion = false, VoterBeingId = voter11.Id, ElectionId = Election5.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate17);
            Candidate Candidate18 = new Candidate { Id = Guid.Parse("473f81e3-aa8b-457a-9d83-cffb1d277dec"), isNeutralOpinion = false, VoterBeingId = voter12.Id, ElectionId = Election5.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate18);
            Candidate Candidate19 = new Candidate { Id = Guid.Parse("62055ca5-e962-4fff-b32d-48d7c71f9f23"), isNeutralOpinion = false, VoterBeingId = voter13.Id, ElectionId = Election5.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate19);
            Candidate Candidate20 = new Candidate { Id = Guid.Parse("701bd96b-02e4-47af-92f4-d1e49951a2dc"), isNeutralOpinion = false, VoterBeingId = voter14.Id, ElectionId = Election5.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate20);

            // Election6 candidates
            Candidate Candidate21 = new Candidate { Id = Guid.Parse("1590c51e-7122-48e3-bcd1-12a43e170ede"), isNeutralOpinion = false, VoterBeingId = voter6.Id, ElectionId = Election6.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate21);
            Candidate Candidate22 = new Candidate { Id = Guid.Parse("950a79f2-bed6-40e1-8cb6-6d89ab553188"), isNeutralOpinion = false, VoterBeingId = voter7.Id, ElectionId = Election6.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate22);
            Candidate Candidate23 = new Candidate { Id = Guid.Parse("a5452366-7898-47cd-a5bb-0e4a95ed319b"), isNeutralOpinion = false, VoterBeingId = voter8.Id, ElectionId = Election6.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate23);
            Candidate Candidate24 = new Candidate { Id = Guid.Parse("fda62639-faf3-4178-90fe-7da3484c48af"), isNeutralOpinion = false, VoterBeingId = voter9.Id, ElectionId = Election6.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate24);


            // Election7 candidates
            Candidate Candidate29 = new Candidate { Id = Guid.Parse("943633be-4f99-44d1-b9cf-04434f03fc6d"), isNeutralOpinion = false, VoterBeingId = voter2.Id, ElectionId = Election7.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate29);
            Candidate Candidate25 = new Candidate { Id = Guid.Parse("7f2811bc-48df-478d-83ad-649a7e2a8195"), isNeutralOpinion = false, VoterBeingId = voter3.Id, ElectionId = Election7.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate25);
            Candidate Candidate26 = new Candidate { Id = Guid.Parse("a49e5a59-c2b5-451f-a620-425adec7c44d"), isNeutralOpinion = false, VoterBeingId = voter4.Id, ElectionId = Election7.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate26);
            Candidate Candidate27 = new Candidate { Id = Guid.Parse("910fb488-8da3-4183-a92a-17d046d59553"), isNeutralOpinion = false, VoterBeingId = voter5.Id, ElectionId = Election7.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate27);
            Candidate Candidate28 = new Candidate { Id = Guid.Parse("85c45467-32c4-4dae-8023-06df1df955b4"), isNeutralOpinion = false, VoterBeingId = voter6.Id, ElectionId = Election7.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate28);

            // Election8 candidates
            Candidate Candidate30 = new Candidate { Id = Guid.Parse("342a2bbc-d9ec-41d8-b45d-91ffa59975b2"), isNeutralOpinion = false, VoterBeingId = voter4.Id, ElectionId = Election8.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate30);
            Candidate Candidate31 = new Candidate { Id = Guid.Parse("c0e68551-3e5e-4268-89e9-b6310de3956b"), isNeutralOpinion = false, VoterBeingId = voter5.Id, ElectionId = Election8.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate31);
            Candidate Candidate32 = new Candidate { Id = Guid.Parse("6c44ea03-afa1-4546-94fa-0cf086293fc2"), isNeutralOpinion = false, VoterBeingId = voter6.Id, ElectionId = Election8.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate32);

            // Election9 candidates
            Candidate Candidate33 = new Candidate { Id = Guid.Parse("80cd8f07-8c6e-4279-b62e-9883aedf0d56"), isNeutralOpinion = false, VoterBeingId = voter4.Id, ElectionId = Election9.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate33);
            Candidate Candidate34 = new Candidate { Id = Guid.Parse("ba5a0188-6983-40d9-9364-9113509e97ee"), isNeutralOpinion = false, VoterBeingId = voter5.Id, ElectionId = Election9.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate34);

            // Election10 candidates
            Candidate Candidate35 = new Candidate { Id = Guid.Parse("e139e024-45d8-4e24-83fb-1b394bd458cb"), isNeutralOpinion = false, VoterBeingId = voter6.Id, ElectionId = Election10.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate35);
            Candidate Candidate36 = new Candidate { Id = Guid.Parse("3a884c97-5e90-4e3d-87c0-5d96745e54a9"), isNeutralOpinion = false, VoterBeingId = voter7.Id, ElectionId = Election10.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate36);
            Candidate Candidate37 = new Candidate { Id = Guid.Parse("0bc40cad-00fe-4ca8-a789-91b59f0c5a2f"), isNeutralOpinion = false, VoterBeingId = voter8.Id, ElectionId = Election10.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate37);
            Candidate Candidate38 = new Candidate { Id = Guid.Parse("ea67fb4e-329b-4340-9a71-2fb944461ca5"), isNeutralOpinion = false, VoterBeingId = voter9.Id, ElectionId = Election10.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate38);

            // Election11 candidates
            Candidate Candidate39 = new Candidate { Id = Guid.Parse("dd50e713-110f-4ea5-91a2-9144d95cc1fa"), isNeutralOpinion = false, VoterBeingId = voter10.Id, ElectionId = Election11.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate39);
            Candidate Candidate40 = new Candidate { Id = Guid.Parse("5d1858eb-06f1-40a4-ad71-04f5f2ec37bf"), isNeutralOpinion = false, VoterBeingId = voter11.Id, ElectionId = Election11.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate40);
            Candidate Candidate41 = new Candidate { Id = Guid.Parse("08828a2a-b8aa-4c7e-be3a-7961b8e40daf"), isNeutralOpinion = false, VoterBeingId = voter12.Id, ElectionId = Election11.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate41);
            Candidate Candidate42 = new Candidate { Id = Guid.Parse("0668f926-7d1f-4f3d-b0a0-3dbc90357eb7"), isNeutralOpinion = false, VoterBeingId = voter13.Id, ElectionId = Election11.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate42);
            Candidate Candidate43 = new Candidate { Id = Guid.Parse("83c9cd1b-4212-426b-985f-1770676c0be9"), isNeutralOpinion = false, VoterBeingId = voter14.Id, ElectionId = Election11.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate43);

            // Election12 candidates
            Candidate Candidate44 = new Candidate { Id = Guid.Parse("73da9cde-8eed-4743-b369-8cb2280e4278"), isNeutralOpinion = false, VoterBeingId = voter6.Id, ElectionId = Election12.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate44);
            Candidate Candidate45 = new Candidate { Id = Guid.Parse("2548f0bc-8005-42cc-a938-dd9c19b29216"), isNeutralOpinion = false, VoterBeingId = voter7.Id, ElectionId = Election12.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate45);
            Candidate Candidate46 = new Candidate { Id = Guid.Parse("b2b4e50f-ad7d-4736-8d9d-5067b7a4f173"), isNeutralOpinion = false, VoterBeingId = voter8.Id, ElectionId = Election12.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate46);
            Candidate Candidate47 = new Candidate { Id = Guid.Parse("e3bdc2bb-b5d0-4b0b-b187-3e43454f5f34"), isNeutralOpinion = false, VoterBeingId = voter9.Id, ElectionId = Election12.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate47);

            // Election13 candidates
            Candidate Candidate48 = new Candidate { Id = Guid.Parse("be668d55-1706-4f8b-98eb-c0af8a7341fe"), isNeutralOpinion = false, VoterBeingId = voter6.Id, ElectionId = Election13.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate48);
            Candidate Candidate49 = new Candidate { Id = Guid.Parse("15ca3802-01d6-4f7d-beef-fb99a6485606"), isNeutralOpinion = false, VoterBeingId = voter7.Id, ElectionId = Election13.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate49);
            Candidate Candidate50 = new Candidate { Id = Guid.Parse("224cdcc9-ae8e-4668-b88a-ceb9e3927594"), isNeutralOpinion = false, VoterBeingId = voter8.Id, ElectionId = Election13.Id };
            modelBuilder.Entity<Candidate>().HasData(Candidate50);

            // seeding data to Vote table
            //below code has been written using Excel sheet
            Vote Vote1 = new Vote { Id = Guid.Parse("36ee9f08-6a2e-46ee-87a0-5c41eada7e1e"), CandidateId = Candidate2.Id, ElectionId = Election1.Id, VoterId = voter15.Id, Datetime = Election1.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote1);
            Vote Vote2 = new Vote { Id = Guid.Parse("14c2a76e-8ae3-41e7-a6f3-063378d7f48d"), CandidateId = Candidate3.Id, ElectionId = Election1.Id, VoterId = voter6.Id, Datetime = Election1.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote2);
            Vote Vote3 = new Vote { Id = Guid.Parse("4f14d76a-e8f4-424f-b644-628ad7ea4789"), CandidateId = Candidate4.Id, ElectionId = Election1.Id, VoterId = voter2.Id, Datetime = Election1.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote3);
            Vote Vote4 = new Vote { Id = Guid.Parse("39320365-ffb5-4b7f-9cf2-636792171d50"), CandidateId = Candidate5.Id, ElectionId = Election1.Id, VoterId = voter3.Id, Datetime = Election1.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote4);
            Vote Vote5 = new Vote { Id = Guid.Parse("b3366d5b-81b0-4642-8b18-a1f02edd1bb0"), CandidateId = Candidate6.Id, ElectionId = Election1.Id, VoterId = voter5.Id, Datetime = Election1.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote5);
            Vote Vote6 = new Vote { Id = Guid.Parse("3a4890fc-b4b8-48c8-817b-032e40733cc7"), CandidateId = Candidate7.Id, ElectionId = Election2.Id, VoterId = voter8.Id, Datetime = Election2.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote6);
            Vote Vote7 = new Vote { Id = Guid.Parse("cb0d8440-09a3-4b0b-9d9a-a83c904d28db"), CandidateId = Candidate8.Id, ElectionId = Election2.Id, VoterId = voter7.Id, Datetime = Election2.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote7);
            Vote Vote8 = new Vote { Id = Guid.Parse("5280803f-064c-4521-9090-602391203c36"), CandidateId = Candidate9.Id, ElectionId = Election2.Id, VoterId = voter13.Id, Datetime = Election2.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote8);
            Vote Vote9 = new Vote { Id = Guid.Parse("43a2f571-3b74-48b6-a7cf-dccb1d5c4d73"), CandidateId = Candidate10.Id, ElectionId = Election3.Id, VoterId = voter10.Id, Datetime = Election3.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote9);
            Vote Vote10 = new Vote { Id = Guid.Parse("55ee050f-4d87-4767-a4ef-2a35e3eb48c0"), CandidateId = Candidate11.Id, ElectionId = Election3.Id, VoterId = voter11.Id, Datetime = Election3.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote10);
            Vote Vote11 = new Vote { Id = Guid.Parse("456395da-1f42-4805-8956-22b3415ef3bf"), CandidateId = Candidate12.Id, ElectionId = Election4.Id, VoterId = voter9.Id, Datetime = Election4.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote11);
            Vote Vote12 = new Vote { Id = Guid.Parse("818ffa39-73db-46ef-94fd-ef4a7398a0b0"), CandidateId = Candidate13.Id, ElectionId = Election4.Id, VoterId = voter14.Id, Datetime = Election4.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote12);
            Vote Vote13 = new Vote { Id = Guid.Parse("5d5ac6a8-f532-450e-bcdf-1033e509f6af"), CandidateId = Candidate14.Id, ElectionId = Election4.Id, VoterId = voter11.Id, Datetime = Election4.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote13);
            Vote Vote14 = new Vote { Id = Guid.Parse("fe0bba6d-5313-4ef4-a82f-a03bce1490b6"), CandidateId = Candidate15.Id, ElectionId = Election4.Id, VoterId = voter3.Id, Datetime = Election4.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote14);
            Vote Vote15 = new Vote { Id = Guid.Parse("0e677ef4-877a-4e1b-8b3d-c4161ad59573"), CandidateId = Candidate16.Id, ElectionId = Election5.Id, VoterId = voter10.Id, Datetime = Election5.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote15);
            Vote Vote16 = new Vote { Id = Guid.Parse("a90dc979-1564-4afc-957b-7b52808b13b2"), CandidateId = Candidate17.Id, ElectionId = Election5.Id, VoterId = voter10.Id, Datetime = Election5.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote16);
            Vote Vote17 = new Vote { Id = Guid.Parse("ff30a50c-4d16-4285-92f0-6d354e2c46e3"), CandidateId = Candidate18.Id, ElectionId = Election5.Id, VoterId = voter3.Id, Datetime = Election5.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote17);
            Vote Vote18 = new Vote { Id = Guid.Parse("bf00dbe4-cb16-4e78-bdd1-f772bf3ab5c0"), CandidateId = Candidate19.Id, ElectionId = Election5.Id, VoterId = voter14.Id, Datetime = Election5.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote18);
            Vote Vote19 = new Vote { Id = Guid.Parse("bd42eff3-3764-4258-9ff0-6885528d759e"), CandidateId = Candidate20.Id, ElectionId = Election5.Id, VoterId = voter16.Id, Datetime = Election5.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote19);
            Vote Vote20 = new Vote { Id = Guid.Parse("9cc35050-e8be-407c-b659-2c31618a933c"), CandidateId = Candidate21.Id, ElectionId = Election6.Id, VoterId = voter11.Id, Datetime = Election6.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote20);
            Vote Vote21 = new Vote { Id = Guid.Parse("0a0a0c7e-a626-4e67-a3ae-09def01656fb"), CandidateId = Candidate22.Id, ElectionId = Election6.Id, VoterId = voter4.Id, Datetime = Election6.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote21);
            Vote Vote22 = new Vote { Id = Guid.Parse("b5c9ec2c-7454-4ed1-8337-af2e253914fa"), CandidateId = Candidate23.Id, ElectionId = Election6.Id, VoterId = voter3.Id, Datetime = Election6.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote22);
            Vote Vote23 = new Vote { Id = Guid.Parse("88a5aa50-35a5-46b1-87b6-938c37743f20"), CandidateId = Candidate24.Id, ElectionId = Election6.Id, VoterId = voter15.Id, Datetime = Election6.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote23);
            Vote Vote24 = new Vote { Id = Guid.Parse("e7550fda-85a5-40ae-9b5f-f92a0a0e1251"), CandidateId = Candidate25.Id, ElectionId = Election7.Id, VoterId = voter10.Id, Datetime = Election7.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote24);
            Vote Vote25 = new Vote { Id = Guid.Parse("003f2db0-9ea2-441e-a210-1ebf8624d119"), CandidateId = Candidate26.Id, ElectionId = Election7.Id, VoterId = voter5.Id, Datetime = Election7.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote25);
            Vote Vote26 = new Vote { Id = Guid.Parse("a2fb9c59-9a62-40b0-ab87-d82a323d831c"), CandidateId = Candidate27.Id, ElectionId = Election7.Id, VoterId = voter12.Id, Datetime = Election7.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote26);
            Vote Vote27 = new Vote { Id = Guid.Parse("e538cd70-c0a0-4d99-8c41-387c9eb85ead"), CandidateId = Candidate28.Id, ElectionId = Election7.Id, VoterId = voter9.Id, Datetime = Election7.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote27);
            Vote Vote28 = new Vote { Id = Guid.Parse("3e0a8ac7-ea3a-4fa9-a4c9-6645e90fe39d"), CandidateId = Candidate29.Id, ElectionId = Election7.Id, VoterId = voter12.Id, Datetime = Election7.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote28);
            Vote Vote29 = new Vote { Id = Guid.Parse("245a1edc-a691-437f-ba17-e9bece210180"), CandidateId = Candidate30.Id, ElectionId = Election8.Id, VoterId = voter10.Id, Datetime = Election8.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote29);
            Vote Vote30 = new Vote { Id = Guid.Parse("9597de35-91e3-4687-8462-ddbf771fbc1c"), CandidateId = Candidate31.Id, ElectionId = Election8.Id, VoterId = voter7.Id, Datetime = Election8.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote30);
            Vote Vote31 = new Vote { Id = Guid.Parse("c808d0f3-bb45-4f47-9301-b57daefff788"), CandidateId = Candidate32.Id, ElectionId = Election8.Id, VoterId = voter1.Id, Datetime = Election8.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote31);
            Vote Vote32 = new Vote { Id = Guid.Parse("6ddc6066-a16c-4bc1-98c8-1731ce829c84"), CandidateId = Candidate33.Id, ElectionId = Election9.Id, VoterId = voter11.Id, Datetime = Election9.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote32);
            Vote Vote33 = new Vote { Id = Guid.Parse("ed596e4f-483f-4aac-b64b-73ff72d70ed3"), CandidateId = Candidate34.Id, ElectionId = Election9.Id, VoterId = voter16.Id, Datetime = Election9.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote33);
            Vote Vote34 = new Vote { Id = Guid.Parse("264d9788-70f3-491f-9060-d47266f0f87b"), CandidateId = Candidate35.Id, ElectionId = Election10.Id, VoterId = voter14.Id, Datetime = Election10.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote34);
            Vote Vote35 = new Vote { Id = Guid.Parse("2274d833-785e-49d7-bb13-0df3aa309f1b"), CandidateId = Candidate36.Id, ElectionId = Election10.Id, VoterId = voter2.Id, Datetime = Election10.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote35);
            Vote Vote36 = new Vote { Id = Guid.Parse("b888eead-142f-4bae-a112-d01309834c92"), CandidateId = Candidate37.Id, ElectionId = Election10.Id, VoterId = voter7.Id, Datetime = Election10.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote36);
            Vote Vote37 = new Vote { Id = Guid.Parse("68cf5aa5-e4d1-4145-830f-b7e18e8e3431"), CandidateId = Candidate38.Id, ElectionId = Election10.Id, VoterId = voter8.Id, Datetime = Election10.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote37);
            Vote Vote38 = new Vote { Id = Guid.Parse("fce56560-9e1d-48a5-b187-371ac09c2474"), CandidateId = Candidate39.Id, ElectionId = Election11.Id, VoterId = voter11.Id, Datetime = Election11.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote38);
            Vote Vote39 = new Vote { Id = Guid.Parse("cb50c637-6fea-4dba-ab71-fc5188529b40"), CandidateId = Candidate40.Id, ElectionId = Election11.Id, VoterId = voter1.Id, Datetime = Election11.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote39);
            Vote Vote40 = new Vote { Id = Guid.Parse("2331dece-90f6-4bd7-9906-d89020500426"), CandidateId = Candidate41.Id, ElectionId = Election11.Id, VoterId = voter15.Id, Datetime = Election11.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote40);
            Vote Vote41 = new Vote { Id = Guid.Parse("e46c62ec-3851-4d58-8319-ddf5c3b9aeec"), CandidateId = Candidate42.Id, ElectionId = Election11.Id, VoterId = voter13.Id, Datetime = Election11.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote41);
            Vote Vote42 = new Vote { Id = Guid.Parse("407a9cbc-9645-4114-abbc-17ea795167fa"), CandidateId = Candidate43.Id, ElectionId = Election11.Id, VoterId = voter12.Id, Datetime = Election11.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote42);
            Vote Vote43 = new Vote { Id = Guid.Parse("04b06cd3-e4c7-4458-8c7f-b0c3c9bb5b38"), CandidateId = Candidate44.Id, ElectionId = Election12.Id, VoterId = voter15.Id, Datetime = Election12.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote43);
            Vote Vote44 = new Vote { Id = Guid.Parse("41020320-e8c3-4b72-9623-b9fbac10ed0d"), CandidateId = Candidate45.Id, ElectionId = Election12.Id, VoterId = voter12.Id, Datetime = Election12.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote44);
            Vote Vote45 = new Vote { Id = Guid.Parse("5ef28023-87d9-42c3-a516-b497fb6b7d54"), CandidateId = Candidate46.Id, ElectionId = Election12.Id, VoterId = voter12.Id, Datetime = Election12.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote45);
            Vote Vote46 = new Vote { Id = Guid.Parse("656377ff-1e3c-4753-8c6a-35bd48a6a799"), CandidateId = Candidate47.Id, ElectionId = Election12.Id, VoterId = voter8.Id, Datetime = Election12.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote46);
            Vote Vote47 = new Vote { Id = Guid.Parse("a84e9896-61af-4d10-95a0-35fab153e3ea"), CandidateId = Candidate48.Id, ElectionId = Election13.Id, VoterId = voter5.Id, Datetime = Election13.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote47);
            Vote Vote48 = new Vote { Id = Guid.Parse("25b1721e-a7c2-4e56-aa8d-c747c81897e9"), CandidateId = Candidate49.Id, ElectionId = Election13.Id, VoterId = voter10.Id, Datetime = Election13.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote48);
            Vote Vote49 = new Vote { Id = Guid.Parse("df0a5fda-6d72-41d2-aa3d-84585e34a206"), CandidateId = Candidate50.Id, ElectionId = Election13.Id, VoterId = voter1.Id, Datetime = Election13.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote49);
            Vote Vote50 = new Vote { Id = Guid.Parse("15cf520b-b01b-4587-ba67-2c418c19fc91"), CandidateId = Candidate4.Id, ElectionId = Election1.Id, VoterId = voter4.Id, Datetime = Election1.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote50);
            Vote Vote51 = new Vote { Id = Guid.Parse("5247a247-0bcd-4a42-977d-bcc0e5fa4980"), CandidateId = Candidate5.Id, ElectionId = Election1.Id, VoterId = voter4.Id, Datetime = Election1.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote51);
            Vote Vote52 = new Vote { Id = Guid.Parse("006699e1-f7d3-4c31-9fba-1e800c9df7ce"), CandidateId = Candidate6.Id, ElectionId = Election1.Id, VoterId = voter2.Id, Datetime = Election1.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote52);
            Vote Vote53 = new Vote { Id = Guid.Parse("eccbaa29-04b0-43f6-a2d2-cbb49a5b72e9"), CandidateId = Candidate8.Id, ElectionId = Election2.Id, VoterId = voter7.Id, Datetime = Election2.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote53);
            Vote Vote54 = new Vote { Id = Guid.Parse("30f074fd-87e4-4b85-af55-f0f833f3f90f"), CandidateId = Candidate11.Id, ElectionId = Election3.Id, VoterId = voter2.Id, Datetime = Election3.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote54);
            Vote Vote55 = new Vote { Id = Guid.Parse("71a4aaad-57cf-430d-9b63-afaf0fced0ec"), CandidateId = Candidate13.Id, ElectionId = Election4.Id, VoterId = voter5.Id, Datetime = Election4.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote55);
            Vote Vote56 = new Vote { Id = Guid.Parse("cdf05f25-4010-44a4-9ce3-1f9e3ed5e9fb"), CandidateId = Candidate14.Id, ElectionId = Election4.Id, VoterId = voter10.Id, Datetime = Election4.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote56);
            Vote Vote57 = new Vote { Id = Guid.Parse("d44c1d90-2f6b-4bfc-97f9-cce6b627d0f6"), CandidateId = Candidate17.Id, ElectionId = Election5.Id, VoterId = voter4.Id, Datetime = Election5.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote57);
            Vote Vote58 = new Vote { Id = Guid.Parse("23eb7fa2-767b-4db5-9b97-567614641e6e"), CandidateId = Candidate18.Id, ElectionId = Election5.Id, VoterId = voter9.Id, Datetime = Election5.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote58);
            Vote Vote59 = new Vote { Id = Guid.Parse("945073a8-900c-4f7b-b0ba-e090833d170c"), CandidateId = Candidate19.Id, ElectionId = Election5.Id, VoterId = voter3.Id, Datetime = Election5.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote59);
            Vote Vote60 = new Vote { Id = Guid.Parse("e911bd84-df40-4cb6-abf2-7ad2be76a7ba"), CandidateId = Candidate22.Id, ElectionId = Election6.Id, VoterId = voter16.Id, Datetime = Election6.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote60);
            Vote Vote61 = new Vote { Id = Guid.Parse("dfb69202-05e0-4500-8654-bdfba9115213"), CandidateId = Candidate23.Id, ElectionId = Election6.Id, VoterId = voter11.Id, Datetime = Election6.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote61);
            Vote Vote62 = new Vote { Id = Guid.Parse("1db62365-1956-464f-93da-17be78eb72b6"), CandidateId = Candidate26.Id, ElectionId = Election7.Id, VoterId = voter5.Id, Datetime = Election7.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote62);
            Vote Vote63 = new Vote { Id = Guid.Parse("9e54fa6d-dd10-4f8b-9b2d-e116afe7ae1e"), CandidateId = Candidate27.Id, ElectionId = Election7.Id, VoterId = voter5.Id, Datetime = Election7.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote63);
            Vote Vote64 = new Vote { Id = Guid.Parse("e3778148-e2b9-4c4c-a8a7-0a1ccbb7581d"), CandidateId = Candidate31.Id, ElectionId = Election8.Id, VoterId = voter9.Id, Datetime = Election8.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote64);
            Vote Vote65 = new Vote { Id = Guid.Parse("5456d7e8-31e0-478d-96e3-19ee84db18a1"), CandidateId = Candidate32.Id, ElectionId = Election8.Id, VoterId = voter8.Id, Datetime = Election8.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote65);
            Vote Vote66 = new Vote { Id = Guid.Parse("ea724645-1765-4184-9abf-e6fc092111b6"), CandidateId = Candidate34.Id, ElectionId = Election9.Id, VoterId = voter8.Id, Datetime = Election9.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote66);
            Vote Vote67 = new Vote { Id = Guid.Parse("eab4cbf7-5a40-4705-a44f-6b2c66ef3e6f"), CandidateId = Candidate36.Id, ElectionId = Election10.Id, VoterId = voter13.Id, Datetime = Election10.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote67);
            Vote Vote68 = new Vote { Id = Guid.Parse("e41222a9-0379-4043-b16b-bf7c6fa22f92"), CandidateId = Candidate40.Id, ElectionId = Election11.Id, VoterId = voter16.Id, Datetime = Election11.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote68);
            Vote Vote69 = new Vote { Id = Guid.Parse("f703c6d5-df3d-4685-9e9c-30a3b12251d7"), CandidateId = Candidate41.Id, ElectionId = Election11.Id, VoterId = voter9.Id, Datetime = Election11.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote69);
            Vote Vote70 = new Vote { Id = Guid.Parse("f91effc8-32f9-4df8-af49-532e6a3fac6c"), CandidateId = Candidate45.Id, ElectionId = Election12.Id, VoterId = voter9.Id, Datetime = Election12.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote70);
            Vote Vote71 = new Vote { Id = Guid.Parse("98745388-580d-4ad9-b86b-cdc233bb36ee"), CandidateId = Candidate46.Id, ElectionId = Election12.Id, VoterId = voter15.Id, Datetime = Election12.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote71);
            Vote Vote72 = new Vote { Id = Guid.Parse("38658e94-887c-4204-a1fd-748f2f350316"), CandidateId = Candidate48.Id, ElectionId = Election13.Id, VoterId = voter9.Id, Datetime = Election13.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote72);
            Vote Vote73 = new Vote { Id = Guid.Parse("c8c4cbcd-6e14-49fb-bc06-a0d36e0138d6"), CandidateId = Candidate4.Id, ElectionId = Election1.Id, VoterId = voter6.Id, Datetime = Election1.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote73);
            Vote Vote74 = new Vote { Id = Guid.Parse("3819ecca-827b-4553-a198-660a528eedf8"), CandidateId = Candidate5.Id, ElectionId = Election1.Id, VoterId = voter14.Id, Datetime = Election1.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote74);
            Vote Vote75 = new Vote { Id = Guid.Parse("4d530b58-cc2f-4dc3-b6ae-3a74f1566798"), CandidateId = Candidate6.Id, ElectionId = Election1.Id, VoterId = voter3.Id, Datetime = Election1.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote75);
            Vote Vote76 = new Vote { Id = Guid.Parse("8311ab82-b877-48cd-86ce-b1c36d8cfcd7"), CandidateId = Candidate8.Id, ElectionId = Election2.Id, VoterId = voter2.Id, Datetime = Election2.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote76);
            Vote Vote77 = new Vote { Id = Guid.Parse("eba857fa-ce45-4de9-a248-75c4a06937ea"), CandidateId = Candidate11.Id, ElectionId = Election3.Id, VoterId = voter8.Id, Datetime = Election3.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote77);
            Vote Vote78 = new Vote { Id = Guid.Parse("44b051c8-16d4-49cf-b4f7-d633a35e7a09"), CandidateId = Candidate13.Id, ElectionId = Election4.Id, VoterId = voter5.Id, Datetime = Election4.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote78);
            Vote Vote79 = new Vote { Id = Guid.Parse("0f5c9d34-7139-4ca6-b313-b3ac327991cd"), CandidateId = Candidate14.Id, ElectionId = Election4.Id, VoterId = voter9.Id, Datetime = Election4.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote79);
            Vote Vote80 = new Vote { Id = Guid.Parse("a8a776d8-cb34-48cf-ace6-468add23bef8"), CandidateId = Candidate17.Id, ElectionId = Election5.Id, VoterId = voter8.Id, Datetime = Election5.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote80);
            Vote Vote81 = new Vote { Id = Guid.Parse("7e42dd44-725d-48db-94e9-1a37bbe035ae"), CandidateId = Candidate18.Id, ElectionId = Election5.Id, VoterId = voter2.Id, Datetime = Election5.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote81);
            Vote Vote82 = new Vote { Id = Guid.Parse("be260190-421a-41b7-8bb4-11da2893c7cb"), CandidateId = Candidate19.Id, ElectionId = Election5.Id, VoterId = voter8.Id, Datetime = Election5.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote82);
            Vote Vote83 = new Vote { Id = Guid.Parse("0f67792f-0b6d-40b3-9bab-37db57ea9821"), CandidateId = Candidate22.Id, ElectionId = Election6.Id, VoterId = voter5.Id, Datetime = Election6.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote83);
            Vote Vote84 = new Vote { Id = Guid.Parse("0dae9b5b-5400-4876-94bc-c5852e003fd6"), CandidateId = Candidate23.Id, ElectionId = Election6.Id, VoterId = voter2.Id, Datetime = Election6.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote84);
            Vote Vote85 = new Vote { Id = Guid.Parse("ce388d9b-ec9c-4dda-9a2b-a4ccea22ae24"), CandidateId = Candidate26.Id, ElectionId = Election7.Id, VoterId = voter9.Id, Datetime = Election7.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote85);
            Vote Vote86 = new Vote { Id = Guid.Parse("a5f8d5b7-7909-46f8-8cdc-f560da208636"), CandidateId = Candidate27.Id, ElectionId = Election7.Id, VoterId = voter13.Id, Datetime = Election7.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote86);
            Vote Vote87 = new Vote { Id = Guid.Parse("14a79102-e54f-435d-90eb-8035c2d799ce"), CandidateId = Candidate31.Id, ElectionId = Election8.Id, VoterId = voter8.Id, Datetime = Election8.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote87);
            Vote Vote88 = new Vote { Id = Guid.Parse("98d446d2-d5d9-406a-afc9-8e0dcbe700fc"), CandidateId = Candidate32.Id, ElectionId = Election8.Id, VoterId = voter11.Id, Datetime = Election8.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote88);
            Vote Vote89 = new Vote { Id = Guid.Parse("bfa10354-a4aa-4938-a4b0-d665b5819f5e"), CandidateId = Candidate34.Id, ElectionId = Election9.Id, VoterId = voter8.Id, Datetime = Election9.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote89);
            Vote Vote90 = new Vote { Id = Guid.Parse("6c55a82c-72f2-4edf-a3a3-2c4c7579ab1d"), CandidateId = Candidate36.Id, ElectionId = Election10.Id, VoterId = voter15.Id, Datetime = Election10.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote90);
            Vote Vote91 = new Vote { Id = Guid.Parse("5a85227c-49fc-4788-a4aa-6f54a11d20be"), CandidateId = Candidate40.Id, ElectionId = Election11.Id, VoterId = voter3.Id, Datetime = Election11.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote91);
            Vote Vote92 = new Vote { Id = Guid.Parse("bc8ec1c0-61ae-4b05-9b10-d634846d340a"), CandidateId = Candidate41.Id, ElectionId = Election11.Id, VoterId = voter14.Id, Datetime = Election11.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote92);
            Vote Vote93 = new Vote { Id = Guid.Parse("96ca05c2-ff5a-4cc8-991f-8ee3ec63d5b5"), CandidateId = Candidate45.Id, ElectionId = Election12.Id, VoterId = voter2.Id, Datetime = Election12.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote93);
            Vote Vote94 = new Vote { Id = Guid.Parse("02029116-35e6-4860-897b-abf35aade507"), CandidateId = Candidate46.Id, ElectionId = Election12.Id, VoterId = voter8.Id, Datetime = Election12.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote94);
            Vote Vote95 = new Vote { Id = Guid.Parse("d6c9a4ff-e41d-41e6-b0e6-4532f87fd557"), CandidateId = Candidate48.Id, ElectionId = Election13.Id, VoterId = voter9.Id, Datetime = Election13.StartDate.AddHours(1) }; modelBuilder.Entity<Vote>().HasData(Vote95);


        }


        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<Voter> Voter { get; set; }
        public virtual DbSet<Vote> Vote { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Election> Election { get; set; }
        //public virtual DbSet<ElectionVoter> ElectionVoter { get; set; }
        //public virtual DbSet<ElectionCandidate> ElectionCandidate { get; set; }
        

        public DbSet<WebApplication1.Models.ViewModels.PersonViewModel> PersonViewModel { get; set; }
        public DbSet<WebApplication1.Models.ViewModels.StateViewModel> StateViewModel { get; set; }
        public DbSet<WebApplication1.Models.ViewModels.CandidateViewModel> CandidateViewModel { get; set; }
        public DbSet<WebApplication1.Models.ViewModels.VoterStateViewModel> VoterStateViewModel { get; set; }




        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
            {//I had to override this method bcuz when EF was trying to create the model he found one-to-one relationship Vote->Voter
                //and Voter->Vote, it was confused how to create foreign keys, so here I am clarifying it
                modelBuilder.Entity<Voter>()
                    .HasOne(v => v.Vote)
                    .WithOne(v => v.Voter)
                    .HasForeignKey<Vote>(v=>v.VoterID);
            }*/


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    /*
        //     * FROM DOCS.MICROSOFT
        //     Many-to-many relationships without an entity class to represent the join table are not yet supported.
        //     However, you can represent a many-to-many relationship by including an entity class for the join table
        //     and mapping two separate one-to-many relationships.
        //     */
        //     /*
        //     I WROTE THE BELOW LINE OF CODE BECUZ WHEN I PROCEEDED MIGRATION WITHOUT THIS  IT WAS TELLING ME THAT
        //     I wrote the below line of code becuz when I proceeded migration without this it was telling me that ElectionVoter doesn't have
        //     a key, so the below line of code makes the combination of the two foreign key compose the primary key
        //     */
        //    /*modelBuilder.Entity<ElectionVoter>().HasKey(x => new { x.VoterId, x.ElectionId });*/

        //    /*modelBuilder.Entity<ElectionCandidate>().HasKey(x => new { x.CandidateId, x.ElectionId });*/


        //    /*IMPORTANT: I HAD TO CANCEL THIS METHOD BECAUSE THE MODEL ELECTIONCANDIDATE.CS AND ELECTIONVOTER.CS BOTH SHOULD HAVE THEIR OWN KEY 'ID'
        //     BECUZ THEIR REPOSITORIES ARE GOIN INMPLEMENT IREPOSITORY.CS INTERFACE* WHICH ITS METHOD GETBYID() ACCEPT ONE PARAMETER 'ID' NOT TWO*/                
        //}
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Election)
                .WithMany(v => v.Votes)
                .HasForeignKey<Vote>(v=>v.ElectionID);
        }*/

    }
}
