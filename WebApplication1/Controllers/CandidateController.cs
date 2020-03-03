using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class CandidateController : Controller
    {
        public IRepository<Candidate> _candidateRepository { get; }
        public CandidateController(IRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public IActionResult Index()
        {

            return View(_candidateRepository.GetAll());
        }


        public IActionResult Details(Guid id)
        {
            return View(convertCandidate_toCandidateViewModel(_candidateRepository.GetById(id)));
        }


        public IActionResult Delete(Guid id)
        {
            return View(convertCandidate_toCandidateViewModel(_candidateRepository.GetById(id)));
        }
        [HttpPost]
        public IActionResult DeleteCandidate(Guid id)
        {
            try
            {
                _candidateRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }







        //******************** UTILITIES
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

            return myList;
        }

    }
}