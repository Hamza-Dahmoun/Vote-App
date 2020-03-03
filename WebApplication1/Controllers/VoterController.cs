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

    public class VoterController : Controller
    {
        //private readonly VoteDBContext _db;
        //public VoterController(VoteDBContext db)
        //{
        //    _db = db;
        //}
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _db.Dispose();
        //    }            
        //}

        public IRepository<Voter> _voterRepository { get; }
        public VoterController(IRepository<Voter> voterRepository)
        {
            _voterRepository = voterRepository;
        }
        
        public IActionResult Index()
        {
            try
            {
                //return View(convertVoterList_toPersonViewModelList(_db.Voter.ToList()));
                return View(convertVoterList_toPersonViewModelList(_voterRepository.GetAll()));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Details(Guid id)
        {
            return View(convertVoter_toPersonViewModel(_voterRepository.GetById(id)));
        }

  





        

        //******************** UTILITIES
        public PersonViewModel convertVoter_toPersonViewModel(Voter voter)
        {
            PersonViewModel p = new PersonViewModel
            {
                Id = voter.Id,
                FirstName = voter.FirstName,
                LastName = voter.LastName,
                StructureName = voter.Structure?.Name,
                StructureLevel = voter.Structure?.Level.Name
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