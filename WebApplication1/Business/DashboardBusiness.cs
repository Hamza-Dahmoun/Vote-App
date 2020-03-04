using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;


namespace WebApplication1.Business
{
    public class DashboardBusiness
    {
        //We are going to different repositories to fill a dashboard
        public IRepository<Voter> _voterRepository { get; }
        public IRepository<Candidate> _candidateRepository { get; }
        public IRepository<Structure> _structureRepository { get; }


        public DashboardBusiness(IRepository<Voter> voterRepository, IRepository<Candidate> candidateRepository, IRepository<Structure> structureRepository)
        {
            //lets inject the repositories into the constructor

            _voterRepository = voterRepository;
            _candidateRepository = candidateRepository;
            _structureRepository = structureRepository;
        }

        //public DashboardViewModel getDashboard()
        //{
        //    //this function returns a dashboard object filled with data

        //    List<Candidate> candidates = _candidateRepository.GetAll().ToList();
            

        //    DashboardViewModel d = new DashboardViewModel
        //    {
        //        NbCandidates = candidates.Count,
        //        NbVoters = _voterRepository.GetAll().Count,
        //        NbVotes = 1,
        //        ParticipationRate = 2,
        //        Candidates = candidates
        //    };
        //    return d;
        //}
    }
}
