using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    [NotMapped]
    public class DashboardViewModel
    {
        public int NbElections { get; set; }
        public int NbCandidates { get; set; }
        public int NbVoters { get; set; }
        public int NbVotes { get; set; }
        //public double ParticipationRate { get; set; }
        public List<CandidateViewModel> Candidates { get; set; }
        //public bool UserHasVoted { get; set; }
        public Guid CurrentElectionId { get; set; }
    }
}
