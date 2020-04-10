using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

            //the below service is used to store a new user for each new voter
        private readonly UserManager<IdentityUser> _userManager;
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<State> _structureRepository { get; }
        public VoterController(IRepository<Voter> voterRepository, IRepository<State> structureRepository, UserManager<IdentityUser> userManager)
        {
            _voterRepository = voterRepository;
            _structureRepository = structureRepository;
            _userManager = userManager;
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
        {
            //this method will return an empty VoterStructureViewModel but with a list of all Structures, in a view
            VoterStructureViewModel vs = new VoterStructureViewModel
            {
                Structures = _structureRepository.GetAll()
            };
        return View(vs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VoterStructureViewModel vs)
        {
            if (ModelState.IsValid)
            {
                //this method receives a VoterStructureViewModel object, and based on it, it creates a voter object and stores it in the DB
                Voter v = new Voter
                {
                    Id = Guid.NewGuid(),
                    FirstName = vs.FirstName,
                    LastName = vs.LastName,
                    Structure = _structureRepository.GetById(vs.StructureID)
                };

                //now lets add this new voter as a new user to the IdentityDB using UserManager<IdentityUser> service
                //we'll set its usernam/email, and set 'Pa$$w0rd' as the password
                string username = v.FirstName.ToLower() +"."+ v.LastName.ToLower();
                var user = new IdentityUser { UserName = username };
                //CreateAsync() is an asynchronous method, we have to mark this method with 'async task'
                var result = await _userManager.CreateAsync(user, "Pa$$w0rd");//this password will be automatically hashed
                if (result.Succeeded)
                {
                    var result1 = await _userManager.AddToRoleAsync(user, "PreVoter");
                    if (result1.Succeeded)
                    {
                        //the user has been stored successully lets insert now the new voter
                        v.UserId = Guid.Parse(user.Id);
                        _voterRepository.Add(v);
                        return RedirectToAction(nameof(Index));
                    }
                }
                
                //N.B: Is it possible to move the above block of code that is responsible of adding a user
                //to another file (e.g: UserRepository) so that we seperate concerns?
            }
            return View();
        }


        public IActionResult Delete(Guid id)
        {
            var voter = _voterRepository.GetById(id);
            return View(convertVoter_toPersonViewModel(voter));
        }
        [HttpPost]
        public IActionResult DeleteVoter(Guid id)
        {
            try
            {
                _voterRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //If there is an error return the same Delete view
                return View();
            }
        }



        public IActionResult Edit(Guid Id)
        {
            //In here we are going to return a view where a voter is displayed with his Structure but the structure is in
            //a list of structures
            var voter = _voterRepository.GetById(Id);
            VoterStructureViewModel voterstructure = new VoterStructureViewModel
            {
                Id = voter.Id,
                FirstName = voter.FirstName,
                LastName = voter.LastName,                
                Structures = _structureRepository.GetAll()
            };
            /*just in case user wanted to edit info of Neutral vote which doesn't have a structure*/
            if (voter.Structure != null)
            {
                voterstructure.StructureID = voter.Structure.Id;
            }
            

            return View(voterstructure);
        }
        [HttpPost]
        public IActionResult EditVoter(VoterStructureViewModel voterstructure)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Voter v = new Voter
            {
                Id = voterstructure.Id,
                FirstName = voterstructure.FirstName,
                LastName = voterstructure.LastName,
                Structure = _structureRepository.GetById(voterstructure.StructureID)
            };
            _voterRepository.Edit(voterstructure.Id, v);
            return RedirectToAction(nameof(Index));
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