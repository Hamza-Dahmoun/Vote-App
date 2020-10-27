using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.BusinessService
{
    public class DashboardBusiness
    {
        private readonly CandidateBusiness _candidateBusiness;
        private readonly VoterBusiness _voterBusiness;
        private readonly VoteBusiness _voteBusiness;
        private readonly ElectionBusiness _electionBusiness;

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
    }
}
