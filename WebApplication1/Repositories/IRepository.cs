﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.Models.Helpers;

namespace WebApplication1.Models.Repositories
{
    public interface IRepository<T>
    {
        //This interfce contains declaration of CRUD methods without their implementation, and it has a Generic type
        //so that it can be re-used by other classes that implement this interface
        IList<T> GetAll();
        T GetById(Guid Id);
        T GetByIdReadOnly(Guid Id);
        int Add(T item);
        int Edit(Guid Id, T item);
        int Delete(Guid Id);
        List<T> GetAllFiltered(Expression<Func<T, bool>> predicate);
        T GetOneFiltered(Expression<Func<T, bool>> predicate);
        int CountAll();
        int CountAllFiltered(Expression<Func<T, bool>> predicate);

        //for jQuery datatables
        PagedResult<T> GetAllPaged(string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10);
        PagedResult<T> GetAllFilteredPaged(Expression<Func<T, bool>> predicate, string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10);
    }
}
