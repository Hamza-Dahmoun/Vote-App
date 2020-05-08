using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

namespace WebApplication1.Business
{
    public static class VoteUtilities
    {
        /*
        THIS CLASS IS STATIC, IT CAN BE USED WITHOUT INSTANCIATION.
        THIS CLASS HAS A STATIC FIELD THAT IS ASSIGNED A REPOSITORY WHEN CALLING A METHOD.
        THE METHOD ACCEPS A REPOSITORY INSTANCE AS PARAMATER AND ASSIGN IT TO THIS CLASS FIELD SO THAT USING THE METHODS OF THE REPOSITORY 
        WILL BE POSSIBLE.
        THIS IS CALLED METHOD DEPENDANCY INJECTION
         */
        public static IRepository<Vote> _voteRepository;


        //Note that this method uses _voterRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static bool hasVoted(IRepository<Vote> voteRepository, Guid ElectionId, Guid VoterId)
        {//this is using Method Dependancy Injection
            _voteRepository = voteRepository;


            //declaring an expression that is special to Vote objects
            System.Linq.Expressions.Expression<Func<Vote, bool>> expr = v => v.Election.Id == ElectionId && v.Voter.Id == VoterId;


            var votes = _voteRepository.GetAllFiltered(expr);
            if (votes.Count() > 0)
                return true;
            else return false;
        }

        //Note that this method uses _voterRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static int getNumberOfVotesByElection(IRepository<Vote> voteRepository, Guid ElectionId)
        {//this is using Method Dependancy Injection

            //this method returns the number of votes by election

            _voteRepository = voteRepository;
            //I used GroupBy() so that I get the rows by voter to count how many voters, not how many vote ... It worked like Distinct()
            int votesNumber = _voteRepository.GetAll().Where(v => v.Election.Id == ElectionId).GroupBy(v => v.Voter).Count();
            return votesNumber;
        }
    }
}
