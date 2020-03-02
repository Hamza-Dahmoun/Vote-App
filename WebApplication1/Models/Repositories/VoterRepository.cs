using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.Repositories;

namespace WebApplication1.Models.Repositories
{
    public class VoterRepository : IRepository<Voter>
    {
        public void Add(Voter item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Guid id, Voter item)
        {
            throw new NotImplementedException();
        }

        public IList<Voter> GetAll()
        {
            throw new NotImplementedException();
        }

        public Voter GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
