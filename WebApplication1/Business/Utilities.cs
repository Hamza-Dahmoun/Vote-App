using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Business
{
    public static class Utilities
    {
        public static CandidateViewModel convertCandidate_toCandidateViewModel(Candidate candidate)
        {
            CandidateViewModel c = new CandidateViewModel
            {
                Id = candidate.Id,
                isNeutralOpinion = candidate.isNeutralOpinion,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                StateName = candidate.State?.Name,
                VotesCount = candidate.Votes.Count(),
            };
            if (candidate.VoterBeing.hasVoted())
                c.hasVoted = "Yes";
            else c.hasVoted = "No";
            return c;
        }

        public static List<CandidateViewModel> convertCandidateList_toPersonViewModelList(IList<Candidate> candidates)
        {
            List<CandidateViewModel> myList = new List<CandidateViewModel>();
            foreach (var item in candidates)
            {
                myList.Add(convertCandidate_toCandidateViewModel(item));
            }

            return myList.OrderByDescending(c => c.VotesCount).ToList();
        }




        public static PersonViewModel convertVoter_toPersonViewModel(Voter voter)
        {
            PersonViewModel p = new PersonViewModel
            {
                Id = voter.Id,
                FirstName = voter.FirstName,
                LastName = voter.LastName,
                StateName = voter.State?.Name
            };
            if (voter.hasVoted())
                p.hasVoted = "Yes";
            else p.hasVoted = "No";

            return p;
        }

        public static List<PersonViewModel> convertVoterList_toPersonViewModelList(IList<Voter> voters)
        {
            List<PersonViewModel> myList = new List<PersonViewModel>();
            foreach (var item in voters)
            {
                myList.Add(convertVoter_toPersonViewModel(item));
            }

            return myList;
        }




        public static StateViewModel convertState_toStateViewModel(State state)
        {
            StateViewModel s = new StateViewModel
            {
                Id = state.Id,
                Name = state.Name
            };
            return s;
        }

        public static List<StateViewModel> convertStateList_toStateViewModelList(IList<State> states)
        {
            List<StateViewModel> myList = new List<StateViewModel>();
            foreach (var item in states)
            {
                myList.Add(convertState_toStateViewModel(item));
            }
            return myList;
        }





        public static ElectionViewModel convertElection_toElectionViewModel(Election election)
        {
            ElectionViewModel e = new ElectionViewModel
            {
                Id = election.Id,
                Name = election.Name,
                StartDate = election.StartDate,
                DurationInDays = election.DurationInDays,
                HasNeutral = election.HasNeutral,
                NumberOfCandidates = election.ElectionCandidates.Count(),
                NumberOfVoters = election.ElectionVoters.Count()
            };
            return e;
        }

        public static List<ElectionViewModel> convertElectionList_toElectionViewModelList(IList<Election> elections)
        {
            List<ElectionViewModel> myList = new List<ElectionViewModel>();
            foreach (var item in elections)
            {
                myList.Add(convertElection_toElectionViewModel(item));
            }

            return myList.OrderBy(c => c.StartDate).ToList();
        }
    }
}
