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
using System.Net;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Microsoft.Extensions.Localization;
using WebApplication1.BusinessService;

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

        //Lets create a private readonly field IStringLocalizer<Messages> so that we can use Localization service, we'll inject it inside the constructor
        private readonly IStringLocalizer<Messages> _messagesLoclizer;


        private readonly StateBusiness _stateBusiness;

        private readonly VoterBusiness _voterBusiness;

        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public StateController(IRepository<State> stateRepository, 
            ILogger<StateController> logger, 
            IStringLocalizer<Messages> messagesLoclizer,
            StateBusiness stateBusiness,
            VoterBusiness voterBusiness)
        {
            _logger = logger;
            _messagesLoclizer = messagesLoclizer;
            _stateBusiness = stateBusiness;
            _voterBusiness = voterBusiness;
        }



        public IActionResult Index()
        {

            _logger.LogInformation("State/Index() action is called");
            try
            {
                _logger.LogInformation("Calling StateRepository.GetAll() method");
                List<State> states = _stateBusiness.GetAll();
                ViewBag.statesCount = states.Count;

                _logger.LogInformation("Calling _stateBusiness.ConvertStateList_ToStateViewModelList() method");
                //List<StateViewModel> svmList = Utilities.convertStateList_toStateViewModelList(states);
                List<StateViewModel> svmList = _stateBusiness.ConvertStateList_ToStateViewModelList(states);
                _logger.LogInformation("Returning a list of StateViewModels to the Index view");
                return View(svmList);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }
        }

        public IActionResult Details(Guid id)
        {
            _logger.LogInformation("State/Details() action is called");
            try
            {
                if (id == null)
                {
                    _logger.LogError("Passed parameter 'id' is null");
                    throw new BusinessException(_messagesLoclizer["Passed parameter 'id' can not be null"]);
                }

                _logger.LogInformation("Calling StateRepository.GetById() method");
                State s = _stateBusiness.GetById(id);

                if (s == null)
                {
                    _logger.LogError("State not found");
                    throw new BusinessException(_messagesLoclizer["State not found"]);
                }

                _logger.LogInformation("Calling _stateBusiness.ConvertState_ToStateViewModel() method");
                //StateViewModel svm = Utilities.convertState_toStateViewModel(s);
                StateViewModel svm = _stateBusiness.ConvertState_ToStateViewModel(s);

                _logger.LogInformation("Returning a StateViewModel to the Details view");
                return View(svm);
            }
            catch (BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
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
                    int updatedRows = _stateBusiness.Add(state);
                    
                    if (updatedRows > 0)
                    {
                        //row updated successfully in the DB
                        _logger.LogInformation("Stated added successfully, redirection to Index");                        
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        //row not updated in the DB
                        throw new DataNotUpdatedException(_messagesLoclizer["Data not updated, operation failed."]);
                    }
                    
                }
                //so the model isn't valid, lets keep the user in the same view so that he could read the validation msgs
                _logger.LogInformation("Model is not valid");
                //so there is a business rule not met, lets throw a businessException and catch it
                throw new BusinessException(_messagesLoclizer["Information provided not valid"]);
                
            }
            catch (DataNotUpdatedException bnu)
            {
                _logger.LogError(bnu.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", bnu.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View(state);
            }
            catch (Exception E)
            {
                //so something went wrong, lets log it and inform the user thru Error.cshtml to call the system admin
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }            
        }



        public IActionResult Delete(Guid id)
        {
            _logger.LogInformation("State/Delete() action is called");
            try
            {
                if(id == null)
                {
                    _logger.LogError("Passed parameter 'id' is null");
                    throw new BusinessException(_messagesLoclizer["Passed parameter 'id' can not be null"]);
                }
                
                _logger.LogInformation("Calling StateRepository.GetById() method");
                var state = _stateBusiness.GetById(id);
                if(state == null)
                {
                    _logger.LogError("State not found");
                    throw new BusinessException(_messagesLoclizer["State not found"]);
                }

                _logger.LogInformation("Returning State instance to the view");
                return View(state);
            }
            catch(BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }            
        }

        [HttpPost]
        public IActionResult DeleteState(Guid id)
        {
            //we'll first update the Voters related to the State by making StateID in them 'null' then proceed removing the State row
            _logger.LogInformation("State/DeleteState() action is called");
            try
            {
                if (id == null)
                {
                    _logger.LogError("Passed parameter 'id' is null");
                    throw new BusinessException(_messagesLoclizer["Passed parameter 'id' can not be null"]);
                }

                _voterBusiness.MakeVotersStatesNull(id);
                //declaring an expression that is special to Election objects
                /*System.Linq.Expressions.Expression<Func<Voter, bool>> expr = v => v.State.Id == id;
                _logger.LogInformation("Calling VotereRepository.GetAllFiltered() method");
                var voters = _voterBusiness.GetAllFiltered(expr);
                //now lets update each voter by removing its relation to the state
                foreach (var voter in voters)
                {
                    voter.State = null;
                    _logger.LogInformation("Updating the Voter id= " + voter.Id);

                    int updatedRows = _voterBusiness.Edit(voter.Id, voter);
                    if (updatedRows <1)
                    {
                        //row not updated in the DB
                        throw new DataNotUpdatedException(_messagesLoclizer["Data not updated, operation failed."]);
                    }
                }*/





                _logger.LogInformation("Calling StateRepository.Delete() method");
                
                
                int updatedRows2 = _stateBusiness.Delete(id);
                if (updatedRows2 > 0)
                {
                    //row updated successfully in the DB
                    _logger.LogInformation("Redirecting to Index view");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //row not updated in the DB
                    throw new DataNotUpdatedException(_messagesLoclizer["Data not updated, operation failed."]);
                }


            }
            catch (DataNotUpdatedException bnu)
            {
                _logger.LogError(bnu.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", bnu.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }         
        }






        public IActionResult Edit(Guid id)
        {
            _logger.LogInformation("State/Edit() action is called");
            try
            {
                if (id == null)
                {
                    _logger.LogError("Passed parameter 'id' is null");
                    throw new BusinessException(_messagesLoclizer["Passed parameter 'id' can not be null"]);
                }

                _logger.LogInformation("Calling StateRepository.GetById() method");
                var state = _stateBusiness.GetById(id);
                if (state == null)
                {
                    _logger.LogError("State not found");
                    throw new BusinessException(_messagesLoclizer["State not found"]);
                }

                _logger.LogInformation("Returning State instance to the view");
                return View(state);
            }
            catch (BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
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
                    
                    int updatedRows = _stateBusiness.Edit(id, state);
                    if (updatedRows > 0)
                    {
                        //row updated successfully in the DB
                        _logger.LogInformation("Redirecting to Index view");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        //row not updated in the DB
                        throw new DataNotUpdatedException(_messagesLoclizer["Data not updated, operation failed."]);
                    }                    
                }
                //so there is a business rule not met, lets throw a businessException and catch it
                throw new BusinessException(_messagesLoclizer["Information provided not valid"]);
            }
            catch (DataNotUpdatedException bnu)
            {
                _logger.LogError(bnu.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", bnu.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View(state);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }            
        }



        [HttpPost]
        public IActionResult ExportToExcel()
        {
            //This function download list of all States as excel file
            try
            {
                var stream = new System.IO.MemoryStream();
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    
                    var states = _stateBusiness.GetAll();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(_messagesLoclizer["States"]);

                    worksheet.Cells[1, 1].Value = _messagesLoclizer["Name"];
                    worksheet.Row(1).Style.Font.Bold = true;


                    for (int c = 2; c < states.Count + 2; c++)
                    {
                        worksheet.Cells[c, 1].Value = states[c - 2].Name;
                    }

                    package.Save();
                }

                string fileName = _messagesLoclizer["States"] + ".xlsx";
                string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;
                return File(stream, fileType, fileName);
            }
            catch(Exception E)
            {
                throw E;
            }
            
        }

    }
}