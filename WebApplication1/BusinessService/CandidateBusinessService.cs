using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.Business;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.BusinessService
{
    public class CandidateBusinessService
    {
        //the below are services we're going to use in this file, they will be injected in the constructor
        private readonly IRepository<Vote> _voteRepository;
        //this is used to get the currentUser
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        //Lets create a private readonly field IStringLocalizer<Messages> so that we can use Localization service, we'll inject it inside the constructor
        private readonly IStringLocalizer<Messages> _messagesLocalizer;
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<Voter> _voterRepository;
        private readonly IRepository<Election> _electionRepository;
        //private readonly ILogger _logger;

        public CandidateBusinessService(IRepository<Vote> voteRepository,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IStringLocalizer<Messages> messagesLocalizer,
            IRepository<Candidate> candidateRepository,
            IRepository<Voter> voterRepository,
            IRepository<Election> electionRepository
            //ILogger logger,
            )
        {
            _voteRepository = voteRepository;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _messagesLocalizer = messagesLocalizer;
            _candidateRepository = candidateRepository;
            _voterRepository = voterRepository;
            _electionRepository = electionRepository;
            //_logger = logger;
        }

        public Candidate GetById(Guid Id)
        {
            try
            {
                Candidate candidate =  _candidateRepository.GetById(Id);
                if (candidate == null)
                {
                    throw new BusinessException(_messagesLocalizer["Candidate not found"] + ".");
                }
                return candidate;
            }
            catch (BusinessException E)
            {
                throw E;
            }
            catch (Exception E)
            {
                throw E;
            }
        }
        

        public List<Candidate> GetAll()
        {
            try
            {
                return (List<Candidate>)_candidateRepository.GetAll();
            }
            catch (Exception E)
            {
                throw E;
            }
        }




        public int Add(Candidate candidate)
        {
            try
            {
                int updatedRows = _candidateRepository.Add(candidate);
                if (updatedRows > 0)
                {
                    return updatedRows;
                }
                else
                {
                    //row not updated in the DB
                    throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                }
            }
            catch (DataNotUpdatedException E)
            {
                throw E;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public int Delete(Guid Id)
        {
            try
            {
                int updatedRows = _candidateRepository.Delete(Id);
                if (updatedRows > 0)
                {
                    return updatedRows;
                }
                else
                {
                    //row not updated in the DB
                    throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                }
            }
            catch (DataNotUpdatedException E)
            {
                throw E;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<Candidate> GetAllFiltered(Expression<Func<Candidate, bool>> predicate)
        {
            try
            {
                return _candidateRepository.GetAllFiltered(predicate);
            }
            catch (Exception E)
            {
                throw E;
            }
        }



        private Candidate GetOneFiltered(Expression<Func<Candidate, bool>> predicate)
        {
            try
            {
                return _candidateRepository.GetOneFiltered(predicate);
            }
            catch (Exception E)
            {
                throw E;
            }
        }



        public int CountAll()
        {
            try
            {
                return _candidateRepository.CountAll();
            }
            catch (Exception E)
            {
                throw E;
            }
        }



        private Candidate GetCandidate_byVoter_byElection(Voter voter, Election election)
        {
            //this method gets a candidate by its voterId and its ElectionId

            try
            {
                //declaring an expression that is special to Candidate objects and it compares the election instance of the candidates 
                //with 'election' parameter and voterBeing with voter parameter

                Expression<Func<Candidate, bool>> expr = i => i.Election == election && i.VoterBeingId == voter.Id;
                return GetOneFiltered(expr);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<Candidate> GetCandidate_byElection(Election election)
        {
            try
            {
                //declaring an expression that is special to Candidate objects and it compares the election instance of the candidates 
                //with 'election' parameter
                Expression<Func<Candidate, bool>> expr = i => i.Election == election;
                return GetAllFiltered(expr);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        private List<Candidate> GetCandidate_byElectionId(Guid electionId)
        {
            try
            {
                //declaring an expression that is special to Candidate objects and it compares the election instance of the candidates 
                //with 'election' parameter
                Expression<Func<Candidate, bool>> expr = i => i.ElectionId == electionId;
                return GetAllFiltered(expr);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<CandidateViewModel> GetCandidateViewModelList_byOneCandidateID(Guid candidateId)
        {
            //This method returns a list of CandidatesViewModel taking one paramater candidateId
            try
            {
                //_logger.LogInformation("Running _candidateBusiness.GetCandidateViewModelList_byOneCandidateID() method");
                Candidate candid = GetById(candidateId);
                //Election election = _electionRepository.GetById(candid.Election.Id);
                var candidates = GetCandidate_byElectionId(candid.Election.Id);     
                List<CandidateViewModel> candidatesViewModel = ConvertCandidateList_ToCandidateViewModelList(candidates);
                return candidatesViewModel;
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public List<Voter> GetVoterBeing_ofCandidatesList_byElectionId(Guid electionId)
        {
            //this methods returns a list of voters who are considered as candidates for this election except the neutral opinion
            try
            {
                var candidates = GetCandidate_byElectionId(electionId);
                List<Voter> voters = new List<Voter>();
                foreach (var item in candidates)
                {
                    if (!item.isNeutralOpinion)
                        voters.Add(_voterRepository.GetById(item.VoterBeingId));
                }
                return voters;

            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<Voter> GetVoterBeing_ofCandidatesList_byElection(Election election)
        {
            //this methods returns a list of voters who are considered as candidates for this election except the neutral opinion
            try
            {
                var candidates = GetCandidate_byElection(election);
                List<Voter> voters = new List<Voter>();
                foreach (var item in candidates)
                {
                    if (!item.isNeutralOpinion)
                        voters.Add(_voterRepository.GetById(item.VoterBeingId));
                }
                return voters;

            }
            catch (Exception E)
            {
                throw E;
            }
        }



        private CandidateViewModel ConvertCandidate_ToCandidateViewModel(Candidate candidate)
        {
            try
            {
                CandidateViewModel c = new CandidateViewModel
                {
                    Id = candidate.Id,
                    isNeutralOpinion = candidate.isNeutralOpinion,
                    VotesCount = candidate.Votes.Count(),
                };
                if (!candidate.isNeutralOpinion)
                {
                    Voter v = _voterRepository.GetById(candidate.VoterBeingId);
                    c.FirstName = v.FirstName;
                    c.LastName = v.LastName;

                    if (v.State != null)
                    {
                        c.StateName = v.State.Name;
                    }
                    else
                    {
                        c.StateName = "";
                    }
                    
                }
                else
                {
                    c.FirstName = NeutralOpinion.Neutral.ToString();// "Neutral";
                    c.LastName = NeutralOpinion.Opinion.ToString();// "Opinion";
                }

                return c;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<CandidateViewModel> ConvertCandidateList_ToCandidateViewModelList(IList<Candidate> candidates)
        {
            List<CandidateViewModel> myList = new List<CandidateViewModel>();
            foreach (var item in candidates)
            {
                myList.Add(ConvertCandidate_ToCandidateViewModel(item));
            }

            return myList.OrderByDescending(c => c.VotesCount).ToList();
        }

        private VoterCandidateEntityViewModel ConvertCandidate_ToVoterCandidateEntityViewModel(Candidate c)
        {
            try
            {
                Voter v = _voterRepository.GetById(c.VoterBeingId);
                VoterCandidateEntityViewModel vc = new VoterCandidateEntityViewModel();
                vc.VoterId = v.Id.ToString();
                vc.FirstName = v.FirstName;
                vc.LastName = v.LastName;

                if (v.State != null)
                {
                    vc.StateName = v.State.Name;
                }
                else
                {
                    vc.StateName = "";
                }
                
                vc.CandidateId = c.Id.ToString();
                return vc;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<VoterCandidateEntityViewModel> ConvertCandidateList_ToVoterCandidateEntityViewModelList(
            List<VoterCandidateEntityViewModel> myList,
            List<Candidate> candidateList)
        {
            try
            {
                foreach (var item in candidateList)
                {
                    myList.Add(ConvertCandidate_ToVoterCandidateEntityViewModel(item));
                }

                return myList;
            }
            catch (Exception E)
            {
                throw E;
            }
        }



        private List<Candidate> GetCandidatesOfCurrentElection()
        {
            //this method returns a list of candidates that are related to the current election
            //_logger.LogInformation("VoteBusiness.GetCandidatesOfCurrentElection() is called");
            try
            {
                //_logger.LogInformation("Calling ElectionBusiness.GetCurrentElection() method");

                //declaring an expression that is special to Election objects
                //a current Election is the one that 'Date.Now' is between the startDate and the endDate(endDate = startDate + duration in days)
                Expression<Func<Election, bool>> electionExpr = e => DateTime.Now.Date >= e.StartDate && DateTime.Now.Date.AddDays(-e.DurationInDays) <= e.StartDate;

                Election currentElection = _electionRepository.GetAllFiltered(electionExpr).FirstOrDefault();
                if (currentElection == null)
                {
                    //_logger.LogError("Current election not found");
                    throw new BusinessException(_messagesLocalizer["Current election not found"]);
                }

                //_logger.LogInformation("Calling CandidateBusiness.GetCandidate_byElection() method");

                //declaring an expression that is special to Candidate objects and it compares the election instance of the candidates 
                //with 'election' parameter
                Expression<Func<Candidate, bool>> candidateExpr = i => i.Election == currentElection;
                var candidates = _candidateRepository.GetAllFiltered(candidateExpr);
                if (candidates == null || candidates.Count == 0)
                {
                    //_logger.LogError("No candidates found for this election");
                    throw new BusinessException(_messagesLocalizer["No candidates found for this election"]);
                }
                return candidates;
            }
            catch (BusinessException E)
            {
                throw E;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<CandidateViewModel> GetCandidatesViewModelList_OfCurrentElection()
        {
            //this method returns a list of canddiatesViewModel of the current election
            //_logger.LogInformation("VoteBusiness.GetCandidatesViewModelList_OfCurrentElection() is called");
            try
            {
                var candidates = GetCandidatesOfCurrentElection();
                //_logger.LogInformation("Calling _candidateBusiness.ConvertCandidateList_ToCandidateViewModelList() method");
                List<CandidateViewModel> cvmList = ConvertCandidateList_ToCandidateViewModelList(candidates);
                //_logger.LogInformation("Returning a list of CandidateViewModel to Index view");
                return cvmList;
            }
            catch (BusinessException E)
            {
                throw E;
            }
            catch (Exception E)
            {
                throw E;
            }
        }


        public Candidate AddNewCandidate(Guid voterId, Guid electionId)
        {
            try
            {
                if (electionId == null || voterId == null)
                {
                    throw new BusinessException(_messagesLocalizer["Properties voterId and electionId can not be null."]);
                }

                Voter voter = _voterRepository.GetById(voterId);
                if (voter == null)
                {
                    throw new BusinessException(_messagesLocalizer["Voter not found"] + ".");
                }

                Candidate newCandidate = new Candidate
                {
                    Id = Guid.NewGuid(),
                    VoterBeingId = voter.Id,
                    ElectionId = electionId,
                };

                int updatedRows = Add(newCandidate);
                if (updatedRows >0)
                {
                    return newCandidate;
                }
                else
                {
                    //row not updated in the DB
                    throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                }
            }
            catch (DataNotUpdatedException E)
            {
                throw E;
            }
            catch (BusinessException E)
            {
                throw E;
            }
            catch (Exception E)
            {
                throw E;
            }

        }

        public void RemoveCandidateByElectionIdAndVoterId(Guid electionId, Guid voterId)
        {
            //this function takes an ElectionId and a VoterId and delete the corresponding Candidate from DB
            try
            {
                if (electionId == null || voterId == null)
                {
                    throw new BusinessException(_messagesLocalizer["Properties voterId and electionId can not be null."]);
                }

                Voter voter = _voterRepository.GetById(voterId);
                if (voter == null)
                {
                    throw new BusinessException(_messagesLocalizer["Corresponding Voter instance not found."]);
                }

                Election election = _electionRepository.GetById(electionId);
                if (election == null)
                {
                    throw new BusinessException(_messagesLocalizer["Election instance not found."]);
                }


                Candidate myCandidate = GetCandidate_byVoter_byElection(voter, election);

                int updatedRows = _candidateRepository.Delete(myCandidate.Id);
                if (updatedRows > 0)
                {
                    //do nothing
                }
                else
                {
                    //row not updated in the DB
                    throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                }
            }
            catch(DataNotUpdatedException E)
            {
                throw E;
            }
            catch (BusinessException E)
            {
                throw E;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public void RemoveCandidateByID(string candidateId)
        {
            //this function takes a candidateId and delete the corresponding Candidate from DB
            try
            {
                if (String.IsNullOrEmpty(candidateId))
                {
                    throw new BusinessException(_messagesLocalizer["candidateId cannot be null."]);
                }

                Candidate myCandidate = GetById(Guid.Parse(candidateId));
                if (myCandidate == null)
                {
                    throw new BusinessException(_messagesLocalizer["Candidate not found."]);
                }


                int updatedRows3 = Delete(myCandidate.Id);
                if (updatedRows3 > 0)
                {
                    //do nothing
                }
                else
                {
                    //row not updated in the DB
                    throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                }
            }
            catch (DataNotUpdatedException E)
            {
                throw E;
            }
            catch (BusinessException E)
            {
                throw E;
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
