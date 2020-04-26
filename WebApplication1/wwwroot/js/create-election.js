/**
 *
 * SCENARIO: A USER ENTERS ALL DATA OF AN ELECION AND SUBMIT. IF IT WAS SUCCESSFULLY INSERTED WE'LL GET A RESPONSE FROM SERVER CONTAINING THE
 * ELECTION ITSELF AND A LIST OF VOTERS.
 * THE LIST OF VOTERS WILL BE DISPLAYED FOR THE USER TO SELECT WHICH ONES ARE THE CANDIDATES.
 * WHEN A USER SELECT A CANDIDATE, WE'LL USE JQUERY TO SEND THE ID OF THE ELECTION ALONG WITH THE ID OF THE VOTER SELECTED AS A CANDIDATE
 * TO THE SERVER
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
        error: function () {
            //alert("error");
            document.getElementById("send-election-spinner").style.display = "none";
            document.getElementById("send-election-button").style.display = "block";
        },
        success: function (response) {
            //'response' represents the object returned from the api which is the Election object newly stored in the db
            console.log(response);
            //alert("success" + response);
            //electionId = response.Election.Id;
            electionId = response.ElectionId;
            disableAllInputs();
            changeBgColor();
            displayVoters(response.Voters);
            scrollDown();
            //window.location.href = "Home/Index";
        }
    });
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
        let addCandidateButton = document.createElement("a");
        addCandidateButton.innerHTML = "Select as Candidate";
        addCandidateButton.style.cursor = "pointer";
        addCandidateButton.setAttribute("voterid", voters[i].Id);
        addCandidateButton.addEventListener("click", sendCandidate);
        tdButton.appendChild(addCandidateButton);

        let removeCandidateButton = document.createElement("a");
        removeCandidateButton.innerHTML = "Remove Candidate";
        removeCandidateButton.style.cursor = "pointer";
        removeCandidateButton.style.color = "red";
        removeCandidateButton.style.display = "none";
        removeCandidateButton.setAttribute("voterid", voters[i].Id);
        removeCandidateButton.addEventListener("click", removeCandidate);
        tdButton.appendChild(removeCandidateButton);

        //now lets add a spinner to be shown when one of these buttons is clicked
        let spinner = document.createElement("span");
        spinner.className = "spinner-border";
        spinner.style.display = "none";
        tdButton.appendChild(spinner);

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
    //first of all lets display the spinner and hide the button
    displayBrotherSpinner(event.target);
    event.target.style.display = "none";

    let voterid = event.target.getAttribute("voterid");

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
    }
}

function removeCandidate(event) {
    //this function remove the relation between election and candidates

    //first of all lets display the spinner and hide the button
    displayBrotherSpinner(event.target);
    event.target.style.display = "none";

    let voterid = event.target.getAttribute("voterid");

    //Send the JSON data of voterId and electionId to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: "/Election/RemoveCandidate",
        data: JSON.stringify({ electionId: electionId, voterId: voterid }),
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
