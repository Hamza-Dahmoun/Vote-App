/**
 *
 * SCENARIO: 
 * A USER ENTERS ALL DATA OF AN ELECION AND SUBMIT. IF IT WAS SUCCESSFULLY INSERTED WE'LL GET A RESPONSE FROM SERVER CONTAINING THE
 * ELECTION ITSELF, AND INVOKE JQUERY DATATABLE SERVER SIDE PROCESSING TO GET THE LIST OF VOTERS WITH A SELECT CANDIDATE BUTTON.
 * USER SELECT WHICH ONES ARE THE CANDIDATES. ON EACH CLICK A JQUERY AJAX REQUEST IS SENT TO THE SERVER TO CREATE A NEW CANDIDATE FOR THIS ELECTION.
 * IF THE CANDIDATE WAS CREATED SUCCESSFULLY, WE'LL DISPLAY IT IN A CANDIDATES AREA FOR THE USER TO SEE WHAT HE ALREADY SELECTED ASS CANDIDATE.
 * THE CANDIDATES ARE DUSPLAYED WITH A REMOVE BUTTON IN CASE USER WANTED TO REMOVE A CANDIDATE FROM THE ELECTION, IT ALSO RELEASE A JQUERY AJAX CALL
 * TO THE SERVER TO REMOVE THE CONCERNED CANDIDATE.
 * WHEN DONE USER GOES BACK TO THE HOME PAGE
 * 
 * */
//this variable is gonna be assigned the new election Id in server side
var electionId;
//add change event to has-neutral
document.getElementById("has-neutral").addEventListener("click", changeHasNautral);
//add click event to send-election-button
document.getElementById("send-election-button").addEventListener("click", sendElection);
var hasNeutral = false;

//this function change the value of the variable hasNeutral according to the user checking the checkbox #has-neutral
function changeHasNautral() {
    if (event.target.checked) {
        hasNeutral = true;
    }
    else {
        hasNeutral = false;
    }
}


//this function sends the new election object (without the list of the concerned voters & candidates) to the server to be stored in the db (step1)
function sendElection() {
/*
 HTML5 form validation won't work in our case because we are submitting data to the server using ajax, the browser displays the validation msgs but the ajax call is made anyway
in our case becuz it supposed that since we're using js to call ajax then we are in charge of the form validation. What we need is: in js code before the ajax call we need to check if the form is valid using form.checkValidity() js function, if true we'll call server using ajax,
else we'll do nothing because the browser will display validation msgs to the user.
for more check this answer
----- FROM SO:
The HTML5 form validation process is limited to situations where the form is being submitted via a submit button. 
The Form submission algorithm explicitly says that validation is not performed when the form is submitted via the submit() method. 
Apparently, the idea is that if you submit a form via JavaScript, you are supposed to do validation.
However, you can request (static) form validation against the constraints defined by HTML5 attributes, using the checkValidity() method. 
If you would like to display the same error messages as the browser would do in HTML5 form validation, 
I’m afraid you would need to check all the constrained fields, since the validityMessage property is a property of fields (controls), 
not the form.
 */
    if (document.getElementById("step-one-form").checkValidity()) {
        //form is valid based on HTML5 attributes

        //lets prevent form from refreshing the page after ajax call, becuz all the work we're in charge of it using js
        document.getElementById("step-one-form").addEventListener('submit', function (event) { event.preventDefault(); });


        var newElection =
        {
            Name: document.getElementById("election-name").value,
            StartDate: document.getElementById("start-date-election").value,
            DurationInDays: parseInt(document.getElementById("duration-in-days").value),
            HasNeutral: hasNeutral
        };
        //alert(JSON.stringify(newElection));
        //lets hide the button and display the spinner next to it
        document.getElementById("send-election-spinner").style.display = "block";
        document.getElementById("send-election-button").style.display = "none";
        //Send the JSON object Election to Controller using AJAX.
        $.ajax({
            type: "POST",
            url: "/Election/ValidateElection",
            data: JSON.stringify(newElection),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (response) {
                //alert("error");
                document.getElementById("send-election-spinner").style.display = "none";
                document.getElementById("send-election-button").style.display = "block";
                //in here 'response' represents the following object {success: false, message ='...text here...'}
                //which I sent after creating an Error HttpContext.Response.StatusCode = 500 ...see the Catch block of code in the backend
                //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

                //so there is a server error, lets display the error msg

                document.getElementById("redModal").querySelector("h4").innerText = resources[currentUserLanguage]["Error!"];
                document.getElementById("redModal").querySelector("p").innerText = response.responseJSON.message;
                $('#redModal').modal('show');
            },
            success: function (response) {
                //'response' represents the object returned from the api which is the Election object newly stored in the db
                console.log(response);
                //alert("success" + response);
                //electionId = response.Election.Id;
                electionId = response.ElectionId;
                disableAllInputs();
                changeBgColor();

                prepareVotersjQueryDatatable(response.ElectionId);
                scrollDown();
                //window.location.href = "Home/Index";
            }
        });
    }
    else {
        //do nothing becauz the form is not valid and the browser will display the validation msgs to the user
    }
}
//this function disable all the new Election inputs, and hide the submit button. It is called after storing this new Election successfully
function disableAllInputs() {
    //console.log("disabling inputs started");
    let election_inputs = document.getElementById("election-input-container").querySelectorAll("input");
    for (let i = 0; i < election_inputs.length; i++) {
        //console.log(election_inputs[i]);
        election_inputs[i].disabled = "true";
    }
    document.getElementById("send-election-spinner").style.display = "none";
    document.getElementById("send-election-button").style.display = "none";
    //console.log("disabling inputs done");
}
function changeBgColor() {
    let stepOne = document.getElementById("step-one");
    console.log(stepOne);
    stepOne.classList.add("successBG");
    stepOne.classList.add("successText");
}
function scrollDown() {
    //this function scroll down to step2
    document.getElementById("step-two").scrollIntoView();
}



