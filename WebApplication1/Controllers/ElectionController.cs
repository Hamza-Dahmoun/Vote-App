using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

namespace WebApplication1.Controllers
{
    //the below attribute will permit only authorized users to access HomeController, anonymous access will be deactivated
    [Authorize]
    //the below attribute will permit only users with set of roles contained in the policy 'ManageElections'
    //(you can check the set of roles related to this policy in ConfigureServices() in Startup file)
    [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
    //[Authorize(Roles = "Administrator")]
    public class ElectionController : Controller
    {
        //the below are services we're going to use in this controller, they will be injected in the constructor
        public IRepository<Election> _electionRepository { get; }
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<Candidate> _candidateRepository { get; }
        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public ElectionController(IRepository<Voter> voterRepository, IRepository<Candidate> candidateRepository, IRepository<Election> electionRepository)
        {
            _voterRepository = voterRepository;
            _candidateRepository = candidateRepository;
            _electionRepository = electionRepository;
        }


        // GET: Election
        public ActionResult Index()
        {
            try
            {
                return View(_electionRepository.GetAll());
            }
            catch
            {
                return View();
            }            
        }

        // GET: Election/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Election/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Election/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Election/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Election/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Election/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Election/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}