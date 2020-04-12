using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

namespace WebApplication1.Business
{
    public static class VoterUtilities
    {/*
        THIS CLASS IS STATIC, IT CAN BE USED WITHOUT INSTANCIATION.
        THIS CLASS HAS A STATIC FIELD THAT IS ASSIGNED A REPOSITORY WHEN CALLING A METHOD.
        THE METHOD ACCEPS A REPOSITORY INSTANCE AS PARAMATER AND ASSIGN IT TO THIS CLASS FIELD SO THAT USING THE METHODS OF THE REPOSITORY 
        WILL BE POSSIBLE.
        THIS IS CALLED METHOD DEPENDANCY INJECTION
         */
        public static IRepository<Voter> _voterRepository;



        //Note that this method uses _voterRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static Voter getVoterByUserId(Guid userId, IRepository<Voter> voterRepository)
        {//this is using Method Dependancy Injection
            _voterRepository = voterRepository;
            return _voterRepository.GetAll().SingleOrDefault(v => v.UserId == userId);
        }

        //Note that this method uses _voterRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static int getNumberOfVoterWithVote(IRepository<Voter> voterRepository)
        {
            _voterRepository = voterRepository;
            return _voterRepository.GetAll().Where(v => v.hasVoted() == true).Count();
        }

    }
}
