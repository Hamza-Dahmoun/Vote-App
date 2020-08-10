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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //I OVERRIDED THIS METHOD SO I CAN SEED MINIMUM DATA TO THE DATABASE FOR BETTER USER EXPERIENCE
        //    //FOR PEOPLE WHO GET THE SOURCE CODE OF THE APP AND ANT TO TRY IT

        //    //  seeding data to State table
        //    State Oran = new State { Id = Guid.Parse("fa9b72ee-dfcc-4353-b195-5c2855b1343f"), Name = "Oran" };
        //    State Meca = new State { Id = Guid.Parse("32065802-7f25-47bc-8987-dd4fdb2829c4"), Name = "Meca" };
        //    State Cairo = new State { Id = Guid.Parse("3138e047-e80f-44a1-ae1d-96804784f807"), Name = "Cairo" };
        //    State ElQuds = new State { Id = Guid.Parse("33f88529-1a04-4d5e-84dc-135646948bc0"), Name = "ElQuds" };
        //    State Algiers = new State { Id = Guid.Parse("918333ff-ac52-4424-a3d9-09bee4c39b91"), Name = "Algiers" };
        //    modelBuilder.Entity<State>().HasData(
        //     Oran
        //        );
            

        //    //  seeding data to Voter table
        //    Voter voter1 = new Voter { Id = Guid.Parse("0d925194-fee5-4750-a53c-b36a47afeeab"), FirstName = "Hamza", LastName = "Dahmoun", State = Oran };
        //    modelBuilder.Entity<Voter>().HasData(voter1);
            
        //    /*Voter voter2 = new Voter { Id = Guid.Parse("8dd6ebe1-e2e4-4555-ac61-9887cebebf61"), FirstName = "Ikram", LastName = "Dahmoun", StateId = Guid.Parse("fa9b72ee-dfcc-4353-b195-5c2855b1343f") };
        //    Voter voter3 = new Voter { Id = Guid.Parse("df4b1285-b216-488c-88b7-0de75528f2fc"), FirstName = "Ahmed", LastName = "Mohamed", StateId = Guid.Parse("918333ff-ac52-4424-a3d9-09bee4c39b91") };
        //    Voter voter4 = new Voter { Id = Guid.Parse("783118b1-111d-4c6e-b75e-dcd721ccfc2c"), FirstName = "Mahmoud", LastName = "Dahmoun", StateId = Guid.Parse("918333ff-ac52-4424-a3d9-09bee4c39b91") };
        //    Voter voter5 = new Voter { Id = Guid.Parse("0642267b-8df3-405d-b596-e50c2cdeefde"), FirstName = "Lakhdar", LastName = "Zitouni", StateId = Guid.Parse("918333ff-ac52-4424-a3d9-09bee4c39b91") };
        //    Voter voter6 = new Voter { Id = Guid.Parse("c81388cf-be3e-42fc-b791-02eccce16095"), FirstName = "Mohamed", LastName = "Zitouni", StateId = Guid.Parse("32065802-7f25-47bc-8987-dd4fdb2829c4") };
        //    Voter voter7 = new Voter { Id = Guid.Parse("4ff2b64f-17fe-4621-9310-eeb59a9af847"), FirstName = "Yasser", LastName = "Zitouni", StateId = Guid.Parse("32065802-7f25-47bc-8987-dd4fdb2829c4") };
        //    Voter voter8 = new Voter { Id = Guid.Parse("62ae0555-718d-4623-a12a-7ae4a2e26aef"), FirstName = "Sidahmed", LastName = "Dahmoun", StateId = Guid.Parse("3138e047-e80f-44a1-ae1d-96804784f807") };
        //    Voter voter9 = new Voter { Id = Guid.Parse("6c9de511-db59-4be8-9315-d58700ce10be"), FirstName = "Bilal", LastName = "Dahmoun", StateId = Guid.Parse("3138e047-e80f-44a1-ae1d-96804784f807") };
        //    Voter voter10 = new Voter { Id = Guid.Parse("cd285ce6-b6e9-4933-8e07-6d50e0251788"), FirstName = "Maria", LastName = "Hafsa", StateId = Guid.Parse("33f88529-1a04-4d5e-84dc-135646948bc0") };
        //    Voter voter11 = new Voter { Id = Guid.Parse("d454ae67-913e-4f7b-82c1-4da539737199"), FirstName = "Brahim", LastName = "Roudjai", StateId = Guid.Parse("33f88529-1a04-4d5e-84dc-135646948bc0") };
        //    Voter voter12 = new Voter { Id = Guid.Parse("18537eea-e8f8-49cd-8ddf-10646c9a9f21"), FirstName = "Amine", LastName = "Brahem", StateId = Guid.Parse("33f88529-1a04-4d5e-84dc-135646948bc0") };
        //    Voter voter13 = new Voter { Id = Guid.Parse("d789a80a-493f-4248-90b0-35edab1c3c63"), FirstName = "Azzedine", LastName = "Brahmi", StateId = Guid.Parse("3138e047-e80f-44a1-ae1d-96804784f807") };
        //    Voter voter14 = new Voter { Id = Guid.Parse("e61f81ab-a991-4298-b668-c973a5b75dc9"), FirstName = "Larbi", LastName = "Fkaier", StateId = Guid.Parse("32065802-7f25-47bc-8987-dd4fdb2829c4") };
        //    Voter voter15 = new Voter { Id = Guid.Parse("1d6d6442-25ac-4cf9-aa44-0d91622a4927"), FirstName = "Djamel", LastName = "Tahraoui", StateId = Guid.Parse("32065802-7f25-47bc-8987-dd4fdb2829c4") };
        //    Voter voter16 = new Voter { Id = Guid.Parse("71ad22cf-168a-40be-9867-4a7ebe34c339"), FirstName = "Mohamed", LastName = "Chikhi", StateId = Guid.Parse("918333ff-ac52-4424-a3d9-09bee4c39b91") };*/
        //    /*
        //    modelBuilder.Entity<Voter>(entity =>
        //    {
        //        entity.HasData(voter1);
        //        entity.HasOne(d => d.State);
        //    });
        //    */
        //    /*modelBuilder.Entity<Voter>(v =>
        //    {
        //        v.HasData(voter1);
        //        v.OwnsOne(v => v.State).HasData(Oran);
        //    }*/
        //        /*,
        //        voter2,
        //        voter3,
        //        voter4,
        //        voter5,
        //        voter6,
        //        voter7,
        //        voter8,
        //        voter9,
        //        voter10,
        //        voter11,
        //        voter12,
        //        voter13,
        //        voter14,
        //        voter15,
        //        voter16
        //        );*/

        //}


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
