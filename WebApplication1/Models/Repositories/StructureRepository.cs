using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class StructureRepository : IRepository<Structure>
    {
        protected readonly VoteDBContext _dbContext;
        private readonly DbSet<Structure> _dbSet;
        public StructureRepository(VoteDBContext dbContext)
        {//lets inject dbContext service
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Structure>();
        }



        public void Add(Structure item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Guid Id, Structure item)
        {
            throw new NotImplementedException();
        }

        public IList<Structure> GetAll()
        {
            //use eager loading to bring Level data too
            return _dbSet.Include(s => s.Level).ToList();
            //return _dbSet.ToList();
        }

        public Structure GetById(Guid Id)
        {
            //use eager loading to bring Level data too
            return _dbSet.Include(s=>s.Level).SingleOrDefault(s=>s.Id == Id);
            //return _dbSet.Find(Id);
        }
    }
}
