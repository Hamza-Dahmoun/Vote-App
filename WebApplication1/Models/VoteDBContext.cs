﻿using Microsoft.EntityFrameworkCore;
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
            modelBuilder.Entity<State>().HasData(
                new State { Id = Guid.Parse("fa9b72ee-dfcc-4353-b195-5c2855b1343f"), Name = "Oran" },
                new State { Id = Guid.Parse("32065802-7f25-47bc-8987-dd4fdb2829c4"), Name = "Meca" },
                new State { Id = Guid.Parse("3138e047-e80f-44a1-ae1d-96804784f807"), Name = "Cairo" },
                new State { Id = Guid.Parse("33f88529-1a04-4d5e-84dc-135646948bc0"), Name = "ElQuds" }
                );

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
