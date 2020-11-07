using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
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
    public class ElectionBusinessService
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

        public ElectionBusinessService(IRepository<Vote> voteRepository,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IStringLocalizer<Messages> messagesLocalizer,
            IRepository<Candidate> candidateRepository,
            IRepository<Voter> voterRepository,
            IRepository<Election> electionRepository)
        {
            _voteRepository = voteRepository;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _messagesLocalizer = messagesLocalizer;
            _candidateRepository = candidateRepository;
            _voterRepository = voterRepository;
            _electionRepository = electionRepository;
        }


        public Election GetById(Guid Id)
        {
            try
            {
                return _electionRepository.GetById(Id);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public Election GetByIdReadOnly(Guid Id)
        {
            try
            {
                return _electionRepository.GetByIdReadOnly(Id);
            }
            catch (Exception E)
            {
                throw E;
            }
        }
        public List<Election> GetAllFiltered(Expression<Func<Election, bool>> predicate)
        {
            try
            {
                return _electionRepository.GetAllFiltered(predicate);
            }
            catch (Exception E)
            {
                throw E;
            }
        }



        public List<Election> GetAll()
        {
            try
            {
                return (List<Election>)_electionRepository.GetAll();
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
                return _electionRepository.CountAll();
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public int Add(Election election)
        {
            try
            {
                int updatedRows = _electionRepository.Add(election);
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
                int updatedRows = _electionRepository.Delete(Id);
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

        public Election GetOneFiltered(Expression<Func<Election, bool>> predicate)
        {
            try
            {
                return _electionRepository.GetOneFiltered(predicate);
            }
            catch (Exception E)
            {
                throw E;
            }
        }



        public PagedResult<Election> GetAllFilteredPaged(Expression<Func<Election, bool>> predicate, string orderBy, string orderDirection, int startRowIndex = 0, int maxRows = 10)
        {
            try
            {
                return _electionRepository.GetAllFilteredPaged(predicate, orderBy, orderDirection, startRowIndex, maxRows);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        

        public int Edit(Guid Id, Election election)
        {
            try
            {
                int updatedRows = _electionRepository.Edit(Id, election);
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

        public Election GetCurrentElection()
        {
            try
            {
                //declaring an expression that is special to Election objects
                //a current Election is the one that 'Date.Now' is between the startDate and the endDate(endDate = startDate + duration in days)
                System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => DateTime.Now.Date >= e.StartDate && DateTime.Now.Date.AddDays(-e.DurationInDays) <= e.StartDate;

                Election currentElection = GetAllFiltered(expr).FirstOrDefault();
                return currentElection;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public int GetElectionsInSamePeriod(DateTime startDate, int durationInDays)
        {
            //this method returns elections from db that are happening in the period between 'startDate' and 'durationInDays'
            //this method is used when adding a new Election, there should be no elections in the same period
            //and used when editing an Election, there should be only one election in the same period in the db which is the election instance to edit

            try
            {
                //to do so we have to check if one of these cases exist:
                //(https://stackoverflow.com/questions/13513932/algorithm-to-detect-overlapping-periods)
                //(tired to think of my own solution right now, lets just use this, it works)
                DateTime endDate = startDate.AddDays(durationInDays);
                //declaring an expression that is special to Election objects
                System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => e.StartDate <= endDate && startDate <= e.StartDate.AddDays(e.DurationInDays);
                var elections = GetAllFiltered(expr).ToList();

                return elections.Count;
            }
            catch (Exception E)
            {
                throw E;
            }
        }


        public ElectionViewModel ConvertElection_ToElectionViewModel(Election election)
        {
            try
            {
                ElectionViewModel e = new ElectionViewModel
                {
                    Id = election.Id,
                    Name = election.Name,
                    StartDate = election.StartDate,
                    DurationInDays = election.DurationInDays,
                    HasNeutral = election.HasNeutral,
                    NumberOfCandidates = election.Candidates.Where(c => c.isNeutralOpinion != true).Count(),
                    NumberOfVotes = election.Votes.Count()
                };

                return e;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public List<Election> GetPreviousElections()
        {
            //declaring an expression that is special to Election objects
            System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => e.StartDate.AddDays(e.DurationInDays) < DateTime.Now;
            var previousElections = GetAllFiltered(expr).ToList();
            return previousElections;
        }
        public int CountPreviousElections()
        {
            int count = GetPreviousElections().Count();
            return count;
        }


        public List<Election> GetFutureElections()
        {
            //declaring an expression that is special to Election objects
            System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => e.StartDate > DateTime.Now;
            var futureElections = GetAllFiltered(expr).ToList();
            return futureElections;
        }
        public int CountFutureElections()
        {
            int count = GetFutureElections().Count();
            return count;
        }


        public void AddNewElection(Election election)
        {
            //this method takes an election instance and add it to the db .. it is used when adding a new election by ElectionController/ValidateVote()
            try
            {
                //first of all lets check if this election is in future, if it is not then we'll not edit it
                if (election.StartDate <= DateTime.Now)
                {
                    //so it is not a future election
                    throw new BusinessException(_messagesLocalizer["A New Election should take place in a future date."]);
                }

                if (GetElectionsInSamePeriod(election.StartDate, election.DurationInDays) > 0)
                {
                    //so there is other existing elections which the period overlap with this new election's period
                    throw new BusinessException(_messagesLocalizer["There is an existing Election during the same period."]);
                }
                if (election.DurationInDays < 0 || election.DurationInDays > 5)
                {
                    //so the number of days is invalid

                    throw new BusinessException(_messagesLocalizer["The duration of the Election should be from one to five days."]);
                }

                //if election has a neutral opinion then we should add it to the db
                if (election.HasNeutral)
                {
                    Candidate neutralOpinion = new Candidate
                    {
                        Id = Guid.NewGuid(),
                        isNeutralOpinion = true,
                        Election = election
                    };

                    int updatedRows = Add(election);
                    if (updatedRows > 0)
                    {
                        //row updated successfully in the DB
                        int updatedRows2 = _candidateRepository.Add(neutralOpinion);
                        if (updatedRows2 < 1)
                        {
                            //row not updated in the DB
                            throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                        }
                    }
                    else
                    {
                        //row not updated in the DB
                        throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                    }

                }
                else
                {
                    int updatedRows = Add(election);
                    if (updatedRows < 1)
                    {
                        //row not updated in the DB
                        throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                    }
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


        public PagedResult<Voter> GetVotersByElection_ExcludingAlreadyCandidates_ForDataTable(
            Guid electionId,
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
                //lets first get the list of voterswho are already candidates of this election                
                List<Voter> alreadyCandidates = GetVoterBeing_ofCandidatesList_byElectionId(electionId);
                List<Guid> excludedVotersIDs = alreadyCandidates.Select(v => v.Id).ToList();

                Expression<Func<Voter, bool>> expr;
                //now lets look for a value in FirstName/LastName/StateName if user asked to
                if (!string.IsNullOrEmpty(searchValue))
                {
                    expr =
                        v => v.FirstName.Contains(searchValue) ||
                        v.LastName.Contains(searchValue) ||
                        (v.State != null && v.State.Name.Contains(searchValue))
                        && !excludedVotersIDs.Contains(v.Id);
                }
                else
                {
                    //lets send a linq Expression exrpessing that we don't want voters who are already candidates                                        
                    expr = v => !excludedVotersIDs.Contains(v.Id);
                }

                //lets get the list of voters filtered and paged
                PagedResult<Voter> pagedResult = _voterRepository.GetAllFilteredPaged(expr, sortColumnName, sortColumnDirection, skip, pageSize);
                return pagedResult;
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

        private List<Voter> GetVoterBeing_ofCandidatesList_byElectionId(Guid electionId)
        {
            //this methods returns a list of voters who are considered as candidates for this election except the neutral opinion
            try
            {
                var candidates = GetCandidatesListByElectionId(electionId);
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
        private List<Candidate> GetCandidatesListByElectionId(Guid electionId)
        {
            try
            {
                //declaring an expression that is special to Candidate objects and it compares the election instance of the candidates 
                //with 'election' parameter
                System.Linq.Expressions.Expression<Func<Candidate, bool>> expr = i => i.ElectionId == electionId;
                return _candidateRepository.GetAllFiltered(expr);
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
