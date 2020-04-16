using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;

namespace WebApplication1.Business
{
    public static  class CandidateUtilities
    {
        /*
        THIS CLASS IS STATIC, IT CAN BE USED WITHOUT INSTANCIATION.
        THIS CLASS HAS A STATIC FIELD THAT IS ASSIGNED A REPOSITORY WHEN CALLING A METHOD.
        THE METHOD ACCEPTS A REPOSITORY INSTANCE AS PARAMATER AND ASSIGN IT TO THIS CLASS'S FIELD SO THAT USING THE METHODS OF THE REPOSITORY 
        WILL BE POSSIBLE.
        THIS IS CALLED METHOD DEPENDANCY INJECTION
         */
        public static IRepository<Candidate> _candidateRepository;



        //Note that this method uses _candidateRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static bool IsCandidate(Voter voter, IRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;

            Candidate candidate = _candidateRepository.GetAll().SingleOrDefault(c => c.VoterBeing?.Id == voter.Id);

            if (candidate != null)
                return true;
            else return false;

        }

        public static Candidate GetNeutralCandidate()
        {
            //This method returns the neutral opinion candidate, there is a maximum of one row, or there isn't yet
            return _candidateRepository.GetAll().SingleOrDefault(c => c.isNeutralOpinion == true);
        }
    }
}
