using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.Models.Helpers;

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



        public int Add(Voter item)
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

        public int Edit(Guid Id, Voter item)
        {
            try
            {
                var myVoter = GetById(Id);
                myVoter.FirstName = item.FirstName;
                myVoter.LastName = item.LastName;
                myVoter.State = item.State;
                myVoter.StateId = item.StateId;
                return _dbContext.SaveChanges();
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public IList<Voter> GetAll()
        {
            try
            {
                //use eager loading to bring State data too
                return _dbSet.Include(v => v.State).AsNoTracking().ToList();
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public List<Voter> GetAllFiltered(Expression<Func<Voter, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            try
            {
                //use eager loading to bring other tables data 
                return _dbSet.Where(predicate).Include(v => v.State).AsNoTracking().ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public PagedResult<Voter> GetAllFilteredPaged(Expression<Func<Voter, bool>> predicate, string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            //this function returns 'maxRows' row of voters, skipping 'startRowIndex', ordered by the column 'orderBy' and direction of ordering
            //according to 'orderDirection' .... all filtered according to the expression 'predicate'
            try
            {
                //in case there is no ordering requested
                List<Voter> voters = _dbSet.Where(predicate).Include(v => v.State).AsNoTracking().ToList();
                int totalCount = voters.Count;
                voters = voters.Skip(startRowIndex).Take(maxRows).ToList();
                PagedResult<Voter> p = new PagedResult<Voter>(voters, totalCount);
                return p;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public PagedResult<Voter> GetAllPaged(string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            //this function returns 'maxRows' row of voters, skipping 'startRowIndex', ordered by the column 'orderBy' and direction of ordering
            //according to 'orderDirection'
            try
            {
                //in case there is no ordering requested
                var voters = _dbSet.Include(v => v.State).AsNoTracking().ToList();
                int totalCount = voters.Count;
                voters = voters.Skip(startRowIndex).Take(maxRows).ToList();
                PagedResult<Voter> p = new PagedResult<Voter>(voters, totalCount);
                return p;
            }
            catch (Exception E)
            {
                throw E;
            }

        }

        public Voter GetById(Guid Id)
        {
            try
            {
                //use eager loading to bring State data 
                return _dbSet.Include(v => v.State).SingleOrDefault(v => v.Id == Id);
            }
            catch (Exception E)
            {
                throw E;
            }            
        }
        public Voter GetByIdReadOnly(Guid Id)
        {
            try
            {
                //use eager loading to bring State data 
                return _dbSet.Include(v => v.State).AsNoTracking().SingleOrDefault(v => v.Id == Id);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public Voter GetOneFiltered(Expression<Func<Voter, bool>> predicate)
        {
            try
            {
                return _dbSet.Include(v => v.State).AsNoTracking().SingleOrDefault(predicate);
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

        public int CountAllFiltered(Expression<Func<Voter, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
