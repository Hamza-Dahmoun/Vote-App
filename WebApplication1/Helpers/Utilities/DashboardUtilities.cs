﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Business
{
    public static class DashboardUtilities
    {
        /*
        THIS CLASS IS STATIC, IT CAN BE USED WITHOUT INSTANCIATION.
        THIS CLASS HAS A STATIC FIELDS THAT IS ASSIGNED A REPOSITORIES WHEN CALLING A METHOD.
        THE METHOD ACCEPTS REPOSITORIES INSTANCES AS PARAMATERS AND ASSIGN THEM TO THIS CLASS'S FIELDS SO THAT USING THE METHODS OF THE REPOSITORIES 
        WILL BE POSSIBLE.
        THIS IS CALLED METHOD DEPENDANCY INJECTION
         */
        public static IRepository<Candidate> _candidateRepository;
        public static IRepository<Voter> _voterRepository;
        public static IRepository<Vote> _voteRepository;
        public static IRepository<Election> _electionRepository;


        //Note that this method uses _candidateRepository, so it depends to it, and we passed the repository object as a pramater. This is called Method Dependancy Injection
        public static DashboardViewModel getDashboard(
            IRepository<Candidate> candidateRepository,
            IRepository<Voter> voterRepository,
            IRepository<Vote> voteRepository,
            /*electionRepository is passed only to be passed to getCurrentElection() method, this is why we didn't add it as a field*/
            IRepository<Election> electionRepository,
            IdentityUser user)
        {
            //this function returns a dashboard object filled with data
            //it is asynchronous becuz it uses another method which uses an asynchronous method GetUserAsync()

            try
            {
                _candidateRepository = candidateRepository;
                _voterRepository = voterRepository;
                _voteRepository = voteRepository;
                _electionRepository = electionRepository;
                
                //Now lets get the currentUser to check if he has voted or not yet
                var currentUser = user;

                DashboardViewModel d = new DashboardViewModel
                {
                    NbElections = _electionRepository.CountAll(),
                    NbCandidates = _candidateRepository.CountAll(),
                    NbVoters = _voterRepository.CountAll(),
                    NbVotes = _voteRepository.CountAll()
                };
                return d;
            }
            catch(Exception E)
            {
                throw E;
            }            
        }
    }
}
