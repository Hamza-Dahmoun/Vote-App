using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;


namespace WebApplication1.Business
{
    public class DashboardBusiness
    {
        /*
         THIS CLASS CONTAINS STATIC METHODS SO THAT THEY CAN BE USE ELSEWHERE WITHOUT THE NEED OF INSTANCIATING AN OBJECT OF THE CLASS
             */

        //We are going to use different repositories to fill a dashboard
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<Candidate> _candidateRepository { get; }
        public IRepository<State> _stateRepository { get; }
        public IRepository<Vote> _voteRepository { get; }

        public DashboardBusiness()
        {

        }
        public DashboardBusiness(IRepository<Voter> voterRepository, IRepository<Candidate> candidateRepository,
            IRepository<State> stateRepository, IRepository<Vote> voteRepository)
        {
            //lets inject the repositories into the constructor

            _voterRepository = voterRepository;
            _candidateRepository = candidateRepository;
            _stateRepository = stateRepository;
            _voteRepository = voteRepository;
        }

        public static string sayHello()
        {
            return "Hello";
        }

        /*public static async Task<DashboardViewModel> getDashboard()
        {
            //this function returns a dashboard object filled with data
            //it is asynchronous becuz it uses another method which uses an asynchronous method GetUserAsync()

            List<CandidateViewModel> candidates = convertCandidateList_toPersonViewModelList(_candidateRepository.GetAll().ToList());

            int NbCandidates = candidates.Count;
            int NbVoters = _voterRepository.GetAll().Count;
            int NbVotes = _voteRepository.GetAll().Count;
            int votersWithVote = getNumberOfVoterWithVote();
            //Now lets get the currentUser to check if he has voted or not yet
            var currentUser = await getCurrentUser();
            bool userHasVoted = getVoterByUserId(Guid.Parse(currentUser.Id)).hasVoted();
            DashboardViewModel d = new DashboardViewModel
            {
                NbCandidates = NbCandidates,
                NbVoters = NbVoters,
                NbVotes = NbVotes,
                ParticipationRate = (double)votersWithVote / (double)NbVoters,
                Candidates = candidates,
                UserHasVoted = userHasVoted
            };
            return d;
        }*/
    }
}
