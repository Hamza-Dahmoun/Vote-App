using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    [NotMapped]
    public class StateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LevelName { get; set; }
        public int LevelValue { get; set; }
    }
}
