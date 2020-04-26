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

        //Note that this method uses _candidateRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        /*public static Candidate GetNeutralCandidate(IRepository<Candidate> candidateRepository)
        {
            //This method returns the neutral opinion candidate, there is a maximum of one row, or there isn't yet
            
            _candidateRepository = candidateRepository;
            try
            {
                return _candidateRepository.GetAll().SingleOrDefault(c => c.isNeutralOpinion == true);
            }
            catch(Exception E)
            {
                throw E;
            }
        }*/



        
        //Note that this method uses _candidateRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static Candidate GetCandidate_byVoter_byElection(IRepository<Candidate> candidateRepository, Voter voter, Election election)
        {
            //this method gets a candidate by its voterId and its ElectionId
            _candidateRepository = candidateRepository;
            try
            {
                return _candidateRepository.GetAll().SingleOrDefault(c => c.VoterBeing == voter && c.Election == election);
            }
            catch(Exception E)
            {
                throw E;
            }
        }




        //Note that this method uses _candidateRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static List<Candidate> GetCandidate_byElection(IRepository<Candidate> candidateRepository, Election election)
        {
            //this method gets a list of candidates by its ElectionId
            _candidateRepository = candidateRepository;
            try
            {
                return _candidateRepository.GetAll().Where(c => c.Election == election).ToList();
            }
            catch (Exception E)
            {
                throw E;
            }
        }
    }
}
