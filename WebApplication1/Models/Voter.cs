using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Voter : Person
    {//A Voter is a person with a collection of votes
        public ICollection<Vote> Votes { get; set; }
        //public ICollection<ElectionVoter> ElectionVoters { get; set; }

        public bool hasVoted()
        {
            if (this.Votes != null && this.Votes.Count > 0)
                return true;
            return false;
        }
    }
}
