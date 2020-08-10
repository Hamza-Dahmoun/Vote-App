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
            Voter voter1 = new Voter { Id = Guid.Parse("0d925194-fee5-4750-a53c-b36a47afeeab"), FirstName = "Hamza", LastName = "Dahmoun", StateId= Oran.Id };
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



            //  seeding data to State table
            Election Election1 = new Election { Id = Guid.Parse("ef62cf71-3892-4f12-8f54-4a80580eb3b0"), Name = "Election Test 1", DurationInDays=3, HasNeutral=false, StartDate=DateTime.Parse("01/01/2011") };
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
