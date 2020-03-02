using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{

    public class VoterController : Controller
    {
        private readonly VoteDBContext _db;
        public VoterController(VoteDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            try
            {
                List<PersonViewModel> myList = new List<PersonViewModel>();
                List<Voter> list = _db.Voter.ToList();
                foreach (var item in list)
                {
                    PersonViewModel p = new PersonViewModel
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        StructureName = item.Structure?.Name,
                        StructureLevel = item.Structure?.Level.Name
                    };
                if (item.hasVoted())
                    p.hasVoted = "Yes";
                else p.hasVoted = "No";

                    myList.Add(p);
                }
                return View(myList);
            }
            catch
            {

                return View();
            }
        }





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }            
        }
    }
}