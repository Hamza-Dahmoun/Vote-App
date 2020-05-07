using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

namespace WebApplication1.Business
{
    public static class ElectionUtilities
    {
        /*
        THIS CLASS IS STATIC, IT CAN BE USED WITHOUT INSTANCIATION.
        THIS CLASS HAS A STATIC FIELD THAT IS ASSIGNED A REPOSITORY WHEN CALLING A METHOD.
        THE METHOD ACCEPTS A REPOSITORY INSTANCE AS PARAMATER AND ASSIGN IT TO THIS CLASS FIELD SO THAT USING THE METHODS OF THE REPOSITORY 
        WILL BE POSSIBLE.
        THIS IS CALLED METHOD DEPENDANCY INJECTION
         */
        public static IRepository<Election> _electionRepository;


        //Note that this method uses _electionRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static Election getCurrentElection(IRepository<Election> electionRepository)
        {
            //this is using Method Dependancy Injection
            _electionRepository = electionRepository;
            try
            {
                //declaring an expression that is special to Election objects
                //a current Election is the one that 'Date.Now' is between the startDate and the endDate(endDate = startDate + duration in days)
                System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => DateTime.Now.Date >= e.StartDate && DateTime.Now.Date.AddDays(-e.DurationInDays) <= e.StartDate;
                Election currentElection = _electionRepository.GetOneFiltered(expr);
                return currentElection;
            }
            catch (Exception E)
            {
                throw E;
            }            
        }
        #region SAME METHOD AS ABOVE BUT FILTERING DATA BEFORE KNOWING ABOUT EXPRESSION CLASS
        /*//Note that this method uses _electionRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static Election getCurrentElection(IRepository<Election> electionRepository)
        {//this is using Method Dependancy Injection
            _electionRepository = electionRepository;
            //a current Election is the one that 'Date.Now' is between the startDate and the endDate(endDate = startDate + duration in days)
            Election currentElection = _electionRepository.GetAll().FirstOrDefault(e => DateTime.Now.Date >= e.StartDate && DateTime.Now.Date.AddDays(-e.DurationInDays) <= e.StartDate);
            return currentElection;
        }*/
        #endregion

        //Note that this method uses _electionRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static bool isThereElectionInSamePeriod(IRepository<Election> electionRepository, DateTime startDate, int durationInDays)
        {//this is using Method Dependancy Injection
            
            //this method checks if there is an existing election instance in the db in the same duration of a new election that is going to be added to 
            //the db as well. If there is, the new election will not be inserted to the db
            _electionRepository = electionRepository;

            //to do so we have to check if one of these cases exist:
            //(https://stackoverflow.com/questions/13513932/algorithm-to-detect-overlapping-periods)
            //(tired to think of my own solution right now, lets just use this, it works)
            DateTime endDate = startDate.AddDays(durationInDays);
            var elections = _electionRepository.GetAll().Where(e => e.StartDate <= endDate && startDate <= e.StartDate.AddDays(e.DurationInDays)).ToList();

            if (elections.Count > 0)
                return true;
            else return false;
        }

    }
}