//********************************************* AFTER USING JQUERY DATATABLES *********************/
function prepareVotersjQueryDatatable(electionId) {
    console.log("-" + electionId + "-");
    document.getElementById("step-two").style.display = "block";
    //this function send a request to the server to get the list of voters not candidates to a fiven election
    $("#voters-table").DataTable(
        {
            "processing": true,//whether to show 'processing' indicator when waiting for a processing result or not
            "serverSide": true,//for server side processing
            "filter": true,//this is for disable filter (search box)
            "ajax": {
                "url": '/Election/VotersDataTable/' /*+ electionId*/,
                "type": 'POST',
                "data": function (d) {
                    d.electionId = electionId;
                    //d.myKey = "myValue";
                    // d.custom = $('#myInput').val();
                    // etc
                },
                /*
                 WHEN I USED THE BELOW TO SEND ELECTIONID TO THE SERVER, I GOT EVERY LETTER AND NUMBER OF THE GUID SENT AS A SEPARATE PARAMETER!
                 THE ABOVE WAY IS THE ONE MENTIONED IN THE DOCUMENTATION
                 "data": JSON.stringify({electionId: electionId}),
                 or
                 "data": electionId,
                 */
                
                "error": function (reason) {
                    //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

                    //so there is a server error, lets display the error msg
                    let errorParag = document.createElement("p");
                    let responseMsg = document.createElement("div");
                    responseMsg.className = "alert alert-danger";
                    errorParag.innerHTML = "<strong>" + resources[currentUserLanguage]["Error when retrieving Candidates!"]+"</strong> " + reason.responseJSON.message;
                    responseMsg.appendChild(errorParag);
                    document.getElementById("step-two").appendChild(responseMsg);
                    //now lets hide the datatable wrapper (a div created by jquery which surrounds the table)
                    document.getElementById("step-two").querySelector(".dataTables_wrapper").style.display = "none";
                }
            },
            "columnDefs": [
                { "type": "numeric-comma", targets: "_all" }
            ],
            "columns":
                [//These are the columns to be displayed, and they are the fields of the voters objects brought from the server
                    { "data": "Id", "visible": false, "searchable": false },
                    { "data": "FirstName", "title": resources[currentUserLanguage]["FirstName"], "name": "FirstName", "visible": true, "searchable": true, "sortable": false },
                    { "data": "LastName", "title": resources[currentUserLanguage]["LastName"], "name": "LastName", "visible": true, "searchable": true, "sortable": false },
                    { "data": "State.Name", "title": resources[currentUserLanguage]["State"], "visible": true, "searchable": true, "sortable": false },
                    {
                        "data": null, "searchable": false, "sortable": false,
                        "render": function (data, type, row, meta) {
                            var button =
                                "<a class='select-candidate-btn' title='Select this Voter as a Candidate' voterid=" +
                                row.Id + " voterfullname='" + row.FirstName + " " + row.LastName
                                + "' onclick='selectNewCandidate()'>Select as Candidate</a>"
                                + "<div class='spinner-border text-success hidden-spinner'></div>"
                                ;

            
                            return button;
                        }
                    }
                ]
        }
    );

}

