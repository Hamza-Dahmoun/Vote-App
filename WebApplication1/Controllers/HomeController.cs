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
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using WebApplication1.BusinessService;

namespace WebApplication1.Controllers
{
    //the below attribute will permit only authorized users to access HomeController, anonymous access will be deactivated
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //Lets create a private readonly field IStringLocalizer<Messages> so that we can use Localization service, we'll inject it inside the constructor
        private readonly IStringLocalizer<Messages> _messagesLoclizer;

        //the below are services we're going to use in this controller, they will be injected in the constructor
        //this is only used to get able to generate a 'code' needed to reset the password, and to get the currentUser ID
        private readonly UserManager<IdentityUser> _userManager;
        //Creting  private readonly field of type IMemoryCach
        private readonly IMemoryCache _memoryCache;
        private readonly ElectionBusinessService _electionBusiness;
        private readonly DashboardBusinessService _dashboardBusiness;
        private readonly CandidateBusinessService _candidateBusiness;

        //Lets inject the services using the constructor, this is called Constructor Dependency Injection
        public HomeController(
            IMemoryCache memoryCache,
            ILogger<HomeController> logger,
            IRepository<Election> electionRepository,
            UserManager<IdentityUser> userManager,
            IStringLocalizer<Messages> messagesLoclizer,
            ElectionBusinessService electionBusiness,
            DashboardBusinessService dashboardBusiness,
            CandidateBusinessService candidateBusiness)
        {
            _logger = logger;
            _electionBusiness = electionBusiness;
            _userManager = userManager;
            _memoryCache = memoryCache;
            _messagesLoclizer = messagesLoclizer;
            _dashboardBusiness = dashboardBusiness;
            _candidateBusiness = candidateBusiness;
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


                    _logger.LogInformation("Calling DashboardBusiness.GetDashboard() method");
                    //Now lets Get a Cached Dashboard or Create it and Cached it
                    var cachedDashboard = _memoryCache.GetOrCreate(typeof(DashboardViewModel), d =>
                    {
                        d.AbsoluteExpiration = DateTime.Now.AddMinutes(3);
                        return _dashboardBusiness.GetDashboard();
                    });
                    if(cachedDashboard == null)
                    {
                        throw new BusinessException(_messagesLoclizer["Dashboard is null! Please contact your administrator."]);
                    }
                    //so the above GetOrCreate() method tries to get a cached dashboard from the memory, and if it doesn't find any it will create
                    //an instance of the dashboard and cach it in memory for Three minutes


                    //Now lets count futureElections and previousElections and store them in a ViewBag to be displayed inside Excel Download buttons
                    ViewBag.futureElectionsCount = _electionBusiness.CountFutureElections();
                    ViewBag.previousElectionsCount = _electionBusiness.CountPreviousElections();


                    _logger.LogInformation("Returning dashboard instance to the view");
                    return View(cachedDashboard);



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
                    throw new BusinessException(_messagesLoclizer["electionId can not be null."]);
                }

                //this method returns a list of candidates (of an election) ordered by their number of votes
                var election = _electionBusiness.GetById(electionId);
                if (election == null)
                {
                    throw new BusinessException(_messagesLoclizer["Election is not found."]);
                }

                _logger.LogInformation("Calling method CandidateBusiness.GetCandidate_byElection()");

                var candidates = _candidateBusiness.GetCandidate_byElection(election);
                _logger.LogInformation("Calling _candidateBusiness.ConvertCandidateList_ToCandidateViewModelList() method");

                List<CandidateViewModel> candidatesViewModel = _candidateBusiness.ConvertCandidateList_ToCandidateViewModelList(candidates);

