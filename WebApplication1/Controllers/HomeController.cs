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
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    //the below attribute will permit only authorized users to access HomeController, anonymous access will be deactivated
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //the below are services we're going to use in this controller, they will be injected in the constructor
        public IRepository<Candidate> _candidateRepository { get; }
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<Vote> _voteRepository { get; }
        public IRepository<Election> _electionRepository { get; }
        //this is only used to get able to generate a 'code' needed to reset the password, and to get the currentUser ID
        private readonly UserManager<IdentityUser> _userManager;
        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public HomeController(
            ILogger<HomeController> logger,
            IRepository<Candidate> candidateRepository,
            IRepository<Voter> voterRepository,
            IRepository<Vote> voteRepository,
            IRepository<Election> electionRepository,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
            _voterRepository = voterRepository;
            _voteRepository = voteRepository;
            _electionRepository = electionRepository;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Voter") || User.IsInRole("Administrator"))
            {
                //the user has a voter Role, lets display the dashboard
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                DashboardViewModel d = DashboardUtilities.getDashboard(_candidateRepository, _voterRepository, _voteRepository, _electionRepository, currentUser);
                return View(d);


                /*
                THIS IS HOW I USED TO LOAD THE OLD DASHBOARD BEFORE INTRODUCING THE ELECTION NOTION 
                */
                //we load an empty view, then it'll be filled using jQuery Ajax
                //return View();

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
        [HttpPost]
        public async Task<IActionResult> GetResultsOfElection([FromBody] Guid electionId)
        {
            if (electionId == null || electionId == Guid.Empty)
            {
                return BadRequest();
            }
            try
            {

                //this method returns a list of candidates (of an election) ordered by their number of votes
                var election = _electionRepository.GetById(electionId);
                var candidates = CandidateUtilities.GetCandidate_byElection(_candidateRepository, election);
                List<CandidateViewModel> candidatesViewModel = Utilities.convertCandidateList_toCandidateViewModelList(_voterRepository, candidates);
                //lets serialize the list of candidatesviewmodel as json object
                var json = JsonConvert.SerializeObject(candidatesViewModel.OrderByDescending(c => c.VotesCount));
                return Ok(json);
            }
            catch
            {
                return BadRequest();
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






    }
}
