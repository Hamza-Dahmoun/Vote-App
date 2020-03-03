using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public interface IRepository<T>
    {
        //This interfce contains declaration of CRUD methods without their implementation, and it has a Generic type
        //so that it can be re-used by other classes that implement this interface
        IList<T> GetAll();
        T GetById(int Id);
        void Add(T item);
        void Edit(int Id, T item);
        void Delete(int Id);
    }
}
