using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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



        public void Add(Voter item)
        {
            _dbContext.Add(item);
            _dbContext.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            _dbContext.Remove(this.GetById(Id));
            _dbContext.SaveChanges();
        }

        public void Edit(Guid Id, Voter item)
        {
            var myVoter = GetById(Id);
            myVoter.FirstName = item.FirstName;
            myVoter.LastName = item.LastName;
            myVoter.State = item.State;
            _dbContext.SaveChanges();
        }

        public IList<Voter> GetAll()
        {
            //use eager loading to bring State data too
            return _dbSet.Include(v=>v.State).ToList();
        }

        public List<Voter> GetAllFiltered(Expression<Func<Voter, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            try
            {
                //use eager loading to bring other tables data 
                return _dbSet.Where(predicate).Include(v => v.State).ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<Voter> GetAllFilteredPaged(Expression<Func<Voter, bool>> predicate, string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            //this function returns 'maxRows' row of voters, skipping 'startRowIndex', ordered by the column 'orderBy' and direction of ordering
            //according to 'orderDirection' .... all filtered according to the expression 'predicate'
            try
            {
                //I don't know the name of the property I am going to sort with, so I'll use Refection API
                //to get the name of the property then I'll use it to order the list

                System.Reflection.PropertyInfo propertyName = typeof(Voter).GetProperty(orderBy);

                if (orderDirection == "asc")
                {
                    //use eager loading to bring other tables data 
                    return _dbSet.Where(predicate).OrderBy(v => propertyName.GetValue(v)).Include(v => v.State).Skip(startRowIndex).Take(maxRows).ToList();
                }
                if (orderDirection == "disc")
                {
                    //use eager loading to bring other tables data 
                    return _dbSet.Where(predicate).OrderByDescending(v => propertyName.GetValue(v)).Include(v => v.State).Skip(startRowIndex).Take(maxRows).ToList();
                }
                //in case there is no ordering requested
                return _dbSet.Where(predicate).Include(v => v.State).Skip(startRowIndex).Take(maxRows).ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<Voter> GetAllPaged(string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            //this function returns 'maxRows' row of voters, skipping 'startRowIndex', ordered by the column 'orderBy' and direction of ordering
            //according to 'orderDirection'
            try
            {
                //I don't know the name of the property I am going to sort with, so I'll use Refection API
                //to get the name of the property then I'll use it to order the list

                System.Reflection.PropertyInfo propertyName = typeof(Voter).GetProperty(orderBy);
                
                if (orderDirection == "asc")
                {
                    //use eager loading to bring other tables data 
                    return _dbSet.OrderBy(v => propertyName.GetValue(v)).Include(v => v.State).Skip(startRowIndex).Take(maxRows).ToList();
                }
                if (orderDirection == "disc")
                {
                    //use eager loading to bring other tables data 
                    return _dbSet.OrderByDescending(v => propertyName.GetValue(v)).Include(v => v.State).Skip(startRowIndex).Take(maxRows).ToList();
                }
                //in case there is no ordering requested
                return _dbSet.Include(v => v.State).Skip(startRowIndex).Take(maxRows).ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public Voter GetById(Guid Id)
        {
            //use eager loading to bring State data 
            return _dbSet.Include(v => v.State).SingleOrDefault(v=>v.Id == Id);
        }

        public Voter GetOneFiltered(Expression<Func<Voter, bool>> predicate)
        {
            return _dbSet.Include(v => v.State).SingleOrDefault(predicate);
        }
    }
}
