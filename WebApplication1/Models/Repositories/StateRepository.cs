using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class StateRepository : IRepository<State>
    {
        //Registering the services needed
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

        public List<State> GetAllFiltered(Expression<Func<State, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            try
            {
                //use eager loading to bring other tables data 
                return _dbSet.Where(predicate).ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<State> GetAllFilteredPaged(Expression<Func<State, bool>> predicate, string orderBy, int startRowIndex = 0, int maxRows = 10)
        {
            throw new NotImplementedException();
        }

        public List<State> GetAllPaged(string orderBy, int startRowIndex = 0, int maxRows = 10)
        {
            throw new NotImplementedException();
        }

        public State GetById(Guid Id)
        {
            return _dbSet.SingleOrDefault(s=>s.Id == Id);
            //return _dbSet.Find(Id);
        }

        public State GetOneFiltered(Expression<Func<State, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }
    }
}
