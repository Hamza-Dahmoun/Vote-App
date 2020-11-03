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
    }
}
