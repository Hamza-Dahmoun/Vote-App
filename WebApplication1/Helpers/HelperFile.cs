using System;
using System.Collections.Generic;
using WebApplication1.Models.ViewModels;

struct response_Voters_and_NewElection
{
    //this Structure is used to return a javascript object containing the newly inserted election ID and a list of voters for user to select
    //from them ass candidates
    //it is used inside ElectionController
    public Guid ElectionId;
    public List<PersonViewModel> Voters;
}

public class CandidateElectionRelation
{
    //this class is used to get the data sent by jQuery ajax to the method AddCandidates() below
    //it is used inside ElectionController
    public Guid voterId { get; set; }
    public Guid electionId { get; set; }
}

public struct TemporaryElection
{
    //this struct is used to receive the data sent by jquery ajax to ElectionController/EditElection()
    //it is used inside ElectionController
    public string Id { get; set; }
    public string Name { get; set; }
    public string StartDate { get; set; }
    public string DurationInDays { get; set; }
    public string HasNeutral { get; set; }
}

public struct CurrentElectionDashboard
{
    //this struct is used to return the current election to a jquery ajax call of ElectionController/GetCurrentElection()
    //to be displayed in home page
    //it is used inside ElectionController
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public int DurationInDays { get; set; }
    public int CandidatesCount { get; set; }
    public double ParticipationRate { get; set; }
    public bool HasUserVoted { get; set; }
}