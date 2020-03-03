using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    public class StructureViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LevelName { get; set; }
        public int LevelValue { get; set; }
    }
}
