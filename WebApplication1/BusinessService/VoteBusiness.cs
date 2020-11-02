using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
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
    public class VoteBusiness
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
        private readonly VoterBusiness _voterBusiness;

        public VoteBusiness(IRepository<Vote> voteRepository,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IStringLocalizer<Messages> messagesLoclizer,
            IRepository<Candidate> candidateRepository,
            IRepository<Voter> voterRepository,
            IRepository<Election> electionRepository,
            VoterBusiness voterBusiness)
        {
            _voteRepository = voteRepository;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _messagesLoclizer = messagesLoclizer;
            _candidateRepository = candidateRepository;
            _voterRepository = voterRepository;
            _electionRepository = electionRepository;
            _voterBusiness = voterBusiness;
        }




        public async Task<int> AddVotes(List<string> candidateIdList)
        {
            try
            {
                //lets first get the concerned election
                Candidate firstOne = _candidateRepository.GetById(Guid.Parse(candidateIdList.FirstOrDefault()));
                Election election = _electionRepository.GetById(firstOne.Election.Id);
                if (election == null)
                {
                    //_logger.LogError("Cannot validate for null election");
                    throw new BusinessException(_messagesLoclizer["Cannot validate vote of null election"]);
                }

                //lets get the voter instance of the current user, so that we use its id with his votes
                var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                //_logger.LogInformation("Calling VoterUtilities.getVoterByUserId() method");
                Voter currentVoter = _voterBusiness.GetVoterByUserId(Guid.Parse(currentUser.Id));
                if (currentVoter == null)
                {
                    //_logger.LogError("Voter instance was not found for current user");
                    throw new BusinessException(_messagesLoclizer["Voter instance was not found for current user"]);
                }


                //_logger.LogInformation("Going to add Vote instance to the DB foreach Candidate");
                Vote v = new Vote();
                //lets add 'Vote' objects to the db
                foreach (var candidateId in candidateIdList)
                {
                    v.Id = Guid.NewGuid();
                    Candidate candidate = _candidateRepository.GetById(Guid.Parse(candidateId));
                    if (candidate == null)
                    {
                        //_logger.LogError("Candidate instance was not found for " + candidateId);
                        throw new BusinessException(_messagesLoclizer["Candidate instance was not found for"] + " " + candidateId);
                    }
                    v.Candidate = candidate;
                    v.Voter = currentVoter;
                    v.Datetime = DateTime.Now;
                    v.Election = election;

                    int updatedRows = _voteRepository.Add(v);
                    if (updatedRows < 1)
                    {
                        //row not updated in the DB
                        throw new DataNotUpdatedException(_messagesLoclizer["Data not updated, operation failed."]);
                    }
                }
                return candidateIdList.Count();
            }
            catch(DataNotUpdatedException E)
            {
                throw E;
            }
            catch (BusinessException E)
            {
                throw E;
            }
        }


        public int Delete(Guid Id)
        {
            try
            {
                return _voteRepository.Delete(Id);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<Vote> GetAllFiltered(Expression<Func<Vote, bool>> predicate)
        {
            try
            {
                return _voteRepository.GetAllFiltered(predicate);
            }
            catch (Exception E)
            {
                throw E;
            }
        }
        public List<Vote> GetAllFilteredReadOnly(Expression<Func<Vote, bool>> predicate)
        {
            try
            {
                return _voteRepository.GetAllFilteredReadOnly(predicate);
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
                return _voteRepository.CountAll();
            }
            catch (Exception E)
            {
                throw E;
            }
        }


        public int GetNumberOfVotersVotedOnElection(Guid ElectionId)
        {//this method returns the number of voters who voted in a given election
            try
            {
                //declaring an expression that is special to Vote objects
                System.Linq.Expressions.Expression<Func<Vote, bool>> expr = v => v.Election.Id == ElectionId;

                //I used GroupBy() so that I get the rows by voter to count how many voters, not how many vote ... It worked like Distinct()
                int votesNumber = GetAllFiltered(expr).GroupBy(v => v.Voter).Count();
                return votesNumber;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public bool HasVoted(Guid ElectionId, Guid VoterId)
        {
            try
            {
                //declaring an expression that is special to Vote objects
                System.Linq.Expressions.Expression<Func<Vote, bool>> expr = v => v.Election.Id == ElectionId && v.Voter.Id == VoterId;

                var votes = GetAllFiltered(expr);
                if (votes.Count() > 0)
                    return true;
                else return false;
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
