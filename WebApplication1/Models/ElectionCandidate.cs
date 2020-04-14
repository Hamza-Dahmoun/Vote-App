using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ElectionCandidate
    {
        /*
             * FROM DOCS.MICROSOFT
             Many-to-many relationships without an entity class to represent the join table are not yet supported.
             However, you can represent a many-to-many relationship by including an entity class for the join table
             and mapping two separate one-to-many relationships.
             
            SO I HAD TO DO THIS TO MANAGE THE RELATIONSHIP MANY TO MANY BETWEEN: ELECTION <--> CANDIDATE
             */
             [Key]
        public Guid Id { get; set; }
        public Guid ElectionId { get; set; }
        public Election Election { get; set; }
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
