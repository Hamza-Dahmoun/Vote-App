using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Voter : Person
    {
        public Vote Vote { get; set; }

        public bool hasVoted()
        {
            if (this.Vote != null)
                return true;
            return false;
        }
    }
}
