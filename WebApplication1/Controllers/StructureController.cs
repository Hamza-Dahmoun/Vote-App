using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

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
            return View();
        }
    }
}