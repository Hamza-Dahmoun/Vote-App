//Start loading things once the page is ready
document.addEventListener("DOMContentLoaded", loadElectionsData);



function loadElectionsData() {
    //1- Load Coming Elections, hide table and display spinner
    let comingElectionsArea = document.getElementById("coming-elections-area");
    displayElement(comingElectionsArea.querySelector(".spinner-border"));
    hideElement(comingElectionsArea.querySelector(".table"));
    loadComingElections();


    //2- Load Previous Elections
    let previousElectionsArea = document.getElementById("previous-elections-area");
    displayElement(previousElectionsArea.querySelector(".spinner-border"));
    hideElement(previousElectionsArea.querySelector(".table"));
    loadPreviousElections();


    //3- Load Current Election
    let currentElectionArea = document.getElementById("current-election-area");
    displayElement(currentElectionArea.querySelector(".spinner-border"));
    loadCurrentElection();
}




function loadComingElections() {
    //this function load a list of coming elections using jQuery ajax

    $.ajax({
        type: "POST",
        url: "/Election/GetComingElections",
        /*data: JSON.stringify(document.getElementById("candidate-id-holder").value),*/
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            alert("error");
        },
        success: function (response) {
            //'response' represents the object returned from the api which is a list of future elections
            console.log(response);
            //console.log(response.length);
            displayComingElections(response);

            //window.location.href = "Home/Index";
        }
    });

}
function displayComingElections(comingElections) {
    let comingElectionsArea = document.getElementById("coming-elections-area");
    let table = comingElectionsArea.querySelector(".table");

    let tableBody = table.querySelector("tbody");

    for (let i = 0; i < comingElections.length; i++) {

        let tdName = document.createElement("td");
        tdName.innerText = comingElections[i].Name;

        let tdStartDate = document.createElement("td");
        tdStartDate.innerText = comingElections[i].StartDate;

        let tdDurationInDays = document.createElement("td");
        tdDurationInDays.innerText = comingElections[i].DurationInDays;

        let tdCandidatesCount = document.createElement("td");
        tdCandidatesCount.innerText = comingElections[i].Count;

        let electionRow = document.createElement("tr");
        electionRow.appendChild(tdName);
        electionRow.appendChild(tdStartDate);
        electionRow.appendChild(tdDurationInDays);
        electionRow.appendChild(tdCandidatesCount);

        tableBody.appendChild(electionRow);
    }


    //now lets display the tabl and hide the spinner
    hideElement(comingElectionsArea.querySelector(".spinner-border"));
    displayElement(table);
    //now lets make it a jquery datatables
    $('#coming-elections-table').DataTable();
}



