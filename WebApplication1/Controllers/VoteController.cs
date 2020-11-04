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
using Microsoft.Extensions.Localization;
using WebApplication1.BusinessService;

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
        private readonly CandidateBusiness _candidateBusiness;
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<Election> _electionRepository { get; }
        //this is only used to get able to generate a 'code' needed to reset the password, and to get the currentUser ID
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<VoteController> _logger;
        //Lets create a private readonly field IStringLocalizer<Messages> so that we can use Localization service, we'll inject it inside the constructor
        private readonly IStringLocalizer<Messages> _messagesLoclizer;
        private readonly VoteBusiness _voteBusiness;
        private readonly ElectionBusiness _electionBusiness;
        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public VoteController(
            IRepository<Candidate> candidateRepository, 
            IRepository<Voter> voterRepository,
            IRepository<Election> electionRepository,
            UserManager<IdentityUser> userManager,
            ILogger<VoteController> logger,
            IStringLocalizer<Messages> messagesLoclizer,
            CandidateBusiness candidateBusiness,
            ElectionBusiness electionBusiness,
            VoteBusiness voteBusiness)
        {
            _candidateBusiness = candidateBusiness;
            _candidateRepository = candidateRepository;
            _voterRepository = voterRepository;
            _electionRepository = electionRepository;
            _userManager = userManager;
            _logger = logger;
            _messagesLoclizer = messagesLoclizer;
            _electionBusiness = electionBusiness;
            _voteBusiness = voteBusiness;
        }


        public IActionResult Index()
        {
            _logger.LogInformation("VoteController/Index() action is called");
            try
            {
                //this action returns a view containing all candidates of the current election for the user to vote on five of them maximum
                _logger.LogInformation("Calling _voteBusiness.GetCandidatesViewModelList_OfCurrentElection() method");
                List<CandidateViewModel> cvmList = _voteBusiness.GetCandidatesViewModelList_OfCurrentElection();
                _logger.LogInformation("Returning a list of CandidateViewModel to Index view");
                return View(cvmList);
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
        public async Task<IActionResult> ValidateVote([FromBody] List<string> candidateIdList)
        {//this action gets the list of the candidates ids that the user voted on, and add them to the db as vote objects, 
            //and return a list of candidates ordered by number of votes

            //it can return two sort of Exception, one when voting, the second when retrieving results of the election

            _logger.LogInformation("VoteController/ValidateVote() method is called");

            int exceptionDifferentiator = 0;
            try
            {
                int updatedRows = await _voteBusiness.AddVotes(candidateIdList);
                if (updatedRows != candidateIdList.Count())
                {
                    //not all rows updated in the DB
                    throw new DataNotUpdatedException(_messagesLoclizer["Data not updated, operation failed."]);
                }
                _logger.LogInformation("Added Vote instance to the DB foreach Candidate");

                exceptionDifferentiator = 1;
                                
                //everything is okey, lets return a list of candidates with votes counter ordered so that the winner is the first
                _logger.LogInformation("Calling CandidateBusiness.GetCandidateViewModelList_byOneCandidateID() method");                
                List<CandidateViewModel>  candidatesViewModel = _candidateBusiness.GetCandidateViewModelList_byOneCandidateID(Guid.Parse(candidateIdList.FirstOrDefault()));
                
                //lets serialize the list of candidatesviewmodel as json object
                _logger.LogInformation("Going to serialize the list of CandidateViewModels");
                var json = JsonConvert.SerializeObject(candidatesViewModel.OrderByDescending(c => c.VotesCount));
                _logger.LogInformation("Returning the list of CadidateViewModel as a json");
                return Ok(json);
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
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = _messagesLoclizer["Error When Validating Votes!"] + " " + be.Message });
            }
            catch (Exception E)
            {
                //there are two reasons for possible exceptions: one when voting, the second when retrieving results of the election
                if(exceptionDifferentiator == 0)
                {
                    //so the exception happened when trying to validate the votes
                    _logger.LogError("Exception When Validating Votes! " + E.Message);
                    HttpContext.Response.StatusCode = 500;
                    return Json(new { Message = _messagesLoclizer["Error When Validating Votes!"] + " " +  E.Message });
                    //In above code I created an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                }
                else
                {
                    //so the exception happened when trying to get the results of the election
                    _logger.LogError("Exception When Trying to Get The Results! " + E.Message);
                    HttpContext.Response.StatusCode = 500;
                    return Json(new { Message = _messagesLoclizer["Error When Trying to Get The Results!"] + " " + E.Message });
                    //In above code I created an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                }
            }
        }
        

        
    }
}