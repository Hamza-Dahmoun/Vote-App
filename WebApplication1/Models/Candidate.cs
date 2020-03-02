using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Candidate
    {
        public ICollection<Vote> Votes { get; set; }
    }
}
