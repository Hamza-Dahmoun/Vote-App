using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public VoteBusiness(IRepository<Vote> voteRepository,
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
                Voter currentVoter = VoterUtilities.getVoterByUserId(Guid.Parse(currentUser.Id), _voterRepository);
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


    }
}
