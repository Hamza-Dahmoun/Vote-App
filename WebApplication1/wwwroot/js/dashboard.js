//Start loading things once the page is ready
document.addEventListener("DOMContentLoaded", loadElectionsData);



function loadElectionsData() {
    //1- Load Coming Elections, hide table and display spinner
    let comingElectionsArea = document.getElementById("coming-elections-area");
    displayElement(comingElectionsArea.querySelector(".spinner-border"));
    hideElement(comingElectionsArea.querySelector(".table"));
    loadComingElections();


    //2- Load Previous Elections Datatable (the commented line were used before using jQuery Datatables)
    //let previousElectionsArea = document.getElementById("previous-elections-area");
    //displayElement(previousElectionsArea.querySelector(".spinner-border"));
    //hideElement(previousElectionsArea.querySelector(".table"));
    //loadPreviousElections();
    loadPreviousElectionsDatatable();


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




function loadPreviousElectionsDatatable() {
    //now lets make it a jquery datatables server side processing
    //this function passes some parameters to initializeDatatable() to get jQueryDatatables running


    //first of all lets make the table full width
    document.getElementById("previous-elections-table").style.width = "100%";

    /*
    let resultsButton = document.createElement("a");
    resultsButton.setAttribute("electionId", previousElections[i].Id);
    resultsButton.style.color = "#3d7e9a";
    resultsButton.style.cursor = "pointer";
    resultsButton.setAttribute("title", "Show Details");
    resultsButton.innerText = "Results";
    resultsButton.classList.add("results-in-div-btn");
    resultsButton.addEventListener("click", getElectionResults);
    let tdResultsButton_andSpinner = document.createElement("td");
    tdResultsButton_andSpinner.appendChild(resultsButton);
    let resultsButtonSpinner = document.createElement("span");
    resultsButtonSpinner.className = "spinner-border text-primary";
    resultsButtonSpinner.style.display = "none";
    tdResultsButton_andSpinner.appendChild(resultsButtonSpinner);
    */


    let tableSelector = "#previous-elections-table";
    let url = '/Election/PreviousElectionsDataTable';
    let columnsArray =
        [//These are the columns to be displayed, and they are the fields of the voters objects brought from the server
            { "data": "Id", "visible": false, "searchable": false },
            { "data": "Name", "title": "Name", "name": "Name", "visible": true, "searchable": true, "sortable": false },
            { "data": "StartDate", "title": "Start Date", "name": "Start Date", "visible": true, "searchable": true, "sortable": false },
            { "data": "DurationInDays", "title": "Duration (days)", "name": "Duration (days)", "visible": true, "searchable": true, "sortable": false },
            //{ "data": "CandidatesCount", "title": "N° Of Candidates", "name": "N° Of Candidates", "visible": true, "searchable": true, "sortable": false }
            {
                "data": null, "searchable": false, "sortable": false,
                "render": function (data, type, row, meta) {
                    var buttons =
                        "<a class=\"results-in-div-btn\" title=\"Show Results\" electionId='" + row.Id + "'>Results</a>"
                        
                        /*+
                        "<a class='table-button button-details' title='Details' href=" + model + '/Details/' + row.Id + "><i class='fa fa-file-text'></i></a>" + " " +
                        "<a class='table-button button-delete' title='Delete' href=" + model + '/Delete/' + row.Id + "><i class='fa fa-trash'></i></a>"
                        */;
                    return buttons;
                }
            }
        ]
        ;
    //initializeDatatable(tableSelector, url, columnsArray, "Voter");
    //the above function exists in the file ~/js/jQueryDatatablesInitialization.js

    $(tableSelector).DataTable(
        {
            "processing": true,//whether to show 'processing' indicator when waiting for a processing result or not
            "serverSide": true,//for server side processing
            "filter": true,//this is for enabling filter (search box)
            "ajax": {
                "url": url,
                "type": 'POST',
                "datatype": 'json'
            },
            "columnDefs": [
                { "type": "numeric-comma", targets: "_all" }
            ],
            "columns": columnsArray,
            }
        }
    );

    /*$(tableSelector).on('click', 'a', function (event) {
        //var data = $(tableSelector).row($(this).parents('tr')).data();
        //alert(data[0] + "'s salary is: " + data[2]);
    });*/
    //the below line of code is special to jQuery, it adds a click event to an element which isn't drown in the dom yet
    $(tableSelector).on('click', 'a', getElectionResults);
}
function loadPreviousElections() {
    
    //this function load a list of previous elections using jQuery ajax
    $.ajax({
        type: "POST",
        url: "/Election/GetPreviousElections",
        //data: JSON.stringify(document.getElementById("candidate-id-holder").value),
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
        resultsButton.innerText = "Results";
        resultsButton.classList.add("results-in-div-btn");
        resultsButton.addEventListener("click", getElectionResults);
        let tdResultsButton_andSpinner = document.createElement("td");
        tdResultsButton_andSpinner.appendChild(resultsButton);
        let resultsButtonSpinner = document.createElement("span");
        resultsButtonSpinner.className = "spinner-border text-primary";
        resultsButtonSpinner.style.display = "none";
        tdResultsButton_andSpinner.appendChild(resultsButtonSpinner);


        let pdfButton = document.createElement("a");
        pdfButton.setAttribute("electionId", previousElections[i].Id);
        pdfButton.setAttribute("electionName", previousElections[i].Name);
        pdfButton.setAttribute("electionCandidatesCount", previousElections[i].CandidatesCount);
        pdfButton.setAttribute("electionStartDate", previousElections[i].StartDate);
        pdfButton.setAttribute("electionDuration", previousElections[i].DurationInDays);
        pdfButton.style.color = "#3d7e9a";
        pdfButton.style.cursor = "pointer";
        pdfButton.setAttribute("title", "Download PDF");
        //pdfButton.innerHTML = '<i class="fa fa-file-pdf-o" aria-hidden="true"></i>';
        pdfButton.classList.add("fa");
        pdfButton.classList.add("fa-file-pdf-o");
        //pdfButton.innerText = "Download PDF";
        pdfButton.classList.add("results-in-pdf-btn");
        pdfButton.addEventListener("click", getElectionResults);
        let tdResultsPDF = document.createElement("td");
        tdResultsPDF.appendChild(pdfButton);
        let pdfButtonSpinner = document.createElement("span");
        pdfButtonSpinner.className = "spinner-border text-primary";
        pdfButtonSpinner.style.display = "none";
        tdResultsPDF.appendChild(pdfButtonSpinner);



        let electionRow = document.createElement("tr");
        electionRow.appendChild(tdName);
        electionRow.appendChild(tdStartDate);
        electionRow.appendChild(tdDurationInDays);
        electionRow.appendChild(tdCandidatesCount);
        electionRow.appendChild(tdResultsButton_andSpinner);
        electionRow.appendChild(tdResultsPDF);

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
    //lets hide the button and display the spinner
    //displayElement(document.getElementById("current-election-results-spinner"));
    //console.log(event);
    //console.log(event.target);
    //console.log(event.target.getAttribute("electionid"));
    

    /*
    displayElement(event.target.parentElement.querySelector(".spinner-border"));
    hideElement(event.target);
    */
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
            //console.log(response);
            //lets hide the spinner and display the button
            //hideElement(document.getElementById("current-election-results-spinner"));

            /*
            hideElement(event.target.parentElement.querySelector(".spinner-border"));
            displayElement(event.target);
            */


            //this function is released in two places:
            //1 - displaying the results of an election from the left side
            //2 - displaying the results of an election in a pdf file
            //so lets check which action to do based on which event.target launched this function

            //displayElectionResults(response);
            
            let classes = event.target.className;
            if (classes.includes("results-in-pdf-btn")) {
                //so it is the pdf button
                let electionInfo = {};//empty object
                electionInfo.name = event.target.getAttribute("electionName");
                electionInfo.candidatesCount = event.target.getAttribute("electionCandidatesCount");
                electionInfo.startDate = event.target.getAttribute("electionStartDate");
                electionInfo.duration = event.target.getAttribute("electionDuration");

                buildPdf(electionInfo, response);
            }
            else {
                //so it iif the sliding div button
                displayElectionResults(response);
            }
            
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


function buildPdf(electionInfo, results) {
    console.log(results);

    let electionName = electionInfo.name;
    let candidatesCount = electionInfo.candidatesCount;
    let electionStartDate = electionInfo.startDate;
    let electionDuration = electionInfo.duration;
    let neutralVotes = 0;
    let totalVotes = 0;
    let myRows = [
        [{ text: 'Rank', style: 'tableHeader' }, { text: 'Candidate', style: 'tableHeader' }, { text: 'Votes', style: 'tableHeader' }]
    ];
    for (let i = 0; i < results.length; i++) {
        if (results[i].isNeutralOpinion == true) {
            //lets assign the neutral votes number to a variable
            neutralVotes = results[i].VotesCount;
        }
        else {
            //lets build the array which will be injected to the body of table in the document content below
            //candidates are brought ordered from the server so just push the m to the array
            myRows.push(
                [
                    { text: i+1 },
                    { text: results[i].FirstName + " " + results[i].LastName },
                    { text: results[i].VotesCount }
                ]
            );
        }
        totalVotes = totalVotes + results[i].VotesCount; 
    }

    var documentDefinition = {
        pageMargins: [20, 60, 40, 60],
        pageSize: 'A4',
        footer: //we're going to get the footer from the below function which returns a valid pdfMake element composed of {text, style}
            function (currentPage, pageCount) {
            return { text: currentPage.toString() + ' of ' + pageCount, style: "footer" }
        },
        //the below represents the content of the document
        content:
            [
                {
                    text: 'Election Results', style: 'title'
                },
                {
                    text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                    style: "text"
                },
                //'pdfmake does not generate pdfs from the html. Rather, it generates them directly from javascript.',
                //'It is very fast, but very limited, especially compared to PHP alternatives.', 
                {
                    text: "- Election Name: " + electionName,
                    style: "points"
                },
                {
                    text: "- Start Date: " + electionStartDate,
                    style: "points"
                },
                {
                    text: "- Duration: " + electionDuration,
                    style: "points"
                },
                {
                    text: "- Number of Candidates: " + candidatesCount,
                    style: "points"
                },
                {
                    text: "Results:",
                    style: "resultsTitle"
                },
                {
                    table:
                    {
                        headerRows: 1,
                        widths: ['*', '*', '*', '*'],
                        body: myRows
                    }
                },
                {
                    text: "Notes: ",
                    style: "notes"
                },
                {
                    text: "Neutral Votes: " + neutralVotes + ".",
                    style: "points"
                },
                {
                    text: "Total Number of Votes: " + totalVotes + ".",
                    style: "points"
                },
            ],
        ////the below represents the styling area of the document, it is an object which its properties are objects too (like classes in CSS) which have properties
        //to express the styling
        styles:
        {
            title:
            {
                fontSize: 26,
                bold: false,
                margin: [0, 0, 0, 60],
                //margin: [left, top, right, bottom],
                alignment: 'center'
            },
            text:
            {
                fontSize: 13,
                bold: false,
                margin: [0, 20, 0, 50],
                alignment: 'justify',
                //lineHeight: "18px"
            },
            points:
            {
                fontSize: 12,
                bold: false,
                margin: [0, 4, 0, 4],
                alignment: 'left',
            },
            resultsTitle:
            {
                fontSize: 18,
                bold: false,
                margin: [0, 40, 0, 20],
                alignment: 'left'
            },
            tableHeader:
            {
                margin: [0, 0, 0, 0],
                fillColor: '#3262ec',
                color: '#ffffff'
            },
            notes: {
                fontSize: 15,
                bold: true,
                italics: true,
                margin: [0, 40, 0, 9],
                alignment: 'left',  
            },
            footer:
            {
                fontSize: 12,
                italics: true,
                alignment: 'center',  
            }
        }
    };
    //now lets download the pdf document since we've built it
    pdfMake.createPdf(documentDefinition).download('election-result.pdf');
}


function pdfForElement(id) {
    function ParseContainer(cnt, e, p, styles) {
        var elements = [];
        var children = e.childNodes;
        if (children.length != 0) {
            for (var i = 0; i < children.length; i++) p = ParseElement(elements, children[i], p, styles);
        }
        if (elements.length != 0) {
            for (var i = 0; i < elements.length; i++) cnt.push(elements[i]);
        }
        return p;
    }

    function ComputeStyle(o, styles) {
        for (var i = 0; i < styles.length; i++) {
            var st = styles[i].trim().toLowerCase().split(":");
            if (st.length == 2) {
                switch (st[0]) {
                    case "font-size":
                        {
                            o.fontSize = parseInt(st[1]);
                            break;
                        }
                    case "text-align":
                        {
                            switch (st[1]) {
                                case "right":
                                    o.alignment = 'right';
                                    break;
                                case "center":
                                    o.alignment = 'center';
                                    break;
                            }
                            break;
                        }
                    case "font-weight":
                        {
                            switch (st[1]) {
                                case "bold":
                                    o.bold = true;
                                    break;
                            }
                            break;
                        }
                    case "text-decoration":
                        {
                            switch (st[1]) {
                                case "underline":
                                    o.decoration = "underline";
                                    break;
                            }
                            break;
                        }
                    case "font-style":
                        {
                            switch (st[1]) {
                                case "italic":
                                    o.italics = true;
                                    break;
                            }
                            break;
                        }
                }
            }
        }
    }

    function ParseElement(cnt, e, p, styles) {
        if (!styles) styles = [];
        if (e.getAttribute) {
            var nodeStyle = e.getAttribute("style");
            if (nodeStyle) {
                var ns = nodeStyle.split(";");
                for (var k = 0; k < ns.length; k++) styles.push(ns[k]);
            }
        }

        switch (e.nodeName.toLowerCase()) {
            case "#text":
                {
                    var t = {
                        text: e.textContent.replace(/\n/g, "")
                    };
                    if (styles) ComputeStyle(t, styles);
                    p.text.push(t);
                    break;
                }
            case "b":
            case "strong":
                {
                    //styles.push("font-weight:bold");
                    ParseContainer(cnt, e, p, styles.concat(["font-weight:bold"]));
                    break;
                }
            case "u":
                {
                    //styles.push("text-decoration:underline");
                    ParseContainer(cnt, e, p, styles.concat(["text-decoration:underline"]));
                    break;
                }
            case "i":
                {
                    //styles.push("font-style:italic");
                    ParseContainer(cnt, e, p, styles.concat(["font-style:italic"]));
                    //styles.pop();
                    break;
                    //cnt.push({ text: e.innerText, bold: false });
                }
            case "span":
                {
                    ParseContainer(cnt, e, p, styles);
                    break;
                }
            case "br":
                {
                    p = CreateParagraph();
                    cnt.push(p);
                    break;
                }
            case "table":
                {
                    var t = {
                        table: {
                            widths: [],
                            body: []
                        }
                    }
                    var border = e.getAttribute("border");
                    var isBorder = false;
                    if (border)
                        if (parseInt(border) == 1) isBorder = true;
                    if (!isBorder) t.layout = 'noBorders';
                    ParseContainer(t.table.body, e, p, styles);

                    var widths = e.getAttribute("widths");
                    if (!widths) {
                        if (t.table.body.length != 0) {
                            if (t.table.body[0].length != 0)
                                for (var k = 0; k < t.table.body[0].length; k++) t.table.widths.push("*");
                        }
                    } else {
                        var w = widths.split(",");
                        for (var k = 0; k < w.length; k++) t.table.widths.push(w[k]);
                    }
                    cnt.push(t);
                    break;
                }
            case "tbody":
                {
                    ParseContainer(cnt, e, p, styles);
                    //p = CreateParagraph();
                    break;
                }
            case "tr":
                {
                    var row = [];
                    ParseContainer(row, e, p, styles);
                    cnt.push(row);
                    break;
                }
            case "td":
                {
                    p = CreateParagraph();
                    var st = {
                        stack: []
                    }
                    st.stack.push(p);

                    var rspan = e.getAttribute("rowspan");
                    if (rspan) st.rowSpan = parseInt(rspan);
                    var cspan = e.getAttribute("colspan");
                    if (cspan) st.colSpan = parseInt(cspan);

                    ParseContainer(st.stack, e, p, styles);
                    cnt.push(st);
                    break;
                }
            case "div":
            case "p":
                {
                    p = CreateParagraph();
                    var st = {
                        stack: []
                    }
                    st.stack.push(p);
                    ComputeStyle(st, styles);
                    ParseContainer(st.stack, e, p);

                    cnt.push(st);
                    break;
                }
            default:
                {
                    console.log("Parsing for node " + e.nodeName + " not found");
                    break;
                }
        }
        return p;
    }

    function ParseHtml(cnt, htmlText) {
        var html = $(htmlText.replace(/\t/g, "").replace(/\n/g, ""));
        var p = CreateParagraph();
        for (var i = 0; i < html.length; i++) ParseElement(cnt, html.get(i), p);
    }

    function CreateParagraph() {
        var p = {
            text: []
        };
        return p;
    }
    content = [];
    ParseHtml(content, document.getElementById(id).outerHTML);
    return pdfMake.createPdf({
        pageOrientation: 'landscape',
        pageMargins: [3, 20, 3, 20],
        //lineHeight: 15,
        //layout: {
        //    paddingLeft: function(i, node) { return 4; },
        //    paddingRight: function(i, node) { return 4; },
        //    paddingTop: function(i, node) { return 20; },
        //    paddingBottom: function(i, node) { return 20; },
        //    // fillColor: function (i, node) { return null; }
        //},
        content: content
    });
}