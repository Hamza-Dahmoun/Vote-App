using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.Models.Helpers;

namespace WebApplication1.Models.Repositories
{
    public class CandidateRepository : IRepository<Candidate>
    {
        //Registering the services needed
        protected readonly VoteDBContext _dBContext;
        private readonly DbSet<Candidate> _dbSet;

        public CandidateRepository(VoteDBContext dBContext)
        {
            //Injecting the dependencies (services) needed (Constructor Dependency Injection)
            _dBContext = dBContext;
            _dbSet = _dBContext.Set<Candidate>();
        }

        public int Add(Candidate item)
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
                _dBContext.Remove(this.GetById(Id));
                return _dBContext.SaveChanges();
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public int Edit(Guid Id, Candidate item)
        {
            throw new NotImplementedException();
        }

        public IList<Candidate> GetAll()
        {
            try
            {
                //use eager loading to bring State data 
                return _dbSet/*.Include(c => c.State)*/.Include(c=>c.Election).Include(c => c.Votes)/*.Include(c => c.VoterBeing)*/.ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
            
            //return _dbSet.ToList();
        }

        public IList<Candidate> GetAllReadOnly()
        {
            try
            {
                //use eager loading to bring State data 
                return _dbSet/*.Include(c => c.State)*/.Include(c => c.Election).Include(c => c.Votes)/*.Include(c => c.VoterBeing)*/.AsNoTracking().ToList();
            }
            catch (Exception E)
            {
                throw E;
            }

            //return _dbSet.ToList();
        }

        public List<Candidate> GetAllFiltered(Expression<Func<Candidate, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            try
            {
                //use eager loading to bring other tables data 
                return _dbSet.Where(predicate)/*.Include(c => c.State)*/.Include(c => c.Election).Include(c => c.Votes)/*.Include(c => c.VoterBeing)*/.ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }
        public List<Candidate> GetAllFilteredReadOnly(Expression<Func<Candidate, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            try
            {
                //use eager loading to bring other tables data 
                return _dbSet.Where(predicate)/*.Include(c => c.State)*/.Include(c => c.Election).Include(c => c.Votes)/*.Include(c => c.VoterBeing)*/.AsNoTracking().ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public PagedResult<Candidate> GetAllFilteredPaged(Expression<Func<Candidate, bool>> predicate, string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            throw new NotImplementedException();
        }

        public PagedResult<Candidate> GetAllFilteredPagedReadOnly(Expression<Func<Candidate, bool>> predicate, string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            throw new NotImplementedException();
        }

        public PagedResult<Candidate> GetAllPaged(string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            throw new NotImplementedException();
        }
        
        public PagedResult<Candidate> GetAllPagedReadOnly(string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            throw new NotImplementedException();
        }

        public Candidate GetById(Guid Id)
        {
            try
            {
                //use eager loading to bring State data and Votes data and VoterBeing data too
                return _dbSet.Include(c => c.Election)/*.Include(c=>c.State)*/.Include(c => c.Votes)/*.Include(c => c.VoterBeing)*/.SingleOrDefault(c => c.Id == Id);
            }
            catch (Exception E)
            {
                throw E;
            }            
        }
        
        public Candidate GetByIdReadOnly(Guid Id)
        {
            try
            {
                //use eager loading to bring State data and Votes data and VoterBeing data too
                return _dbSet.Include(c => c.Election)/*.Include(c=>c.State)*/.Include(c => c.Votes)/*.Include(c => c.VoterBeing)*/.AsNoTracking().SingleOrDefault(c => c.Id == Id);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public Candidate GetOneFiltered(Expression<Func<Candidate, bool>> predicate)
        {
            try
            {
                //use eager loading to bring State data and Votes data and VoterBeing data too
                return _dbSet.Include(c => c.Election)/*.Include(c=>c.State)*/.Include(c => c.Votes)/*.Include(c => c.VoterBeing)*/.SingleOrDefault(predicate);
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public Candidate GetOneFilteredReadOnly(Expression<Func<Candidate, bool>> predicate)
        {
            try
            {
                //use eager loading to bring State data and Votes data and VoterBeing data too
                return _dbSet.Include(c => c.Election)/*.Include(c=>c.State)*/.Include(c => c.Votes)/*.Include(c => c.VoterBeing)*/.AsNoTracking().SingleOrDefault(predicate);
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

        //public Candidate GetByVoterBeingId(Guid VoterBeingId)
        //{
        //    Candidate candidate = _dBContext.Candidate.Include(c => c.VoterBeing).SingleOrDefault(c => c.VoterBeing.Id == VoterBeingId);
        //    //return _dbSet.SingleOrDefault(c => c.VoterBeing.Id == VoterBeingId);
        //    return candidate;
        //}
    }
}
