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
            Vote Vote2= new Vote { Id = Guid.Parse("7c6fadf7-089b-4836-a849-1a81f2c0b84f"), CandidateId = Candidate2.Id, ElectionId = Election2.Id, VoterId = voter2.Id, Datetime = Election2.StartDate.AddHours(1) };
            modelBuilder.Entity<Vote>().HasData(Vote2);
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
