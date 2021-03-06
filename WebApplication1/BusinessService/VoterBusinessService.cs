﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.Business;
using WebApplication1.Models;
using WebApplication1.Models.Helpers;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.BusinessService
{
    public class VoterBusinessService
    {
        //the below are services we're going to use in this file, they will be injected in the constructor
        private readonly IRepository<Vote> _voteRepository;
        //this is used to get the currentUser
        //Lets create a private readonly field IStringLocalizer<Messages> so that we can use Localization service, we'll inject it inside the constructor
        private readonly IStringLocalizer<Messages> _messagesLocalizer;
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<Voter> _voterRepository;
        private readonly UserBusinessService _userBusiness;
        //private readonly ILogger _logger;

        public VoterBusinessService(IRepository<Vote> voteRepository,
            IStringLocalizer<Messages> messagesLocalizer,
            IRepository<Candidate> candidateRepository,
            IRepository<Voter> voterRepository,
            UserBusinessService userBusiness
            //ILogger logger
            )
        {
            _voteRepository = voteRepository;
            _messagesLocalizer = messagesLocalizer;
            _candidateRepository = candidateRepository;
            _voterRepository = voterRepository;
            _userBusiness  = userBusiness;
            //_logger = logger;
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
        public List<Voter> GetAllFilteredReadOnly(Expression<Func<Voter, bool>> predicate)
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
                int updatedRows = _voterRepository.Edit(Id, state);
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
                int updatedRows = _voterRepository.Add(state);
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
                int updatedRows = _voterRepository.Delete(Id);
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
                Expression<Func<Voter, bool>> expr = v => v.UserId == userId;

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

        public List<Voter> GetCorrespondingVoters(List<Candidate> candidates)
        {
            try
            {
                List<Voter> voters = new List<Voter>();
                foreach (var candidate in candidates)
                {
                    voters.Add(GetById(candidate.VoterBeingId));
                }
                return voters;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public void MakeVotersStatesNull(Guid stateId)
        {
            //this method get a list of Voters by their stateId and make their StateId 'null'
            try
            {
                //declaring an expression that is special to Election objects
                Expression<Func<Voter, bool>> expr = v => v.State.Id == stateId;
                var voters = GetAllFiltered(expr);
                //now lets update each voter by removing its relation to the state
                foreach (var voter in voters)
                {
                    voter.State = null;
                    voter.StateId = null;

                    int isRowUpdated = Edit(voter.Id, voter);
                    if (isRowUpdated < 1)
                    {
                        //row not updated in the DB
                        throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                    }
                }
            }
            catch (DataNotUpdatedException bnu)
            {
                throw bnu;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public async Task AddNewVoter(VoterStateViewModel vs)
        {
            try
            {
                //this method receives a VoterStateViewModel object, and based on it, it creates a voter object and stores it in the DB
                //after adding his corresponding user
                //_logger.LogInformation("Creating a new Voter instance");
                Voter v = new Voter
                {
                    Id = Guid.NewGuid(),
                    FirstName = vs.FirstName,
                    LastName = vs.LastName,
                    StateId = vs.StateID
                };

                //lets add the new user and use its ID in our new voter instance
                v.UserId = await AddNewUser_FromNewVoter(v);
                //_logger.LogInformation("Adding the new voter to the DB");
                int updatedRows = Add(v);
                if (updatedRows > 0)
                {
                    //row updated successfully in the DB
                    //_logger.LogInformation("The new voter is added to the DB successfully");
                }
                else
                {
                    //row not updated in the DB
                    throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                }
            }
            catch (BusinessException E)
            {
                throw E;
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

        private async Task<Guid> AddNewUser_FromNewVoter(Voter v)
        {
            //this method adds the voter as a new user to the IdentityDB using UserManager<IdentityUser> service
            //and returns this new user ID 
            try
            {                
                //we'll set its usernam/email, and set 'Pa$$w0rd' as the password
                string username = v.FirstName.ToLower() + "." + v.LastName.ToLower();
                return await _userBusiness.AddNewUser(username);
                
            }
            catch(BusinessException E)
            {
                throw E;
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

             
        public async Task DeleteVoter(Guid voterId)
        {
            //this method takes a voter id and delete him from db
            //but before that, it delete all his votes instances, all his candidates instances, and his user accound from identityDB
            try
            {
                //1- delete all votes instances of this voter
                DeleteAllVotesOfVoter(voterId);

                //2- delete all candidates instances of this voter
                DeleteAllCandidatesOfVoter(voterId);

                //3- delete all candidates instances of this voter
                await _userBusiness.DeleteUser(GetById(voterId).UserId.ToString());

                //4- delete voter from db
                //_logger.LogInformation("Going to delete the Voter instance");
                int updatedRows5 = Delete(voterId);
                if (updatedRows5 > 0)
                {
                    //row updated successfully in the DB
                    //_logger.LogInformation("Done deleting the Voter instance");
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
        
        private void DeleteAllVotesOfVoter(Guid voterId)
        {
            //this function takes a voterId and delete all Votes instances of this Voter
            try
            {
                //declaring an expression that is special to Vote objects
                Expression<Func<Vote, bool>> expr1 = e => e.VoterId == voterId;
                //_logger.LogInformation("Calling VoteRepository.GetAllFiltered() method");
                List<Vote> votesList = _voteRepository.GetAllFiltered(expr1);

                //_logger.LogInformation("Going to delete all Vote instances of a Voter");
                foreach (var vote in votesList)
                {
                    int updatedRows2 = _voteRepository.Delete(vote.Id);
                    if (updatedRows2 < 1)
                    {
                        //row not updated in the DB
                        throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                    }
                }
                //_logger.LogInformation("Done deleting all Vote instances of a Voter");
            }
            catch(BusinessException E)
            {
                throw E;
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

        private void DeleteAllCandidatesOfVoter(Guid voterId)
        {
            //this function takes a voter Id and delete his Candidates instances
            try
            {
                //declaring an expression that is special to Vote objects
                Expression<Func<Candidate, bool>> expr2 = e => e.VoterBeingId == voterId;
                //_logger.LogInformation("Going to get all Candidates instances of the Voter");
                List<Candidate> candidatesList = _candidateRepository.GetAllFiltered(expr2);

                //_logger.LogInformation("Going to delete all Candidates instances of the Voter");
                foreach (var candidate in candidatesList)
                {
                    int updatedRows3 = _candidateRepository.Delete(candidate.Id);
                    if (updatedRows3 < 1)
                    {
                        //row not updated in the DB
                        throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                    }
                }
                //_logger.LogInformation("Done deleting all Candidates instances of the Voter");
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


        public PagedResult<Voter> GetVoters_ForDataTable(
            string searchValue, 
            string sortColumnName, 
            string sortColumnDirection, 
            int pageSize,
            int skip)
        {
            //This method is called by jQuery datatables to get paged data
            //First, we'll try to read the variables sent from the jQuery request, and then, based on these variables' values we'll query
            //the db

            try
            {
                //now lets look for a value in FirstName/LastName/StateName if user asked to
                if (!string.IsNullOrEmpty(searchValue))
                {
                    //declaring an expression that is special to Voter objects
                    Expression<Func<Voter, bool>> expr =
                        v => v.FirstName.Contains(searchValue) ||
                        v.LastName.Contains(searchValue) ||
                        (v.State != null && v.State.Name.Contains(searchValue));

                    //lets get the list of voters filtered and paged
                    PagedResult<Voter> pagedResult = GetAllFilteredPaged(expr, sortColumnName, sortColumnDirection, skip, pageSize);

                    return pagedResult;
                }
                else
                {
                    //so user didn't ask for filtering, he only asked for paging

                    //lets get the list of voters paged
                    PagedResult<Voter> pagedResult = GetAllPaged(sortColumnName, sortColumnDirection, skip, pageSize);

                    return pagedResult;
                }
            }
            catch (BusinessException E)
            {
                throw E;
            }
            catch (Exception E)
            {
                throw E ;
            }
        }
    }
}
