using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Business;
using WebApplication1.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace WebApplication1.Controllers
{
    //the below attribute will permit only authorized users to access HomeController, anonymous access will be deactivated
    [Authorize]
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IRepository<Candidate> _candidateRepository { get; }
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<Vote> _voteRepository { get; }
        //this is only used to get able to generate a 'code' needed to reset the password, and to get the currentUser ID
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ILogger<HomeController> logger, IRepository<Candidate> candidateRepository, IRepository<Voter> voterRepository, IRepository<Vote> voteRepository, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
            _voterRepository = voterRepository;
            _voteRepository = voteRepository;
            _userManager = userManager;
        }

        
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Voter") || User.IsInRole("Administrator"))
            {
                //string hello = DashboardBusiness.sayHello();
                //the user has a voter Role, lets display the dashboard
                DashboardViewModel d = await getDashboard();
                //d.UserHasVoted = false;
                return View(d);
            }
            else
            {
                //so this user has 'PreVoter', this happened in only one case: this is a new user who didn't change his password.
                //    He should be redirected to ResetPassword view.
                //    Once he change his password he will be provided the role 'Voter' 

                var user = await _userManager.GetUserAsync(HttpContext.User);
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                return RedirectToPage("/Account/ResetPassword", new { area = "Identity", code }); //this returns 'code must be supplied o reset password'
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }









        //********************** UTILITIES
        public async Task<DashboardViewModel> getDashboard()
        {
            //this function returns a dashboard object filled with data
            //it is asynchronous becuz it uses another method which uses an asynchronous method GetUserAsync()

            List<CandidateViewModel> candidates = Utilities.convertCandidateList_toPersonViewModelList(_candidateRepository.GetAll().ToList());

            int NbCandidates = candidates.Count;
            int NbVoters = _voterRepository.GetAll().Count;
            int NbVotes = _voteRepository.GetAll().Count;
            int votersWithVote = VoterUtilities.getNumberOfVoterWithVote(_voterRepository);
            //Now lets get the currentUser to check if he has voted or not yet
            var currentUser = await _userManager.GetUserAsync(HttpContext.User)/*getCurrentUser()*/;
            bool userHasVoted = VoterUtilities.getVoterByUserId(Guid.Parse(currentUser.Id), _voterRepository).hasVoted();
            DashboardViewModel d = new DashboardViewModel
            {
                NbCandidates = NbCandidates,
                NbVoters = NbVoters,
                NbVotes = NbVotes,
                ParticipationRate = (double)votersWithVote / (double)NbVoters,
                Candidates = candidates,
                UserHasVoted = userHasVoted
            };
            return d;
        }


    }
}
