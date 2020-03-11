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
    public class StructureController : Controller
    {
        public IRepository<Structure> _structureRepository { get; }
        public StructureController(IRepository<Structure> structureRepository)
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
        public StructureViewModel convertStructure_toStructureViewModel(Structure structure)
        {
            StructureViewModel s = new StructureViewModel
            {
                Id = structure.Id,
                Name = structure.Name,
                LevelName = structure.Level?.Name,
                LevelValue = structure.Level.LevelValue
            };
            return s;
        }

        public List<StructureViewModel> convertStructureList_toStructureViewModelList(IList<Structure> structures)
        {
            List<StructureViewModel> myList = new List<StructureViewModel>();
            foreach (var item in structures)
            {
                myList.Add(convertStructure_toStructureViewModel(item));
            }
            return myList;
        }
    }
}