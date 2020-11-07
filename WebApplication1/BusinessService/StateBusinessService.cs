using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.Business;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.BusinessService
{
    public class StateBusinessService
    {
        //the below are services we're going to use in this file, they will be injected in the constructor
        private readonly IRepository<State> _stateRepository;
        //Lets create a private readonly field IStringLocalizer<Messages> so that we can use Localization service, we'll inject it inside the constructor
        private readonly IStringLocalizer<Messages> _messagesLocalizer;

        public StateBusinessService(IRepository<State> stateRepository, IStringLocalizer<Messages> messagesLocalizer)
        {
            _stateRepository = stateRepository;
            _messagesLocalizer = messagesLocalizer;
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
                State state =_stateRepository.GetById(Id);
                if (state == null)
                {
                    throw new BusinessException(_messagesLocalizer["State not found"] + ".");
                }
                return state;
            }
            catch (BusinessException E)
            {
                throw E;
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
                int updatedRows = _stateRepository.Add(state);
                if (updatedRows > 0)
                {
                    return updatedRows;
                }
                else
                {
                    //row not updated in the DB
                    throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                }
            }
            catch (DataNotUpdatedException E)
            {
                throw E;
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
                int updatedRows = _stateRepository.Delete(Id);
                if (updatedRows > 0)
                {
                    return updatedRows;
                }
                else
                {
                    //row not updated in the DB
                    throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                }
            }
            catch (DataNotUpdatedException E)
            {
                throw E;
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
                int updatedRows = _stateRepository.Edit(Id, state);
                if (updatedRows > 0)
                {
                    return updatedRows;
                }
                else
                {
                    //row not updated in the DB
                    throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                }
            }
            catch (DataNotUpdatedException E)
            {
                throw E;
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
