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

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IRepository<Candidate> _candidateRepository { get; }
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<Vote> _voteRepository { get; }

        public HomeController(ILogger<HomeController> logger, IRepository<Candidate> candidateRepository, IRepository<Voter> voterRepository, IRepository<Vote> voteRepository)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
            _voterRepository = voterRepository;
            _voteRepository = voteRepository;
        }

        public IActionResult Index()
        {
            DashboardViewModel d = getDashboard();
            return View(d);
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
        public DashboardViewModel getDashboard()
        {
            //this function returns a dashboard object filled with data

            List<CandidateViewModel> candidates = convertCandidateList_toPersonViewModelList(_candidateRepository.GetAll().ToList());

            int NbCandidates = candidates.Count;
            int NbVoters = _voterRepository.GetAll().Count;
            int NbVotes = _voteRepository.GetAll().Count;
            int votersWithVote = getNumberOfVoterWithVote();

            DashboardViewModel d = new DashboardViewModel
            {
                NbCandidates = NbCandidates,
                NbVoters = NbVoters,
                NbVotes = NbVotes,
                ParticipationRate = (double)votersWithVote / (double)NbVoters,
                Candidates = candidates,
                UserHasVoted = true
            };
            return d;
        }

        public int getNumberOfVoterWithVote()
        {
            return _voterRepository.GetAll().Where(v => v.hasVoted() == true).Count();
        }




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

            return myList.OrderByDescending(c=>c.VotesCount).ToList();
        }

        public bool IsCandidate(Voter voter)
        {

            Candidate candidate = _candidateRepository.GetAll().SingleOrDefault(c => c.VoterBeing?.Id == voter.Id);

            if (candidate != null)
                return true;
            else return false;

        }
    }
}
