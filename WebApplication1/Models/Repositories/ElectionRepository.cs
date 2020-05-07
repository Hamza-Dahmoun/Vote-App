using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
