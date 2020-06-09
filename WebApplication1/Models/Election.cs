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
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        //public ICollection<ElectionVoter> ElectionVoters { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
        public ICollection<Vote> Votes { get; set; }

        [Required(ErrorMessage = "Required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Required")]
        [Range(1, 5, ErrorMessage = "An Election must last at least one day and less than five days {1}")]
        public int DurationInDays { get; set; }
        public bool HasNeutral { get; set; }
        /*public Guid NeutralCandidateID { get; set; }*/
    }
}
