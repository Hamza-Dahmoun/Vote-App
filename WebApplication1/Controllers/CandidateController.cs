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
        public IRepository<Voter> _voterRepository { get; }
        public CandidateController(IRepository<Candidate> candidateRepository, IRepository<Voter> voterRepository)
        {
            _candidateRepository = candidateRepository;
            _voterRepository = voterRepository;
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



        public IActionResult GoToVotersList()
        {
            return View(convertVoterList_toPersonViewModelList(_voterRepository.GetAll()));
        }

        public IActionResult SelectNewCandidate(Guid voterId)
        {
            try
            {
                Voter voter = _voterRepository.GetById(voterId);
                _candidateRepository.Add(
                    new Candidate
                    {
                        Id = Guid.NewGuid(),
                        FirstName = voter.FirstName,
                        LastName = voter.LastName,
                        VoterBeing = voter,
                        Structure = voter.Structure
                    }
                    );
                return RedirectToAction(nameof(GoToVotersList));
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

        public bool IsCandidate(Voter voter)
        {

            Candidate candidate = _candidateRepository.GetAll().SingleOrDefault(c => c.VoterBeing?.Id == voter.Id);

            if (candidate != null)
                return true;
            else return false;

        }


        //



        public PersonViewModel convertVoter_toPersonViewModel(Voter voter)
        {
            PersonViewModel p = new PersonViewModel
            {
                Id = voter.Id,
                FirstName = voter.FirstName,
                LastName = voter.LastName,
                StructureName = voter.Structure?.Name,
                StructureLevel = voter.Structure?.Level.Name,
                IsCandidate = IsCandidate(voter)
            };
            if (voter.hasVoted())
                p.hasVoted = "Yes";
            else p.hasVoted = "No";

            return p;
        }

        public List<PersonViewModel> convertVoterList_toPersonViewModelList(IList<Voter> voters)
        {
            List<PersonViewModel> myList = new List<PersonViewModel>();
            foreach (var item in voters)
            {
                myList.Add(convertVoter_toPersonViewModel(item));
            }

            return myList;
        }

        
    }
}