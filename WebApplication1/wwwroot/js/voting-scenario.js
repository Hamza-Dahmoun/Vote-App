//add click event listener to candidate-container divs
var maxSelection = 5;
var selectedIdArray = new Array();
var neutralCandidateId = document.getElementById("neutral-candidate").querySelector(".hidden-candidateId").textContent /*"d3e32681-27af-4758-8bb7-5558bd2c7c55"*/;
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
    //but before it checks if "Neutral Opinion" is selected and tries to unselect it
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
        success: function (r) {
            console.log("success");
            window.location.href = "Home/Index";
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
    if (neutralArea.className.includes("selected-candidate")) {
        neutralArea.className = "candidate-container";
        neutralArea.querySelector(".fa-check-circle-o").style.display = "none";
    }
}
