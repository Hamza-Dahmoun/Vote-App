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
        {//this is using Method Dependancy Injection
            _electionRepository = electionRepository;
            //a current Election is the one that 'Date.Now' is between the startDate and the endDate(endDate = startDate + duration in days)
            Election currentElection = _electionRepository.GetAll().FirstOrDefault(e => DateTime.Now.Date >=e.StartDate && DateTime.Now.Date.AddDays(- e.DurationInDays) <= e.StartDate);
            return currentElection;
        }

    }
}