function selectNewCandidate() {
    //this function add the selected voter from jquery datatable to the db as a candidate related to the new election, and then reload the voters datatable

    let clickedButton = event.target; 

    //first of all lets display the spinner and hide the button    
    clickedButton.style.display = "none";
    clickedButton.parentElement.querySelector(".spinner-border").style.display = "block";

    //now lets store full name of the selected candidate in a variable, we'll use it if the voter has been added to candidate in db successfully
    let candidateFullName = clickedButton.getAttribute("voterfullname");

    let voterid = clickedButton.getAttribute("voterid"); 

    //Send the JSON data of voterId and electionId to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: "/Election/AddCandidates",
        data: JSON.stringify({ electionId: electionId, voterId: voterid }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            //which I sent after creating an Error HttpContext.Response.StatusCode = 500 ...see the Catch block of code in the backend
            //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

            //so there is a server error, lets display the error msg
            clickedButton.parentElement.querySelector(".spinner-border").style.display = "none";
            clickedButton.style.display = "block";
            document.getElementById("redModal").querySelector("h4").innerText = resources[currentUserLanguage]["Error"]+"!";
            document.getElementById("redModal").querySelector("p").innerText = response.responseJSON.message;
            $('#redModal').modal('show');

        },
        success: function (response) {
            //'response' represents the object returned from the api which is the Election object newly stored in the db
            //console.log(response);
            //alert("success" + response);

            //now lets display the selected candidate into Candidates area
            //alert(candidateFullName + " has been selected successfully");
            displayAddedCandidate(candidateFullName, voterid);
            //Now lets refresh jquery datatables.. this is speial to iquery datatables
            $("#voters-table").DataTable().ajax.reload();            
        }
    });
}
function displayAddedCandidate(candidateFullName, voterid) {
    //This function write the new candidates of the new electino in a special area, and give the user a button for each candidate to remove this
    //candidate from the election

    //lets first hide the transparent candidate which has=s text 'No Candidates Selected'
    document.getElementsByClassName("transparent-candidate")[0].style.display = "none";

    let p = document.createElement("p");
    p.innerText = candidateFullName;
    let spinner = document.createElement("div");
    spinner.className = "spinner-border text-danger hidden-spinner centered-spinner";
    let closeButton = document.createElement("a");
    closeButton.innerText = resources[currentUserLanguage]["Remove"];
    closeButton.setAttribute("voterid", voterid);
    closeButton.setAttribute("title", resources[currentUserLanguage]["Remove Candidate"]);
    closeButton.className = "remove-candidate-btn";
    closeButton.addEventListener("click", removeCandidateFromElection);
    let div = document.createElement("div");
    div.className = "one-container";
    div.appendChild(p);
    div.appendChild(spinner);
    div.appendChild(closeButton);

    let candidatesArea = document.getElementById("candidates-container");
    candidatesArea.appendChild(div);

    //document.getElementById("selected-candidates-area").style.display = "block";
}
function removeCandidateFromElection() {
    //this function removes a candidate from the newly created election (from db and from ui)
    let clickedButton = event.target;
    //first of all lets display the spinner and hide the button    
    clickedButton.style.display = "none";
    clickedButton.parentElement.querySelector(".spinner-border").style.display = "block";

    //console.log("I'm going to remove the candiate from the election");
    let removeButton = event.target;

    let voterid = event.target.getAttribute("voterid");

    //Send the JSON data of voterId and electionId to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: "/Election/RemoveCandidate",
        data: JSON.stringify({ electionId: electionId, voterId: voterid }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            //which I sent after creating an Error HttpContext.Response.StatusCode = 500 ...see the Catch block of code in the backend
            //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

            //so there is a server error, lets display the error msg
            clickedButton.parentElement.querySelector(".spinner-border").style.display = "none";
            clickedButton.style.display = "block";
            document.getElementById("redModal").querySelector("h4").innerText = resources[currentUserLanguage]["Error"]+"!";
            document.getElementById("redModal").querySelector("p").innerText = response.responseJSON.message;
            $('#redModal').modal('show');
        },
        success: function (response) {
            //console.log(response);
            //alert("success" + response);

            //now lets remove the concerned candidate from the Candidates area
            console.log("i just removed the candidate from DB:");
            console.log(removeButton);
            removeAddedCandidateFromUI(removeButton);
            //Now lets refresh jquery datatables.. this is speial to iquery datatables
            $("#voters-table").DataTable().ajax.reload();            
        }
    });
}
function removeAddedCandidateFromUI(element) {
    console.log("I'm going to remove candidate from UI:")
    console.log(element);
    //this function removes the candidate conntainer which the user has just removed from the election
    let candidateContainer = element.parentElement; //event.target.parentElement();
    document.getElementById("candidates-container").removeChild(candidateContainer);
}