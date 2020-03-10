namespace WebApplication1
{
    //here we'll create two enumerations. these enum values will be called in ConfigureServices() to create policies
    //with some roles. these policies are going to be used above actions with the attribute [Authorize(Policy="PolicyName")]
    //to authorize only special policies (that have special roles) to do some actions
    //note that we can use [Authorize(Users="user1", "user2"...)] or [Authorize(Roles = "role1", "role2"...)] but in case we wanted
    //to change roles we'll be forced to change them in all controllers, Policy let us change only in Startup.cs file
    public enum VoteAppPolicies
    {
        ManageElections,
        DoVote
    }
}