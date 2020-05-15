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

    //console.log("trying to load data of " + JSON.stringify(electionId));
    //get the list of candidates using the id of the election
    $.ajax({
        type: "POST",
        url: "/Election/GetCandidatesList_andVotersList_byElectionId",
        data: JSON.stringify(document.getElementById("election-holder-id").value),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            alert("error");
        },
        success: function (response) {
            //'response' represents the object returned from the api
            //console.log(response);       

            //lets hide the spinner
            document.getElementById("candidate-list-spinner").style.display = "none";
            displayVoters(response);

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