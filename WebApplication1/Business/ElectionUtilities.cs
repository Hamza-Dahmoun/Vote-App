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
                //Election currentElection = _electionRepository.GetOneFiltered(expr);
                Election currentElection = _electionRepository.GetAllFiltered(expr).FirstOrDefault();
                return currentElection;
            }
            catch (Exception E)
            {
                throw E;
            }            
        }
        


        //Note that this method uses _electionRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static int getElectionsInSamePeriod(IRepository<Election> electionRepository, DateTime startDate, int durationInDays)
        {//this is using Method Dependancy Injection

            try
            {
                //this method returns elections from db that are happening in the period between 'startDate' and 'durationInDays'
                //this method is used when adding a new Election, there should be no elections in the same period
                //and used when editing an Election, there should be only one election in the same period in the db which is the election instance to edit
                _electionRepository = electionRepository;

                //to do so we have to check if one of these cases exist:
                //(https://stackoverflow.com/questions/13513932/algorithm-to-detect-overlapping-periods)
                //(tired to think of my own solution right now, lets just use this, it works)
                DateTime endDate = startDate.AddDays(durationInDays);
                //declaring an expression that is special to Election objects
                System.Linq.Expressions.Expression<Func<Election, bool>> expr = e => e.StartDate <= endDate && startDate <= e.StartDate.AddDays(e.DurationInDays);
                var elections = _electionRepository.GetAllFiltered(expr).ToList();

                return elections.Count;

                //if (elections.Count > 0)
                //    return true;
                //else return false;
            }
            catch (Exception E)
            {
                throw E;
            }
        }




        

    }
}
