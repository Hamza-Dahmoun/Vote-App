﻿using System;
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
            _logger.LogInformation("State/Index() action is called");
            try
            {
                _logger.LogInformation("Calling StateRepository.GetAll() method");
                List<State> states = _stateRepository.GetAll().ToList();
                _logger.LogInformation("Calling Utilities.convertStateList_toStateViewModelList() method");
                List<StateViewModel> svmList = Utilities.convertStateList_toStateViewModelList(states);
                _logger.LogInformation("Returning a list of StateViewModels to the Index view");
                return View(svmList);
            }
            catch(Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                return BadRequest(E.Message);
            }            
        }

        public IActionResult Details(Guid id)
        {
            _logger.LogInformation("State/Details() action is called");
            try
            {
                _logger.LogInformation("Calling StateRepository.GetById() method");
                State s = _stateRepository.GetById(id);
                _logger.LogInformation("Calling Utilities.convertState_toStateViewModel() method");
                StateViewModel svm = Utilities.convertState_toStateViewModel(s);
                _logger.LogInformation("Returning a StateViewModel to the Details view");
                return View(svm);
            }
            catch(Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
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
            _logger.LogInformation("State/Create() action is called");
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Model is valid");
                    state.Id = Guid.NewGuid();
                    _logger.LogInformation("Calling StateRepository.Add() to add state instance to the DB");
                    _stateRepository.Add(state);
                    _logger.LogInformation("Stated added successfully, redirection to Index");
                    return RedirectToAction(nameof(Index));
                }
                //so the model isn't valid, lets keep the user in the same view so that he could read the validation msgs
                return View(state);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                return BadRequest(E.Message);
            }            
        }



        public IActionResult Delete(Guid id)
        {
            _logger.LogInformation("State/Delete() action is called");
            try
            {
                _logger.LogInformation("Calling StateRepository.GetById() method");
                var state = _stateRepository.GetById(id);
                _logger.LogInformation("Returning State instance to the view");
                return View(state);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                return BadRequest(E.Message);
            }            
        }

        [HttpPost]
        public IActionResult DeleteState(Guid id)
        {
            _logger.LogInformation("State/DeleteState() action is called");
            try
            {
                _logger.LogInformation("Calling StateRepository.Delete() method");
                _stateRepository.Delete(id);                
                _logger.LogInformation("Redirecting to Index view");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                return BadRequest(E.Message);
            }         
        }






        public IActionResult Edit(Guid id)
        {
            _logger.LogInformation("State/Edit() action is called");
            try
            {
                _logger.LogInformation("Calling StateRepository.GetById() method");
                var state = _stateRepository.GetById(id);
                _logger.LogInformation("Returning State instance to the view");
                return View(state);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                return BadRequest(E.Message);
            }            
        }
        [HttpPost]
        public IActionResult Edit(Guid id, State state)
        {
            _logger.LogInformation("State/Edit() action is called");
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Calling StateRepository.Edit() method");
                    _stateRepository.Edit(id, state);
                    _logger.LogInformation("Redirecting to Index view");
                    return RedirectToAction(nameof(Index));
                }
                return View(state);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                return BadRequest(E.Message);
            }            
        }

        
    }
}