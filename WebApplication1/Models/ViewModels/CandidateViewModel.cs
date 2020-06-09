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
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "State")]
        public string StateName { get; set; }
        [Display(Name = "Votes")]
        public int VotesCount { get; set; }
        //public string hasVoted { get; set; }
        /*public Guid electionId { get; set; }*/
    }
}
