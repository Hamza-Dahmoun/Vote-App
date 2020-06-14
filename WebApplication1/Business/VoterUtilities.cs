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
        {
            try
            {
                //this is using Method Dependancy Injection
                _voterRepository = voterRepository;


                //declaring an expression that is special to Voter objects
                System.Linq.Expressions.Expression<Func<Voter, bool>> expr = v => v.UserId == userId;


                return _voterRepository.GetOneFiltered(expr);
            }
            catch(Exception E)
            {
                throw E;
            }            
        }



        //Note that this method uses _voterRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static List<Voter> getOtherVoters(IRepository<Voter> voterRepository, List<Voter> voters)
        {//this is using Method Dependancy Injection

            try
            {
                //this method takes a list of Voters and return a list of voters different than those mentioned previously

                _voterRepository = voterRepository;
                return _voterRepository.GetAll().Except(voters).ToList();
            }
            catch(Exception E)
            {
                throw E;
            }            
        }



        //Note that this method uses _voterRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static string getStateName(IRepository<Voter> voterRepository, Guid voterId)
        {//this is using Method Dependancy Injection

            try
            {
                //this method returns the state name of a voter, I found myself obliged to write this bcuz I am using eager loading
                //and when I wanted to access a candidate's state name thru candidate.voterbeing.state.name it was always null

                _voterRepository = voterRepository;
                return _voterRepository.GetById(voterId).State.Name;
            }
            catch(Exception E)
            {
                throw E;
            }
        }


        
    }
}
