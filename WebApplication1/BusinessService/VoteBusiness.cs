using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

namespace WebApplication1.BusinessService
{
    public class VoteBusiness
    {
        //the below are services we're going to use in this file, they will be injected in the constructor
        private readonly IRepository<Vote> _voteRepository;

        public VoteBusiness(IRepository<Vote> voteRepository)
        {
            _voteRepository = voteRepository;
        }
    }
}
