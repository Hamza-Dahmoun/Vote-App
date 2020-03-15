using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    public class VoterStructureViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public Structure Structure { get; set; }

        //the below list of structures is used to be displayed when adding a new voter
        public IList<Structure> Structures { get; set; }

    }
}
