using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Election
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        //public ICollection<Voter> Voters{ get; set; }
        //public ICollection<Candidate> Candidates { get; set; }
        //public ICollection<Vote> Votes{ get; set; } THIS IS ACCESSIBLE THRU VOTERS
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int DurationInDays { get; set; }
        public bool HasNeutral { get; set; }
        public Guid NeutralCandidateID { get; set; }
    }
}
