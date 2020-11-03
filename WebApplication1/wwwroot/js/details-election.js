//WE SHOULD LOAD THE LIST OF CANIDIDATES RELATED TO THIS ELECTION WHEN THE DOCUMENT IS READY

//window.onload = loadCandidatesList();
document.addEventListener("DOMContentLoaded", loadCandidatesList);

function loadCandidatesList() {
    //get the list of candidates using the id of the election
    $.ajax({
        type: "POST",
        url: "/Election/GetCandidatesList_byElectionId_ExcepNeutralOpinion",
        data: JSON.stringify(document.getElementById("candidate-id-holder").value),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

            //so there is a server error, lets display the error msg

            document.getElementById("redModal").querySelector("h4").innerText = resources[currentUserLanguage]["Error"] + "!";
            document.getElementById("redModal").querySelector("p").innerText = response.responseJSON.message;
            $('#redModal').modal('show');
            //now lets hide the spinner
            hideElement(document.getElementById("candidates-table-spinner"));
        },
        success: function (response) {
            //'response' represents the object returned from the api which is the Election object newly stored in the db
            console.log(response);

            displayCandidates(response);
        }
    });
}
function displayElement(elt) {
    //this function displays an element
    elt.style.display = "block";
}
function hideElement(elt) {
    //this function hides an element
    elt.style.display = "none";
}

function displayCandidates(candidatesList) {
    var tableBody = document.getElementsByTagName("tbody")[0];
    for (let i = 0; i < candidatesList.length; i++) {
        let tdFirstName = document.createElement("td");
        tdFirstName.innerText = candidatesList[i].FirstName;
        let tdLastName = document.createElement("td");
        tdLastName.innerText = candidatesList[i].LastName;
        let tdState = document.createElement("td");
        tdState.innerText = candidatesList[i].StateName;

        let candidateRow = document.createElement("tr");
        candidateRow.appendChild(tdFirstName);
        candidateRow.appendChild(tdLastName);
        candidateRow.appendChild(tdState);

        tableBody.appendChild(candidateRow);
    }
    document.getElementById("candidates-table").style.display = "block";
    document.getElementById("candidates-table-spinner").style.display = "none";
}