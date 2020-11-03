using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    [NotMapped]
    public class CandidateViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Is Neutral Opinion?")]
        public bool isNeutralOpinion { get; set; }
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Display(Name = "State")]
        public string StateName { get; set; }
        [Display(Name = "Votes")]
        public int VotesCount { get; set; }
    }
}
