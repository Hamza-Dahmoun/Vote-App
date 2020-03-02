using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public interface IRepository<T>
    {
        //this interface contains declaration of CRUD methods without their implementation, and it has a Generic type
        //so that it can be re-used by other classes that implement this interface
        IList<T> GetAll();
        T GetById(Guid id);
        void Add(T item);
        void Edit(Guid id, T item);
        void Delete(Guid id);
    }
}
