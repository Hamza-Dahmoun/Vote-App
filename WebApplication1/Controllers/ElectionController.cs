using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    //[Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
    //[Authorize(Roles = "Administrator")]
    public class ElectionController : Controller
    {
        //the below are services we're going to use in this controller, they will be injected in the constructor
        public IRepository<Election> _electionRepository { get; }

        public IRepository<Voter> _voterRepository { get; }
        private readonly VoterBusinessService _voterBusiness;
        private readonly ElectionBusinessService _electionBusiness;

        public IRepository<Candidate> _candidateRepository { get; }
        private readonly CandidateBusinessService _candidateBusiness;
        //this is only used to get the currentUser so that we check whether he voted or not in order to generate the dashboard
        private readonly UserManager<IdentityUser> _userManager;
        //Lets create a private readonly field IStringLocalizer<Messages> so that we can use Localization service, we'll inject it inside the constructor
        private readonly IStringLocalizer<Messages> _messagesLoclizer;
        private readonly VoteBusinessService _voteBusiness;
        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public ElectionController(
            VoteBusinessService voteBusiness,
            IRepository<Vote> voteRepository,
            IRepository<Voter> voterRepository,
            IRepository<Candidate> candidateRepository,
            IRepository<Election> electionRepository,
            UserManager<IdentityUser> userManager,
            IStringLocalizer<Messages> messagesLoclizer,
            VoterBusinessService voterBusiness,
            CandidateBusinessService candidateBusiness,
            ElectionBusinessService electionBusiness)
        {
            _voterBusiness = voterBusiness;
            _voteBusiness = voteBusiness;
            _voterRepository = voterRepository;
            _candidateRepository = candidateRepository;
            _candidateBusiness = candidateBusiness;
            _electionRepository = electionRepository;
            _electionBusiness = electionBusiness;
            _userManager = userManager;
            _messagesLoclizer = messagesLoclizer;
        }


        // GET: Election
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public ActionResult Index()
        {
            try
            {
                ViewBag.electionsCount = _electionBusiness.CountAll();
                //returning a list of Elections but without their neutral candidate
                
                return View(_electionBusiness.GetAll().OrderByDescending(d => d.StartDate).
                Select(e => new Election
                {
                    Id = e.Id,
                    Name = e.Name,
                    StartDate = e.StartDate,
                    DurationInDays = e.DurationInDays,
                    HasNeutral = e.HasNeutral,
                    Candidates = e.Candidates.Where(c => c.isNeutralOpinion != true).ToList()
                }));
            }
            catch(Exception E)
            {
                throw E;
            }
        }


        // GET: Election/Details/5
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public ActionResult Details(Guid id)
        {
            try
            {
                if (id == null)
                {
                    throw new BusinessException(_messagesLoclizer["Passed parameter 'id' can not be null"]);
                }

                Election e = _electionBusiness.GetById(id);
                if (e == null)
                {
                    throw new BusinessException(_messagesLoclizer["Election not found"]);
                }

                return View(_electionBusiness.ConvertElection_ToElectionViewModel(e));
            }
            catch (BusinessException be)
            {
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (Exception E)
            {
                throw E;
            }            
        }


        // GET: Election/Create
        //if we try to imagine the scenario of adding a new Election I think it will be done in two steps:
        //--> Step1: Adding info of the election(name, duration,,,) and send them to backend using api method in inside the controller,
        //then send back the response to javascript to redirect to step2
        //--> Step2: Selecting the candidates of this Election from a list of Voters, and send them to backend using api method in inside the controller,
        //then send back the response to javascript to redirect to Index.
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public ActionResult Create()
        {
            //return an empty view
            return View();
        }

               


        
        
        //Step(1): Adding info of the election(name, duration,,,) and send them to backend using api method in inside the controller,
        //then send back the response to javascript to redirect to step2
        //this is a web api called when adding a new Election instance
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]        
        public async Task<IActionResult> ValidateElection([FromBody] Election election)
        {
            //this method is called using ajax calls
            //so if a business rule is not met we'll throw a businessException and catch it to create and internal server error and return its msg
            //as json

            //Step(1): Adding info of the election(name, duration,,,) and send them to backend using api method in inside the controller,
            //then send back the response to javascript to redirect to step2

            try
            {
                if (ModelState.IsValid)
                {
                    election.Id = Guid.NewGuid();

                    _electionBusiness.AddNewElection(election);

                    response_Voters_and_NewElection r;
                    r.ElectionId = election.Id;
                    r.Voters = _voterBusiness.ConvertVoterList_ToPersonViewModelList(_voterBusiness.GetAll());

                    //lets serialize the struct we've got and send it back as a reponse
                    var json = JsonConvert.SerializeObject(r);                
                return Ok(json);
                }
                else
                {
                    //Model is not valid
                    throw new BusinessException(_messagesLoclizer["Data not valid, please check again."]);
                }

            }
            catch (DataNotUpdatedException bnu)
            {
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", bnu.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (BusinessException be)
            {
                //lets create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = be.Message });
            }
            catch (Exception E)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
                //In above code I created an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
            }
        }
        
        
        [HttpPost]
        //If I leave [FromBody] next to the parameter the request will not even access this method, bcuz jquery already has passed parameters
        
        public async Task<IActionResult> VotersDataTable(Guid electionId)
        {
            //returns a list of voters 

            //This method is called by jQuery datatables to get paged data
            //First, we'll try to read the variables sent from the jQuery request, and then, based on these variables' values we'll query
            //the db


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
                PagedResult<Voter> pagedResult = _electionBusiness.GetVotersByElection_ExcludingAlreadyCandidates_ForDataTable(
                    electionId,
                    searchValue,
                    sortColumnName,
                    sortColumnDirection,
                    pageSize,
                    skip);


                //lets assign totalRecords the correct value
                totalRecords = pagedResult.TotalCount;

                //now lets return json data so that it is understandable by jQuery                
                var json = JsonConvert.SerializeObject(new
                {
                    draw = draw,
                    recordsFiltered = totalRecords,
                    recordsTotal = totalRecords,
                    data = _voterBusiness.ConvertVoterList_ToPersonViewModelList(pagedResult.Items)
                });
                return Ok(json);
            }
            catch (BusinessException E)
            {
                //lets create and return an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
            }
            catch (Exception E)
            {
                //lets create and return an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
            }
        }

        //Step(2): Adding one Candidate to the Election each time
        //this is a web api called when user selects candidates from voters list to the election he created or he is editing
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public async Task<IActionResult> AddCandidate([FromBody] CandidateElectionRelation mydata)
        {
            //this method is called using ajax calls
            //it is used when editing/creating an election
            //it takes {voterId, electionId} and create new candidate from it and add it to db ... used when creating/editing an old election
            //so if a business rule is not met we'll throw a businessException and catch it to create and internal server error and return its msg
            //as json
            
            try
            {
                Candidate newCandidate = _candidateBusiness.AddNewCandidate(mydata.voterId, mydata.electionId);
                //row updated successfully in the DB
                //now lets return json data so that it is understandable by jQuery                
                var json = JsonConvert.SerializeObject(new
                {
                    candidateId = newCandidate.Id
                });
                return Ok(json);
            }
            catch (DataNotUpdatedException bnu)
            {
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", bnu.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (BusinessException be)
            {
                //lets create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = be.Message });
            }
            catch (Exception E)
            {
                //lets create and return an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
            }
        }

        //this is a web api called when user remove a candidate from an election
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public async Task<IActionResult> RemoveCandidateByElectionAndVoter([FromBody] CandidateElectionRelation mydata)
        {
            //this method is called using ajax calls
            //so if a business rule is not met we'll throw a businessException and catch it to create and internal server error and return its msg
            //as json



            //this function removes a candidate from the db using its electionID and its voterBeing ID
            //.. used when creating an election
            try
            {
                _candidateBusiness.RemoveCandidateByElectionIdAndVoterId(mydata.electionId, mydata.voterId);
                
                //row updated successfully in the DB
                return Json(new { success = true });
            }
            catch (DataNotUpdatedException bnu)
            {
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", bnu.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (BusinessException be)
            {
                //lets create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = be.Message });
            }
            catch (Exception E)
            {
                //lets create and return an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
            }
        }

        //this is a web api called when user remove a candidate from an election
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public async Task<IActionResult> RemoveCandidate_byID([FromBody] string candidateId)
        {
            //this method is called using ajax calls
            //so if a business rule is not met we'll throw a businessException and catch it to create and internal server error and return its msg
            //as json


            //this function removes a candidate from the db using its ID .. used when editing an election
            try
            {
                _candidateBusiness.RemoveCandidateByID(candidateId);
                //row updated successfully in the DB
                return Json(new { success = true });
            }
            catch (DataNotUpdatedException bnu)
            {
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", bnu.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (BusinessException be)
            {
                //lets create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = be.Message });
            }
            catch (Exception E)
            {
                //lets create and return an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
            }
        }



        //this method is called using jQuery ajax and it returns a list of candidates related to the election
        //it is called when displaying the details of an election (it gets also neutral opinion)
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public async Task<IActionResult> GetCandidatesList_byElectionId([FromBody] string electionId)
        {
            //this method is called using ajax calls
            //so if a business rule is not met we'll throw a businessException and catch it to create and internal server error and return its msg
            //as json



            try
            {
                if (String.IsNullOrEmpty(electionId))
                {
                    throw new BusinessException(_messagesLoclizer["electionId cannot be null."]);
                }
                Election e = _electionBusiness.GetById(Guid.Parse(electionId));
                if (e == null)
                {
                    throw new BusinessException(_messagesLoclizer["Election is not found."]);
                }

                //lets serialize the list of candidates of the election we've got and send it back as a reponse
                //note that I didn't retrieve candidates as they are, I selected only needed attributes bcuz when i tried serializing
                //candidates objects as they are I got this error "self referencing loop detected with type" it means json tried to serialize the candidate object
                //but it found that each candidate has an Election object, and this election object has a list of candidates and so on, so i excluded election
                //from the selection to avoid the infinite loop
                
                var candidates = _candidateBusiness.GetCandidate_byElection(e);
                if (candidates == null)
                {
                    throw new BusinessException(_messagesLoclizer["Candidates List not found."]);
                }

                var json = JsonConvert.SerializeObject(_candidateBusiness.ConvertCandidateList_ToCandidateViewModelList(candidates));
                return Ok(json);
                
            }
            catch (BusinessException be)
            {
                //lets create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = be.Message });
            }
            catch (Exception E)
            {
                //lets create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
            }
        }


        //this method is called using jQuery ajax and it returns a list of candidates related to the election
        //it is called when displaying the details of an election (it gets also neutral opinion)
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public async Task<IActionResult> GetCandidatesList_byElectionId_ExcepNeutralOpinion([FromBody] string electionId)
        {
            //this method is called using ajax calls
            //so if a business rule is not met we'll throw a businessException and catch it to create and internal server error and return its msg
            //as json


            try
            {
                if (String.IsNullOrEmpty(electionId))
                {
                    throw new BusinessException(_messagesLoclizer["electionId cannot be null."]);
                }
                Election e = _electionBusiness.GetById(Guid.Parse(electionId));
                if (e == null)
                {
                    throw new BusinessException(_messagesLoclizer["Election is not found."]);
                }
                //lets serialize the list of candidates of the election we've got and send it back as a reponse
                //note that I didn't retrieve candidates as they are, I selected only needed attributes bcuz when i tried serializing
                //candidates objects as they are I got this error "self referencing loop detected with type" it means json tried to serialize the candidate object
                //but it found that each candidate has an Election object, and this election object has a list of candidates and so on, so i excluded election
                //from the selection to avoid the infinite loop

                //declaring an expression that is special to Election objects
                Expression<Func<Candidate, bool>> expr = e => e.Election.Id == Guid.Parse(electionId) && e.isNeutralOpinion !=true;

                var candidates = _candidateBusiness.GetAllFiltered(expr);
                if (candidates == null)
                {
                    throw new BusinessException(_messagesLoclizer["Candidates List not found."]);
                }


                var json = JsonConvert.SerializeObject(_candidateBusiness.ConvertCandidateList_ToCandidateViewModelList(candidates));
                return Ok(json);

            }
            catch (BusinessException be)
            {
                //lets create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = be.Message });
            }
            catch (Exception E)
            {
                //lets create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
            }
        }




        // GET: Election/Edit/5
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public ActionResult Edit(Guid id)
        {
            try
            {
                if (id == null)
                {
                    throw new BusinessException(_messagesLoclizer["Passed parameter 'id' can not be null"]);
                }

                Election election = _electionBusiness.GetById(id);
                if (election == null)
                {
                    throw new BusinessException(_messagesLoclizer["State not found"]);
                }

                return View(election);
            }
            catch (BusinessException be)
            {
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        // POST: Election/Edit/5
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public async Task<IActionResult> EditElection([FromBody] TemporaryElection election)
        {
            //this method is called using ajax calls
            //so if a business rule is not met we'll throw a businessException and catch it to create and internal server error and return its msg
            //as json
            try
            {
                if (ModelState.IsValid)
                {
                    _electionBusiness.EditElection(election);

                    return Json(new { success = true });
                }
                else
                {
                    //Model is not valid

                    //so there is a business rule not met, lets throw a businessException and catch it
                    throw new BusinessException(_messagesLoclizer["Data not valid, please check again."]);                    
                }
            }
            catch (DataNotUpdatedException bnu)
            {
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", bnu.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (BusinessException be)
            {
                //lets create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = be.Message });
            }
            catch (Exception E)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
                //In above code I created an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
            }
        }



        // GET: Election/Delete/5
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public ActionResult Delete(Guid id)
        {
            try
            {
                if (id == null)
                {
                    throw new BusinessException(_messagesLoclizer["Passed parameter 'id' can not be null"]);
                }

                var election = _electionBusiness.GetById(id);
                if (election == null)
                {
                    throw new BusinessException(_messagesLoclizer["Election not found"]);
                }


                return View(_electionBusiness.ConvertElection_ToElectionViewModel(election));
            }
            catch (BusinessException be)
            {
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        // POST: Election/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public ActionResult DeleteElection(Guid id)
        {
            //removing an election means removing all votes and candidates of it
            try
            {
                
                if (id == null)
                {
                    throw new BusinessException(_messagesLoclizer["Passed parameter 'id' can not be null"]);
                }

                //1- Remove all Votes related to this Election
                //declaring an expression that is special to Vote objects
                Expression<Func<Vote, bool>> expr1 = e => e.Election.Id == id;
                List<Vote> votesList = _voteBusiness.GetAllFiltered(expr1);
                foreach (var vote in votesList)
                {                    
                    int updatedRows5 = _voteBusiness.Delete(vote.Id);
                    if (updatedRows5 < 1)
                    {
                        //row not updated in the DB
                        throw new DataNotUpdatedException(_messagesLoclizer["Data not updated, operation failed."]);
                    }
                }


                //2- Remove all Candidates of this Election
                //declaring an expression that is special to Election objects
                Expression<Func<Candidate, bool>> expr2 = e => e.Election.Id == id;
                List<Candidate> candidatesList = _candidateBusiness.GetAllFiltered(expr2);
                foreach (var candidate in candidatesList)
                {                    
                    int updatedRows6 = _candidateBusiness.Delete(candidate.Id);
                    if (updatedRows6 < 1)
                    {
                        //row not updated in the DB
                        throw new DataNotUpdatedException(_messagesLoclizer["Data not updated, operation failed."]);
                    }
                }


                //3- Now remove the Election from the db                
                int updatedRows = _electionBusiness.Delete(id);
                if (updatedRows > 0)
                {
                    //row updated successfully in the DB
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
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", bnu.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (BusinessException be)
            {
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View(nameof(Delete));
            }
            catch(Exception E)
            {
                return View();
            }
        }















        //this method is called using jQuery ajax and it returns a list of future elections
        //it is called when displaying the home page
        //I couldn't move it to ElectionUtilities.cs file bcuz if I did they will need _electionRepository to be passed
        //as a parameter (Method Dependancy Injection), and we know this method is called using jQuery ajax, there is no way to pass
        //_repositoryElection as a paramter from frontend (jQuery ajax)
        //since it is used in dashboard, all authenticated users can use it
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetComingElections()
        {
            //this method is called using ajax calls
            //so if a business rule is not met we'll throw a businessException and catch it to create and internal server error and return its msg
            //as json


            try
            {
                //lets serialize the list of elections we've got and send it back as a reponse
                //note that I didn't retrieve elections as they are, I selected only needed attributes bcuz when i tried serializing
                //elections objects as they are I got this error "self referencing loop detected with type" it means json tried to serialize the election object
                //but it found that each election has an Candidates objects list, and each candidate of them has an election and so on, so i excluded Candidate
                //from the selection to avoid the infinite loop

                //declaring an expression that is special to Election objects
                Expression<Func<Election, bool>> expr = e => e.StartDate > DateTime.Now;

                var futureElections = _electionBusiness.GetAllFiltered(expr).Select(e => new { e.Name, e.StartDate, e.DurationInDays, e.Candidates.Count});




                JsonSerializerSettings settings = new JsonSerializerSettings { DateFormatString = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern };
                var json = JsonConvert.SerializeObject(futureElections, settings);
                return Ok(json);


            }
            catch (Exception E)
            {
                HttpContext.Response.StatusCode = 500;

                return Json(new { Message = E.Message });
            }
        }



        //this method is called using jQuery ajax and it returns the current election
        //it is called when displaying the home page
        //I couldn't move it to ElectionUtilities.cs file bcuz if I did they will need _electionRepository to be passed
        //as a parameter (Method Dependancy Injection), and we know this method is called using jQuery ajax, there is no way to pass
        //_repositoryElection as a paramter from frontend (jQuery ajax)
        //since it is used in dashboard, all authenticated users can use it
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetCurrentElection()
        {
            try
            {
                //lets serialize the list of elections we've got and send it back as a reponse
                //note that I didn't retrieve elections as they are, I selected only needed attributes bcuz when i tried serializing
                //elections objects as they are I got this error "self referencing loop detected with type" it means json tried to serialize the election object
                //but it found that each election has an Candidates objects list, and each candidate of them has an election and so on, so i excluded Candidate
                //from the selection to avoid the infinite loop


                //declaring an expression that is special to Election objects
                Expression<Func<Election, bool>> expr =
                    e => e.StartDate <= DateTime.Now && DateTime.Now <= e.StartDate.AddDays(e.DurationInDays);



                var currentElection = _electionBusiness.GetOneFiltered(expr)
                    ;

                
                if (currentElection != null)
                {
                    CurrentElectionDashboard a = new CurrentElectionDashboard();
                    a.Id = currentElection.Id;
                    a.Name = currentElection.Name;
                    a.StartDate = currentElection.StartDate;
                    a.DurationInDays = currentElection.DurationInDays;
                    a.CandidatesCount = currentElection.Candidates.Count;

                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);

                    bool userHasVoted = false;
                    if (!await _userManager.IsInRoleAsync(currentUser, "Voter"))
                    {
                        userHasVoted = false;
                    }
                    else
                    {
                        userHasVoted = _voteBusiness.HasVoted(currentElection.Id, _voterBusiness.GetVoterByUserId(Guid.Parse(currentUser.Id)).Id);
                    }
                    
                    a.HasUserVoted = userHasVoted;

                    a.ParticipationRate = (double)_voteBusiness.GetNumberOfVotersVotedOnElection(currentElection.Id) / _voterBusiness.GetAll().Count;

                    //lets build the settings so that when serializing the object into json we will respect the datetime format according to the selected culture by user
                    JsonSerializerSettings settings = new JsonSerializerSettings { DateFormatString = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern };
                    var json = JsonConvert.SerializeObject(a, settings);
                    return Ok(json);
                }
                else
                {
                    //so there is no election currently, lets return null as a response
                    var json = JsonConvert.SerializeObject(null);

                    return Ok(json);
                }

            }
            catch (Exception E)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
                //In above code I created an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
            }
        }

        #region JQUERY DATATABLES REGION
        public IActionResult PreviousElectionsDataTable()
        {
            //This method is called by jQuery datatables to get paged data
            //First, we'll try to read the variables sent from the jQuery request, and then, based on these variables' values we'll query
            //the db


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



                //declaring an expression that is special to Election objects
                Expression<Func<Election, bool>> expr;
                //now lets look for a value in FirstName/LastName/StateName if user asked to
                if (!string.IsNullOrEmpty(searchValue))
                {
                     expr = e => e.StartDate.AddDays(e.DurationInDays) < DateTime.Now
                    && e.Name.Contains(searchValue);
                }
                else
                {
                    //so user didn't ask for filtering, he only asked for paging
                    expr = e => e.StartDate.AddDays(e.DurationInDays) < DateTime.Now;
                }
                //lets get the list of elections paged
                PagedResult<Election> pagedResult1 = _electionBusiness.GetAllFilteredPaged(expr, sortColumnName, sortColumnDirection, skip, pageSize);

                //Now the pagedResult we've got has two properties: pagedResult.Items and pagedResult.TotalCount
                //the first one is a list of Elections with their Candidates icnluded. When I try to serialise it to json
                //I got the following error: 
                //Self referencing loop detected for property 'Election' with type 'WebApplication1.Models.Election'. Path 'data[1].Candidates[0]'.
                //so what we need is to create another PagedResult<Election> object 'pagedResult2' and fill it with data that we have in pagedResult1
                //but instead of filling the list o candidates for each Election we'll only fill the count of the candidates
                //which means we'll use a list of ElectionViewModels instead of Elections


                List<ElectionViewModel> electionViewModels = new List<ElectionViewModel>();
                    foreach (var election in pagedResult1.Items)
                    {
                        ElectionViewModel e = new ElectionViewModel();
                        e.Id = election.Id;
                        e.Name = election.Name;
                        e.StartDate = election.StartDate;
                        e.DurationInDays = election.DurationInDays;
                        e.HasNeutral = election.HasNeutral;
                        e.NumberOfCandidates = election.Candidates.Count;
                        e.NumberOfVotes = election.Votes.Count;
                        electionViewModels.Add(e);
                    }
                    PagedResult<ElectionViewModel> pagedResult2 = new PagedResult<ElectionViewModel>(electionViewModels, pagedResult1.TotalCount);

                    //lets assign totalRecords the correct value
                    totalRecords = pagedResult2.TotalCount;

                //now lets return json data so that it is understandable by jQuery      
                JsonSerializerSettings settings = new JsonSerializerSettings { DateFormatString = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern };
                var json = JsonConvert.SerializeObject(new
                {
                    draw = draw,
                    recordsFiltered = totalRecords,
                    recordsTotal = totalRecords,
                    data = pagedResult2.Items
                }, settings);
                return Ok(json);
                
            }
            catch(Exception E)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
                //In above code I created an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
            }
        }


        #endregion


        [HttpPost]
        public IActionResult ExportToExcel()
        {
            //This function download list of all Elections as excel file
            try
            {
                var stream = new System.IO.MemoryStream();
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    var elections = _electionBusiness.GetAll();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(_messagesLoclizer["Elections"]);

                    worksheet.Cells[1, 1].Value = _messagesLoclizer["Name"];
                    worksheet.Cells[1, 2].Value = _messagesLoclizer["Start Date"];
                    worksheet.Cells[1, 3].Value = _messagesLoclizer["Duration (d)"];
                    worksheet.Cells[1, 4].Value = _messagesLoclizer["Neutral Candidate (Y/N)"];
                    worksheet.Cells[1, 5].Value = _messagesLoclizer["Candidates"];
                    worksheet.Row(1).Style.Font.Bold = true;


                    for (int c = 2; c < elections.Count + 2; c++)
                    {
                        worksheet.Cells[c, 1].Value = elections[c - 2].Name;
                        worksheet.Cells[c, 2].Value = elections[c - 2].StartDate.ToShortDateString();
                        worksheet.Cells[c, 3].Value = elections[c - 2].DurationInDays;
                        if (elections[c - 2].HasNeutral)
                        {
                            worksheet.Cells[c, 4].Value = _messagesLoclizer["Y"];
                        }
                        else
                        {
                            worksheet.Cells[c, 4].Value = _messagesLoclizer["N"];
                        }
                        worksheet.Cells[c, 5].Value = elections[c - 2].Candidates.Count.ToString();
                    }

                    package.Save();
                }

                StringBuilder fileName = new StringBuilder();
                fileName.Append(_messagesLoclizer["Elections"] + ".xlsx");

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