function loadCurrentElection() {
    //this function load the current election using jQuery ajax

    $.ajax({
        type: "POST",
        url: "/Election/GetCurrentElection",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            alert("error");
        },
        success: function (response) {
            //'response' represents the object returned from the api which is the current election
            console.log(response);
            //console.log(response.length);
            displayCurrentElection(response);

            //window.location.href = "Home/Index";
        }
    });
}
function displayCurrentElection(currentElection) {
    //console.log(currentElection);

    let currentElectionArea = document.getElementById("current-election-area");
    hideElement(currentElectionArea.querySelector(".spinner-border"));

    if (currentElection == null) {
        //so there is no current election, lets just display a simple text
        let p = document.createElement("p");
        p.innerText = "There is no Election currently!";
        currentElectionArea.appendChild(p);
    }
    else {
        //so there is an election currently, lets display its info with a 'Vote' button
        let name = document.createElement("p");
        name.innerText = "Name: ";
        let strongName = document.createElement("strong");
        strongName.innerText = currentElection.Name + " | " + currentElection.CandidatesCount + " Candidates";
        name.appendChild(strongName);

        let startdate = document.createElement("p");
        startdate.innerText = "Date: ";
        let strongDate = document.createElement("strong");
        strongDate.innerText = currentElection.StartDate;
        startdate.appendChild(strongDate);

        let durationindays = document.createElement("p");
        durationindays.innerText = "Duration (days): ";
        let strongDuration = document.createElement("strong");
        strongDuration.innerText = currentElection.DurationInDays;
        durationindays.appendChild(strongDuration);

        let participationRate = document.createElement("p");
        participationRate.innerText = "Participation Rate: ";
        let strongParticipationRate = document.createElement("strong");
        strongParticipationRate.innerText = Math.floor(parseFloat(currentElection.ParticipationRate * 100)) + "%";
        participationRate.appendChild(strongParticipationRate);

        currentElectionArea.appendChild(name);
        currentElectionArea.appendChild(startdate);
        currentElectionArea.appendChild(durationindays);
        currentElectionArea.appendChild(participationRate);


        //lets now display the Vote button only if he hasnt voted yet
        //else, we'll display 'Results' button
        if (!currentElection.HasUserVoted) {
            let voteButton = document.createElement("a");
            voteButton.className = "btn btn-default animated-button";
            voteButton.setAttribute("title", "Go Vote");
            //voteButton.setAttribute("href", "Vote/Index/" + currentElection.Id);
            //currentElectionId in Vote/Index() was always received as empty Guid .. lets just get the current election inside the Index() action
            voteButton.setAttribute("href", "/Vote/Index/");
            let icon = document.createElement("i");
            icon.className = "fa fa-chevron-right";
            icon.innerText = "Vote";
            voteButton.appendChild(icon);
            currentElectionArea.appendChild(voteButton);
        }
        else {
            let resultsButton = document.createElement("a");
            resultsButton.setAttribute("electionId", currentElection.Id);
            resultsButton.style.color = "#3d7e9a";
            resultsButton.style.cursor = "pointer";
            resultsButton.setAttribute("title", "Show Details");
            resultsButton.innerText = "Show Results";
            resultsButton.addEventListener("click", getElectionResults);
            let div = document.createElement("div");
            div.appendChild(resultsButton);
            let spinner = document.createElement("div");
            spinner.className = "spinner-border text-primary";
            spinner.setAttribute("Id", "current-election-results-spinner");
            spinner.style.display = "none";
            div.appendChild(spinner);
            currentElectionArea.appendChild(div);
        }

    }
}





function loadPreviousElections() {
    //this function load a list of previous elections using jQuery ajax

    $.ajax({
        type: "POST",
        url: "/Election/GetPreviousElections",
        /*data: JSON.stringify(document.getElementById("candidate-id-holder").value),*/
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            alert("error");
        },
        success: function (response) {
            //'response' represents the object returned from the api which is a list of previous elections
            console.log(response);
            //console.log(response.length);
            displayPreviousElections(response);

            //window.location.href = "Home/Index";
        }
    });

}
function displayPreviousElections(previousElections) {
    //console.log(previousElections);

    let previousElectionsArea = document.getElementById("previous-elections-area");
    let table = previousElectionsArea.querySelector(".table");

    let tableBody = table.querySelector("tbody");

    for (let i = 0; i < previousElections.length; i++) {

        let tdName = document.createElement("td");
        tdName.innerText = previousElections[i].Name;

        let tdStartDate = document.createElement("td");
        tdStartDate.innerText = previousElections[i].StartDate;

        let tdDurationInDays = document.createElement("td");
        tdDurationInDays.innerText = previousElections[i].DurationInDays;

        let tdCandidatesCount = document.createElement("td");
        tdCandidatesCount.innerText = previousElections[i].CandidatesCount;

        let resultsButton = document.createElement("a");
        resultsButton.setAttribute("electionId", previousElections[i].Id);
        resultsButton.style.color = "#3d7e9a";
        resultsButton.style.cursor = "pointer";
        resultsButton.setAttribute("title", "Show Details");
        resultsButton.innerText = "Show Results";
        resultsButton.addEventListener("click", getElectionResults);
        let tdResultsButton_andSpinner = document.createElement("td");
        tdResultsButton_andSpinner.appendChild(resultsButton);
        let spinner = document.createElement("span");
        spinner.className = "spinner-border text-primary";
        spinner.style.display = "none";
        tdResultsButton_andSpinner.appendChild(spinner);

        let electionRow = document.createElement("tr");
        electionRow.appendChild(tdName);
        electionRow.appendChild(tdStartDate);
        electionRow.appendChild(tdDurationInDays);
        electionRow.appendChild(tdCandidatesCount);
        electionRow.appendChild(tdResultsButton_andSpinner);


        tableBody.appendChild(electionRow);
    }


    //now lets display the tabl and hide the spinner
    hideElement(previousElectionsArea.querySelector(".spinner-border"));
    displayElement(table);
    //now lets make it a jquery datatables
    $('#previous-elections-table').DataTable();
}








