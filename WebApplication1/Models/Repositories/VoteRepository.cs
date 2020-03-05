using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class VoteRepository:IRepository<Vote>
    {
        protected readonly VoteDBContext _dbContext;
        private readonly DbSet<Vote> _dbSet;

        public VoteRepository(VoteDBContext dbContext)
        {
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

        public Vote GetById(Guid Id)
        {
            //use eager loading to bring Candidate data too
            return _dbSet.Include(v => v.Candidate).SingleOrDefault(v=>v.Id == Id);
        }
    }
}
