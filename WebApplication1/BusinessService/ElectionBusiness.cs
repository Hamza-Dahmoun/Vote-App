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

namespace WebApplication1.BusinessService
{
    public class ElectionBusiness
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

        public ElectionBusiness(IRepository<Vote> voteRepository,
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
                return _electionRepository.Add(election);
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
                return _electionRepository.Delete(Id);
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
                return _electionRepository.Edit(Id, election);
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
