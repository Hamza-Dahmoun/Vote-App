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

namespace WebApplication1.Controllers
{
    //the below attribute will permit only authorized users to access HomeController, anonymous access will be deactivated
    [Authorize]
    public class VoteController : Controller
    {
        public IRepository<Candidate> _candidateRepository { get; }
        public IRepository<Vote> _voteRepository { get; }
        public IRepository<Voter> _voterRepository { get; }
        //this is only used to get able to generate a 'code' needed to reset the password, and to get the currentUser ID
        private readonly UserManager<IdentityUser> _userManager;
        public VoteController(IRepository<Candidate> candidateRepository, IRepository<Vote> voteRepository, IRepository<Voter> voterRepository, UserManager<IdentityUser> userManager)
        {
            _candidateRepository = candidateRepository;
            _voteRepository = voteRepository;
            _voterRepository = voterRepository;
            _userManager = userManager;
        }


        public IActionResult Index()
        {//this action returns a view containing all candidates for the user to vote on five of them maximum
            return View(convertCandidateList_toPersonViewModelList(_candidateRepository.GetAll()));
        }

        [HttpPost]
        public async Task<IActionResult> ValidateVote([FromBody] List<string> candidateIdList)
        {//this action get the list of the candidates ids that the user voted on, and add them to the db as vote objects, and redirect to the
            //dashboard in home controller

            //lets get the voter instance of the current user, so that we use its id with his votes
            var currentUser = await getCurrentUser();
            Voter currentVoter = getVoterByUserId(Guid.Parse(currentUser.Id));


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
            //BROWSERS IGNORE THE REDIRECT BECUZ THE ASSUME JS CODE WHICH DID THE AJAX CALL WILL BE IN CHARGE OF THE SUCCESS
            //RESPONSE TO REDIRECT: WINDOW.LOCATION.HREF="CONTROLLERNAME/ACTION"
            //return RedirectToAction("Index", "Home");
            return  Json(new
            {
                success = true
            });
        }






        //************** UTILITIES
        public CandidateViewModel convertCandidate_toCandidateViewModel(Candidate candidate)
        {
            CandidateViewModel c = new CandidateViewModel
            {
                Id = candidate.Id,
                isNeutralOpinion = candidate.isNeutralOpinion,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                StructureName = candidate.Structure?.Name,
                StructureLevel = candidate.Structure?.Level.Name,
                VotesCount = candidate.Votes.Count(),
            };
            if (candidate.VoterBeing.hasVoted())
                c.hasVoted = "Yes";
            else c.hasVoted = "No";
            return c;
        }

        public List<CandidateViewModel> convertCandidateList_toPersonViewModelList(IList<Candidate> candidates)
        {
            List<CandidateViewModel> myList = new List<CandidateViewModel>();
            foreach (var item in candidates)
            {
                myList.Add(convertCandidate_toCandidateViewModel(item));
            }

            return myList.OrderByDescending(c => c.VotesCount).ToList();
        }

        public Task<IdentityUser> getCurrentUser()
        {//this returns the current user instance, I'll use its Id to get its corresponding Voter instance
            return _userManager.GetUserAsync(HttpContext.User);
        }
        public Voter getVoterByUserId(Guid userId)
        {
            return _voterRepository.GetAll().SingleOrDefault(v => v.UserId == userId);
        }
    }
}