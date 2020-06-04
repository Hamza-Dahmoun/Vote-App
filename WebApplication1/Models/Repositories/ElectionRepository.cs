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
        public void Add(Election item)
        {
            _dBContext.Add(item);
            _dBContext.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            _dBContext.Remove(GetById(Id));
            _dBContext.SaveChanges();
        }

        public void Edit(Guid Id, Election item)
        {
            var myElection = GetById(Id);
            myElection.Name = item.Name;
            myElection.StartDate = item.StartDate;
            myElection.HasNeutral = item.HasNeutral;
            myElection.DurationInDays = item.DurationInDays;
            _dBContext.SaveChanges();
        }

        public IList<Election> GetAll()
        {
            //use eager loading to bring Candidaates data too
            return _dbSet.Include(e => e.Candidates).Include(e => e.Votes).ToList();
        }

        public List<Election> GetAllFiltered(Expression<Func<Election, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            try
            {
                //use eager loading to bring other tables data 
                return _dbSet.Where(predicate).Include(e => e.Votes).Include(e => e.Candidates).ToList();
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
                /* WHEN I TRIED SORTING I HAD THE BELOW ERROR
                 System.InvalidOperationException : 'The LINQ expression 'DbSet<Voter>.OrderBy(v => __propertyName_0.GetValue(v))' 
                 could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation
                 explicitly by inserting a call to either AsEnumerable(), AsAsyncEnumerable(), ToList(), or ToListAsync(). 
                 See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.'

                 After looking for this error in the internet but couldn't find a solution to this problem, and the linq query is too simple
                 to change and write it in another way. So for now I'll keep everything as it is now, but remove Ordering from the linq query.
                 So the new methods GetAllPaged() and GetAllFilteredPaged() will become like this:
                 */

                /*
                //I don't know the name of the property I am going to sort with, so I'll use Refection API
                //to get the name of the property then I'll use it to order the list

                System.Reflection.PropertyInfo propertyName = typeof(Voter).GetProperty(orderBy);

                if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrEmpty(orderDirection))
                {
                    if (orderDirection == "asc")
                    {
                        //use eager loading to bring other tables data 
                        return _dbSet.Where(predicate).OrderBy(v => propertyName.GetValue(v)).Include(v => v.State).Skip(startRowIndex).Take(maxRows).ToList();
                    }
                    if (orderDirection == "desc")
                    {
                        //use eager loading to bring other tables data 
                        return _dbSet.Where(predicate).OrderByDescending(v => propertyName.GetValue(v)).Include(v => v.State).Skip(startRowIndex).Take(maxRows).ToList();
                    }
                }*/

                //in case there is no ordering requested
                List<Election> elections = _dbSet.Where(predicate)./*Include(v => v.Candidates).*/ToList();
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
                /* WHEN I TRIED SORTING I HAD THE BELOW ERROR
                 System.InvalidOperationException : 'The LINQ expression 'DbSet<Voter>.OrderBy(v => __propertyName_0.GetValue(v))' 
                 could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation
                 explicitly by inserting a call to either AsEnumerable(), AsAsyncEnumerable(), ToList(), or ToListAsync(). 
                 See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.'

                 After looking for this error in the internet but couldn't find a solution to this problem, and the linq query is too simple
                 to change and write it in another way. So for now I'll keep everything as it is now, but remove Ordering from the linq query.
                 So the new methods GetAllPaged() and GetAllFilteredPaged() will become like this:
                 */

                /*
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
                }*/
                //in case there is no ordering requested
                var elections = _dbSet
                    /*.Include(e => e.Candidates).Select(e => new { e.Id, e.Name, e.StartDate, e.DurationInDays, e.HasNeutral, e.Candidates.Count})*/.ToList();
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
            return _dbSet.Include(e=>e.Candidates).Include(e => e.Votes).SingleOrDefault(e=>e.Id == Id);
        }

        public Election GetOneFiltered(Expression<Func<Election, bool>> predicate)
        {
            return _dbSet.Include(e => e.Candidates).Include(e => e.Votes).SingleOrDefault(predicate); 
        }
    }
}
