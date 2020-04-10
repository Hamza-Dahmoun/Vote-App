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
    public class StateController : Controller
    {
        public IRepository<State> _structureRepository { get; }
        public StateController(IRepository<State> structureRepository)
        {
            _structureRepository = structureRepository;
        }



        public IActionResult Index()
        {
            return View(convertStructureList_toStructureViewModelList(_structureRepository.GetAll()).OrderBy(svm=>svm.LevelValue));
        }

        public IActionResult Details(Guid id)
        {
            return View(convertStructure_toStructureViewModel(_structureRepository.GetById(id)));
        }





        //******************** UTILITIES
        public StateViewModel convertStructure_toStructureViewModel(State structure)
        {
            StateViewModel s = new StateViewModel
            {
                Id = structure.Id,
                Name = structure.Name,
                LevelName = structure.Level?.Name,
                LevelValue = structure.Level.LevelValue
            };
            return s;
        }

        public List<StateViewModel> convertStructureList_toStructureViewModelList(IList<State> structures)
        {
            List<StateViewModel> myList = new List<StateViewModel>();
            foreach (var item in structures)
            {
                myList.Add(convertStructure_toStructureViewModel(item));
            }
            return myList;
        }
    }
}