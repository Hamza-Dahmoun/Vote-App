using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Helpers;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.BusinessService
{
    public class VoterBusiness
    {
        //the below are services we're going to use in this file, they will be injected in the constructor
        private readonly IRepository<Vote> _voteRepository;
        //this is used to get the currentUser
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        //Lets create a private readonly field IStringLocalizer<Messages> so that we can use Localization service, we'll inject it inside the constructor
        private readonly IStringLocalizer<Messages> _messagesLoclizer;
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<Voter> _voterRepository;
        private readonly IRepository<Election> _electionRepository;

        public VoterBusiness(IRepository<Vote> voteRepository,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IStringLocalizer<Messages> messagesLoclizer,
            IRepository<Candidate> candidateRepository,
            IRepository<Voter> voterRepository,
            IRepository<Election> electionRepository)
        {
            _voteRepository = voteRepository;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _messagesLoclizer = messagesLoclizer;
            _candidateRepository = candidateRepository;
            _voterRepository = voterRepository;
            _electionRepository = electionRepository;
        }


        public List<Voter> GetAllFiltered(Expression<Func<Voter, bool>> predicate)
        {
            try
            {
                return _voterRepository.GetAllFiltered(predicate);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public int Edit(Guid Id, Voter state)
        {
            try
            {
                return _voterRepository.Edit(Id, state);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<Voter> GetAll()
        {
            try
            {
                return (List<Voter>)_voterRepository.GetAll();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public Voter GetById(Guid Id)
        {
            try
            {
                return _voterRepository.GetById(Id);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public int Add(Voter state)
        {
            try
            {
                return _voterRepository.Add(state);
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
                return _voterRepository.Delete(Id);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public PagedResult<Voter> GetAllFilteredPaged(Expression<Func<Voter, bool>> predicate, string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            try
            {
                return _voterRepository.GetAllFilteredPaged(predicate, orderBy, orderDirection, startRowIndex, maxRows);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public PagedResult<Voter> GetAllPaged(string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            try
            {
                return _voterRepository.GetAllPaged(orderBy, orderDirection, startRowIndex, maxRows);
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
                return _voterRepository.CountAll();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public Voter GetOneFiltered(Expression<Func<Voter, bool>> predicate)
        {
            try
            {
                return _voterRepository.GetOneFiltered(predicate);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public Voter GetVoterByUserId(Guid userId)
        {
            try
            {
                //declaring an expression that is special to Voter objects
                System.Linq.Expressions.Expression<Func<Voter, bool>> expr = v => v.UserId == userId;

                return GetOneFiltered(expr);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<Voter> GetOtherVoters(List<Voter> voters)
        {
            try
            {
                //this method takes a list of Voters and return a list of voters different than those mentioned previously

                return GetAll().Except(voters).ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public string GetStateNameByVoterId(Guid voterId)
        {
            try
            {
                //this method returns the state name of a voter, I found myself obliged to write this bcuz I am using eager loading
                //and when I wanted to access a candidate's state name thru candidate.voterbeing.state.name it was always null

                Voter v = GetById(voterId);
                if (v.State != null)
                {
                    return v.State.Name;
                }
                return "";
            }
            catch (Exception E)
            {
                throw E;
            }
        }


        public PersonViewModel ConvertVoter_ToPersonViewModel(Voter voter)
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

                return p;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<PersonViewModel> ConvertVoterList_ToPersonViewModelList(IList<Voter> voters)
        {
            try
            {
                List<PersonViewModel> myList = new List<PersonViewModel>();
                foreach (var item in voters)
                {
                    myList.Add(ConvertVoter_ToPersonViewModel(item));
                }

                return myList;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        private VoterCandidateEntityViewModel ConvertVoter_ToVoterCandidateEntityViewModel(Voter v)
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
            catch (Exception E)
            {
                throw E;
            }
        }
                
        public List<VoterCandidateEntityViewModel> ConvertVoterList_ToVoterCandidateEntityViewModelList(
            List<VoterCandidateEntityViewModel> myList,
            List<Voter> voterList)
        {//this is used and called when editing an election
            try
            {
                foreach (var item in voterList)
                {
                    myList.Add(ConvertVoter_ToVoterCandidateEntityViewModel(item));
                }

                return myList;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

    }
}
