using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    //the below attribute will permit only authorized users to access HomeController, anonymous access will be deactivated
    [Authorize]
    //the below attribute will permit only users with set of roles contained in the policy 'ManageElections'
    //(you can check the set of roles related to this policy in ConfigureServices() in Startup file)
    [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
    //[Authorize(Roles = "Administrator")]
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
        public IRepository<Structure> _structureRepository { get; }
        public VoterController(IRepository<Voter> voterRepository, IRepository<Structure> structureRepository)
        {
            _voterRepository = voterRepository;
            _structureRepository = structureRepository;
        }
        
        public IActionResult Index()
        {
            try
            {
                //return View(_db.Voter.ToList());
                return View(_voterRepository.GetAll());
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

  
        public IActionResult Create()
        {//this method will return an empty VoterStructureViewModel but with a list of all Structures, in a view
            VoterStructureViewModel vs = new VoterStructureViewModel
            {
                Structures = _structureRepository.GetAll()
            };
        return View(vs);
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