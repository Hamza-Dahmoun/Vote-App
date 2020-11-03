//adding onChange event to input so that in case user started typing we'll display the button 'Save' to him
let inputs = document.getElementsByClassName("changeable");
for (let i = 0; i < inputs.length; i++) {
    inputs[i].addEventListener("keyup", displayButton);
    inputs[i].addEventListener("change", displayButton);//becuz the above doesn't work properly for input numbers field
}
function displayButton() {
    document.getElementById("submit-updated-election").style.display = "block";
    //lets hide the response msg in case it is the second update
    document.getElementById("response-msg").style.display = "none";
}
function hideButton() {
    document.getElementById("submit-updated-election").style.display = "none";
}

//this is updating election part
document.getElementById("submit-updated-election").addEventListener("click", sendUpdatedElection);
function sendUpdatedElection() {
/*
HTML5 form validation won't work in our case because we are submitting data to the server using ajax, the browser displays the validation msgs but the ajax call is made anyway
in our case becuz it supposed that since we're using js to call ajax then we are in charge of the form validation. What we need is: in js code before the ajax call we need 
to check if the form is valid using form.checkValidity() js function, if true we'll call server using ajax,
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

        let electionId = document.getElementById("election-holder-id").value;
        let electionName = document.getElementById("election-name").value;

        let electionStartDate = document.getElementById("start-date-election").value;
        let electionDuration = document.getElementById("duration-in-days").value;
        let electionHasNeutral = getCheckBoxValue("has-neutral").toString();
        console.log(electionHasNeutral);
        hideButton();
        displaySpinner();

        //Send the JSON data of election instance to Controller using AJAX.
        $.ajax({
            type: "POST",
            url: "/Election/EditElection",
            data: JSON.stringify({ Id: electionId, Name: electionName, StartDate: electionStartDate, DurationInDays: electionDuration, HasNeutral: electionHasNeutral }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (response) {
                //in here 'response' represents the following object {success: false, message ='...text here...'}
                //which I sent after creating an Error HttpContext.Response.StatusCode = 500 ...see the Catch block of code in the backend
                //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

                //so there is a server error, lets display the error msg

                document.getElementById("redModal").querySelector("h4").innerText = resources[currentUserLanguage]["Error!"];
                document.getElementById("redModal").querySelector("p").innerText = response.responseJSON.message;
                $('#redModal').modal('show');

                hideSpinner();
            },
            success: function (response) {
                console.log("success");               
                hideSpinner();
                displayResponseMsg(true);
            }
        });
    }
    else {
        //do nothing becauz the form is not valid and the browser will display the validation msgs to the user
    }    
}

function hideSpinner() {
    document.getElementById("update-candidate-spinner").style.display = "none";
}
function displaySpinner() {
    document.getElementById("update-candidate-spinner").style.display = "block";
}
function displayResponseMsg(success) {
    let responseMsg = document.getElementById("response-msg");
    let p = responseMsg.querySelector("p");
    if (success) {
        responseMsg.className = "alert alert-success";        
        p.innerHTML = "<strong>" + resources[currentUserLanguage]["Success"] + "!</strong>" + " " + resources[currentUserLanguage]["The updates have been done successfully"];
    }
    else {
        responseMsg.className = "alert alert-danger";        
        p.innerHTML = "<strong>" + resources[currentUserLanguage]["Error!"] + "</strong>" + " " + resources[currentUserLanguage]["Something went wrong, please try again!"];
    }
    responseMsg.style.display = "block";
}


function getCheckBoxValue(id) {
    let checkBox = document.getElementById(id);
    if (checkBox.checked) {
        return true;
    }
    else {
        return false;
    }
}



//********************************************* AFTER USING JQUERY DATATABLES *********************/
//We'll load list of candidates related to this electino in candidates area, and load other voters who are not candidates in the datatable

document.addEventListener("DOMContentLoaded", function () {
    let electionId = document.getElementById("election-holder-id").value;
    loadCandidates(electionId);
    prepareVotersjQueryDatatable(electionId);
});


function prepareVotersjQueryDatatable(electionId) {
    //console.log("-" + electionId + "-");
    //I USED TO LOAD THE TABLE HIDDEN THEN IN JS CODE I DISPLAY IT, BUT THIS MISBEHAVED AND THE TABLE DIDNT HAVE A PROPER WIDTH


    //this function send a request to the server to get the list of voters not candidates to a fiven election
    $("#voters-table").DataTable(
        {
            "processing": true,//whether to show 'processing' indicator when waiting for a processing result or not
            "serverSide": true,//for server side processing
            "filter": true,//this is for disable filter (search box)
            "ajax": {
                "url": '/Election/VotersDataTable/',
                "type": 'POST',
                "data": function (d) {
                    d.electionId = electionId;
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
                    errorParag.innerHTML = "<strong>Error when retrieving Candidates!</strong> " + reason.responseJSON.message;
                    responseMsg.appendChild(errorParag);
                    document.getElementById("step-two").appendChild(responseMsg);
                    //now lets hide the datatable wrapper (a div created by jquery which surrounds the table)
                    document.getElementById("step-two").querySelector(".dataTables_wrapper").style.display = "none";
                    
                }
            },
            "columnDefs": [
                { "type": "numeric-comma", targets: "_all" }
            ],
            "language": getTranslatedDataTable(),
            "columns":
                [//These are the columns to be displayed, and they are the fields of the voters objects brought from the server
                    { "data": "Id", "visible": false, "searchable": false },
                    { "data": "FirstName", "title": resources[currentUserLanguage]["FirstName"], "name": "FirstName", "visible": true, "searchable": true, "sortable": false },
                    { "data": "LastName", "title": resources[currentUserLanguage]["LastName"], "name": "LastName", "visible": true, "searchable": true, "sortable": false },
                    { "data": "StateName", "title": resources[currentUserLanguage]["State"], "visible": true, "searchable": true, "sortable": false },
                    {
                        "data": null, "searchable": false, "sortable": false,
                        "render": function (data, type, row, meta) {
                            var button =
                                "<a class='select-candidate-btn' title="+resources[currentUserLanguage]['Select this Voter as a Candidate']+" voterid=" +
                                row.Id + " voterfullname='" + row.FirstName + " " + row.LastName
                                + "' onclick='selectNewCandidate()'>" + resources[currentUserLanguage]["Select as Candidate"]+"</a>"
                                + "<div class='spinner-border text-success hidden-spinner'></div>"
                                ;


                            return button;
                        }
                    }
                ]
        }
    );

}

function loadCandidates(electionId) {
    //get the list of candidates using the id of the election except the neutral opinion

    //lets first display the spinner 
    document.getElementById("candidates-spinner").style.display = "block";
    $.ajax({
        type: "POST",
        url: "/Election/GetCandidatesList_byElectionId_ExcepNeutralOpinion",
        data: JSON.stringify(electionId),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            //which I sent after creating an Error HttpContext.Response.StatusCode = 500 ...see the Catch block of code in the backend
            //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

            //so there is a server error, lets display the error msg
            document.getElementById("candidates-spinner").style.display = "none";
            document.getElementById("redModal").querySelector("h4").innerText = resources[currentUserLanguage]["Error!"];
            document.getElementById("redModal").querySelector("p").innerText = response.responseJSON.message;
            $('#redModal').modal('show');
        },
        success: function (response) {
            //'response' represents the object returned from the api
            document.getElementById("candidates-spinner").style.display = "none";
            console.log(response);
            displayCandidates(response);
        }
    });
}
function displayCandidates(response) {
    //display the list of candidates of this election .. this function is used in the first page load
    if (response.length == 0 || response == null) {
        //lets display a container stating "No candidates selected"
        let div = document.createElement("div");
        div.className = "one-container transparent-candidate";
        let p = document.createElement("p");
        p.innerText = resources[currentUserLanguage]["No Candidates Selected"];
        div.appendChild(p);
        document.getElementById("candidates-container").appendChild(div);
    }
    for (let i = 0; i < response.length; i++) {

        let p = document.createElement("p");
        p.innerText = response[i].FirstName + " " + response[i].LastName;
        let spinner = document.createElement("div");
        spinner.className = "spinner-border text-danger hidden-spinner centered-spinner";
        let closeButton = document.createElement("a");
        closeButton.innerText = resources[currentUserLanguage]["Remove"];
        closeButton.setAttribute("candidateid", response[i].Id);
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

    }
}
function removeCandidateFromElection() {
    //this function removes a canidate from db and from the ui ... then it reloads the jquery datatable of voters
    let removeButton = event.target;
    removeButton.style.display = "none";

    //lets first display the spinner
    let spinner = removeButton.parentElement.querySelector(".spinner-border");
    spinner.style.display = "block";

    let candidateId = removeButton.getAttribute("candidateid");
    $.ajax({
        type: "POST",
        url: "/Election/RemoveCandidate_byID",
        data: JSON.stringify(candidateId),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            //which I sent after creating an Error HttpContext.Response.StatusCode = 500 ...see the Catch block of code in the backend
            //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

            //so there is a server error, lets display the error msg
            removeButton.parentElement.querySelector(".spinner-border").style.display = "none";
            removeButton.style.display = "block";
            document.getElementById("redModal").querySelector("h4").innerText = resources[currentUserLanguage]["Error!"];
            document.getElementById("redModal").querySelector("p").innerText = response.responseJSON.message;
            $('#redModal').modal('show');

        },
        success: function (response) {
            //'response' represents the object returned from the api

            //now lets remove the candidate container from DOM slowly
            let candidateContainer = removeButton.parentElement;
            let allCandidatesContainer = document.getElementById("candidates-container");
            hide_andDeleteElt(allCandidatesContainer, candidateContainer);
            //now lets reload voters datatable who aren't candidates for this election .. this code is speial to jquery datatables
            $("#voters-table").DataTable().ajax.reload();     
        }
    });
}
function hide_andDeleteElt(parentElt, childElt) {
    console.log("going to animate and remove elt");
    //this function add to an element a class to make it fade, and wait for a period of time equal to the animation-duration,
    //then remove elt from DOM
    childElt.classList.add("hiding-container");

    //now lets wait for the animation of hiding the container to complete, then remove the elt from dom
    setTimeout(function () {
        parentElt.removeChild(childElt);
    }, 500);
    console.log("elt animated and removed");
}

