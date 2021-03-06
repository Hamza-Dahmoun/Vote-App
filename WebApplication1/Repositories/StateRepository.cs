﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.Models.Helpers;

namespace WebApplication1.Models.Repositories
{
    public class StateRepository : IRepository<State>
    {
        //Registering the services needed
        protected readonly VoteDBContext _dbContext;
        private readonly DbSet<State> _dbSet;

        public StateRepository(VoteDBContext dbContext)
        {//lets inject dbContext service
            _dbContext = dbContext;
            _dbSet = dbContext.Set<State>();
        }



        public int Add(State item)
        {
            try
            {
                _dbContext.Add(item);
                return _dbContext.SaveChanges();
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public int Delete(Guid Id)
        {
            try
            {
                var item = GetById(Id);
                _dbContext.Remove(item);
                return _dbContext.SaveChanges();
            }
            catch(Exception E)
            {
                throw E;
            }            
        }

        public int Edit(Guid Id, State item)
        {
            try
            {
                var myState = GetById(Id);
                myState.Name = item.Name;
                return _dbContext.SaveChanges();
            }
            catch(Exception E)
            {
                throw E;
            }            
        }

        public IList<State> GetAll()
        {
            try
            {
                return _dbSet.AsNoTracking().ToList();
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        
        public List<State> GetAllFiltered(Expression<Func<State, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            try
            {
                //use eager loading to bring other tables data 
                return _dbSet.Where(predicate).AsNoTracking().ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }


        public PagedResult<State> GetAllFilteredPaged(Expression<Func<State, bool>> predicate, string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            throw new NotImplementedException();
        }

        public PagedResult<State> GetAllPaged(string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            throw new NotImplementedException();
        }

        public State GetById(Guid Id)
        {
            try
            {
                return _dbSet.SingleOrDefault(s => s.Id == Id);
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public State GetByIdReadOnly(Guid Id)
        {
            try
            {
                return _dbSet.AsNoTracking().SingleOrDefault(s => s.Id == Id);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public State GetOneFiltered(Expression<Func<State, bool>> predicate)
        {
            try
            {
                return _dbSet.AsNoTracking().SingleOrDefault(predicate);
            }
            catch(Exception E)
            {
                throw E;
            }            
        }

        public int CountAll()
        {
            int count = 0;
            count = _dbSet.AsNoTracking().Count();
            return count;
        }
        public int CountAllFiltered(Expression<Func<State, bool>> predicate)
        {
            //this function uses the linq expression passed in the object 'predicate' of 'Expression' class to filter the rows from the db
            try
            {
                return _dbSet.Count(predicate);
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
