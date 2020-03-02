using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    public class PersonViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StructureName { get; set; }
        public string StructureLevel { get; set; }
        public string hasVoted { get; set; }
    }
}
