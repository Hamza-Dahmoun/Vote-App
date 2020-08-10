using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Candidate/*:Person*/
    {
        //a Candidate may have a collection of Votes
        //a Candidate should in a relation btween the Voter and an Election. Voter can be candidate in many Elections.
        //an Election can have many Voters as Candidates.

        public Guid Id { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public Election Election { get; set; }
        public Guid ElectionId { get; set; }
        public Voter VoterBeing { get; set; }
        public Guid VoterBeingId { get; set; }

        public bool isNeutralOpinion { get; set; }
    }
}
