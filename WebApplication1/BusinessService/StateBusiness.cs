using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

namespace WebApplication1.BusinessService
{
    public class StateBusiness
    {
        //the below are services we're going to use in this file, they will be injected in the constructor
        private readonly IRepository<State> _stateRepository;

        public StateBusiness(IRepository<State> stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public List<State> GetAll()
        {
            try
            {
                return (List<State>)_stateRepository.GetAll();
            }
            catch(Exception E)
            {
                throw E;
            }
        }
        public State GetById(Guid Id)
        {
            try
            {
                return _stateRepository.GetById(Id);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public int Add(State state)
        {
            try
            {
                return _stateRepository.Add(state);
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
