using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class ElectionVoterRepository:IRepository<ElectionVoter>
    {
        //registering the needed services
        protected readonly VoteDBContext _dbContext;
        private readonly DbSet<ElectionVoter> _dbSet;

        //Injecting the services thru the constructor (Constructor Dependency Injection)
        public ElectionVoterRepository(VoteDBContext voteDBContext)
        {
            //Injecting the services thru the constructor (Constructor Dependency Injection)
            _dbContext = voteDBContext;
            _dbSet = _dbContext.Set<ElectionVoter>();

        }

        public void Add(ElectionVoter item)
        {
            _dbContext.Add(item);
            _dbContext.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            _dbContext.Remove(GetById(Id));
        }

        public void Edit(Guid Id, ElectionVoter item)
        {
            var myItem = GetById(Id);
            myItem.ElectionId = item.ElectionId;
            myItem.VoterId = item.VoterId;
            _dbContext.SaveChanges();
        }

        public IList<ElectionVoter> GetAll()
        {
            return _dbSet.ToList();
        }

        public ElectionVoter GetById(Guid Id)
        {
            return _dbSet.SingleOrDefault(e=>e.Id == Id);
        }
    }
}
