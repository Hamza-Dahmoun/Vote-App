using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class ElectionController : Controller
    {
        //the below are services we're going to use in this controller, they will be injected in the constructor
        public IRepository<Election> _electionRepository { get; }
        //public IRepository<ElectionVoter> _electionVoterRepository { get; }
        //public IRepository<ElectionCandidate> _electionCandidateRepository { get; }
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<Candidate> _candidateRepository { get; }
        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public ElectionController(
            IRepository<Voter> voterRepository,
            IRepository<Candidate> candidateRepository,
            IRepository<Election> electionRepository
            //IRepository<ElectionVoter> electionVoterRepository
            /*, IRepository<ElectionCandidate> electionCandidateRepository*/)
        {
            _voterRepository = voterRepository;
            _candidateRepository = candidateRepository;
            _electionRepository = electionRepository;
            //_electionVoterRepository = electionVoterRepository;
            //_electionCandidateRepository = electionCandidateRepository;
        }


        // GET: Election
        public ActionResult Index()
        {
            //try
            //{
                //returning a list of ElectionViewModel
                return View(Utilities.convertElectionList_toElectionViewModelList(_electionRepository.GetAll()));
            //}
            //catch
            //{
            //    return View();
            //}            
        }

        // GET: Election/Details/5
        public ActionResult Details(Guid id)
        {
            return View(Utilities.convertElection_toElectionViewModel(_electionRepository.GetById(id)));
        }

        // GET: Election/Create
        //if we try to imagine the scenario of adding a new Election I think it will be done in two steps:
        //--> Step1: Adding info of the election(name, duration,,,) and send them to backend using api method in inside the controller,
        //then send back the response to javascript to redirect to step2
        //--> Step2: Selecting the candidates of this Election from a list of Voters, and send them to backend using api method in inside the controller,
        //then send back the response to javascript to redirect to Index.

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
            //this Structure is used to return a javascript object containing the newly inserted election and a list of voters for user to select
            //from them ass candidates
            public Election Election;
            public List<PersonViewModel> Voters;
        }
        //this is a web api called when adding a new Election instance
        [HttpPost]
        //[ValidateAntiForgeryToken] If I uncomment this the api will not work
        public async Task<IActionResult> ValidateElection([FromBody] Election election)
        {
            //Step(1): Adding info of the election(name, duration,,,) and send them to backend using api method in inside the controller,
            //then send back the response to javascript to redirect to step2

            try
            {
                if (ModelState.IsValid)
                {
                    election.Id = Guid.NewGuid();
                    //if election has a neutral opinion then we should add it to the db
                    if (election.HasNeutral)
                    {
                        Candidate neutral = CandidateUtilities.GetNeutralCandidate(_candidateRepository);
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
                    //-------IMPORTANT: THIS ACTION IS ACCESSIBLE USING AN AJAX CALL, IN THIS CASE, TRYING TO REDIRECTTOACTION FROM
                    //C# CODE WILL EXECUTED THE ACTION BUT THE BROWSER WILL IGNORE REDIRECTING, USER WILL STAY IN THE SAME PAGE
                    //BROWSERS IGNORE THE REDIRECT BECUZ IT ASSUME JS CODE WHICH DID THE AJAX CALL WILL BE IN CHARGE OF THE SUCCESS
                    //RESPONSE TO REDIRECT: WINDOW.LOCATION.HREF="CONTROLLERNAME/ACTION"
                    //return RedirectToAction("Index", "Home");
                    response_Voters_and_NewElection r;
                    r.Election = election;
                    r.Voters = Utilities.convertVoterList_toPersonViewModelList(_voterRepository.GetAll());

                    //lets serialize the struct we've got and send it back as a reponse
                    var json = JsonConvert.SerializeObject(r);
                    return Ok(json);
                }
                else
                {
                    return BadRequest();
                    //return Json(new { ErrorMessage = "Error" });
                }
                
            }
            catch(Exception E)
            {
                return BadRequest();
                //return Json(new { ErrorMessage = "Error" });
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
                        FirstName = voter.FirstName,
                        LastName = voter.LastName,
                        VoterBeing = voter,
                        State = voter.State,
                        Election = _electionRepository.GetById(mydata.electionId)
                    }
                    );
                return Json(new { success = true }); ;
            }
            catch (Exception E)
            {
                return BadRequest();
            }
        }


        //this is a web api called when user remove a candidate from an election
        [HttpPost]
        public async Task<IActionResult> RemoveCandidate([FromBody] CandidateElectionRelation mydata)
        {
            try
            {
                if (mydata.electionId == null || mydata.voterId == null)
                {
                    return BadRequest();
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
                return BadRequest();
            }
        }




        //this method is called using jQuery ajax and it returns a list of candidates related to the election
        //it is called when displaying the details of an election
        [HttpPost]
        public async Task<IActionResult> GetCandidatesList_byElectionId([FromBody] string electionId)
        {
            try
            {
                Election e = _electionRepository.GetById(Guid.Parse(electionId));
                //lets serialize the list of candidates of the election we've got and send it back as a reponse
                //note that I didn't retrieve candidates as they are, I selected only needed attributes bcuz when i tried serializing
                //candidates objects as they are I got this error "self referencing loop detected with type" it means json tried to serialize the candidate object
                //but it found that each candidate has an Election object, and this election object has a list of candidates and so on, so i excluded election
                //from the selection to avoid the infinite loop
                var candidates = e.Candidates.Select(p => new { p.FirstName, p.LastName, p.State}).ToList();
                var json = JsonConvert.SerializeObject(candidates);
                return Ok(json);
                
            }
            catch(Exception E)
            {
                return BadRequest();
            }
        }


        


        //this method is called using jQuery ajax and it returns a list of candidates related to the election folllowed by the list of all voters 
        //it is called when displaying the election for editing
        [HttpPost]
        public async Task<IActionResult> GetCandidatesList_andVotersList_byElectionId([FromBody] string electionId)
        {
            try
            {
                Election election = _electionRepository.GetById(Guid.Parse(electionId));
                //lets serialize the list of candidates of the election we've got and send it back as a reponse
                //note that I didn't retrieve candidates as they are, I selected only needed attributes bcuz when i tried serializing
                //candidates objects as they are I got this error "self referencing loop detected with type" it means json tried to serialize the candidate object
                //but it found that each candidate has an Election object, and this election object has a list of candidates and so on, so i excluded election
                //from the selection to avoid the infinite loop


                var candidates = CandidateUtilities.GetCandidate_byElection(_candidateRepository, election);
                List<VoterCandidateEntityViewModel> entityList = new List<VoterCandidateEntityViewModel>();
                entityList = Utilities.convertCandidateList_toVoterCandidateEntityViewModelList(entityList, candidates);
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
        public ActionResult Edit(Guid id)
        {
            Election election = _electionRepository.GetById(id);
            return View(election);
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
        public async Task<IActionResult> EditElection([FromBody] TemporaryElection election)
        {
            try
            {

                Election myElection = new Election
                {
                    Id = Guid.Parse(election.Id),
                    Name = election.Name,
                    StartDate = DateTime.Parse(election.StartDate),
                    DurationInDays = int.Parse(election.DurationInDays),
                    HasNeutral = bool.Parse(election.HasNeutral)
                };
                _electionRepository.Edit(myElection.Id, myElection);
                return Json(new { success = true });
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Election/Delete/5
        public ActionResult Delete(Guid id)
        {
            var election = _electionRepository.GetById(id);
            return View(Utilities.convertElection_toElectionViewModel(election));
        }

        // POST: Election/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteElection(Guid id)
        {
            try
            {
                _electionRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}