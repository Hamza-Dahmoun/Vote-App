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
        //this is only used to get able to generate a 'code' needed to reset the password, and to get the currentUser ID
        private readonly UserManager<IdentityUser> _userManager;
        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public VoteController(IRepository<Candidate> candidateRepository, IRepository<Vote> voteRepository, IRepository<Voter> voterRepository, UserManager<IdentityUser> userManager)
        {
            _candidateRepository = candidateRepository;
            _voteRepository = voteRepository;
            _voterRepository = voterRepository;
            _userManager = userManager;
        }


        public IActionResult Index()
        {//this action returns a view containing all candidates for the user to vote on five of them maximum
            return View(Utilities.convertCandidateList_toPersonViewModelList(_candidateRepository.GetAll()));
        }

        [HttpPost]
        public async Task<IActionResult> ValidateVote([FromBody] List<string> candidateIdList)
        {//this action get the list of the candidates ids that the user voted on, and add them to the db as vote objects, and redirect to the
            //dashboard in home controller

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
                _voteRepository.Add(v);
            }

            //-------IMPORTANT: THIS ACTION IS ACCESSIBLE USING AN AJAX CALL, IN THIS CASE, TRYING TO REDIRECTTOACTION FROM
            //C# CODE WILL EXECUTED THE ACTION BUT THE BROWSER WILL IGNORE REDIRECTING, USER WILL STAY IN THE SAME PAGE
            //BROWSERS IGNORE THE REDIRECT BECUZ IT ASSUME JS CODE WHICH DID THE AJAX CALL WILL BE IN CHARGE OF THE SUCCESS
            //RESPONSE TO REDIRECT: WINDOW.LOCATION.HREF="CONTROLLERNAME/ACTION"
            //return RedirectToAction("Index", "Home");
            return  Json(new
            {
                success = true
            });
        }

        
    }
}