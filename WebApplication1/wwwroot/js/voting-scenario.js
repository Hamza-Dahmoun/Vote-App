//add click event listener to candidate-container divs
var maxSelection = 5;
var selectedIdArray = new Array();
var neutralCandidate = document.getElementById("neutral-candidate");
var neutralCandidateId = "";
if (neutralCandidate != undefined && neutralCandidate != null) {
    neutralCandidateId = neutralCandidate.querySelector(".hidden-candidateId").textContent /*"d3e32681-27af-4758-8bb7-5558bd2c7c55"*/;
}
    
//alert(neutralCandidateId);
var candidates = document.getElementsByClassName("candidate-container");
for (let i = 0; i < candidates.length; i++) {
    candidates[i].addEventListener("click", clickCandidate);
}
//add click event to vote-button
document.getElementById("vote-button").addEventListener("click", sendCandidates);
function clickCandidate() {
    let newSelected;
    let selectedCandidateId;
    if (event.target.tagName == "DIV") {
        //alert("div is clicked");
        newSelected = event.target;
    }
    else {
        //alert("chlild is clicked");
        newSelected = event.target.parentNode;
    }
    selectedCandidateId = newSelected.querySelectorAll(".hidden-candidateId")[0].textContent;
    //alert("its id is: " + selectedCandidateId.textContent);

    if (isAlreadySelected(newSelected)) {
        //alert("it is already selected");
        removeSelection(newSelected, selectedCandidateId)
    }
    else {
        //alert("it is not already selected");
        //there are three cases:
        //1 - user is selecting "Neutral" we'll unselect all candidates and empty the the array
        //2 - user is selecting a new candidate within the five, so we'll add the css class and the id to the array
        //3- or he'is selecting more than 5, do nothing
        if (document.querySelectorAll(".selected-candidate").length < maxSelection) {
            if (selectedCandidateId == neutralCandidateId) {
                //1- user chose to be neutral, lets remove all previous selections and empty the array
                removeAllSelections_selectNeutral(newSelected);
            }
            else {
                //2- user selected another candidate
                addNewSelection(newSelected, selectedCandidateId)
            }
        }
        else {
            //3- do nothing
        }

    }

    //now update data-attribute
    document.getElementById("vote-button").setAttribute("data-candidateId", selectedIdArray);
    //now display/hide the vote button
    displayHideVoteButton();
}
function isAlreadySelected(candidateContainer) {
    if (candidateContainer.className.includes("selected-candidate"))
        return true;
    else return false;
}
function removeSelection(candidateContainer, selectedCandidateId) {
    candidateContainer.className = "candidate-container";
    //now lets remove its id from the array
    const index = selectedIdArray.indexOf(selectedCandidateId);
    if (index > -1) {
        selectedIdArray.splice(index, 1);
    }
    //now lets hide the check icon
    hideCheckIcon(candidateContainer);
}

function addNewSelection(newSelection, selectedCandidateId) {
    //this function will add selection of a candidate
    //but before, it checks if "Neutral Opinion" is selected and tries to unselect it
    //console.log(unselectNeutral);
    unselectNeutral();

    newSelection.classList.add("selected-candidate");
    selectedIdArray.push(selectedCandidateId);
    //now lets hide the check icon
    displayCheckIcon(newSelection);
}

function displayHideVoteButton() {
    if (selectedIdArray.length > 0) {
        document.getElementById("vote-button").style.display = "block";
    }
    else {
        document.getElementById("vote-button").style.display = "none";
    }
}

function displayCheckIcon(selectedCandidate) {
    selectedCandidate.querySelector(".fa-check-circle-o").style.display = "block";
}

function hideCheckIcon(selectedCandidate) {
    selectedCandidate.querySelector(".fa-check-circle-o").style.display = "none";
}


function sendCandidates() {
    //for (let i = 0; i < selectedIdArray.length; i++) {
    //    alert(selectedIdArray[i] + "  --  " + JSON.stringify(selectedIdArray),);
    //}

    //lets hide the voting title
    hideElement(document.getElementById("voting-title"));
    //lets hide the list of candidates
    hideElement(document.getElementsByClassName("voting-container")[0]);
    //lets display the spinner
    displayElement(document.getElementById("voting-spinner"));
    //Send the JSON array to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: "/Vote/ValidateVote",
        data: JSON.stringify(selectedIdArray),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            console.log("error");
        },
        success: function (response) {
            console.log(response);
            //alert("success");
            //window.location.href = "/Home/Index";
            displayCurrentResults(response);
        }
    });
}

function removeAllSelections_selectNeutral(neutralArea) {
    //this function is called when the user chose to be neutral
    let selectedSet = document.querySelectorAll(".selected-candidate");
    for (let i = 0; i < selectedSet.length; i++) {
        selectedSet[i].className = "candidate-container";
        hideCheckIcon(selectedSet[i]);
    }
    neutralArea.classList.add("selected-candidate");
    displayCheckIcon(neutralArea);
    selectedIdArray = [];
    selectedIdArray.push(neutralCandidateId);
}
function unselectNeutral() {
    let neutralArea = document.querySelector("#neutral-candidate");
    if (neutralArea != undefined && neutralArea!= null) {
        if (neutralArea.className.includes("selected-candidate")) {
            neutralArea.className = "candidate-container";
            neutralArea.querySelector(".fa-check-circle-o").style.display = "none";
            selectedIdArray.pop();
        }
        console.log(selectedIdArray);
    }    
}



function hideElement(elt) {
//this function will hide an element
    elt.style.display = "none";
}
function displayElement(elt) {
    //this function will display an element
    elt.style.display = "block";
}




function displayCurrentResults(response) {
    //the response is a list of candidates viewModel ordered by votes count

    for (let i = 0; i < response.length; i++) {
        let one_result_container = document.createElement("div");
        one_result_container.className = "one-result-container";

        let rank_container = document.createElement("rank-container");
        rank_container.className = "rank-container";

        if (i == 0) {
            //so its the current winner candidate
            let icon = document.createElement("i");
            icon.className = "fa fa-trophy";
            let span = document.createElement("span");
            span.appendChild(icon);
            rank_container.appendChild(span);
            //<span><i class="fa fa-trophy" aria-hidden="true"></i></span>
        }
        else {
            let span = document.createElement("span");
            span.innerText = i + 1;
            rank_container.appendChild(span);
            //<span>@i.ToString()</span>
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

        //<div class="candidate-data-container">
         //   <p>@candidate.FirstName @candidate.LastName</p>
          //  <p>@candidate.VotesCount Votes</p>
        //</div>


        let results_container = document.getElementById("results-container");
        results_container.appendChild(one_result_container);
    }

    //now lets hide the spinner
    hideElement(document.getElementById("voting-spinner"));
    //now lets display the result as a flex
    document.getElementById("results-container").style.display = "flex";
}