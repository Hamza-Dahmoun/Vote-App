using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    [NotMapped]
    public class ElectionViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Duration (days)")]
        public int DurationInDays { get; set; }
        [Display(Name = "Is Neutral Opinion?")]
        public bool HasNeutral { get; set; }
        [Display(Name = "Candidates")]
        public int NumberOfCandidates { get; set; }
        [Display(Name = "Voters")]
        public int NumberOfVoters { get; set; }
        [Display(Name = "Votes")]
        public int NumberOfVotes { get; set; }
        [Display(Name = "Winner")]
        public string WinnerfullName { get; set; }
    }
}
