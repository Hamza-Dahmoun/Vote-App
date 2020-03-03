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
            return View(convertCandidate_toPersonViewModel(_candidateRepository.GetById(id)));
        }







        //******************** UTILITIES
        public PersonViewModel convertCandidate_toPersonViewModel(Candidate candidate)
        {
            PersonViewModel p = new PersonViewModel
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                StructureName = candidate.Structure?.Name,
                StructureLevel = candidate.Structure?.Level.Name
            };

            return p;
        }

        public List<PersonViewModel> convertCandidateList_toPersonViewModelList(IList<Candidate> candidates)
        {
            List<PersonViewModel> myList = new List<PersonViewModel>();
            foreach (var item in candidates)
            {
                myList.Add(convertCandidate_toPersonViewModel(item));
            }

            return myList;
        }

    }
}