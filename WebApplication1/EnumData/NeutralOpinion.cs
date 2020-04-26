namespace WebApplication1
{
    //this file holds data (FirstName & LastName) of a candidate that is supposed to be neutral opinion
    //we don't store FirstName & LastName of a candidate in the db, so for now this is the bet way, using Enum!
    public enum NeutralOpinion
    {
        Neutral,
        Opinion
    }
}