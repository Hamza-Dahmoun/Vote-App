using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.UnitOfWorks;

namespace WebApplication1.Models.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VoteDBContext _voteDBContext;

        public UnitOfWork(VoteDBContext voteDBContext)
        {
            _voteDBContext = voteDBContext;
        }
        public IRepository<T> GetRepository<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return _voteDBContext.SaveChanges();
        }
    }
}
