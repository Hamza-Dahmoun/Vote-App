using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class ElectionCandidateRepository : IRepository<ElectionCandidate>
    {
        //Registering the needed services
        protected readonly VoteDBContext _dbContext;
        private readonly DbSet<ElectionCandidate> _dbSet;

        //Injecting the services thru the constructor (Constructor Dependency Injection)
        public ElectionCandidateRepository(VoteDBContext voteDBContext)
        {//Injecting the services thru the constructor (Constructor Dependency Injection)
            _dbContext = voteDBContext;
            _dbSet = _dbContext.Set<ElectionCandidate>();
        }

        public void Add(ElectionCandidate item)
        {
            _dbContext.Add(item);
            _dbContext.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            _dbContext.Remove(GetById(Id));
            _dbContext.SaveChanges();
        }

        public void Edit(Guid Id, ElectionCandidate item)
        {
            var myItem = GetById(Id);
            myItem.ElectionId = item.ElectionId;
            myItem.CandidateId = item.CandidateId;
            _dbContext.SaveChanges();
        }

        public IList<ElectionCandidate> GetAll()
        {
            return _dbSet.ToList();
        }

        public ElectionCandidate GetById(Guid Id)
        {
            return _dbSet.SingleOrDefault(e=>e.Id == Id);
        }
    }
}
