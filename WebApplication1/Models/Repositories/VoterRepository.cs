using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class VoterRepository : IRepository<Voter>
    {
        protected readonly VoteDBContext _dbContext;
        private readonly DbSet<Voter> _dbSet;

        public VoterRepository(VoteDBContext dbContext)
        {
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
            throw new NotImplementedException();
        }

        public void Edit(Guid Id, Voter item)
        {
            throw new NotImplementedException();
        }

        public IList<Voter> GetAll()
        {
            //use eager loading to bring Structure data and Level data too
            return _dbSet.Include(v=>v.Structure).Include(v => v.Structure.Level).ToList();
        }

        public Voter GetById(Guid Id)
        {
            //use eager loading to bring Structure data and Level data too
            return _dbSet.Include(v => v.Structure).Include(v=> v.Structure.Level).SingleOrDefault(v=>v.Id == Id);
        }

        
    }
}
