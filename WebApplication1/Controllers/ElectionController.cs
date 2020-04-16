using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Business;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

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
        public IRepository<ElectionVoter> _electionVoterRepository { get; }
        public IRepository<ElectionCandidate> _electionCandidateRepository { get; }
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<Candidate> _candidateRepository { get; }
        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public ElectionController(
            IRepository<Voter> voterRepository,
            IRepository<Candidate> candidateRepository,
            IRepository<Election> electionRepository,
            IRepository<ElectionVoter> electionVoterRepository,
            IRepository<ElectionCandidate> electionCandidateRepository)
        {
            _voterRepository = voterRepository;
            _candidateRepository = candidateRepository;
            _electionRepository = electionRepository;
            _electionVoterRepository = electionVoterRepository;
            _electionCandidateRepository = electionCandidateRepository;
        }


        // GET: Election
        public ActionResult Index()
        {
            try
            {
                return View(Utilities.convertElectionList_toElectionViewModelList(_electionRepository.GetAll()));
            }
            catch
            {
                return View();
            }            
        }

        // GET: Election/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Election/Create
        //if we try to imagine the scenario of adding a new Election I think it will be done in three steps:
        //--> Step1: Adding info of the election(name, duration,,,) and send them to backend using api method in inside the controller,
        //then send back the response to javascript to redirect to step2
        //--> Step2: Selecting voters concerned by this Election (selecting only, bcuz adding voters is done in VoterController),,, 
        //and send them to backend using api method in inside the controller, then send back the response to javascript to redirect to step3
        //--> Step3: Selecting the candidates of this Election from its selectedvoters, and send them to backend using api method in inside the controller, then send back the response to javascript
        //to redirect to Index.

        public ActionResult Create()
        {
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


        [HttpPost]
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
                    return Json(new { success = true });                    
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




















        // GET: Election/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Election/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Election/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Election/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}