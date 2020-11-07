using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.Models.Helpers;

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

        public int Add(Vote item)
        {
            try
            {
                _dbContext.Add(item);
                return _dbContext.SaveChanges();
            }
            catch(Exception E)
            {
                throw E;
            }            
        }

        public int Delete(Guid Id)
        {
            try
            {
                _dbContext.Remove(this.GetById(Id));
                return _dbContext.SaveChanges();
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public int Edit(Guid Id, Vote item)
        {
            throw new NotImplementedException();
        }

        public IList<Vote> GetAll()
        {
            try
            {
                //use eager loading to bring Candidate data too
                return _dbSet.Include(v => v.Candidate).AsNoTracking().ToList();
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public List<Vote> GetAllFiltered(Expression<Func<Vote, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            try
            {
                //use eager loading to bring other tables data 
                return _dbSet.Where(predicate).Include(v => v.Candidate).AsNoTracking().ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public PagedResult<Vote> GetAllFilteredPaged(Expression<Func<Vote, bool>> predicate, string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            throw new NotImplementedException();
        }
        
        public PagedResult<Vote> GetAllPaged(string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            throw new NotImplementedException();
        }

        public Vote GetById(Guid Id)
        {
            try
            {
                //use eager loading to bring Candidate data too
                return _dbSet.Include(v => v.Candidate).SingleOrDefault(v => v.Id == Id);
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public Vote GetByIdReadOnly(Guid Id)
        {
            try
            {
                //use eager loading to bring Candidate data too
                return _dbSet.Include(v => v.Candidate).AsNoTracking().SingleOrDefault(v => v.Id == Id);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public Vote GetOneFiltered(Expression<Func<Vote, bool>> predicate)
        {
            try
            {
                return _dbSet.Include(v => v.Candidate).AsNoTracking().SingleOrDefault(predicate);
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        
        public int CountAll()
        {
            int count = 0;
            count = _dbSet.AsNoTracking().Count();
            return count;
        }

        public int CountAllFiltered(Expression<Func<Vote, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
