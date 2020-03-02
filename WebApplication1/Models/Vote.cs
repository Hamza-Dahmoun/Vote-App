using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Vote
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime datetime { get; set; }
        [Required]
        public Voter Voter { get; set; }
        [Required]
        public Candidate Candidate { get; set; }

    }
}
