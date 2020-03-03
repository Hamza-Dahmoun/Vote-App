﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
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