using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.BusinessService
{
    public class DashboardBusinessService
    {
        private readonly CandidateBusinessService _candidateBusiness;
        private readonly VoterBusinessService _voterBusiness;
        private readonly VoteBusinessService _voteBusiness;
        private readonly ElectionBusinessService _electionBusiness;

        public DashboardBusinessService(CandidateBusinessService candidateBusiness,
            VoterBusinessService voterBusiness,
            VoteBusinessService voteBusiness,
            ElectionBusinessService electionBusiness
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
