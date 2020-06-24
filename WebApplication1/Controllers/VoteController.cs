using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Repositories;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Business;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    //the below attribute will permit only authorized users to access HomeController, anonymous access will be deactivated
    [Authorize]
    //the below attribute will permit only users with set of roles contained in the policy 'DoVote'
    //(you can check the set of roles related to this policy in ConfigureServices() in Startup file)
    [Authorize(Policy = nameof(VoteAppPolicies.DoVote))]
    public class VoteController : Controller
    {
        //the below are services we're going to use in this controller, they will be injected in the constructor
        public IRepository<Candidate> _candidateRepository { get; }
        public IRepository<Vote> _voteRepository { get; }
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<Election> _electionRepository { get; }
        //this is only used to get able to generate a 'code' needed to reset the password, and to get the currentUser ID
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<VoteController> _logger;
        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public VoteController(
            IRepository<Candidate> candidateRepository, 
            IRepository<Vote> voteRepository, 
            IRepository<Voter> voterRepository,
            IRepository<Election> electionRepository,
            UserManager<IdentityUser> userManager,
            ILogger<VoteController> logger)
        {
            _candidateRepository = candidateRepository;
            _voteRepository = voteRepository;
            _voterRepository = voterRepository;
            _electionRepository = electionRepository;
            _userManager = userManager;
            _logger = logger;
        }


        public IActionResult Index()
        {
            try
            {
                //this action returns a view containing all candidates of the current election for the user to vote on five of them maximum
                Election election = ElectionUtilities.getCurrentElection(_electionRepository);// _electionRepository.GetById(CurrentElectionId);
                var candidates = CandidateUtilities.GetCandidate_byElection(_candidateRepository, election);
                //return View(Utilities.convertCandidateList_toCandidateViewModelList(_voterRepository, _candidateRepository.GetAll()));
                return View(Utilities.convertCandidateList_toCandidateViewModelList(_voterRepository, candidates));
            }
            catch(Exception E)
            {
                return BadRequest(E.Message);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> ValidateVote([FromBody] List<string> candidateIdList)
        {//this action gets the list of the candidates ids that the user voted on, and add them to the db as vote objects, 
            //and return a list of candidates ordered by number of votes

            //it can return two sort of Exception, one when voting, the second when retrieving results of the election


            int exceptionDifferentiator = 0;
            try
            {
                if (candidateIdList == null || candidateIdList.Count <= 0)
                {
                    return BadRequest();
                }
                //lets first get the concerned election
                Candidate firstOne = _candidateRepository.GetById(Guid.Parse(candidateIdList.FirstOrDefault()));
                Election election = _electionRepository.GetById(firstOne.Election.Id);
                if (election == null)
                {
                    return BadRequest();
                }

                //lets get the voter instance of the current user, so that we use its id with his votes
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                Voter currentVoter = VoterUtilities.getVoterByUserId(Guid.Parse(currentUser.Id), _voterRepository);


                Vote v = new Vote();
                //lets add 'Vote' objects to the db
                foreach (var candidateId in candidateIdList)
                {
                    v.Id = Guid.NewGuid();
                    v.Candidate = _candidateRepository.GetById(Guid.Parse(candidateId));
                    v.Voter = currentVoter; // _voterRepository.GetById(Guid.Parse("3fae2a0e-21fe-40d4-a731-6b92bb4fea71"));
                    v.Datetime = DateTime.Now;
                    v.Election = election;
                    _voteRepository.Add(v);
                }

                exceptionDifferentiator = 1;
                
                //-------IMPORTANT: THIS ACTION IS ACCESSIBLE USING AN AJAX CALL, IN THIS CASE, TRYING TO REDIRECTTOACTION FROM
                //C# CODE WILL EXECUTED THE ACTION BUT THE BROWSER WILL IGNORE REDIRECTING, USER WILL STAY IN THE SAME PAGE
                //BROWSERS IGNORE THE REDIRECT BECUZ IT ASSUME JS CODE WHICH DID THE AJAX CALL WILL BE IN CHARGE OF THE SUCCESS
                //RESPONSE TO REDIRECT: WINDOW.LOCATION.HREF="CONTROLLERNAME/ACTION"
                //return RedirectToAction("Index", "Home");
                /*return Json(new
                {
                    success = true
                });*/

                //everything is okey, lets return a list of candidates with votes counter ordered so that the winner is the first
                var candidates = CandidateUtilities.GetCandidate_byElection(_candidateRepository, election);
                List<CandidateViewModel> candidatesViewModel = Utilities.convertCandidateList_toCandidateViewModelList(_voterRepository, candidates);
                //lets serialize the list of candidatesviewmodel as json object
                var json = JsonConvert.SerializeObject(candidatesViewModel.OrderByDescending(c => c.VotesCount));
                return Ok(json);
            }
            catch(Exception E)
            {
                //there are two reasons for possible exceptions: one when voting, the second when retrieving results of the election
                if(exceptionDifferentiator == 0)
                {
                    //so the exception happened when trying to validate the votes
                    HttpContext.Response.StatusCode = 500;
                    return Json(new { Message = "Error When Validating Votes! " +  E.Message });
                    //In above code I created an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                }
                else
                {
                    //so the exception happened when trying to get the results of the election
                    HttpContext.Response.StatusCode = 500;
                    return Json(new { Message = "Error When Trying to Get The Results! " + E.Message });
                    //In above code I created an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                }
            }
        }

        
    }
}