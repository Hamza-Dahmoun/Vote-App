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
        {//I had to override this method bcuz when EF was trying to create the model he found one-to-one relationship Vote->Voter
            //and Voter->Vote, it was confused how to create foreign keys, so here I am clarifying it
            modelBuilder.Entity<Voter>()
                .HasOne(v => v.Vote)
                .WithOne(v => v.Voter)
                .HasForeignKey<Vote>(v=>v.VoterID);
        }

        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<Voter> Voter { get; set; }
        public virtual DbSet<Vote> Vote { get; set; }
        public virtual DbSet<Structure> Structure { get; set; }
        public virtual DbSet<StructureLevel> StructureLevel { get; set; }
        public DbSet<WebApplication1.Models.ViewModels.PersonViewModel> PersonViewModel { get; set; }
        public DbSet<WebApplication1.Models.ViewModels.StructureViewModel> StructureViewModel { get; set; }
        public DbSet<WebApplication1.Models.ViewModels.CandidateViewModel> CandidateViewModel { get; set; }
    }
}
