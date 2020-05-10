using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public interface IRepository<T>
    {
        //This interfce contains declaration of CRUD methods without their implementation, and it has a Generic type
        //so that it can be re-used by other classes that implement this interface
        IList<T> GetAll();
        T GetById(Guid Id);
        void Add(T item);
        void Edit(Guid Id, T item);
        void Delete(Guid Id);
        List<T> GetAllFiltered(Expression<Func<T, bool>> predicate);
        T GetOneFiltered(Expression<Func<T, bool>> predicate);

        //for jQuery datatables
        List<T> GetAllPaged(string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10);
        List<T> GetAllFilteredPaged(Expression<Func<T, bool>> predicate, string orderBy, int startRowIndex = 0, int maxRows = 10);
    }
}
