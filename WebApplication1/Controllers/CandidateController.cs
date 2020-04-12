using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Business;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    //the below attribute will permit only authorized users to access HomeController, anonymous access will be deactivated
    [Authorize]
    //the below attribute will permit only users with set of roles contained in the policy 'ManageElections'
    //(you can check the set of roles related to this policy in ConfigureServices() in Startup file)
    [Authorize(Policy =nameof(VoteAppPolicies.ManageElections))]
    //[Authorize(Roles = "Administrator")]
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
            return View(Utilities.convertCandidate_toCandidateViewModel(_candidateRepository.GetById(id)));
        }


        public IActionResult Delete(Guid id)
        {
            return View(Utilities.convertCandidate_toCandidateViewModel(_candidateRepository.GetById(id)));
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
            return View(Utilities.convertVoterList_toPersonViewModelList(_voterRepository.GetAll()));
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
                        State = voter.State
                    }
                    );
                return RedirectToAction(nameof(GoToVotersList));
            }
            catch
            {
                return View();
            }
            
        }



   
    }
}