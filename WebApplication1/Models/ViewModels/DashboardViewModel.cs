using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    [NotMapped]
    public class DashboardViewModel
    {
        [Display(Name = "Elections")]
        public int NbElections { get; set; }
        [Display(Name = "Candidates")]
        public int NbCandidates { get; set; }
        [Display(Name = "Voters")]
        public int NbVoters { get; set; }
        [Display(Name = "Votes")]
        public int NbVotes { get; set; }
    }
}
