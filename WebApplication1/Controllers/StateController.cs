using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    //the below attribute will permit only authorized users to access HomeController, anonymous access will be deactivated
    [Authorize]
    //the below attribute will permit only users with set of roles contained in the policy 'ManageElections'
    //(you can check the set of roles related to this policy in ConfigureServices() in Startup file)
    [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
    //[Authorize(Roles = "Administrator")]
    public class StateController : Controller
    {
        public IRepository<State> _stateRepository { get; }
        public StateController(IRepository<State> stateRepository)
        {
            _stateRepository = stateRepository;
        }



        public IActionResult Index()
        {
            return View(convertStateList_toStateViewModelList(_stateRepository.GetAll()).OrderBy(svm=>svm.LevelValue));
        }

        public IActionResult Details(Guid id)
        {
            return View(convertState_toStateViewModel(_stateRepository.GetById(id)));
        }


        //1- 'create' button in the 'index' view will execute the method Create(), it will display the Create.cshtml view
        //2- 'Save' button which is inside the form in the 'create' action view will execute the Action Create(Employee e)
        //becuz the form has (asp-action="Create") and the action has [HttpPost]
        //Get : State/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(State state)
        {
            if (ModelState.IsValid)
            {
                state.Id = Guid.NewGuid();
                _stateRepository.Add(state);
                return RedirectToAction(nameof(Index));
            }
            //so the model isn't valid, lets keep the user in the same view so that he could read the validation msgs
            return View(state);
        }


        //******************** UTILITIES
        public StateViewModel convertState_toStateViewModel(State state)
        {
            StateViewModel s = new StateViewModel
            {
                Id = state.Id,
                Name = state.Name
            };
            return s;
        }

        public List<StateViewModel> convertStateList_toStateViewModelList(IList<State> states)
        {
            List<StateViewModel> myList = new List<StateViewModel>();
            foreach (var item in states)
            {
                myList.Add(convertState_toStateViewModel(item));
            }
            return myList;
        }
    }
}