using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public void Add(Candidate item)
        {
            _dBContext.Add(item);
            _dBContext.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            _dBContext.Remove(this.GetById(Id));
            _dBContext.SaveChanges();
        }

        public void Edit(Guid Id, Candidate item)
        {
            throw new NotImplementedException();
        }

        public IList<Candidate> GetAll()
        {
            try
            {
                //use eager loading to bring State data 
                return _dbSet/*.Include(c => c.State)*/.Include(c=>c.Election).Include(c => c.Votes).Include(c => c.VoterBeing).ToList();
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
                return _dbSet.Where(predicate)/*.Include(c => c.State)*/.Include(c => c.Election).Include(c => c.Votes).Include(c => c.VoterBeing).ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public Candidate GetById(Guid Id)
        {
            //use eager loading to bring State data and Votes data and VoterBeing data too
            return _dbSet.Include(c=>c.Election)/*.Include(c=>c.State)*/.Include(c=>c.Votes).Include(c => c.VoterBeing).SingleOrDefault(c=>c.Id == Id);
        }

        public Candidate GetOneFiltered(Expression<Func<Candidate, bool>> predicate)
        {
            //use eager loading to bring State data and Votes data and VoterBeing data too
            return _dbSet.Include(c => c.Election)/*.Include(c=>c.State)*/.Include(c => c.Votes).Include(c => c.VoterBeing).SingleOrDefault(predicate);
        }

        //public Candidate GetByVoterBeingId(Guid VoterBeingId)
        //{
        //    Candidate candidate = _dBContext.Candidate.Include(c => c.VoterBeing).SingleOrDefault(c => c.VoterBeing.Id == VoterBeingId);
        //    //return _dbSet.SingleOrDefault(c => c.VoterBeing.Id == VoterBeingId);
        //    return candidate;
        //}
    }
}
