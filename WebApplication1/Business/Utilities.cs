using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Business
{
    public static class Utilities
    {

        private static CandidateViewModel convertCandidate_toCandidateViewModel(IRepository<Voter> voterRepository, Candidate candidate)
        {//the parameter voterRepository is passed to be used in a Method Dependancy Injection in VoterUtilities.getStateName() method
            try
            {
            CandidateViewModel c = new CandidateViewModel
                {
                    Id = candidate.Id,
                    isNeutralOpinion = candidate.isNeutralOpinion,
                    VotesCount = candidate.Votes.Count(),
                    /*electionId = candidate.Election.Id*/
                };
                if (!candidate.isNeutralOpinion)
                {

                    c.FirstName = candidate.VoterBeing.FirstName;
                    c.LastName = candidate.VoterBeing.LastName;
                    c.StateName = VoterUtilities.getStateName(voterRepository, candidate.VoterBeing.Id);

                    /*if (candidate.VoterBeing.hasVoted())
                        c.hasVoted = "Yes";
                    else c.hasVoted = "No";*/
                }
                else
                {
                    c.FirstName = NeutralOpinion.Neutral.ToString();// "Neutral";
                    c.LastName = NeutralOpinion.Opinion.ToString();// "Opinion";
                }

                return c;
            }
            catch(Exception E)
            {
                throw E;
            }            
        }

        public static List<CandidateViewModel> convertCandidateList_toCandidateViewModelList(
            IRepository<Voter> voterRepository, IList<Candidate> candidates)
        {//the parameter voterRepository is passed to be used in a Method Dependancy Injection in VoterUtilities.getStateName() method
            List<CandidateViewModel> myList = new List<CandidateViewModel>();
            foreach (var item in candidates)
            {
                myList.Add(convertCandidate_toCandidateViewModel(voterRepository, item));
            }

            return myList.OrderByDescending(c => c.VotesCount).ToList();
        }




        public static PersonViewModel convertVoter_toPersonViewModel(Voter voter)
        {
            try
            {
                PersonViewModel p = new PersonViewModel
                {
                    Id = voter.Id,
                    FirstName = voter.FirstName,
                    LastName = voter.LastName,
                    StateName = voter.State?.Name
                };
                /*if (voter.hasVoted())
                    p.hasVoted = "Yes";
                else p.hasVoted = "No";
                */

                return p;
            }
            catch(Exception E)
            {
                throw E;
            }            
        }

        public static List<PersonViewModel> convertVoterList_toPersonViewModelList(IList<Voter> voters)
        {
            try
            {
                List<PersonViewModel> myList = new List<PersonViewModel>();
                foreach (var item in voters)
                {
                    myList.Add(convertVoter_toPersonViewModel(item));
                }

                return myList;
            }
            catch(Exception E)
            {
                throw E;
            }            
        }




        public static StateViewModel convertState_toStateViewModel(State state)
        {
            try
            {
                StateViewModel s = new StateViewModel
                {
                    Id = state.Id,
                    Name = state.Name
                };
                return s;
            }
            catch(Exception E)
            {
                throw E;
            }            
        }

        public static List<StateViewModel> convertStateList_toStateViewModelList(IList<State> states)
        {
            try
            {
                List<StateViewModel> myList = new List<StateViewModel>();
                foreach (var item in states)
                {
                    myList.Add(convertState_toStateViewModel(item));
                }
                return myList;
            }
            catch(Exception E)
            {
                throw E;
            }            
        }





        public static ElectionViewModel convertElection_toElectionViewModel(Election election)
        {
            try
            {
                //int numberOfCandidates  = election.Candidates.Count();
                ElectionViewModel e = new ElectionViewModel
                {
                    Id = election.Id,
                    Name = election.Name,
                    StartDate = election.StartDate,
                    DurationInDays = election.DurationInDays,
                    HasNeutral = election.HasNeutral,
                    NumberOfCandidates = election.Candidates.Where(c => c.isNeutralOpinion != true).Count(),
                    NumberOfVotes = election.Votes.Count()
                    //NumberOfVoters = election.ElectionVoters.Count()
                };

                return e;
            }
            catch(Exception E)
            {
                throw E;
            }            
        }

        public static List<ElectionViewModel> convertElectionList_toElectionViewModelList(IList<Election> elections)
        {
            try
            {
                List<ElectionViewModel> myList = new List<ElectionViewModel>();
                foreach (var item in elections)
                {
                    myList.Add(convertElection_toElectionViewModel(item));
                }

                return myList.OrderBy(c => c.StartDate).ToList();
            }
            catch(Exception E)
            {
                throw E;
            }            
        }







        private static VoterCandidateEntityViewModel convertVoter_toVoterCandidateEntityViewModel(Voter v)
        {
            try
            {
                VoterCandidateEntityViewModel vc = new VoterCandidateEntityViewModel();
                vc.VoterId = v.Id.ToString();
                vc.FirstName = v.FirstName;
                vc.LastName = v.LastName;
                vc.StateName = v.State?.Name;
                return vc;
            }
            catch(Exception E)
            {
                throw E;
            }            
        }
        private static VoterCandidateEntityViewModel convertCandidate_toVoterCandidateEntityViewModel(IRepository<Voter> voterRepository, Candidate c)
        {
            try
            {
                VoterCandidateEntityViewModel vc = new VoterCandidateEntityViewModel();
                vc.VoterId = c.VoterBeing.Id.ToString();
                vc.FirstName = c.VoterBeing.FirstName;
                vc.LastName = c.VoterBeing.LastName;
                vc.StateName = VoterUtilities.getStateName(voterRepository, c.VoterBeing.Id);
                vc.CandidateId = c.Id.ToString();
                return vc;
            }
            catch(Exception E)
            {
                throw E;
            }            
        }
        //this is used and called when editing an election
        public static List<VoterCandidateEntityViewModel> convertVoterList_toVoterCandidateEntityViewModelList(
            List<VoterCandidateEntityViewModel> myList, 
            List<Voter> voterList)
        {
            try
            {
                foreach (var item in voterList)
                {
                    myList.Add(convertVoter_toVoterCandidateEntityViewModel(item));
                }

                return myList;
            }
            catch(Exception E)
            {
                throw E;
            }            
        }
        //this is used and called when editing an election
        public static List<VoterCandidateEntityViewModel> convertCandidateList_toVoterCandidateEntityViewModelList(
            IRepository<Voter> voterRepository,
            List<VoterCandidateEntityViewModel> myList,
            List<Candidate> candidateList)
        {//the parameter voterRepository is passed to be used in a Method Dependancy Injection in VoterUtilities.getStateName() method
            try
            {
                foreach (var item in candidateList)
                {
                    myList.Add(convertCandidate_toVoterCandidateEntityViewModel(voterRepository, item));
                }

                return myList;
            }
            catch(Exception E)
            {
                throw E;
            }            
        }









        public static List<Voter> getCorrespondingVoters(List<Candidate> candidates)
        {
            try
            {
                List<Voter> voters = new List<Voter>();
                foreach (var candidate in candidates)
                {
                    voters.Add(candidate.VoterBeing);
                }
                return voters;
            }
            catch(Exception E)
            {
                throw E;
            }            
        }
    }
}
