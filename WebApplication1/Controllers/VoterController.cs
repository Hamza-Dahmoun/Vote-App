using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Business;
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

        //the below are services we're going to use in this controller, they will be injected in the constructor

        //the below service is used to store a new user for each new voter
        private readonly UserManager<IdentityUser> _userManager;
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<State> _stateRepository { get; }
        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public VoterController(IRepository<Voter> voterRepository, IRepository<State> stateRepository, UserManager<IdentityUser> userManager)
        {
            _voterRepository = voterRepository;
            _stateRepository = stateRepository;
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
            return View(Utilities.convertVoter_toPersonViewModel(_voterRepository.GetById(id)));
        }

  
        public IActionResult Create()
        {
            //this method will return an empty VoterStateViewModel but with a list of all states, in a view
            VoterStateViewModel vs = new VoterStateViewModel
            {
                States = _stateRepository.GetAll()
            };
        return View(vs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VoterStateViewModel vs)
        {
            if (ModelState.IsValid)
            {
                //this method receives a VoterStateViewModel object, and based on it, it creates a voter object and stores it in the DB
                Voter v = new Voter
                {
                    Id = Guid.NewGuid(),
                    FirstName = vs.FirstName,
                    LastName = vs.LastName,
                    State = _stateRepository.GetById(vs.StateID)
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
            return View(Utilities.convertVoter_toPersonViewModel(voter));
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
            //In here we are going to return a view where a voter is displayed with his state but the state is in
            //a list of states
            var voter = _voterRepository.GetById(Id);
            VoterStateViewModel voterstate = new VoterStateViewModel
            {
                Id = voter.Id,
                FirstName = voter.FirstName,
                LastName = voter.LastName,                
                States = _stateRepository.GetAll()
            };
            /*just in case user wanted to edit info of Neutral vote which doesn't have a state*/
            if (voter.State != null)
            {
                voterstate.StateID = voter.State.Id;
            }
            

            return View(voterstate);
        }
        [HttpPost]
        public IActionResult Edit(VoterStateViewModel voterstate)
        {
            if (!ModelState.IsValid)
            {
                if(voterstate.States == null)
                {
                    //in caase the object received doesn't have a list of states
                    voterstate.States = _stateRepository.GetAll();
                }
                return View(voterstate);
            }
            Voter v = new Voter
            {
                Id = voterstate.Id,
                FirstName = voterstate.FirstName,
                LastName = voterstate.LastName,
                State = _stateRepository.GetById(voterstate.StateID)
            };
            _voterRepository.Edit(voterstate.Id, v);
            return RedirectToAction(nameof(Index));
        }





        #region JQUERY DATATABLE REGION
        public IActionResult DataTable()
        {
            //This method is called by jQuery datatables to get paged data
            //First, we'll try to read the variables sent from the jQuery request, and then, based on these variables' values we'll query
            //the db

            
            try
            {
                //lets first get the variables of the request (of the form), and then build the linq query accordingly
                //above each variable I wrote the official doc of jQuery

                /*
                 * draw
                 * integer Type
                 Draw counter. This is used by DataTables to ensure that the Ajax returns from server-side processing requests 
                 are drawn in sequence by DataTables (Ajax requests are asynchronous and thus can return out of sequence). 
                 This is used as part of the draw return parameter (see below).
                 */
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();


                /*
                 * start
                 * integer type
                 Paging first record indicator. This is the start point in the current data set (0 index based - i.e. 0 is the first record).
                 */
                var start = HttpContext.Request.Form["start"].FirstOrDefault();


                /*
                 * length
                 * integer type
                 Number of records that the table can display in the current draw. It is expected that the number of records returned 
                 will be equal to this number, unless the server has fewer records to return. Note that this can be -1 to indicate that 
                 all records should be returned (although that negates any benefits of server-side processing!)
                 */
                var length = HttpContext.Request.Form["length"].FirstOrDefault();


                /*
                 * search[value]
                 * string Type
                 Global search value. To be applied to all columns which have searchable as true.
                 */
                var searchValue = HttpContext.Request.Form["search[value]"].FirstOrDefault();

                /*
                 * order[i][column]
                 * integer Type
                 Column to which ordering should be applied. This is an index reference to the columns array of information 
                 that is also submitted to the server.
                 */
                 //for now we'll consider sorting voters only by their FirstName (second column) later we'll see
                var sortColumnName = HttpContext.Request.Form["order[1][column][name]"].FirstOrDefault();
                //var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                /*
                 * order[i][dir]
                 * integer Type
                 Ordering direction for this column. It will be asc or desc to indicate ascending ordering or descending ordering, respectively.
                 */
                var sortColumnDirection = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();


                //Page Size (10, 20, 50,100) 
                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                //how many rows too skip?
                int skip = start != null ? Convert.ToInt32(start) : 0;

                //totalRecords too inform user
                int totalRecords = 0;

                //lets get all voters (I know I know it is not good, but this is only for testing, I'll write a new method in repositories 
                //and use it to get data filtered and paged later)
                var voters = _voterRepository.GetAll();

                //lets sort voters if user asked to
                if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    //I don't know the name of the property I am going to sort with, so I'll use Refection API
                    //to get the name of the property then I'll use it to order the list

                    System.Reflection.PropertyInfo propertyName = typeof(Voter).GetProperty(sortColumnName);
                    if(sortColumnDirection == "asc")
                    {
                        voters = voters.OrderBy(e => propertyName.GetValue(e)).ToList();
                    }
                    if (sortColumnDirection == "disc")
                    {
                        voters = voters.OrderByDescending(e => propertyName.GetValue(e)).ToList();
                    }                    
                }

                //now lets look for a value in FirstName/LastName/StateName if user asked to
                if (!string.IsNullOrEmpty(searchValue))
                {
                    voters = voters.Where(
                        v => v.FirstName.Contains(searchValue) || 
                        v.LastName.Contains(searchValue) || 
                        v.State.Name.Contains(searchValue))                        
                        .ToList();
                }

                //lets assign totalRecords the correct value
                totalRecords = voters.Count;

                //lets proceed paging and taking only requested rows
                var myData = voters.Skip(skip).Take(pageSize)
                    .Select(v => new
                    {
                        v.Id,
                        v.FirstName,
                        v.LastName,
                        State = v.State.Name
                    }).ToList();

                //now lets return json data so that it is understandable by jQuery
                return Json(
                    new
                    {
                        draw = draw,
                        recordsFiltered = totalRecords,
                        recordsTotal = totalRecords, 
                        data = myData
                    }
                    );
            }
            catch
            {
                return BadRequest();
            }
            
        }
        #endregion


    }
}