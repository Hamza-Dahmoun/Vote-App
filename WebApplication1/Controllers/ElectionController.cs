using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Business;
using WebApplication1.Models;
using WebApplication1.Models.Helpers;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

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
        //public IRepository<ElectionVoter> _electionVoterRepository { get; }
        //public IRepository<ElectionCandidate> _electionCandidateRepository { get; }
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<Vote> _voteRepository { get; }
        public IRepository<Candidate> _candidateRepository { get; }
        //this is only used to get the currentUser so that we check whether he voted or not in order to generate the dashboard
        private readonly UserManager<IdentityUser> _userManager;
        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public ElectionController(
            IRepository<Vote> voteRepository,
            IRepository<Voter> voterRepository,
            IRepository<Candidate> candidateRepository,
            IRepository<Election> electionRepository,
            UserManager<IdentityUser> userManager
            //IRepository<ElectionVoter> electionVoterRepository
            /*, IRepository<ElectionCandidate> electionCandidateRepository*/)
        {
            _voteRepository = voteRepository;
            _voterRepository = voterRepository;
            _candidateRepository = candidateRepository;
            _electionRepository = electionRepository;
            _userManager = userManager;
            //_electionVoterRepository = electionVoterRepository;
            //_electionCandidateRepository = electionCandidateRepository;
        }


        // GET: Election
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public ActionResult Index()
        {
            try
            {
                //returning a list of Elections but without their neutral candidate
                
                return View(_electionRepository.GetAll().OrderByDescending(d => d.StartDate).
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
                    throw new BusinessException("Passed parameter 'id' can not be null");
                }

                Election e = _electionRepository.GetById(id);
                if (e == null)
                {
                    throw new BusinessException("Election not found");
                }

                return View(Utilities.convertElection_toElectionViewModel(e));
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
        // POST: Election/Create
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Election election)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    election.Id = Guid.NewGuid();
                    //if election has a neutral opinion then we should add it to the db
                    if (election.HasNeutral)
                    {
                        Candidate neutral = CandidateUtilities.GetNeutralCandidate();
                        if (neutral == null)
                        {//so there is no neutral opinion candidate in the db yet, lets insert it to use it
                            Candidate neutralOpinion = new Candidate
                            {
                                Id = Guid.NewGuid(),
                                FirstName = "Neutral",
                                LastName = "Opinion",
                                isNeutralOpinion = true
                            };
                            election.NeutralCandidateID = neutralOpinion.Id;
                            _electionRepository.Add(election);
                            _candidateRepository.Add(neutralOpinion);
                        }
                        else
                        {
                            //there is already a neutral opinion candidate stored in the db, lets use it
                            election.NeutralCandidateID = neutral.Id;
                            _electionRepository.Add(election);
                        }
                        
                    }
                    else
                    {
                        _electionRepository.Add(election);
                    }
                    
                    return RedirectToAction(nameof(Index));
                }
                return View(election);
            }
            catch
            {
                return View();
            }
        }
        */



        
        struct response_Voters_and_NewElection
        {
            //this Structure is used to return a javascript object containing the newly inserted election ID and a list of voters for user to select
            //from them ass candidates
            public Guid ElectionId;
            public List<PersonViewModel> Voters;
        }
        //this is a web api called when adding a new Election instance
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        //[ValidateAntiForgeryToken] If I uncomment this the api will not work
        public async Task<IActionResult> ValidateElection([FromBody] Election election)
        {
            //Step(1): Adding info of the election(name, duration,,,) and send them to backend using api method in inside the controller,
            //then send back the response to javascript to redirect to step2

            try
            {
                if (ModelState.IsValid)
                {
                    //first of all lets check if this election is in future, if it is not then we'll not edit it
                    if (election.StartDate <= DateTime.Now)
                    {
                        //so it is not a future election
                        //lets I create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                        HttpContext.Response.StatusCode = 500;
                        return Json(new { Message = "A New Election should take place in a future date." });                        
                    }

                    if (ElectionUtilities.getElectionsInSamePeriod(_electionRepository, election.StartDate, election.DurationInDays)>0)
                    {//so there is other existing elections which the period overlap with this new election's period

                        //lets I create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                        HttpContext.Response.StatusCode = 500;
                        return Json(new { Message = "There is an existing Election during the same period." });
                    }
                    if (election.DurationInDays < 0 || election.DurationInDays > 5)
                    {
                        //so the number of days is invalid
                        //lets create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                        HttpContext.Response.StatusCode = 500;
                        return Json(new { Message = "The duration of the Election should one to five days." });
                    }

                    election.Id = Guid.NewGuid();
                    //if election has a neutral opinion then we should add it to the db
                    if (election.HasNeutral)
                    {
                        Candidate neutralOpinion = new Candidate
                        {
                            Id = Guid.NewGuid(),
                            isNeutralOpinion = true,
                            Election = election
                        };
                        //election.NeutralCandidateID = neutralOpinion.Id;
                        _electionRepository.Add(election);
                        _candidateRepository.Add(neutralOpinion);
                        
                    }
                    else
                    {
                        _electionRepository.Add(election);
                    }
                    //-------IMPORTANT: THIS ACTION IS ACCESSIBLE USING AN AJAX CALL, IN THIS CASE, TRYING TO REDIRECTTOACTION FROM
                    //C# CODE WILL EXECUTED THE ACTION BUT THE BROWSER WILL IGNORE REDIRECTING, USER WILL STAY IN THE SAME PAGE
                    //BROWSERS IGNORE THE REDIRECT BECUZ IT ASSUME JS CODE WHICH DID THE AJAX CALL WILL BE IN CHARGE OF THE SUCCESS
                    //RESPONSE TO REDIRECT: WINDOW.LOCATION.HREF="CONTROLLERNAME/ACTION"
                    //return RedirectToAction("Index", "Home");




                    response_Voters_and_NewElection r;
                    r.ElectionId = election.Id;
                    r.Voters = Utilities.convertVoterList_toPersonViewModelList(
                        _voterRepository.GetAll());

                    //lets serialize the struct we've got and send it back as a reponse
                    var json = JsonConvert.SerializeObject(r);                
                return Ok(json);
                }
                else
                {
                    //Model is not valid

                    //lets I create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                    HttpContext.Response.StatusCode = 500;
                    return Json(new { Message = "Data not valid, please check again." });
                }
                
            }
            catch(Exception E)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
                //In above code I created an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
            }


        }
        [HttpPost]
        //If I leave [FromBody] next to the parameter the request will not even access this method, bcuz jquery already has passed parameters
        public async Task<IActionResult> VotersDataTable(Guid electionId)
        {//returns a list of voters 

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

                
                //lets first get the list of voterswho are already candidates of this election
                Election election = _electionRepository.GetById(electionId);
                List<Voter> alreadyCandidates = CandidateUtilities.GetVoterBeing_ofCandidatesList_byElection(_candidateRepository, election);
                List<Guid> excludedVotersIDs = alreadyCandidates.Select(v => v.Id).ToList();

                System.Linq.Expressions.Expression<Func<Voter, bool>> expr;
                //now lets look for a value in FirstName/LastName/StateName if user asked to
                if (!string.IsNullOrEmpty(searchValue))
                {

                    //declaring an expression that is special to Voter objects according to the search value and the fact that we don't want
                    //voters who are already candidates
                    expr =
                        v => (v.FirstName.Contains(searchValue) ||
                        v.LastName.Contains(searchValue) ||
                        v.State.Name.Contains(searchValue))
                        && !excludedVotersIDs.Contains(v.Id);                    
                }
                else
                {
                    //lets send a linq Expression exrpessing that we don't want voters who are already candidates                                        
                    expr = v => !excludedVotersIDs.Contains(v.Id);

                    //LINQ couldn't be translated with the below expressions
                    //expr = v => !alreadyCandidates.Any(a => a.Id == v.Id);
                    //expr = v => alreadyCandidates.All(a => a.Id != v.Id);
                }
                //lets get the list of voters filtered and paged
                PagedResult<Voter> pagedResult = _voterRepository.GetAllFilteredPaged(expr, sortColumnName, sortColumnDirection, skip, pageSize);
                
                //lets assign totalRecords the correct value
                totalRecords = pagedResult.TotalCount;

                    //now lets return json data so that it is understandable by jQuery                
                    var json = JsonConvert.SerializeObject(new
                    {
                        draw = draw,
                        recordsFiltered = totalRecords,
                        recordsTotal = totalRecords,
                        data = pagedResult.Items
                    }) ;
                    return Ok(json);

                
            }
            catch(Exception E)
            {

                //lets create and return an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message});
            }
        }

        //Step(2): Adding one Candidate to the Election each time
        public class CandidateElectionRelation
        {//this class is used to get the data sent by jQuery ajax to the method AddCandidates() below
            public Guid voterId { get; set; }
            public Guid electionId { get; set; }
        }
        //this is a web api called when user selects candidates from voters list to the election he created or he is editing
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public async Task<IActionResult> AddCandidates([FromBody] CandidateElectionRelation mydata)
        {
            try
            {
                if (mydata.electionId==null || mydata.voterId==null)
                {
                    return BadRequest();
                }
                Voter voter = _voterRepository.GetById(mydata.voterId);
                _candidateRepository.Add(
                    new Candidate
                    {
                        Id = Guid.NewGuid(),
                        /*FirstName = voter.FirstName,
                        LastName = voter.LastName,*/
                        VoterBeing = voter,
                        /*State = voter.State,*/
                        Election = _electionRepository.GetById(mydata.electionId)
                    }
                    );
                return Json(new { success = true }); ;
            }
            catch (Exception E)
            {
                //lets create and return an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
            }
        }
        //this is a web api called when user selects candidates from voters list to the election he created or he is editing
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public async Task<IActionResult> AddCandidate([FromBody] CandidateElectionRelation mydata)
        {
            //difference from above is that it returns the new candidate ID ... it is used when editing an election (after using jquey datatables)
            try
            {
                if (mydata.electionId == null || mydata.voterId == null)
                {
                    //lets create and return an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                    HttpContext.Response.StatusCode = 500;
                    return Json(new { Message = "Election or Voter not found." });
                }
                Voter voter = _voterRepository.GetById(mydata.voterId);
                Candidate newCandidate = new Candidate
                {
                    Id = Guid.NewGuid(),
                    VoterBeing = voter,
                    Election = _electionRepository.GetById(mydata.electionId)
                };
                _candidateRepository.Add(newCandidate);
                //now lets return json data so that it is understandable by jQuery                
                var json = JsonConvert.SerializeObject(new
                {
                    candidateId = newCandidate.Id
                }) ;
                return Ok(json);
                //return Json(new { success = true }); ;
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
        public async Task<IActionResult> RemoveCandidate([FromBody] CandidateElectionRelation mydata)
        {
            //this function removes a candidate from the db using its electionID and its voterBeing ID
            try
            {
                if (mydata.electionId == null || mydata.voterId == null)
                {
                    //lets create and return an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                    HttpContext.Response.StatusCode = 500;
                    return Json(new { Message = "Election Id and Voter Id cannot be Null." });
                }
                Candidate myCandidate = CandidateUtilities.GetCandidate_byVoter_byElection(
                    _candidateRepository, 
                    _voterRepository.GetById(mydata.voterId), 
                    _electionRepository.GetById(mydata.electionId));
                _candidateRepository.Delete(myCandidate.Id);
                return Json(new { success = true});
            }
            catch(Exception E)
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
            //this function removes a candidate from the db using its ID
            try
            {
                if (String.IsNullOrEmpty(candidateId))
                {
                    //lets create and return an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                    HttpContext.Response.StatusCode = 500;
                    return Json(new { Message = "Candidate Id cannot be Null." });
                }
                Candidate myCandidate = _candidateRepository.GetById(Guid.Parse(candidateId)) ;
                _candidateRepository.Delete(myCandidate.Id);
                return Json(new { success = true });
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
            try
            {
                if (String.IsNullOrEmpty(electionId))
                {
                    return BadRequest();
                }
                Election e = _electionRepository.GetById(Guid.Parse(electionId));
                //lets serialize the list of candidates of the election we've got and send it back as a reponse
                //note that I didn't retrieve candidates as they are, I selected only needed attributes bcuz when i tried serializing
                //candidates objects as they are I got this error "self referencing loop detected with type" it means json tried to serialize the candidate object
                //but it found that each candidate has an Election object, and this election object has a list of candidates and so on, so i excluded election
                //from the selection to avoid the infinite loop
                //var candidates = e.Candidates/*.Select(p => new { p.FirstName, p.LastName, p.State})*/.ToList();
                var candidates = CandidateUtilities.GetCandidate_byElection(_candidateRepository, e);
                var json = JsonConvert.SerializeObject(Utilities.convertCandidateList_toCandidateViewModelList(_voterRepository, candidates));
                return Ok(json);
                
            }
            catch(Exception E)
            {
                return BadRequest();
            }
        }


        //this method is called using jQuery ajax and it returns a list of candidates related to the election
        //it is called when displaying the details of an election (it gets also neutral opinion)
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public async Task<IActionResult> GetCandidatesList_byElectionId_ExcepNeutralOpinion([FromBody] string electionId)
        {
            try
            {
                if (String.IsNullOrEmpty(electionId))
                {
                    return BadRequest();
                }
                Election e = _electionRepository.GetById(Guid.Parse(electionId));
                //lets serialize the list of candidates of the election we've got and send it back as a reponse
                //note that I didn't retrieve candidates as they are, I selected only needed attributes bcuz when i tried serializing
                //candidates objects as they are I got this error "self referencing loop detected with type" it means json tried to serialize the candidate object
                //but it found that each candidate has an Election object, and this election object has a list of candidates and so on, so i excluded election
                //from the selection to avoid the infinite loop
                //var candidates = e.Candidates/*.Select(p => new { p.FirstName, p.LastName, p.State})*/.ToList();

                //declaring an expression that is special to Election objects
                System.Linq.Expressions.Expression<Func<Candidate, bool>> expr = e => e.Election.Id == Guid.Parse(electionId) && e.isNeutralOpinion !=true;

                var candidates = _candidateRepository.GetAllFiltered(expr);

                //var candidates = CandidateUtilities.GetCandidate_byElection(_candidateRepository, e);
                var json = JsonConvert.SerializeObject(Utilities.convertCandidateList_toCandidateViewModelList(_voterRepository, candidates));
                return Ok(json);

            }
            catch (Exception E)
            {
                return BadRequest();
            }
        }




        //this method is called using jQuery ajax and it returns a list of candidates related to the election folllowed by the list of all voters 
        //it is called when displaying the election for editing
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public async Task<IActionResult> GetCandidatesList_andVotersList_byElectionId([FromBody] string electionId)
        {
            try
            {
                if (String.IsNullOrEmpty(electionId))
                {
                    return BadRequest();
                }
                Election election = _electionRepository.GetById(Guid.Parse(electionId));
                //lets serialize the list of candidates of the election we've got and send it back as a reponse
                //note that I didn't retrieve candidates as they are, I selected only needed attributes bcuz when i tried serializing
                //candidates objects as they are I got this error "self referencing loop detected with type" it means json tried to serialize the candidate object
                //but it found that each candidate has an Election object, and this election object has a list of candidates and so on, so i excluded election
                //from the selection to avoid the infinite loop


                var candidates = CandidateUtilities.GetCandidate_byElection(_candidateRepository, election);
                List<VoterCandidateEntityViewModel> entityList = new List<VoterCandidateEntityViewModel>();
                entityList = Utilities.convertCandidateList_toVoterCandidateEntityViewModelList(_voterRepository, entityList, candidates);
                /*foreach (var candidate in candidates)
                {
                    VoterCandidateEntityViewModel vc = new VoterCandidateEntityViewModel();
                    vc.VoterId = candidate.VoterBeing.Id.ToString();
                    vc.FirstName = candidate.FirstName;
                    vc.LastName = candidate.LastName;
                    vc.StateName = candidate.State.Name;
                    vc.CandidateId = candidate.Id.ToString();
                    entityList.Add(vc);
                }*/

                var otherVoters = VoterUtilities.getOtherVoters(_voterRepository, Utilities.getCorrespondingVoters(candidates));
                entityList = Utilities.convertVoterList_toVoterCandidateEntityViewModelList(entityList, otherVoters);
                
                /*foreach (var v in otherVoters)
                {
                    VoterCandidateEntityViewModel vc = new VoterCandidateEntityViewModel();
                    vc.VoterId = v.Id.ToString();
                    vc.FirstName = v.FirstName;
                    vc.LastName = v.LastName;
                    vc.StateName = v.State.Name;
                    entityList.Add(vc);
                }*/


                var json = JsonConvert.SerializeObject(entityList);
                return Ok(json);

            }
            catch (Exception E)
            {
                return BadRequest();
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
                    throw new BusinessException("Passed parameter 'id' can not be null");
                }

                Election election = _electionRepository.GetById(id);
                if (election == null)
                {
                    throw new BusinessException("State not found");
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


        public struct TemporaryElection
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string StartDate { get; set; }
            public string DurationInDays { get; set; }
            public string HasNeutral { get; set; }
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
                    //first of all lets check if this election is in future, if it is not then we'll not edit it
                    if (DateTime.Parse(election.StartDate) <= DateTime.Now)
                    {
                        //so it is not a future election
                        //so there is a business rule not met, lets throw a businessException and catch it
                        throw new BusinessException("A New Election should take place in a future date.");
                    }
                    if (ElectionUtilities.getElectionsInSamePeriod(_electionRepository, DateTime.Parse(election.StartDate), int.Parse(election.DurationInDays)) > 1)
                    {
                        //so in addtion to the election instance to edit, there are other elections in the db from the same period
                        //so there is a business rule not met, lets throw a businessException and catch it
                        throw new BusinessException("There is an existing Election during the same period.");
                    }
                    if (int.Parse(election.DurationInDays) <0 || int.Parse(election.DurationInDays) > 5)
                    {
                        //so the number of days is invalid
                        //so there is a business rule not met, lets throw a businessException and catch it
                        throw new BusinessException("The duration of the Election should one to five days.");
                    }
                    //this variable is going to be used when checking if user updated hasNeutral opinion
                    bool oldHasNeutral = _electionRepository.GetById(Guid.Parse(election.Id)).HasNeutral;


                    Election myElection = new Election
                    {
                        Id = Guid.Parse(election.Id),
                        Name = election.Name,
                        StartDate = DateTime.Parse(election.StartDate),
                        DurationInDays = int.Parse(election.DurationInDays),
                        HasNeutral = bool.Parse(election.HasNeutral)
                    };

                    _electionRepository.Edit(myElection.Id, myElection);

                    //if hasNeutral field was updated then we should add/delete neutralCandidate from the db                
                    if (myElection.HasNeutral == oldHasNeutral)
                    {
                        //user didn't update hasNeutral property, lets proceed editing the Election instance
                        //so do nothing
                    }
                    else
                    {
                        //user did updated hasNeutral property
                        if (myElection.HasNeutral)
                        {
                            //lets add a neutral candidate to the db related to this instance of Election
                            Candidate neutralOpinion = new Candidate
                            {
                                Id = Guid.NewGuid(),
                                isNeutralOpinion = true,
                                Election = _electionRepository.GetById(myElection.Id)
                            };
                            _candidateRepository.Add(neutralOpinion);
                        }
                        else
                        {
                            //lets remove a neutral candidate instance from db which is related to this instance of Election
                            System.Linq.Expressions.Expression<Func<Candidate, bool>> expr = e => e.Election.Id == myElection.Id && e.isNeutralOpinion == true;
                            Candidate myNeutralCandidate = _candidateRepository.GetOneFiltered(expr);
                            _candidateRepository.Delete(myNeutralCandidate.Id);
                        }
                    }


                    return Json(new { success = true });
                }
                else
                {
                    //Model is not valid

                    //so there is a business rule not met, lets throw a businessException and catch it
                    throw new BusinessException("Data not valid, please check again.");                    
                }
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
                    throw new BusinessException("Passed parameter 'id' can not be null");
                }

                var election = _electionRepository.GetById(id);
                if (election == null)
                {
                    throw new BusinessException("Election not found");
                }

                return View(Utilities.convertElection_toElectionViewModel(election));
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
                //1- Remove all Votes related to this Election
                //declaring an expression that is special to Vote objects
                System.Linq.Expressions.Expression<Func<Vote, bool>> expr1 = e => e.Election.Id == id;
                List<Vote> votesList = _voteRepository.GetAllFiltered(expr1);
                foreach (var vote in votesList)
                {
                    _voteRepository.Delete(vote.Id);
                }


                //2- Remove all Candidates of this Election
                //declaring an expression that is special to Election objects
                System.Linq.Expressions.Expression<Func<Candidate, bool>> expr2 = e => e.Election.Id == id;
                List<Candidate> candidatesList = _candidateRepository.GetAllFiltered(expr2);
                foreach (var candidate in candidatesList)
                {
                    _candidateRepository.Delete(candidate.Id);
                }


                //3- Now remove the Election from the db
                _electionRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
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
            try
            {
                //lets serialize the list of elections we've got and send it back as a reponse
                //note that I didn't retrieve elections as they are, I selected only needed attributes bcuz when i tried serializing
                //elections objects as they are I got this error "self referencing loop detected with type" it means json tried to serialize the election object
                //but it found that each election has an Candidates objects list, and each candidate of them has an election and so on, so i excluded Candidate
                //from the selection to avoid the infinite loop

                //declaring an expression that is special to Election objects
                System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => e.StartDate > DateTime.Now;

                var futureElections = _electionRepository.GetAllFiltered(expr).Select(e => new { e.Name, e.StartDate, e.DurationInDays, e.Candidates.Count});
                var json = JsonConvert.SerializeObject(futureElections);

                //return Json(new { Success = false, Message = "error testing" });
                return Ok(json);

            }
            catch (Exception E)
            {
                HttpContext.Response.StatusCode = 500;
                ///return BadRequest();
                return Json(new { Message = E.Message });
            }
        }



        //this method is called using jQuery ajax and it returns a list of previous elections
        //it is called when displaying the home page
        //I couldn't move it to ElectionUtilities.cs file bcuz if I did they will need _electionRepository to be passed
        //as a parameter (Method Dependancy Injection), and we know this method is called using jQuery ajax, there is no way to pass
        //_repositoryElection as a paramter from frontend (jQuery ajax)
        //since it is used in dashboard, all authenticated users can use it
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetPreviousElections()
        {
            //this function returns a list of previous elections with their winners
            try
            {
                //lets serialize the list of elections we've got and send it back as a reponse
                //note that I didn't retrieve elections as they are, I selected only needed attributes bcuz when i tried serializing
                //elections objects as they are I got this error "self referencing loop detected with type" it means json tried to serialize the election object
                //but it found that each election has an Candidates objects list, and each candidate of them has an election and so on, so i excluded Candidate
                //from the selection to avoid the infinite loop



                //declaring an expression that is special to Election objects
                System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => e.StartDate.AddDays(e.DurationInDays) < DateTime.Now;


                var futureElections = _electionRepository.GetAllFiltered(expr).
                    Select(e => new
                    {
                        e.Id,
                        e.Name,
                        e.StartDate,
                        e.DurationInDays,
                        CandidatesCount = e.Candidates.Where(c => c.isNeutralOpinion != true).ToList().Count
                    }).
                    ToList();

                var json = JsonConvert.SerializeObject(futureElections);
                return Ok(json);

            }
            catch (Exception E)
            {
                return BadRequest();
            }
        }










        public struct CurrentElectionDashboard
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public int DurationInDays { get; set; }
            public int CandidatesCount { get; set; }
            public double ParticipationRate { get; set; }
            public bool HasUserVoted { get; set; }
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
                System.Linq.Expressions.Expression<Func<Election, bool>> expr =
                    e => e.StartDate <= DateTime.Now && DateTime.Now <= e.StartDate.AddDays(e.DurationInDays);



                var currentElection = _electionRepository.GetOneFiltered(expr)
                    /*.Select(e => new { e.Id, e.Name, e.StartDate, e.DurationInDays, e.Candidates.Count})*/
                    ;

                if(currentElection != null)
                {
                    CurrentElectionDashboard a = new CurrentElectionDashboard();
                    a.Id = currentElection.Id;
                    a.Name = currentElection.Name;
                    a.StartDate = currentElection.StartDate;
                    a.DurationInDays = currentElection.DurationInDays;
                    a.CandidatesCount = currentElection.Candidates.Count;

                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                    bool userHasVoted = VoteUtilities.hasVoted(_voteRepository, currentElection.Id, VoterUtilities.getVoterByUserId(Guid.Parse(currentUser.Id), _voterRepository).Id);
                    a.HasUserVoted = userHasVoted;

                    a.ParticipationRate = (double)VoteUtilities.getNumberOfVotersVotedOnElection(_voteRepository, currentElection.Id) / _voterRepository.GetAll().Count;

                    var json = JsonConvert.SerializeObject(a);
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




                //now lets look for a value in FirstName/LastName/StateName if user asked to
                if (!string.IsNullOrEmpty(searchValue))
                {
                    //declaring an expression that is special to Election objects
                    System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => e.StartDate.AddDays(e.DurationInDays) < DateTime.Now
                    && e.Name.Contains(searchValue);


                    //lets get the list of elections filtered and paged
                    PagedResult<Election> pagedResult1 = _electionRepository.GetAllFilteredPaged(expr, sortColumnName, sortColumnDirection, skip, pageSize);

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
                        electionViewModels.Add(e);
                    }
                    PagedResult<ElectionViewModel> pagedResult2 = new PagedResult<ElectionViewModel>(electionViewModels, pagedResult1.TotalCount);


                    //lets assign totalRecords the correct value
                    totalRecords = pagedResult2.TotalCount;

                    //now lets return json data so that it is understandable by jQuery                
                    var json = JsonConvert.SerializeObject(new
                    {
                        draw = draw,
                        recordsFiltered = totalRecords,
                        recordsTotal = totalRecords,
                        data = pagedResult2.Items
                    });
                    return Ok(json);

                }
                else
                {
                    //so user didn't ask for filtering, he only asked for paging

                    //declaring an expression that is special to Election objects
                    System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => e.StartDate.AddDays(e.DurationInDays) < DateTime.Now;

                    //lets get the list of elections paged
                    PagedResult<Election> pagedResult1 = _electionRepository.GetAllPaged(sortColumnName, sortColumnDirection, skip, pageSize);

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
                    var json = JsonConvert.SerializeObject(new
                    {
                        draw = draw,
                        recordsFiltered = totalRecords,
                        recordsTotal = totalRecords,
                        data = pagedResult2.Items
                    });

                    return Ok(json);
                }
            }
            catch(Exception E)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = E.Message });
                //In above code I created an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
            }
        }


        #endregion
    }
}