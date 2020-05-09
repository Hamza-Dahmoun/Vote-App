using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class VoteRepository:IRepository<Vote>
    {
        //Registering the services needed
        protected readonly VoteDBContext _dbContext;
        private readonly DbSet<Vote> _dbSet;

        public VoteRepository(VoteDBContext dbContext)
        {
            //Injecting the dependencies (services) needed (Constructor Dependency Injection)
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Vote>();
        }

        public void Add(Vote item)
        {
            _dbContext.Add(item);
            _dbContext.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Guid Id, Vote item)
        {
            throw new NotImplementedException();
        }

        public IList<Vote> GetAll()
        {
            //use eager loading to bring Candidate data too
            return _dbSet.Include(v => v.Candidate).ToList();
        }

        public List<Vote> GetAllFiltered(Expression<Func<Vote, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            try
            {
                //use eager loading to bring other tables data 
                return _dbSet.Where(predicate).Include(v => v.Candidate).ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<Vote> GetAllFilteredPaged(Expression<Func<Vote, bool>> predicate, string orderBy, int startRowIndex = 0, int maxRows = 10)
        {
            throw new NotImplementedException();
        }

        public List<Vote> GetAllPaged(string orderBy, int startRowIndex = 0, int maxRows = 10)
        {
            throw new NotImplementedException();
        }

        public Vote GetById(Guid Id)
        {
            //use eager loading to bring Candidate data too
            return _dbSet.Include(v => v.Candidate).SingleOrDefault(v=>v.Id == Id);
        }

        public Vote GetOneFiltered(Expression<Func<Vote, bool>> predicate)
        {
            return _dbSet.Include(v => v.Candidate).SingleOrDefault(predicate);
        }
    }
}
