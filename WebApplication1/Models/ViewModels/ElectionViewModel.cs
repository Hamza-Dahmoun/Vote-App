using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    [NotMapped]
    public class ElectionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationInDays { get; set; }
        public bool HasNeutral { get; set; }
        public int NumberOfCandidates { get; set; }
        public int NumberOfVoters { get; set; }
    }
}
