//adding onChange event to input so that in case user started typing we'll display the button 'Save' to him
let inputs = document.getElementsByClassName("changeable");
for (let i = 0; i < inputs.length; i++) {
    inputs[i].addEventListener("change", displayButton);
}
function displayButton() {
    //console.log("going to display button");
    document.getElementById("submit-updated-election").style.display = "block";
    //console.log("displayed the button");
}
function hideButton() {
    document.getElementById("submit-updated-election").style.display = "none";
}

//this is updating election part
document.getElementById("submit-updated-election").addEventListener("click", sendUpdatedElection);
function sendUpdatedElection() {
    let electionId = document.getElementById("election-holder-id").value;
    let electionName = document.getElementById("election-name").value;
    //console.log(electionName);
    let electionStartDate = document.getElementById("start-date-election").value;
    let electionDuration = document.getElementById("duration-in-days").value;
    let electionHasNeutral = document.getElementById("has-neutral").value;

    hideButton();
    displaySpinner();

    //Send the JSON data of election instance to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: "/Election/EditElection",
        data: JSON.stringify({ Id: electionId, Name: electionName, StartDate: electionStartDate, DurationInDays: electionDuration, HasNeutral: electionHasNeutral }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            //alert("error");
        },
        success: function (response) {
            //console.log(response);
            //alert("success" + response);                
            hideSpinner();
        }
    });
}

function hideSpinner() {
    document.getElementById("update-candidate-spinner").style.display = "none";
}
function displaySpinner() {
    document.getElementById("update-candidate-spinner").style.display = "block";
}




//WE SHOULD LOAD THE LIST OF CANIDIDATES RELATED TO THIS ELECTION WHEN THE DOCUMENT IS READY

//window.onload = loadCandidatesList();
document.addEventListener("DOMContentLoaded", loadCandidatesList);

function loadCandidatesList() {
    let electionId = document.getElementById("election-holder-id").value;
    //console.log("trying to load data of " + JSON.stringify(electionId));
    //get the list of candidates using the id of the election
    $.ajax({
        type: "POST",
        url: "/Election/GetCandidatesList_andVotersList_byElectionId",
        data: JSON.stringify(electionId),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            alert("error");
        },
        success: function (response) {
            //'response' represents the object returned from the api
            //console.log(response);       

            //displayVoters(response);
            document.getElementById("candidates-spinner").style.display = "block";
            loadCandidates(electionId);
            prepareVotersjQueryDatatable(electionId);
            //window.location.href = "Home/Index";
        }
    });
    //console.log("loaded data");
}


function displayVoters(voters) {
    //console.log(voters);
    var tableBody = document.getElementsByTagName("tbody")[0];
    for (let i = 0; i < voters.length; i++) {
        let tdFirstName = document.createElement("td");
        tdFirstName.innerText = voters[i].FirstName;
        let tdLastName = document.createElement("td");
        tdLastName.innerText = voters[i].LastName;
        let tdState = document.createElement("td");
        tdState.innerText = voters[i].StateName;

        let tdButton = document.createElement("td");


        //in case it is a candidate, lets show the button 'Remove Candidate'
        let removeCandidateButton = document.createElement("a");
        removeCandidateButton.innerHTML = "Remove Candidate";
        removeCandidateButton.style.cursor = "pointer";
        removeCandidateButton.style.color = "red";
        removeCandidateButton.setAttribute("candidateid", voters[i].CandidateId);
        removeCandidateButton.setAttribute("voterid", voters[i].VoterId);
        removeCandidateButton.addEventListener("click", removeCandidate);
        tdButton.appendChild(removeCandidateButton);


        //in case it is not a candidate, lets show the button 'Set as a Candidate'                
        let addCandidateButton = document.createElement("a");
        addCandidateButton.innerHTML = "Select as Candidate";
        addCandidateButton.style.cursor = "pointer";
        addCandidateButton.setAttribute("voterid", voters[i].VoterId);
        addCandidateButton.addEventListener("click", sendCandidate);
        tdButton.appendChild(addCandidateButton);

        //add a spinner to be shown when using one of these buttons
        let spinner = document.createElement("span");
        spinner.style.display = "none";
        spinner.className = "spinner-border";
        tdButton.appendChild(spinner);



        if (voters[i].CandidateId != null) {
            //so it is not a candidate, lets display only 'Set as a Candidate'
            removeCandidateButton.style.display = "block";
            addCandidateButton.style.display = "none";
        }
        else {
            //so it is a candidate, lets display only 'Remove Candidate'
            removeCandidateButton.style.display = "none";
            addCandidateButton.style.display = "block";
        }


        let voterRow = document.createElement("tr");
        voterRow.appendChild(tdFirstName);
        voterRow.appendChild(tdLastName);
        voterRow.appendChild(tdState);
        voterRow.appendChild(tdButton);

        tableBody.appendChild(voterRow);
    }
    document.getElementById("step-two").style.display = "block";
}
function sendCandidate(event) {
    //hide this clicked button
    event.target.style.display = "none";
    displayBrotherSpinner(event.target);

    //console.log(event.target);
    let voterid = event.target.getAttribute("voterid");
    let electionId = document.getElementById("election-holder-id").value;
    //console.log(JSON.stringify({ electionId: electionId, voterId: voterid }));
    //Send the JSON data of voterId and electionId to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: "/Election/AddCandidates",
        data: JSON.stringify({ electionId: electionId, voterId: voterid }),//JSON.stringify(newElection),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            //alert("error");

        },
        success: function (response) {
            //'response' represents the object returned from the api which is the Election object newly stored in the db
            //console.log(response);
            //alert("success" + response);
            switchCandidateButton(event.target);

            //window.location.href = "Home/Index";
        }
    });
}
function displayBrotherSpinner(btn) {
    let spinner = btn.parentElement.querySelectorAll(".spinner-border")[0];
    spinner.style.display = "block";
}
function hideBrotherSpinner(btn) {
    let spinner = btn.parentElement.querySelectorAll(".spinner-border")[0];
    spinner.style.display = "none";
}
function switchCandidateButton(element) {
    //first of all lets hide the spinner
    hideBrotherSpinner(element);

    element.style.display = "none";

    let buttons = element.parentElement.querySelectorAll("a");
    for (let i = 0; i < buttons.length; i++) {
        if (buttons[i] != element) {
            //so lets display the other button
            buttons[i].style.display = "block";
        }
        /*if (window.getComputedStyle(buttons[i]).display == "none") {
            //console.log("it was hidden");
            buttons[i].style.display = "block";
        }
        else {
            //console.log("it was visible");
            buttons[i].style.display = "none";
        }*/
    }

}

