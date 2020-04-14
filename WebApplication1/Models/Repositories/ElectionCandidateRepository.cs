using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class ElectionCandidateRepository : IRepository<ElectionCandidate>
    {
        public void Add(ElectionCandidate item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Guid Id, ElectionCandidate item)
        {
            throw new NotImplementedException();
        }

        public IList<ElectionCandidate> GetAll()
        {
            throw new NotImplementedException();
        }

        public ElectionCandidate GetById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