                //lets serialize the list of candidatesviewmodel as json object
                JsonSerializerSettings settings = new JsonSerializerSettings { DateFormatString = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern };
                var json = JsonConvert.SerializeObject(candidatesViewModel.OrderByDescending(c => c.VotesCount), settings);
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

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            //this action is invoked by the selectLanguageDropdownList
            //This action will override a key/value to the localization cookie provider (CookieRequestCultureProvider)
            //with a period of life of one year
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }






        #region futureElectionsExcelExport
        [HttpPost]
        public IActionResult FutureElections_ExportToExcel()
        {
            //This function download list of all Future Elections as excel file
            try
            {
                var stream = new System.IO.MemoryStream();
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    var elections = _electionBusiness.GetFutureElections();

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Elections");

                    worksheet.Cells[1, 1].Value = _messagesLoclizer["Name"];
                    worksheet.Cells[1, 2].Value = _messagesLoclizer["Start Date"];
                    worksheet.Cells[1, 3].Value = _messagesLoclizer["Duration (d)"];
                    worksheet.Cells[1, 4].Value = _messagesLoclizer["Neutral Candidate (Y/N)"];
                    worksheet.Cells[1, 5].Value = _messagesLoclizer["Candidates"];
                    worksheet.Row(1).Style.Font.Bold = true;


                    for (int c = 2; c < elections.Count + 2; c++)
                    {
                        worksheet.Cells[c, 1].Value = elections[c - 2].Name;
                        worksheet.Cells[c, 2].Value = elections[c - 2].StartDate.ToShortDateString();
                        worksheet.Cells[c, 3].Value = elections[c - 2].DurationInDays;
                        if (elections[c - 2].HasNeutral)
                        {
                            worksheet.Cells[c, 4].Value = _messagesLoclizer["Y"];
                        }
                        else
                        {
                            worksheet.Cells[c, 4].Value = _messagesLoclizer["N"];
                        }
                        worksheet.Cells[c, 5].Value = elections[c - 2].Candidates.Count().ToString();
                    }

                    package.Save();
                }

                StringBuilder fileName = new StringBuilder();
                fileName.Append(_messagesLoclizer["futureElections.xlsx"]);

                StringBuilder fileType = new StringBuilder();
                fileType.Append("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                stream.Position = 0;
                return File(stream, fileType.ToString(), fileName.ToString());
            }
            catch (Exception E)
            {
                throw E;
            }
        }
        #endregion

        #region previousElectionsExcelExport
        [HttpPost]
        public IActionResult PreviousElections_ExportToExcel()
        {
            //This function download list of all Future Elections as excel file
            try
            {
                var stream = new System.IO.MemoryStream();
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    var elections = _electionBusiness.GetPreviousElections();

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Elections");

                    worksheet.Cells[1, 1].Value = _messagesLoclizer["Name"];
                    worksheet.Cells[1, 2].Value = _messagesLoclizer["Start Date"];
                    worksheet.Cells[1, 3].Value = _messagesLoclizer["Duration (d)"];
                    worksheet.Cells[1, 4].Value = _messagesLoclizer["Neutral Candidate (Y/N)"];
                    worksheet.Cells[1, 5].Value = _messagesLoclizer["Candidates"];
                    worksheet.Row(1).Style.Font.Bold = true;


                    for (int c = 2; c < elections.Count + 2; c++)
                    {
                        worksheet.Cells[c, 1].Value = elections[c - 2].Name;
                        worksheet.Cells[c, 2].Value = elections[c - 2].StartDate.ToShortDateString();
                        worksheet.Cells[c, 3].Value = elections[c - 2].DurationInDays;
                        if (elections[c - 2].HasNeutral)
                        {
                            worksheet.Cells[c, 4].Value = _messagesLoclizer["Y"];
                        }
                        else
                        {
                            worksheet.Cells[c, 4].Value = _messagesLoclizer["N"];
                        }
                        worksheet.Cells[c, 5].Value = elections[c - 2].Candidates.Count().ToString();
                    }

                    package.Save();
                }

                StringBuilder fileName = new StringBuilder();
                fileName.Append(_messagesLoclizer["previousElections.xlsx"]);

                StringBuilder fileType = new StringBuilder();
                fileType.Append("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                
                stream.Position = 0;
                return File(stream, fileType.ToString(), fileName.ToString());
            }
            catch (Exception E)
            {
                throw E;
            }
        }
        #endregion
    }
}
