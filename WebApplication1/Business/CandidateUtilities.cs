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






        //Note that this method uses _candidateRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy 
        public static Candidate GetCandidate_byVoter_byElection(IRepository<Candidate> candidateRepository, Voter voter, Election election)
        {
            //this method gets a candidate by its voterId and its ElectionId

            _candidateRepository = candidateRepository;
            try
            {
                //declaring an expression that is special to Candidate objects and it compares the election instance of the candidates 
                //with 'election' parameter and voterBeing with voter parameter
                System.Linq.Expressions.Expression<Func<Candidate, bool>> expr = i => i.Election == election && i.VoterBeing == voter;
                return _candidateRepository.GetOneFiltered(expr);                
            }
            catch (Exception E)
            {
                throw E;
            }
        }
        

        //Note that this method uses _candidateRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static List<Candidate> GetCandidate_byElection(IRepository<Candidate> candidateRepository, Election election)
        {
            _candidateRepository = candidateRepository;
            try
            {
                //declaring an expression that is special to Candidate objects and it compares the election instance of the candidates 
                //with 'election' parameter
                System.Linq.Expressions.Expression<Func<Candidate, bool>> expr = i => i.Election == election;                
                return _candidateRepository.GetAllFiltered(expr);
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