function removeCandidate(event) {
    //hide this clicked button
    event.target.style.display = "none";
    displayBrotherSpinner(event.target);

    //this function remove the relation between election and candidates
    let voterId = event.target.getAttribute("voterid");
    //console.log(voterId);
    let electionId = document.getElementById("election-holder-id").value;
    //console.log(electionId);
    //Send the JSON data of voterId and electionId to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: "/Election/RemoveCandidate",
        data: JSON.stringify({ electionId: electionId, voterId: voterId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            //alert("error");
        },
        success: function (response) {
            //console.log(response);
            //alert("success" + response);
            switchCandidateButton(event.target);

            //window.location.href = "Home/Index";
        }
    });
}




//********************************************* AFTER USING JQUERY DATATABLES *********************/
//We'll load list of candidates related to this electino in candidates area, and load other voters who are not candidates in the datatable


function prepareVotersjQueryDatatable(electionId) {
    //console.log("-" + electionId + "-");
    document.getElementById("voters-table").style.display = "block";

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
            },
            "columnDefs": [
                { "type": "numeric-comma", targets: "_all" }
            ],
            "columns":
                [//These are the columns to be displayed, and they are the fields of the voters objects brought from the server
                    { "data": "Id", "visible": false, "searchable": false },
                    { "data": "FirstName", "title": "FirstName", "name": "FirstName", "visible": true, "searchable": true, "sortable": false },
                    { "data": "LastName", "title": "Last Name", "name": "LastName", "visible": true, "searchable": true, "sortable": false },
                    { "data": "State.Name", "title": "State", "visible": true, "searchable": true, "sortable": false },
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

function loadCandidates(electionId) {
    //get the list of candidates using the id of the election except the neutral opinion
    $.ajax({
        type: "POST",
        url: "/Election/GetCandidatesList_byElectionId_ExcepNeutralOpinion",
        data: JSON.stringify(electionId),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            alert("error");
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
        p.innerText = "No Candidates Selected";
        div.appendChild(p);
        document.getElementById("candidates-container").appendChild(div);
    }
    for (let i = 0; i < response.length; i++) {

        let p = document.createElement("p");
        p.innerText = response[i].FirstName + " " + response[i].LastName;
        let spinner = document.createElement("div");
        spinner.className = "spinner-border text-danger hidden-spinner centered-spinner";
        let closeButton = document.createElement("a");
        closeButton.innerText = "Remove";
        closeButton.setAttribute("candidateid", response[i].Id);
        closeButton.setAttribute("title", "Remove Candidate");
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
    //this function removes a canidate from db, then from the ui
    let candidateId = event.target.getAttribute("candidateid");
    $.ajax({
        type: "POST",
        url: "/Election/RemoveCandidate_byID",
        data: JSON.stringify(candidateId),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            alert("error");
        },
        success: function (response) {
            //'response' represents the object returned from the api
            console.log("candidate removed");
        }
    });
}