function selectNewCandidate() {
    //this function will add a new candidate to the db and UI and then reload voters datatable

    let electionId = document.getElementById("election-holder-id").value;
    let selectCandidateButton = event.target;
    let voterId = selectCandidateButton.getAttribute("voterid");
    let voterFullName = selectCandidateButton.getAttribute("voterfullname");

    //lets display the spinner
    selectCandidateButton.parentElement.querySelector(".spinner-border").style.display = "block";
    selectCandidateButton.style.display = "none";

    //Send the JSON data of voterId and electionId to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: "/Election/AddCandidate",
        data: JSON.stringify({ electionId: electionId, voterId: voterId }),//JSON.stringify(newElection),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            //which I sent after creating an Error HttpContext.Response.StatusCode = 500 ...see the Catch block of code in the backend
            //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

            //so there is a server error, lets display the error msg
            selectCandidateButton.parentElement.querySelector(".spinner-border").style.display = "none";
            selectCandidateButton.style.display = "block";
            document.getElementById("redModal").querySelector("h4").innerText = resources[currentUserLanguage]["Error!"];
            document.getElementById("redModal").querySelector("p").innerText = response.responseJSON.message;
            $('#redModal').modal('show');

        },
        success: function (response) {
            //In here the response is the new candidate id
            console.log(response);

            //now lets hide and delete 'no selected candidate' container from the candidates area
            if (document.getElementsByClassName("transparent-candidate").length > 0) {
                //so beore this new candidate, there were none ... lets hide and delete the information 'no candidate' elt
                hide_andDeleteElt(document.getElementById("candidates-container"), document.getElementsByClassName("transparent-candidate")[0]);
            }

            //now lets display a candidate container for this new candidate in the canidates-area            
            let candidateId = response.candidateId;
            displayNewCandidate(voterFullName, candidateId);

            //now lets reload voters datatable who aren't candidates for this election .. this code is speial to jquery datatables
            $("#voters-table").DataTable().ajax.reload();   
        }
    });
}

function displayNewCandidate(candidateFullName, candidateId) {
    let p = document.createElement("p");
    p.innerText = candidateFullName;
    let spinner = document.createElement("div");
    spinner.className = "spinner-border text-danger hidden-spinner centered-spinner";
    let closeButton = document.createElement("a");
    closeButton.innerText = resources[currentUserLanguage]["Remove"];
    closeButton.setAttribute("candidateid", candidateId);
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
}