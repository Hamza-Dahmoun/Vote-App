//WE SHOULD LOAD THE LIST OF CANIDIDATES RELATED TO THIS ELECTION WHEN THE DOCUMENT IS READY

//window.onload = loadCandidatesList();
document.addEventListener("DOMContentLoaded", loadCandidatesList);

function loadCandidatesList() {

    //console.log("trying to load data of " + JSON.stringify(electionId));
    //get the list of candidates using the id of the election
    $.ajax({
        type: "POST",
        url: "/Election/GetCandidatesList_byElectionId",
        data: JSON.stringify(document.getElementById("candidate-id-holder").value),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            alert("error");
        },
        success: function (response) {
            //'response' represents the object returned from the api which is the Election object newly stored in the db
            console.log(response);
            //console.log(response.length);
            displayCandidates(response);

            //window.location.href = "Home/Index";
        }
    });
    //console.log("loaded data");
}
function displayCandidates(candidatesList) {
    //console.log(candidatesList.length);
    //alert("I am displaying list of candidates");
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