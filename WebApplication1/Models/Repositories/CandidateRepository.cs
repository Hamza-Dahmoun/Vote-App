using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class CandidateRepository : IRepository<Candidate>
    {
        protected readonly VoteDBContext _dBContext;
        private readonly DbSet<Candidate> _dbSet;
        public CandidateRepository(VoteDBContext dBContext)
        {
            _dBContext = dBContext;
            _dbSet = _dBContext.Set<Candidate>();
        }

        public void Add(Candidate item)
        {
            throw new NotImplementedException();
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
            //use eager loading to bring Structure data and Level data too
            //return _dbSet.Include(c=>c.Structure).Include(c=>c.Structure.Level).ToList();
            
            return _dbSet.ToList();
        }

        public Candidate GetById(Guid Id)
        {
            //use eager loading to bring Structure data and Level data and Votes data and VoterBeing data too
            return _dbSet.Include(c=>c.Structure).Include(c=>c.Structure.Level).Include(c=>c.Votes).Include(c => c.VoterBeing).SingleOrDefault(c=>c.Id == Id);
        }

        //public Candidate GetByVoterBeingId(Guid VoterBeingId)
        //{
        //    Candidate candidate = _dBContext.Candidate.Include(c => c.VoterBeing).SingleOrDefault(c => c.VoterBeing.Id == VoterBeingId);
        //    //return _dbSet.SingleOrDefault(c => c.VoterBeing.Id == VoterBeingId);
        //    return candidate;
        //}
    }
}
