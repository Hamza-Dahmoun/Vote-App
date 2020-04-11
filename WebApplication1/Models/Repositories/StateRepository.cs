using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class StateRepository : IRepository<State>
    {
        protected readonly VoteDBContext _dbContext;
        private readonly DbSet<State> _dbSet;
        public StateRepository(VoteDBContext dbContext)
        {//lets inject dbContext service
            _dbContext = dbContext;
            _dbSet = dbContext.Set<State>();
        }



        public void Add(State item)
        {
            _dbContext.Add(item);
            _dbContext.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var item = GetById(Id);
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
        }

        public void Edit(Guid Id, State item)
        {
            var myState = GetById(Id);
            myState.Name = item.Name;
            _dbContext.SaveChanges();
        }

        public IList<State> GetAll()
        {
            return _dbSet.ToList();
            //return _dbSet.ToList();
        }

        public State GetById(Guid Id)
        {
            return _dbSet.SingleOrDefault(s=>s.Id == Id);
            //return _dbSet.Find(Id);
        }
    }
}
