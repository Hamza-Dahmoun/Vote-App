using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class VoterRepository : IRepository<Voter>
    {
        protected readonly VoteDBContext _dbContext;
        private readonly DbSet<Voter> _dbSet;

        public VoterRepository(VoteDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Voter>();
        }



        public void Add(Voter item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Guid Id, Voter item)
        {
            throw new NotImplementedException();
        }

        public IList<Voter> GetAll()
        {
            return _dbSet.ToList();
        }

        public Voter GetById(Guid Id)
        {
            return _dbSet.Find(Id);
        }
    }
}
