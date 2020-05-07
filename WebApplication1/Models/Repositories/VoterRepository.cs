using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class VoterRepository : IRepository<Voter>
    {
        //Registering the services needed
        protected readonly VoteDBContext _dbContext;
        private readonly DbSet<Voter> _dbSet;

        public VoterRepository(VoteDBContext dbContext)
        {
            //Injecting the dependencies (services) needed (Constructor Dependency Injection)
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Voter>();
        }



        public void Add(Voter item)
        {
            _dbContext.Add(item);
            _dbContext.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            _dbContext.Remove(this.GetById(Id));
            _dbContext.SaveChanges();
        }

        public void Edit(Guid Id, Voter item)
        {
            var myVoter = GetById(Id);
            myVoter.FirstName = item.FirstName;
            myVoter.LastName = item.LastName;
            myVoter.State = item.State;
            _dbContext.SaveChanges();
        }

        public IList<Voter> GetAll()
        {
            //use eager loading to bring State data too
            return _dbSet.Include(v=>v.State).ToList();
        }

        public List<Voter> GetAllFiltered(Expression<Func<Voter, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            throw new NotImplementedException();
        }

        public Voter GetById(Guid Id)
        {
            //use eager loading to bring State data 
            return _dbSet.Include(v => v.State).SingleOrDefault(v=>v.Id == Id);
        }

        public Voter GetOneFiltered(Expression<Func<Voter, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
