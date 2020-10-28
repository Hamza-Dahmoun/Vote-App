using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

namespace WebApplication1.BusinessService
{
    public class CandidateBusiness
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

        public CandidateBusiness(IRepository<Vote> voteRepository,
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

        public Candidate GetById(Guid Id)
        {
            try
            {
                return _candidateRepository.GetById(Id);
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
                return _candidateRepository.Add(candidate);
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
                return _candidateRepository.Delete(Id);
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

        public Candidate GetOneFiltered(Expression<Func<Candidate, bool>> predicate)
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



        public Candidate GetCandidate_byVoter_byElection(Voter voter, Election election)
        {
            //this method gets a candidate by its voterId and its ElectionId

            try
            {
                //declaring an expression that is special to Candidate objects and it compares the election instance of the candidates 
                //with 'election' parameter and voterBeing with voter parameter
                //System.Linq.Expressions.Expression<Func<Candidate, bool>> expr = i => i.Election == election && i.VoterBeing == voter;
                System.Linq.Expressions.Expression<Func<Candidate, bool>> expr = i => i.Election == election && i.VoterBeingId == voter.Id;
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
                System.Linq.Expressions.Expression<Func<Candidate, bool>> expr = i => i.Election == election;
                return GetAllFiltered(expr);
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
    }
}
