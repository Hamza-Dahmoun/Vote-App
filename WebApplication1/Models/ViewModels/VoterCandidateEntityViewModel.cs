using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    [NotMapped]
    public class VoterCandidateEntityViewModel
    {
        //this is going to be used to store voter and candidate data, so that we can return a list of candidates of an election
        //followed by a list of voters that are not candodates of an eelection ... for user to edit the election to be able to
        //remove candidates and select new candodates

        public string VoterId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "State")]
        public string StateName { get; set; }
        public string CandidateId { get; set; }
    }
}