function displayElement(elt) {
    //this function displays an element
    elt.style.display = "block";
}
function hideElement(elt) {
    //this function hides an element
    elt.style.display = "none";
}





/*THE BELOW METHODS ARE ALWAYS USED TO GET THE RESULTS OF A GIVEN ELECTION AND WRITHE THEM AND DISPLAY THEM IN A SLIDING DIV
THEY ARE USED WHEN LOADING: 1- CURRENT ELECTION RESULTS 2- PREVIOUS ELECTIONS RESULTS*/
function getElectionResults(event) {
    //console.log(event.target.getAttribute("electionId"));

    //lets hide the button and display the spinner
    //displayElement(document.getElementById("current-election-results-spinner"));
    console.log(event);
    console.log(event.target);
    displayElement(event.target.parentElement.querySelector(".spinner-border"));
    hideElement(event.target);
    //this function load the current election results using jQuery ajax
    $.ajax({
        type: "POST",
        url: "/Home/GetResultsOfElection",
        data: JSON.stringify(event.target.getAttribute("electionId")),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            alert("error");
        },
        success: function (response) {
            //'response' represents the object returned from the api which is the an array of candodates ordered by their number of votes
            console.log(response);
            //lets hide the spinner and display the button
            //hideElement(document.getElementById("current-election-results-spinner"));
            hideElement(event.target.parentElement.querySelector(".spinner-border"));
            displayElement(event.target);
            //lets display the results
            displayElectionResults(response);
        }
    });
}
function displayElectionResults(response) {
    console.log(response);
    //this method takes a candidates list ordered by their number of votes (results of a given election)
    //and displays them in a div that will slide into the screen from the right side        

    let electionResultsContainer = document.getElementById("election-results-container");
    //lets erase previous results elements if there are any
    electionResultsContainer.innerHTML = "";


    if (response == null || response.length == 0) {
        //so the respponse is empty, lets just display a text  to tell user that this election has no candidates
        let para = document.createElement("p");
        para.innerText = "It seems this election had no Candidates!";
        para.style.textAlign = "center";
        electionResultsContainer.appendChild(para);
    }
    else {

        for (let i = 0; i < response.length; i++) {
            let one_result_container = document.createElement("div");
            one_result_container.className = "one-result-container";

            let rank_container = document.createElement("div");
            rank_container.className = "rank-container";

            if (i == 0) {
                //so its the current winner candidate
                let icon = document.createElement("i");
                icon.className = "fa fa-trophy";
                let span = document.createElement("span");
                span.appendChild(icon);
                rank_container.appendChild(span);
            }
            else {
                let span = document.createElement("span");
                span.innerText = i + 1;
                rank_container.appendChild(span);
            }
            one_result_container.appendChild(rank_container);

            let candidate_data_container = document.createElement("div");
            candidate_data_container.className = "candidate-data-container";
            let candidateName = document.createElement("p");
            candidateName.innerText = response[i].FirstName + " " + response[i].LastName;
            candidate_data_container.appendChild(candidateName);
            let votesCount = document.createElement("p");
            votesCount.innerText = response[i].VotesCount;
            candidate_data_container.appendChild(votesCount);
            one_result_container.appendChild(candidate_data_container);


            //let results_container = document.getElementById("results-container");
            electionResultsContainer.appendChild(one_result_container);
        }

    }



    //electionResultsContainer.className = "displayed_results_container";
    document.getElementById("election-results-container-parent").className = "displayed_results_container";


    //now lets add a click event to the document so that it also hide the results ... we'll remove the click event once the results are hidden
    //document.addEventListener("click", hideElectionResultsContainer);
    document.getElementById("close-button-container").querySelector("span").addEventListener("click", hideElectionResultsContainer);

}
function hideElectionResultsContainer() {
    //let electionResultsContainer = document.getElementById("election-results-container");
    //electionResultsContainer.className = "hidden_results_container";
    document.getElementById("election-results-container-parent").className = "hidden_results_container";


    //lets remove the click event off the document
    //document.removeEventListener("click", hideElectionResultsContainer);
}
