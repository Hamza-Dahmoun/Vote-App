using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.BusinessService
{
    public class DashboardBusiness
    {
        private readonly CandidateBusiness _candidateBusiness;
        private readonly VoterBusiness _voterBusiness;
        private readonly VoteBusiness _voteBusiness;
        private readonly ElectionBusiness _electionBusiness;
        //this is used to get the currentUser
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public DashboardBusiness(CandidateBusiness candidateBusiness,
            VoterBusiness voterBusiness,
            VoteBusiness voteBusiness,
            ElectionBusiness electionBusiness
            )
        {
            _candidateBusiness = candidateBusiness;
            _voterBusiness = voterBusiness;
            _voteBusiness = voteBusiness;
            _electionBusiness = electionBusiness;
        }

        public DashboardViewModel GetDashboard()
        {
            //this function returns a dashboard object filled with data
            //it is asynchronous becuz it uses another method which uses an asynchronous method GetUserAsync()

            try
            {
                DashboardViewModel d = new DashboardViewModel
                {
                    NbElections = _electionBusiness.CountAll(),
                    NbCandidates = _candidateBusiness.CountAll(),
                    NbVoters = _voterBusiness.CountAll(),
                    NbVotes = _voteBusiness.CountAll()
                };
                return d;
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
