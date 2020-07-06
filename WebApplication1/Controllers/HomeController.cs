using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Business;
using WebApplication1.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Diagnostics;
using OfficeOpenXml;

namespace WebApplication1.Controllers
{
    //the below attribute will permit only authorized users to access HomeController, anonymous access will be deactivated
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        //the below are services we're going to use in this controller, they will be injected in the constructor
        public IRepository<Candidate> _candidateRepository { get; }
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<Vote> _voteRepository { get; }
        public IRepository<Election> _electionRepository { get; }
        //this is only used to get able to generate a 'code' needed to reset the password, and to get the currentUser ID
        private readonly UserManager<IdentityUser> _userManager;
        //Creting  private readonly field of type IMemoryCach
        private readonly IMemoryCache _memoryCache;

        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public HomeController(
            IMemoryCache memoryCache,
            ILogger<HomeController> logger,
            IRepository<Candidate> candidateRepository,
            IRepository<Voter> voterRepository,
            IRepository<Vote> voteRepository,
            IRepository<Election> electionRepository,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
            _voterRepository = voterRepository;
            _voteRepository = voteRepository;
            _electionRepository = electionRepository;
            _userManager = userManager;
            _memoryCache = memoryCache;
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                if (User.IsInRole("Voter") || User.IsInRole("Administrator"))
                {
                    _logger.LogInformation("Going to load the Dashbord");
                    //the user has a voter Role, lets display the dashboard
                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);


                    _logger.LogInformation("Calling DashboardUtilities.getDashboard() method");
                    //Now lets Get a Cached Dashboard or Create it and Cached it
                    var cachedDashboard = _memoryCache.GetOrCreate(typeof(DashboardViewModel), d =>
                    {
                        d.AbsoluteExpiration = DateTime.Now.AddMinutes(3);
                        return DashboardUtilities.getDashboard(_candidateRepository, _voterRepository, _voteRepository, _electionRepository, currentUser); ;
                    });
                    if(cachedDashboard == null)
                    {
                        throw new BusinessException("Dashboard is null! Please contact your administrator.");
                    }
                    //so the above GetOrCreate() method tries to get a cached dashboard from the memory, and if it doesn't find any it will create
                    //an instance of the dashboard and cach it in memory for Three minutes


                    //Now lets count futureElections and previousElections and store them in a ViewBag to be displayed inside Excel Download buttons
                    ViewBag.futureElectionsCount = countFutureElections();
                    ViewBag.previousElectionsCount = countPreviousElections();


                    _logger.LogInformation("Returning dashboard instance to the view");
                    //DashboardViewModel d = DashboardUtilities.getDashboard(_candidateRepository, _voterRepository, _voteRepository, _electionRepository, currentUser);
                    return View(cachedDashboard);


                    /*
                    THIS IS HOW I USED TO LOAD THE OLD DASHBOARD BEFORE INTRODUCING THE ELECTION NOTION 
                    */
                    //we load an empty view, then it'll be filled using jQuery Ajax
                    //return View();

                }
                else
                {
                    //so this user has 'PreVoter', this happened in only one case: this is a new user who didn't change his password.
                    //    He should be redirected to ResetPassword view.
                    //    Once he change his password he will be provided the role 'Voter' 

                    _logger.LogInformation("Redirecting User to reset his password before using the application");
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    return RedirectToPage("/Account/ResetPassword", new { area = "Identity", code }); //this returns 'code must be supplied o reset password'
                }
            }
            catch(BusinessException be)
            {
                _logger.LogError(be.Message);
                //lets now create a suitable message for the user and store it inside a ViewBag (which is a Dynamic Object we can fill it
                //by whatever we want
                BusinessMessage bm = new BusinessMessage("Error", be.Message);
                ViewBag.BusinessMessage = bm;
                return View();
            }
            catch(Exception E)
            {
                _logger.LogError("Exception, " + E.Message);
                throw E;
            }            
        }
        [HttpPost]
        public async Task<IActionResult> GetResultsOfElection([FromBody] Guid electionId)
        {
            //this method is called using ajax calls
            //so if a business rule is not met we'll throw a businessException and catch it to create and internal server error and return its msg
            //as json


            _logger.LogInformation("Method Home/GetResultsOfElection() is called");
            try
            {
                if (electionId == null || electionId == Guid.Empty)
                {
                    throw new BusinessException("electionId can not be null.");
                }

                //this method returns a list of candidates (of an election) ordered by their number of votes
                var election = _electionRepository.GetById(electionId);
                if (election == null)
                {
                    throw new BusinessException("Election is not found.");
                }

                _logger.LogInformation("Calling method CandidateUtilities.GetCandidate_byElection()");
                var candidates = CandidateUtilities.GetCandidate_byElection(_candidateRepository, election);
                _logger.LogInformation("Calling Utilities.convertCandidateList_toCandidateViewModelList() method");
                List<CandidateViewModel> candidatesViewModel = Utilities.convertCandidateList_toCandidateViewModelList(_voterRepository, candidates);
                //lets serialize the list of candidatesviewmodel as json object
                var json = JsonConvert.SerializeObject(candidatesViewModel.OrderByDescending(c => c.VotesCount));
                return Ok(json);
            }
            catch (BusinessException be)
            {
                //lets create an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = be.Message });
            }
            catch (Exception E)
            {
                //lets create an internal server error so that jquery ajax understand that there was an error
                HttpContext.Response.StatusCode = 500;
                //leets log the exception msg to the console window
                _logger.LogError("Exception, " + E.Message);
                return Json(new { Message = E.Message });
                //In above code I created an internal server error so that the response returned is an ERROR, and jQuery ajax will understand that.
            }            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //create the ErrorViewModel instance
            ErrorViewModel error = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            //get the current Exception
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            //fill the exception msg into the errorText field of ErrorViewModel
            error.ErrorText = exception.Error.Message;
            //return the errorViewModel instance to Error.cshtml view
            return View(error);

        }








        #region futureElectionsExcelExport
        [HttpPost]
        public IActionResult futureElections_ExportToExcel()
        {
            //This function download list of all Future Elections as excel file
            try
            {
                var stream = new System.IO.MemoryStream();
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    var elections = futureElections();

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Elections");

                    worksheet.Cells[1, 1].Value = "Name";
                    worksheet.Cells[1, 2].Value = "Start Date";
                    worksheet.Cells[1, 3].Value = "Duration (d)";
                    worksheet.Cells[1, 4].Value = "Neutral Candidate (Y/N)";
                    worksheet.Cells[1, 5].Value = "Candidates";
                    worksheet.Row(1).Style.Font.Bold = true;


                    for (int c = 2; c < elections.Count + 2; c++)
                    {
                        worksheet.Cells[c, 1].Value = elections[c - 2].Name;
                        worksheet.Cells[c, 2].Value = elections[c - 2].StartDate.ToShortDateString();
                        worksheet.Cells[c, 3].Value = elections[c - 2].DurationInDays;
                        if (elections[c - 2].HasNeutral)
                        {
                            worksheet.Cells[c, 4].Value = "Y";
                        }
                        else
                        {
                            worksheet.Cells[c, 4].Value = "N";
                        }
                        worksheet.Cells[c, 5].Value = elections[c - 2].Candidates.Count().ToString();
                    }

                    package.Save();
                }

                string fileName = "Elections.xlsx";
                string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;
                return File(stream, fileType, fileName);
            }
            catch (Exception E)
            {
                throw E;
            }
        }


        public List<Election> futureElections()
        {
            //declaring an expression that is special to Election objects
            System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => e.StartDate > DateTime.Now;
            var futureElections = _electionRepository.GetAllFiltered(expr).ToList();
            return futureElections;
        }
        public int countFutureElections()
        {
            //declaring an expression that is special to Election objects
            System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => e.StartDate > DateTime.Now;
            int count = _electionRepository.GetAllFiltered(expr).Count();
            return count;
        }
        #endregion

        #region previousElectionsExcelExport
        public List<Election> previousElections()
        {
            //declaring an expression that is special to Election objects
            System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => e.StartDate.AddDays(e.DurationInDays) < DateTime.Now;
            var previousElections = _electionRepository.GetAllFiltered(expr).ToList();
            return previousElections;
        }
        public int countPreviousElections()
        {
            //declaring an expression that is special to Election objects
            System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => e.StartDate.AddDays(e.DurationInDays) < DateTime.Now;
            int count = _electionRepository.GetAllFiltered(expr).Count();
            return count;
        }

        #endregion
    }
}
