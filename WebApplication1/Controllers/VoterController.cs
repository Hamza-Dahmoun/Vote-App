using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using WebApplication1.Business;
using WebApplication1.Models;
using WebApplication1.Models.Helpers;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text;
using WebApplication1.BusinessService;
using System.Linq.Expressions;

namespace WebApplication1.Controllers
{
    //the below attribute will permit only authorized users to access HomeController, anonymous access will be deactivated
    [Authorize]
    //the below attribute will permit only users with set of roles contained in the policy 'ManageElections'
    //(you can check the set of roles related to this policy in ConfigureServices() in Startup file)
    [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
    //[Authorize(Roles = "Administrator")]
    public class VoterController : Controller
    {

        //the below are services we're going to use in this controller, they will be injected in the constructor

        //the below service is used to store a new user for each new voter
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<VoterController> _logger;

        private readonly VoterBusinessService _voterBusiness;
        private readonly StateBusinessService _stateBusiness;
        private readonly CandidateBusinessService _candidateBusiness;

        //Lets create a private readonly field IStringLocalizer<Messages> so that we can use Localization service, we'll inject it inside the constructor
        private readonly IStringLocalizer<Messages> _messagesLoclizer;
        private readonly VoteBusinessService _voteBusiness;

        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public VoterController(
            VoteBusinessService voteBusiness,
            VoterBusinessService voterBusiness,
            CandidateBusinessService candidateBusiness,
            UserManager<IdentityUser> userManager,
            ILogger<VoterController> logger,
            IStringLocalizer<Messages> messagesLoclizer,
            StateBusinessService stateBusiness)
        {
            _voteBusiness = voteBusiness;
            _voterBusiness = voterBusiness;
            _candidateBusiness = candidateBusiness;
            _userManager = userManager;
            _logger = logger;
            _messagesLoclizer = messagesLoclizer;
            _stateBusiness = stateBusiness;
        }
        
        public IActionResult Index()
        {
            //we'll return only the number of voters
            //voters row we'll be returned using a jquery datatable
            _logger.LogInformation("Voter/Index() action is called");
            try
            {
                _logger.LogInformation("Calling voterBusiness.CountAll() method");                
                ViewBag.votersCount = _voterBusiness.CountAll();

                _logger.LogInformation("Returning a list of voters to Index view");
                return View();
            }
            catch(Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }
        }

        public IActionResult Details(Guid id)
        {
            _logger.LogInformation("Voter/Details() action is called");
            try
            {
                if (id == null)
                {
                    _logger.LogError("Passed parameter 'id' is null");
                    throw new BusinessException(_messagesLoclizer["Passed parameter 'id' can not be null"]);
                }

                _logger.LogInformation("Calling VoterRepository.GetById() method");
                Voter v = _voterBusiness.GetById(id);
                if (v == null)
                {
                    _logger.LogError("Voter not found");
                    throw new BusinessException(_messagesLoclizer["Voter not found"]);
                }

                _logger.LogInformation("Calling _voterBusiness.ConvertVoter_ToPersonViewModel() method");

                PersonViewModel p = _voterBusiness.ConvertVoter_ToPersonViewModel(v);
                _logger.LogInformation("Returning a PersonViewModel to the Details view");
                return View(p);
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

  
        public IActionResult Create()
        {
            try
            {
                //this method will return an empty VoterStateViewModel but with a list of all states, in a view
                VoterStateViewModel vs = new VoterStateViewModel
                {
                    States = _stateBusiness.GetAll()
                };
                return View(vs);
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Create(VoterStateViewModel vs)
        {
            _logger.LogInformation("Voter/Create() action is called");
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Model is valid");
                    await _voterBusiness.AddNewVoter(vs);

                    //row updated successfully in the DB
                    _logger.LogInformation("The new voter is added to the DB successfully");
                    _logger.LogInformation("Redirecting to the Voter Index view");
                    return RedirectToAction(nameof(Index));
                }
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
                return View(vs);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }
        }


        public IActionResult Delete(Guid id)
        {
            _logger.LogInformation("Voter/Delete() action is called");
            try
            {
                if (id == null)
                {
                    _logger.LogError("Passed parameter 'id' is null");
                    throw new BusinessException(_messagesLoclizer["Passed parameter 'id' can not be null"]);
                }

                _logger.LogInformation("Calling VoterRepository.GetById() method");
                var voter = _voterBusiness.GetById(id);
                if (voter == null)
                {
                    _logger.LogError("Voter not found");
                    throw new BusinessException(_messagesLoclizer["Voter not found"]);
                }

                _logger.LogInformation("Calling _voterBusiness.ConvertVoter_ToPersonViewModel() method");

                PersonViewModel p = _voterBusiness.ConvertVoter_ToPersonViewModel(voter);
                _logger.LogInformation("Returning PersonViewModel to the view");
                return View(p);
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
        public async Task<IActionResult> DeleteVoter(Guid id)
        {//removing a voter means removing all his actions (votes  & identity account)
            _logger.LogInformation("Voter/DeleteVoter() action is called");
            try
            {
                if (id == null)
                {
                    _logger.LogError("Passed parameter 'id' is null");
                    throw new BusinessException(_messagesLoclizer["Passed parameter 'id' can not be null"]);
                }

                _logger.LogInformation("Calling VoterRepository.GetById() method");
                Voter voter = _voterBusiness.GetById(id);
                if (voter == null)
                {
                    _logger.LogError("Voter not found");
                    throw new BusinessException(_messagesLoclizer["Voter not found"]);
                }

                await _voterBusiness.DeleteVoter(id);                

                _logger.LogInformation("Redirecting to Index view");
                return RedirectToAction(nameof(Index));
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
                return View(nameof(Delete));
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E; 
            }
        }



        public IActionResult Edit(Guid Id)
        {
            _logger.LogInformation("Voter/Edit() action is called");
            try
            {
                //In here we are going to return a view where a voter is displayed with his state but the state is in
                //a list of states

                if (Id == null)
                {
                    _logger.LogError("Passed parameter 'id' is null");
                    throw new BusinessException(_messagesLoclizer["Passed parameter 'id' can not be null"]);
                }

                _logger.LogInformation("Calling VoterRepository.GetById() method");
                var voter = _voterBusiness.GetById(Id);
                if (voter == null)
                {
                    _logger.LogError("Voter not found");
                    throw new BusinessException(_messagesLoclizer["Voter not found"]);
                }

                _logger.LogInformation("Creating a VoterStateViewModel for the Voter instance");
                VoterStateViewModel voterstate = new VoterStateViewModel
                {
                    Id = voter.Id,
                    FirstName = voter.FirstName,
                    LastName = voter.LastName,
                    States = _stateBusiness.GetAll()
                };
                /*just in case user wanted to edit info of Neutral vote which doesn't have a state*/
                if (voter.State != null)
                {
                    voterstate.StateID = voter.State.Id;
                }

                _logger.LogInformation("Returning VoterStateViewModel to the View");
                return View(voterstate);
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
        public IActionResult Edit(VoterStateViewModel voterstate)
        {
            _logger.LogInformation("Voter/Edit() action is called");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model is not valid");
                    if (voterstate.States == null)
                    {
                        //in caase the object received doesn't have a list of states
                        voterstate.States = _stateBusiness.GetAll();
                    }
                    _logger.LogInformation("Returning to the view to display validation messages");
                    //so there is a business rule not met, lets throw a businessException and catch it
                    throw new BusinessException(_messagesLoclizer["Information provided not valid"]);
                }
                Voter v = new Voter
                {
                    Id = voterstate.Id,
                    FirstName = voterstate.FirstName,
                    LastName = voterstate.LastName,
                    StateId = voterstate.StateID
                };

                _logger.LogInformation("Calling VoterRepository.Edit() method");
                
                int updatedRows = _voterBusiness.Edit(voterstate.Id, v);
                if (updatedRows > 0)
                {
                    //row updated successfully in the DB
                    _logger.LogInformation("Redirecting to Index action");
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
                //lets refill States list
                voterstate.States = _stateBusiness.GetAll();
                return View(voterstate);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }            
        }





        #region JQUERY DATATABLE REGION

        public IActionResult DataTable()
        {
            //This method is called by jQuery datatables to get paged data
            //this method is called inside Voter/Index.cshtml
            //First, we'll try to read the variables sent from the jQuery request, and then, based on these variables' values we'll query
            //the db

            _logger.LogInformation("Voter/Datatable() method is called");
            try
            {
                //lets first get the variables of the request (of the form), and then build the linq query accordingly
                //above each variable I wrote the official doc of jQuery


                // draw
                // integer Type
                // Draw counter.This is used by DataTables to ensure that the Ajax returns from server - side processing requests
                // are drawn in sequence by DataTables(Ajax requests are asynchronous and thus can return out of sequence). 
                // This is used as part of the draw return parameter(see below).

                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();



                // start
                // integer type
                // Paging first record indicator.This is the start point in the current data set(0 index based -i.e. 0 is the first record).

                var start = HttpContext.Request.Form["start"].FirstOrDefault();



                // length
                // integer type
                // Number of records that the table can display in the current draw. It is expected that the number of records returned 
                // will be equal to this number, unless the server has fewer records to return. Note that this can be -1 to indicate that 
                // all records should be returned (although that negates any benefits of server-side processing!)

                var length = HttpContext.Request.Form["length"].FirstOrDefault();



                // search[value]
                // string Type
                // Global search value. To be applied to all columns which have searchable as true.

                var searchValue = HttpContext.Request.Form["search[value]"].FirstOrDefault();


                // order[i][column]
                // integer Type
                // Column to which ordering should be applied. This is an index reference to the columns array of information
                // that is also submitted to the server.

                var sortColumnName = HttpContext.Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();


                // order[i][dir]
                // integer Type
                // Ordering direction for this column.It will be asc or desc to indicate ascending ordering or descending ordering, respectively.


                var sortColumnDirection = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();


                //Page Size (10, 20, 50,100) 
                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                //how many rows too skip?
                int skip = start != null ? Convert.ToInt32(start) : 0;

                //totalRecords too inform user
                int totalRecords = 0;


                //lets get the list of voters filtered and paged
                PagedResult<Voter> pagedResult = _voterBusiness.GetVoters_ForDataTable(
                    searchValue,
                    sortColumnName,
                    sortColumnDirection,
                    pageSize,
                    skip);

                //lets assign totalRecords the correct value
                totalRecords = pagedResult.TotalCount;

                //now lets return json data so that it is understandable by jQuery   
                _logger.LogInformation("Going to serialize the response");
                JsonSerializerSettings settings = new JsonSerializerSettings { DateFormatString = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern };
                var json = JsonConvert.SerializeObject(new
                {
                    draw = draw,
                    recordsFiltered = totalRecords,
                    recordsTotal = totalRecords,
                    data = _voterBusiness.ConvertVoterList_ToPersonViewModelList(pagedResult.Items)
                }, settings);
                _logger.LogInformation("Return the response as JSON");
                return Ok(json);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                return BadRequest();
            }
        }


        #endregion


        #region SELECT2 REGION
        //this is a web api called when user remove a candidate from an election
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public async Task<IActionResult> States(string q)
        {
            _logger.LogInformation("Voter/States() is called");
            try
            {
                if (String.IsNullOrEmpty(q))
                {
                    _logger.LogError("Parameter is null");
                    return BadRequest();
                }

                //declaring an expression that is special to State objects according to the search value
                Expression<Func<State, bool>> expr;
                expr = s => s.Name.StartsWith(q);

                _logger.LogInformation("Calling StateRepository.GetAllFiltered() method");
                var states = _stateBusiness.GetAllFiltered(expr).Select(s => new { text = s.Name, id = s.Id});
                //now lets return json data

                _logger.LogInformation("Going to  serialize the list of states");
                var json = JsonConvert.SerializeObject(new
                {
                    states
                });

                _logger.LogInformation("Returning list of states as json");
                return Ok(json);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                return BadRequest();
            }
        }

        #endregion


        [HttpPost]
        public IActionResult ExportToExcel()
        {
            //This function download list of all Voters as excel file
            try
            {
                var stream = new System.IO.MemoryStream();
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    var voters = _voterBusiness.GetAll();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(_messagesLoclizer["Voters"]);

                    worksheet.Cells[1, 1].Value = _messagesLoclizer["First Name"];
                    worksheet.Cells[1, 2].Value = _messagesLoclizer["Last Name"];
                    worksheet.Cells[1, 3].Value = _messagesLoclizer["State"];
                    worksheet.Row(1).Style.Font.Bold = true;


                    for (int c = 2; c < voters.Count + 2; c++)
                    {
                        worksheet.Cells[c, 1].Value = voters[c - 2].FirstName;
                        worksheet.Cells[c, 2].Value = voters[c - 2].LastName;
                        worksheet.Cells[c, 3].Value = voters[c - 2].State?.Name;
                    }

                    package.Save();
                }

                StringBuilder fileName = new StringBuilder();
                fileName.Append(_messagesLoclizer["Voters"] + ".xlsx");

                StringBuilder fileType = new StringBuilder();
                fileType.Append("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                stream.Position = 0;
                return File(stream, fileType.ToString(), fileName.ToString());
            }
            catch(Exception E)
            {
                throw E;
            }            
        }

    }
}