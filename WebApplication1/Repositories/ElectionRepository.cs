using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.Models.Helpers;

namespace WebApplication1.Models.Repositories
{
    public class ElectionRepository : IRepository<Election>
    {
        //Registering the services needed
        protected readonly VoteDBContext _dBContext;
        private readonly DbSet<Election> _dbSet;

        public ElectionRepository(VoteDBContext voteDBContext)
        {
            //Injecting the dependencies (services) needed (Constructor Dependency Injection)
            _dBContext = voteDBContext;
            _dbSet = _dBContext.Set<Election>();
        }
        public int Add(Election item)
        {
            try
            {
                _dBContext.Add(item);
                return _dBContext.SaveChanges();
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
                _dBContext.Remove(GetById(Id));
                return _dBContext.SaveChanges();
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public int Edit(Guid Id, Election item)
        {
            try
            {
                var myElection = GetById(Id);
                myElection.Name = item.Name;
                myElection.StartDate = item.StartDate;
                myElection.HasNeutral = item.HasNeutral;
                myElection.DurationInDays = item.DurationInDays;
                return _dBContext.SaveChanges();
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public IList<Election> GetAll()
        {
            try
            {
                //use eager loading to bring Candidaates data too
                return _dbSet.Include(e => e.Candidates).Include(e => e.Votes).AsNoTracking().ToList();
            }
            catch (Exception E)
            {
                throw E;
            }            
        }


        public List<Election> GetAllFiltered(Expression<Func<Election, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            try
            {
                //use eager loading to bring other tables data 
                return _dbSet.Where(predicate).Include(e => e.Votes).Include(e => e.Candidates).AsNoTracking().ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        
        public PagedResult<Election> GetAllFilteredPaged(Expression<Func<Election, bool>> predicate, string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            //this function returns 'maxRows' row of elections, skipping 'startRowIndex' rows, ordered by the column 'orderBy' and direction of ordering
            //according to 'orderDirection' .... all filtered according to the expression 'predicate'
            try
            {
                //in case there is no ordering requested
                List<Election> elections = _dbSet.Where(predicate).Include(v => v.Candidates).Include(v => v.Votes).AsNoTracking().ToList();
                int totalCount = elections.Count;
                elections = elections.Skip(startRowIndex).Take(maxRows).ToList();
                PagedResult<Election> p = new PagedResult<Election>(elections, totalCount);
                return p;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        
        public PagedResult<Election> GetAllPaged(string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            //this function returns 'maxRows' row of elections, skipping 'startRowIndex', ordered by the column 'orderBy' and direction of ordering
            //according to 'orderDirection'
            try
            {
                //in case there is no ordering requested
                var elections = _dbSet.Include(e => e.Candidates).Include(v => v.Votes).AsNoTracking().ToList();
                int totalCount = elections.Count;
                elections = elections.Skip(startRowIndex).Take(maxRows).ToList();
                PagedResult<Election> p = new PagedResult<Election>(elections, totalCount);
                return p;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        
        public Election GetById(Guid Id)
        {
            try
            {
                return _dbSet.Include(e => e.Candidates).Include(e => e.Votes).SingleOrDefault(e => e.Id == Id);
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public Election GetByIdReadOnly(Guid Id)
        {
            try
            {
                return _dbSet.Include(e => e.Candidates).Include(e => e.Votes).AsNoTracking().SingleOrDefault(e => e.Id == Id);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public Election GetOneFiltered(Expression<Func<Election, bool>> predicate)
        {
            try
            {
                return _dbSet.Include(e => e.Candidates).Include(e => e.Votes).AsNoTracking().SingleOrDefault(predicate);
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
        public int CountAllFiltered(Expression<Func<Election, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            try
            {
                return _dbSet.Count(predicate);
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
