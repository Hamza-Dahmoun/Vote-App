using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Repositories;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;


namespace WebApplication1.Controllers
{
    public class VoteController : Controller
    {
        public IRepository<Candidate> _candidateRepository { get; }
        public IRepository<Vote> _voteRepository { get; }
        
        public VoteController(IRepository<Candidate> candidateRepository, IRepository<Vote> voteRepository)
        {
            _candidateRepository = candidateRepository;
            _voteRepository = voteRepository;
        }


        public IActionResult Index()
        {//this action returns a view containing all candidates for the user to vote on five of them maximum
            return View(convertCandidateList_toPersonViewModelList(_candidateRepository.GetAll()));
        }

        [HttpPost]
        public IActionResult ValidateVote([FromBody] List<string> candidateIdList)
        {//this action get the list of the candidates ids that the user voted on, and add them to the db as vote objects, and redirect to the
            //dashboard in home controller

            //lets add 'Vote' objects to the db
            foreach (var candidateId in candidateIdList)
            {
                
            }

            return RedirectToAction("Index", "Home");
        }






        //************** UTILITIES
        public CandidateViewModel convertCandidate_toCandidateViewModel(Candidate candidate)
        {
            CandidateViewModel c = new CandidateViewModel
            {
                Id = candidate.Id,
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
    }
}