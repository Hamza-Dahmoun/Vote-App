using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    [NotMapped]
    public class VoterStateViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public Guid StateID { get; set; }

        //the below list of states is used to be displayed when adding a new voter
        public IList<State> States { get; set; }

    }
}
