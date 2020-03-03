using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

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
    }
}