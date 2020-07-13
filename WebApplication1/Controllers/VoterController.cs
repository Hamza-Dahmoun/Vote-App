using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using WebApplication1.Business;
using WebApplication1.Models;
using WebApplication1.Models.Helpers;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;
using Microsoft.Extensions.Localization;

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
        private readonly ILogger<VoterController> _logger;
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<State> _stateRepository { get; }
        public IRepository<Vote> _voteRepository { get; }
        public IRepository<Candidate> _candidateRepository { get; }

        //Lets create a private readonly field IStringLocalizer<Messages> so that we can use Localization service, we'll inject it inside the constructor
        private readonly IStringLocalizer<Messages> _messagesLoclizer;

        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public VoterController(
            IRepository<Voter> voterRepository,
            IRepository<State> stateRepository,
            IRepository<Vote> voteRepository,
            IRepository<Candidate> candidateRepository,
            UserManager<IdentityUser> userManager,
            ILogger<VoterController> logger,
            IStringLocalizer<Messages> messagesLoclizer)
        {
            _voterRepository = voterRepository;
            _voteRepository = voteRepository;
            _stateRepository = stateRepository;
            _candidateRepository = candidateRepository;
            _userManager = userManager;
            _logger = logger;
            _messagesLoclizer = messagesLoclizer;
        }
        
        public IActionResult Index()
        {
            _logger.LogInformation("Voter/Index() action is called");
            try
            {
                _logger.LogInformation("Calling VoterRepository.GetAll() method");
                List<Voter> voters = _voterRepository.GetAll().ToList();
                ViewBag.votersCount = voters.Count;
                //return View(_db.Voter.ToList());
                _logger.LogInformation("Returning a list of voters to Index view");
                return View(voters);
            }
            catch(Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }
        }

        public IActionResult Details(Guid id)
        {
            _logger.LogInformation("Voter/Details() action is called");
            try
            {
                if (id == null)
                {
                    _logger.LogError("Passed parameter 'id' is null");
                    throw new BusinessException("Passed parameter 'id' can not be null");
                }

                _logger.LogInformation("Calling VoterRepository.GetById() method");
                Voter v = _voterRepository.GetById(id);
                if (v == null)
                {
                    _logger.LogError("Voter not found");
                    throw new BusinessException("Voter not found");
                }

                _logger.LogInformation("Calling Utilities.convertVoter_toPersonViewModel() method");
                PersonViewModel p = Utilities.convertVoter_toPersonViewModel(v);
                _logger.LogInformation("Returning a PersonViewModel to the Details view");
                return View(p);
            }
            catch (BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }            
        }

  
        public IActionResult Create()
        {
            try
            {
                //this method will return an empty VoterStateViewModel but with a list of all states, in a view
                VoterStateViewModel vs = new VoterStateViewModel
                {
                    States = _stateRepository.GetAll()
                };
                return View(vs);
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Create(VoterStateViewModel vs)
        {
            _logger.LogInformation("Voter/Create() action is called");
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Model is valid");
                    //this method receives a VoterStateViewModel object, and based on it, it creates a voter object and stores it in the DB
                    _logger.LogInformation("Creating a new Voter instance");
                    Voter v = new Voter
                    {
                        Id = Guid.NewGuid(),
                        FirstName = vs.FirstName,
                        LastName = vs.LastName,
                        State = _stateRepository.GetById(vs.StateID)
                    };

                    //now lets add this new voter as a new user to the IdentityDB using UserManager<IdentityUser> service
                    //we'll set its usernam/email, and set 'Pa$$w0rd' as the password
                    string username = v.FirstName.ToLower() + "." + v.LastName.ToLower();
                    _logger.LogInformation("Creating a new IdentityUser instance");
                    var user = new IdentityUser { UserName = username };
                    //CreateAsync() is an asynchronous method, we have to mark this method with 'async task'
                    _logger.LogInformation("Storing the new IdentityUser instance in IdentityDB");
                    var result = await _userManager.CreateAsync(user, "Pa$$w0rd");//this password will be automatically hashed
                    
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("The new IdentityUser instance stored successfully in IdentityDB");

                        _logger.LogInformation("Adding 'PreVoter' role to the new IdentityUser");
                        var result1 = await _userManager.AddToRoleAsync(user, "PreVoter");
                        
                        if (result1.Succeeded)
                        {
                            _logger.LogInformation("'PreVoter' role to the new IdentityUser is added successfully");

                            //the user has been stored successully lets insert now the new voter
                            v.UserId = Guid.Parse(user.Id);
                            _logger.LogInformation("Adding the new voter to the DB");
                            _voterRepository.Add(v);
                            _logger.LogInformation("The new voter is added to the DB successfully");
                            _logger.LogInformation("Redirecting to the Voter Index view");
                            return RedirectToAction(nameof(Index));
                        }
                    }

                    //N.B: Is it possible to move the above block of code that is responsible of adding a user
                    //to another file (e.g: UserRepository) so that we seperate concerns?
                }
                _logger.LogInformation("Model is not valid");
                //so there is a business rule not met, lets throw a businessException and catch it
                throw new BusinessException("Information provided not valid");
            }
            catch (BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View(vs);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E; 
            }            
        }


        public IActionResult Delete(Guid id)
        {
            _logger.LogInformation("Voter/Delete() action is called");
            try
            {
                if (id == null)
                {
                    _logger.LogError("Passed parameter 'id' is null");
                    throw new BusinessException("Passed parameter 'id' can not be null");
                }

                _logger.LogInformation("Calling VoterRepository.GetById() method");
                var voter = _voterRepository.GetById(id);
                if (voter == null)
                {
                    _logger.LogError("Voter not found");
                    throw new BusinessException("Voter not found");
                }

                _logger.LogInformation("Calling Utilities.convertVoter_toPersonViewModel() method");
                PersonViewModel p = Utilities.convertVoter_toPersonViewModel(voter);
                _logger.LogInformation("Returning PersonViewModel to the view");
                return View();
            }
            catch (BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }            
        }
        [HttpPost]
        public async Task<IActionResult> DeleteVoter(Guid id)
        {//removing a voter means removing all his actions (votes  & identity account)
            _logger.LogInformation("Voter/DeleteVoter() action is called");
            try
            {
                if (id == null)
                {
                    _logger.LogError("Passed parameter 'id' is null");
                    throw new BusinessException("Passed parameter 'id' can not be null");
                }

                _logger.LogInformation("Calling VoterRepository.GetById() method");
                Voter voter = _voterRepository.GetById(id);
                if (voter == null)
                {
                    _logger.LogError("Voter not found");
                    throw new BusinessException("Voter not found");
                }

                //1- Remove all this voter's votes
                //declaring an expression that is special to Vote objects
                System.Linq.Expressions.Expression<Func<Vote, bool>> expr1 = e => e.Voter == voter;
                _logger.LogInformation("Calling VoteRepository.GetAllFiltered() method");
                List<Vote> votesList = _voteRepository.GetAllFiltered(expr1);

                _logger.LogInformation("Going to delete all Vote instances of a Voter");
                foreach (var vote in votesList)
                {
                    _voteRepository.Delete(vote.Id);
                }
                _logger.LogInformation("Done deleting to delete all Vote instances of a Voter");

                //2- Remove corresponding Candidates objects
                //declaring an expression that is special to Vote objects
                System.Linq.Expressions.Expression<Func<Candidate, bool>> expr2 = e => e.VoterBeing == voter;
                _logger.LogInformation("Going to get all Candidates instances of the Voter");
                List<Candidate> candidatesList = _candidateRepository.GetAllFiltered(expr2);

                _logger.LogInformation("Going to delete all Candidates instances of the Voter");
                foreach (var candidate in candidatesList)
                {
                    _candidateRepository.Delete(candidate.Id);
                }
                _logger.LogInformation("Done deleting all Candidates instances of the Voter");

                //3- Delete this voter's account from the Identity Db
                //lets get the User by his ID

                _logger.LogInformation("Going to get the corresponding IdentityUser of the Voter instance");
                var voterUserAccount = await _userManager.FindByIdAsync(_voterRepository.GetById(id).UserId.ToString());
                
                //DeleteAsync() is an asynchronous method, we have to mark this method with 'async task'
                _logger.LogInformation("Going to delete the corresponding IdentityUser of the Voter instance");
                var result = await _userManager.DeleteAsync(voterUserAccount);
                if (result.Succeeded)
                {
                    _logger.LogInformation("done deleting the corresponding IdentityUser of the Voter instance");

                    //4- Remove the Voter
                    _logger.LogInformation("Going to delete the Voter instance");
                    _voterRepository.Delete(id);
                    _logger.LogInformation("Done deleting the Voter instance");
                }

                _logger.LogInformation("Redirecting to Index view");
                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View(nameof(Delete));
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E; 
            }
        }



        public IActionResult Edit(Guid Id)
        {
            _logger.LogInformation("Voter/Edit() action is called");
            try
            {
                //In here we are going to return a view where a voter is displayed with his state but the state is in
                //a list of states

                if (Id == null)
                {
                    _logger.LogError("Passed parameter 'id' is null");
                    throw new BusinessException("Passed parameter 'id' can not be null");
                }

                _logger.LogInformation("Calling VoterRepository.GetById() method");
                var voter = _voterRepository.GetById(Id);
                if (voter == null)
                {
                    _logger.LogError("Voter not found");
                    throw new BusinessException("Voter not found");
                }

                _logger.LogInformation("Creating a VoterStateViewModel for the Voter instance");
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

                _logger.LogInformation("Returning VoterStateViewModel to the View");
                return View(voterstate);
            }
            catch (BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }            
        }
        [HttpPost]
        public IActionResult Edit(VoterStateViewModel voterstate)
        {
            _logger.LogInformation("Voter/Edit() action is called");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model is not valid");
                    if (voterstate.States == null)
                    {
                        //in caase the object received doesn't have a list of states
                        voterstate.States = _stateRepository.GetAll();
                    }
                    _logger.LogInformation("Returning to the view to display validation messages");
                    //so there is a business rule not met, lets throw a businessException and catch it
                    throw new BusinessException("Information provided not valid");
                }
                Voter v = new Voter
                {
                    Id = voterstate.Id,
                    FirstName = voterstate.FirstName,
                    LastName = voterstate.LastName,
                    State = _stateRepository.GetById(voterstate.StateID)
                };

                _logger.LogInformation("Calling VoterRepository.Edit() method");
                _voterRepository.Edit(voterstate.Id, v);

                _logger.LogInformation("Redirecting to Index action");
                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                //lets refill States list
                voterstate.States = _stateRepository.GetAll();
                return View(voterstate);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }            
        }





        #region JQUERY DATATABLE REGION

        public IActionResult DataTable()
        {
            //This method is called by jQuery datatables to get paged data
            //First, we'll try to read the variables sent from the jQuery request, and then, based on these variables' values we'll query
            //the db

            _logger.LogInformation("Voter/Datatable() method is called");
            try
            {
                //lets first get the variables of the request (of the form), and then build the linq query accordingly
                //above each variable I wrote the official doc of jQuery


                // draw
                // integer Type
                // Draw counter.This is used by DataTables to ensure that the Ajax returns from server - side processing requests
                // are drawn in sequence by DataTables(Ajax requests are asynchronous and thus can return out of sequence). 
                // This is used as part of the draw return parameter(see below).

                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();



                // start
                // integer type
                // Paging first record indicator.This is the start point in the current data set(0 index based -i.e. 0 is the first record).

                var start = HttpContext.Request.Form["start"].FirstOrDefault();



                // length
                // integer type
                // Number of records that the table can display in the current draw. It is expected that the number of records returned 
                // will be equal to this number, unless the server has fewer records to return. Note that this can be -1 to indicate that 
                // all records should be returned (although that negates any benefits of server-side processing!)

                var length = HttpContext.Request.Form["length"].FirstOrDefault();



                // search[value]
                // string Type
                // Global search value. To be applied to all columns which have searchable as true.

                var searchValue = HttpContext.Request.Form["search[value]"].FirstOrDefault();


                // order[i][column]
                // integer Type
                // Column to which ordering should be applied. This is an index reference to the columns array of information
                // that is also submitted to the server.

                var sortColumnName = HttpContext.Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();


                // order[i][dir]
                // integer Type
                // Ordering direction for this column.It will be asc or desc to indicate ascending ordering or descending ordering, respectively.


                var sortColumnDirection = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();


                //Page Size (10, 20, 50,100) 
                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                //how many rows too skip?
                int skip = start != null ? Convert.ToInt32(start) : 0;

                //totalRecords too inform user
                int totalRecords = 0;


                

                //now lets look for a value in FirstName/LastName/StateName if user asked to
                if (!string.IsNullOrEmpty(searchValue))
                {
                    _logger.LogInformation("Going to query the DB based on 'searchValue' in VoterRepository.GetAllFilteredPaged()");
                    //declaring an expression that is special to Voter objects
                    System.Linq.Expressions.Expression<Func<Voter, bool>> expr =
                        v => v.FirstName.Contains(searchValue) ||
                        v.LastName.Contains(searchValue) ||
                        v.State.Name.Contains(searchValue);

                    //lets get the list of voters filtered and paged
                    PagedResult<Voter> pagedResult = _voterRepository.GetAllFilteredPaged(expr, sortColumnName, sortColumnDirection, skip, pageSize);

                    _logger.LogInformation("pagedResult filled by data from DB GetAllFilteredPaged()");

                    //lets assign totalRecords the correct value
                    totalRecords = pagedResult.TotalCount;

                    //now lets return json data so that it is understandable by jQuery   
                    _logger.LogInformation("Going to serialize the response");
                    var json = JsonConvert.SerializeObject(new
                    {
                        draw = draw,
                        recordsFiltered = totalRecords,
                        recordsTotal = totalRecords,
                        data = pagedResult.Items
                    });
                    _logger.LogInformation("Return the response as JSON");
                    return Ok(json);

                }
                else
                {
                    //so user didn't ask for filtering, he only asked for paging

                    //lets get the list of voters paged
                    _logger.LogInformation("Calling VoterRepository.GetAllPaged() method");
                    PagedResult<Voter> pagedResult = _voterRepository.GetAllPaged(sortColumnName, sortColumnDirection, skip, pageSize);

                    //lets assign totalRecords the correct value
                    totalRecords = pagedResult.TotalCount;

                    //now lets return json data so that it is understandable by jQuery                
                    _logger.LogInformation("Going to serialize the response");
                    var json = JsonConvert.SerializeObject(new
                    {
                        draw = draw,
                        recordsFiltered = totalRecords,
                        recordsTotal = totalRecords,
                        data = pagedResult.Items
                    });
                    _logger.LogInformation("Return the response as JSON");
                    return Ok(json);
                }
            }
            catch(Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                return BadRequest();
            }
        }

        /* BEFORE WRITING VOTERREPOSITORY.GETALLPAGED() AND VOTERREPOSITORY.GETALLPAGEDFILTERED()
        public IActionResult DataTable()
        {
            //This method is called by jQuery datatables to get paged data
            //First, we'll try to read the variables sent from the jQuery request, and then, based on these variables' values we'll query
            //the db

            
            try
            {
                //lets first get the variables of the request (of the form), and then build the linq query accordingly
                //above each variable I wrote the official doc of jQuery


                // draw
                // integer Type
                // Draw counter.This is used by DataTables to ensure that the Ajax returns from server - side processing requests
                // are drawn in sequence by DataTables(Ajax requests are asynchronous and thus can return out of sequence). 
                // This is used as part of the draw return parameter(see below).

                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();



                // start
                // integer type
                // Paging first record indicator.This is the start point in the current data set(0 index based -i.e. 0 is the first record).
                 
                var start = HttpContext.Request.Form["start"].FirstOrDefault();


                
                 // length
                 // integer type
                 // Number of records that the table can display in the current draw. It is expected that the number of records returned 
                 // will be equal to this number, unless the server has fewer records to return. Note that this can be -1 to indicate that 
                 // all records should be returned (although that negates any benefits of server-side processing!)
                 
                var length = HttpContext.Request.Form["length"].FirstOrDefault();


                
                 // search[value]
                 // string Type
                 // Global search value. To be applied to all columns which have searchable as true.
                 
                var searchValue = HttpContext.Request.Form["search[value]"].FirstOrDefault();


                // order[i][column]
                // integer Type
                // Column to which ordering should be applied. This is an index reference to the columns array of information
                // that is also submitted to the server.

                var sortColumnName = HttpContext.Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();


                // order[i][dir]
                // integer Type
                // Ordering direction for this column.It will be asc or desc to indicate ascending ordering or descending ordering, respectively.


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
                var json = JsonConvert.SerializeObject(new
                {
                    draw = draw,
                    recordsFiltered = totalRecords,
                    recordsTotal = totalRecords,
                    data = myData
                });
                return Ok(json);
            }
            catch
            {
                return BadRequest();
            }

            //I followed: https://www.c-sharpcorner.com/article/using-jquery-datatables-grid-with-asp-net-core-mvc/
            //and https://datatables.net/manual/server-side#Example-data
        }
        */
        #endregion


        #region SELECT2 REGION
        //this is a web api called when user remove a candidate from an election
        [HttpPost]
        [Authorize(Policy = nameof(VoteAppPolicies.ManageElections))]
        public async Task<IActionResult> States(string q)
        {
            _logger.LogInformation("Voter/States() is called");
            try
            {
                if (String.IsNullOrEmpty(q))
                {
                    _logger.LogError("Parameter is null");
                    return BadRequest();
                }

                //declaring an expression that is special to State objects according to the search value
                System.Linq.Expressions.Expression<Func<State, bool>> expr;
                expr = s => s.Name.StartsWith(q);

                _logger.LogInformation("Calling StateRepository.GetAllFiltered() method");
                var states = _stateRepository.GetAllFiltered(expr).Select(s => new { text = s.Name, id = s.Id});
                //now lets return json data

                _logger.LogInformation("Going to  serialize the list of states");
                var json = JsonConvert.SerializeObject(new
                {
                    states
                });

                _logger.LogInformation("Returning list of states as json");
                return Ok(json);
            }
            catch (Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                return BadRequest();
            }
        }

        #endregion


        [HttpPost]
        public IActionResult ExportToExcel()
        {
            //This function download list of all Voters as excel file
            try
            {
                var stream = new System.IO.MemoryStream();
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    var voters = _voterRepository.GetAll();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Voters");

                    worksheet.Cells[1, 1].Value = "First Name";
                    worksheet.Cells[1, 2].Value = "Last Name";
                    worksheet.Cells[1, 3].Value = "State";
                    worksheet.Row(1).Style.Font.Bold = true;


                    for (int c = 2; c < voters.Count + 2; c++)
                    {
                        worksheet.Cells[c, 1].Value = voters[c - 2].FirstName;
                        worksheet.Cells[c, 2].Value = voters[c - 2].LastName;
                        worksheet.Cells[c, 3].Value = voters[c - 2].State.Name;
                    }

                    package.Save();
                }

                string fileName = "Voters.xlsx";
                string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;
                return File(stream, fileType, fileName);
            }
            catch(Exception E)
            {
                throw E;
            }            
        }

    }
}