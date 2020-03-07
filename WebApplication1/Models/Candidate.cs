using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Candidate:Person
    {//a candidate may have a collection of votes, and he is a voter
        public ICollection<Vote> Votes { get; set; }
        public Voter VoterBeing { get; set; }

        public bool isNeutralOpinion { get; set; }
    }
}
