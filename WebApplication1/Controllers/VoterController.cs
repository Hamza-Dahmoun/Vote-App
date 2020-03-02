using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

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
                List<Voter> list = _db.Voter.ToList();
                return View(list);
            }
            catch
            {

                return View();
            }

        }
    }
}