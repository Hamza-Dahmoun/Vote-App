[Currently doing: Stop using Repositories directly inside Controllers' actions by adding BusinessService files will use Repositories and will be used by Controllers' actions]
# What's In It?

Voting App where an Admin can:
 -  Add/delete/update/read States.
 -  Add/delete/update/read Voters. 
 -  Add/delete/update/read Elections and its Candidates, a Candidate is selected from Voters.
 -  User with role 'Voter' can participate to vote in a current election.
 -  An election is open to all voters to vote.
 -  A Voter is a user with role 'Voter'.
 -  A Voter when added can log in using his first name and last name as following:
   
    username: firstName.lastName  
    password: Pa$$w0rd 
-  A Voter when added will have a user account added automatically with the role 'Prevoter', he has to reset his password to get the role 'Voter'.
-  A Voter can see current election with its results, previous elections with its results and future election info, and can download both results in a pdf format.
 -  An Admin can download listings of States/Voters/Elections in an excel format.
-  A Voter can vote whereas an Admin cannot, he should get the role 'Voter' first.


# How to get it working:
After downloading the app, change the conection string in appsettings.json file, and run the below two commands in the Console Package Manager to create VoteDB and VoteDB-Identity databases
along with reasonable amount of data seeded in them for a better user experience:

update-database -context VoteDBContext


update-database -context IdentityDBContext


Once done, you can use the app using the below users:

- Admin Role: 

username: myadmin

password: Pa$$w0rd

- Voter Role:

username: myvoter 

password: Pa$$w0rd

# Technologies I used:
-  AspNet Core 3.1.
-  C#.
-  HTML5/CSS3.
-  Javascript.
-  jQuery AJAX.
-  Bootstrap.
-  Select2jQuery Datatable.
-  pdfmake.js.

#  Concepts I used for the first time:
-  MVC
-  Repository Design Patter
-  Logging Info & Errors
-  Localization
-  Migrations
-  Select2
-  jQuery Datatable
-  Seeding data