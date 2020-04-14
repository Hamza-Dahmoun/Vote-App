using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ElectionVoter
    {
        /*
             * FROM DOCS.MICROSOFT
             Many-to-many relationships without an entity class to represent the join table are not yet supported.
             However, you can represent a many-to-many relationship by including an entity class for the join table
             and mapping two separate one-to-many relationships.
             */
        public Guid ElectionId { get; set; }
        public Election Election { get; set; }
        public Guid VoterId { get; set; }
        public Voter Voter { get; set; }
    }
}
