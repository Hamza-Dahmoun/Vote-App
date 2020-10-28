using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

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

        public int Delete(Guid Id)
        {
            try
            {
                return _stateRepository.Delete(Id);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public int Edit(Guid Id, State state)
        {
            try
            {
                return _stateRepository.Edit(Id, state);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<State> GetAllFiltered(Expression<Func<State, bool>> predicate)
        {
            try
            {
                return _stateRepository.GetAllFiltered(predicate);
            }
            catch (Exception E)
            {
                throw E;
            }
        }


        public StateViewModel ConvertState_ToStateViewModel(State state)
        {
            try
            {
                StateViewModel s = new StateViewModel
                {
                    Id = state.Id,
                    Name = state.Name
                };
                return s;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<StateViewModel> ConvertStateList_ToStateViewModelList(IList<State> states)
        {
            try
            {
                List<StateViewModel> myList = new List<StateViewModel>();
                foreach (var item in states)
                {
                    myList.Add(ConvertState_ToStateViewModel(item));
                }
                return myList;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

    }
}
