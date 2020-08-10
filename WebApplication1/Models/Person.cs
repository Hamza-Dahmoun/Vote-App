using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Person
    {//a person is related to one 

        public Guid Id { get; set; }
        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "Required")]
        
        public string FirstName { get; set; }
        [Display(Name = "LastName")]
        [Required(ErrorMessage = "Required")]
        
        public string LastName { get; set; }
        [Display(Name = "State")]
        
        public State State { get; set; }

        public Guid UserId { get; set; }        
    }
}
