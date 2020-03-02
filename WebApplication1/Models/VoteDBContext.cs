using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class VoteDBContext:DbContext
    {
        public VoteDBContext():base()
        {

        }
        public VoteDBContext(DbContextOptions<VoteDBContext> options):base(options)
        {

        }


        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<Voter> Voter { get; set; }
        public virtual DbSet<Vote> Vote { get; set; }
        public virtual DbSet<Structure> Structure { get; set; }
        public virtual DbSet<StructureLevel> StructureLevel { get; set; }
    }
}
