using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    [NotMapped]
    public class CandidateViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StructureName { get; set; }
        public string StructureLevel { get; set; }
        public int VotesCount { get; set; }
        public string hasVoted { get; set; }
    }
}
