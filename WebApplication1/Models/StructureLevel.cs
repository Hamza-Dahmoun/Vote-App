using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class StructureLevel
    {//a structurelevel define the level of the structure object, it may have a collection of structures
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int LevelValue { get; set; }
        public ICollection<State> Structures { get; set; }
    }
}