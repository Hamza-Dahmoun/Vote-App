using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Business;
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
        //the below are services we're going to use in this controller, they will be injected in the constructor
        private readonly ILogger<StateController> _logger;
        public IRepository<State> _stateRepository { get; }
        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public StateController(IRepository<State> stateRepository, ILogger<StateController> logger)
        {
            _logger = logger;
            _stateRepository = stateRepository;
        }



        public IActionResult Index()
        {
            try
            {
                return View(Utilities.convertStateList_toStateViewModelList(_stateRepository.GetAll()));
            }
            catch(Exception E)
            {
                
                return BadRequest(E.Message);
            }            
        }

        public IActionResult Details(Guid id)
        {
            try
            {
                return View(Utilities.convertState_toStateViewModel(_stateRepository.GetById(id)));
            }
            catch(Exception E)
            {
                return BadRequest(E.Message);
            }            
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
            try
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
            catch (Exception E)
            {
                //this is msg is going to be displayed as text in blank page
                return BadRequest(E.Message);
            }            
        }



        public IActionResult Delete(Guid id)
        {
            try
            {
                var state = _stateRepository.GetById(id);
                return View(state);
            }
            catch (Exception E)
            {
                //this is msg is going to be displayed as text in blank page
                return BadRequest(E.Message);
            }            
        }

        [HttpPost]
        public IActionResult DeleteState(Guid id)
        {
            try
            {
                _stateRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception E)
            {
                //this is msg is going to be displayed as text in blank page
                return BadRequest(E.Message);
            }         
        }






        public IActionResult Edit(Guid id)
        {
            try
            {
                var state = _stateRepository.GetById(id);
                return View(state);
            }
            catch (Exception E)
            {
                //this is msg is going to be displayed as text in blank page
                return BadRequest(E.Message);
            }            
        }
        [HttpPost]
        public IActionResult Edit(Guid id, State state)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _stateRepository.Edit(id, state);
                    return RedirectToAction(nameof(Index));
                }
                return View(state);
            }
            catch (Exception E)
            {
                //this is msg is going to be displayed as text in blank page
                return BadRequest(E.Message);
            }            
        }

        
    }
}