using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _dbSet.ToList();
        }

        public Election GetById(Guid Id)
        {
            return _dBContext.Election.SingleOrDefault(e=>e.Id == Id);
        }
    }
}
