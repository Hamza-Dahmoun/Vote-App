//Start loading things once the page is ready
document.addEventListener("DOMContentLoaded", loadElectionsData);



function loadElectionsData() {
    //1- Load Coming Elections, hide table and display spinner
    let comingElectionsArea = document.getElementById("coming-elections-area");
    displayElement(comingElectionsArea.querySelector(".spinner-border"));
    hideElement(comingElectionsArea.querySelector(".table"));
    loadComingElections();


    //2- Load Previous Elections Datatable (the commented line were used before using jQuery Datatables)
    loadPreviousElectionsDatatable();


    //3- Load Current Election
    let currentElectionArea = document.getElementById("current-election-area");
    displayElement(currentElectionArea.querySelector(".spinner-border"));
    loadCurrentElection();
}



//Coming Elections are Datatables frontend only
function loadComingElections() {
    //this function load a list of coming elections using jQuery ajax

    $.ajax({
        type: "POST",
        url: "/Election/GetComingElections",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            //in here 'response' represents the following object {success: false, message ='...text here...'}
            //which I sent after creating an Error HttpContext.Response.StatusCode = 500 ...see the Catch block of code in the backend

            //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

            //so there is a server error, lets display the error msg
            let errorParag = document.createElement("p");
            let responseMsg = document.createElement("div");
            responseMsg.className = "alert alert-danger";
            errorParag.innerHTML = "<strong>" + resources[currentUserLanguage]["Error"] + "!</strong> " + response.responseJSON.message;
                        
            responseMsg.appendChild(errorParag);
            document.getElementById("coming-elections-area").appendChild(responseMsg);
            //now lets hide the spinner
            hideElement(document.getElementById("coming-elections-area").querySelector(".spinner-border"));
        },
        success: function (response) {
            //'response' represents the object returned from the api which is a list of future elections
            console.log(response);
            displayComingElections(response);
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
    $('#coming-elections-table').DataTable({
        "language": getTranslatedDataTable()
    });
}



function loadCurrentElection() {
    //this function load the current election using jQuery ajax

    $.ajax({
        type: "POST",
        url: "/Election/GetCurrentElection",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

            //so there is a server error, lets display the error msg
            let errorParag = document.createElement("p");
            let responseMsg = document.createElement("div");
            responseMsg.className = "alert alert-danger";
            errorParag.innerHTML = "<strong>" + resources[currentUserLanguage]["Error"] + "!</strong> " + response.responseJSON.message;
            
            responseMsg.appendChild(errorParag);
            document.getElementById("current-election-area").appendChild(responseMsg);
            //now lets hide the spinner
            hideElement(document.getElementById("current-election-area").querySelector(".spinner-border"));
        },
        success: function (response) {
            //'response' represents the object returned from the api which is the current election
            console.log(response);

            displayCurrentElection(response);
        }
    });
}
function displayCurrentElection(currentElection) {
    let currentElectionArea = document.getElementById("current-election-area");
    hideElement(currentElectionArea.querySelector(".spinner-border"));

    if (currentElection == null) {
        //so there is no current election, lets just display a simple text
        let p = document.createElement("p");
        p.innerText = resources[currentUserLanguage]["There is no Election currently"] + "!";

        currentElectionArea.appendChild(p);
    }
    else {
        //so there is an election currently, lets display its info with a 'Vote' button
        let name = document.createElement("p");
        name.innerText = resources[currentUserLanguage]["Name"] + ": ";
        let strongName = document.createElement("strong");
        strongName.innerText = currentElection.Name + " | " + currentElection.CandidatesCount + " " + resources[currentUserLanguage]["Candidates"];
        name.appendChild(strongName);

        let startdate = document.createElement("p");
        startdate.innerText = resources[currentUserLanguage]["Date"] + ": "; 
        let strongDate = document.createElement("strong");
        strongDate.innerText = currentElection.StartDate;
        startdate.appendChild(strongDate);

        let durationindays = document.createElement("p");
        durationindays.innerText = resources[currentUserLanguage]["Duration (days)"] +  ": ";
        let strongDuration = document.createElement("strong");
        strongDuration.innerText = currentElection.DurationInDays;
        durationindays.appendChild(strongDuration);

        let participationRate = document.createElement("p");
        participationRate.innerText = resources[currentUserLanguage]["Participation Rate"] + ": ";
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
            voteButton.setAttribute("title", resources[currentUserLanguage]["Go Vote"]);
            //currentElectionId in Vote/Index() was always received as empty Guid .. lets just get the current election inside the Index() action
            voteButton.setAttribute("href", "/Vote/Index/");
            let icon = document.createElement("i");
            icon.className = "fa fa-chevron-right";
            icon.innerText = resources[currentUserLanguage]["Vote"];
            voteButton.appendChild(icon);
            currentElectionArea.appendChild(voteButton);
        }
        else {
            let resultsButton = document.createElement("a");
            resultsButton.setAttribute("electionId", currentElection.Id);
            resultsButton.style.color = "#3d7e9a";
            resultsButton.style.cursor = "pointer";
            resultsButton.setAttribute("title", resources[currentUserLanguage]["Show Details"]);
            resultsButton.innerText = resources[currentUserLanguage]["Show Details"];
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
    //this function load previous elections and draw them in a datatable
    //this function replaced the functions: loadPreviousElections() and displayPreviousElections() which were loading all previous elections
    //rows at once and write them in an HTML table


    //first of all lets make the table full width
    document.getElementById("previous-elections-table").style.width = "100%";

    

    let tableSelector = "#previous-elections-table";
    let url = '/Election/PreviousElectionsDataTable';
    let columnsArray =
        [//These are the columns to be displayed, and they are the fields of the voters objects brought from the server
            { "data": "Id", "visible": false, "searchable": false },
            { "data": "Name", "name": "Name", "title": resources[currentUserLanguage]["Name"], "visible": true, "searchable": true, "sortable": true },
            { "data": "StartDate", "name": "Start Date", "title": resources[currentUserLanguage]["Start Date"], "visible": true, "searchable": true, "sortable": true },
            { "data": "DurationInDays", "name": "Duration (days)", "title": resources[currentUserLanguage]["Duration (days)"], "visible": true, "searchable": true, "sortable": false },
            { "data": "NumberOfCandidates", "name": "N° Of Candidates", "title": resources[currentUserLanguage]["N° Of Candidates"], "visible": true, "searchable": true, "sortable": false },
            { "data": "HasNeutral", "name": "Neutral Opinion", "title": resources[currentUserLanguage]["Neutral Opinion"], "visible": true, "searchable": false, "sortable": false },
            { "data": "NumberOfVotes", "name": "Number of Votes", "title": resources[currentUserLanguage]["Number of Votes"], "visible": true, "searchable": false, "sortable": false },
            {
                "data": null, "searchable": false, "sortable": false,
                "render": function (data, type, row, meta) {
                    var buttons =
                        "<a class=\"results-in-div-btn text-primary\" title=\"Show Results\" electionId='" + row.Id + "'>Results</a>"
                        + " "
                        + "<span class='spinner-border text-primary' style='display:none'></span>"
                        + " "
                        + "<a class=\"results-in-pdf-btn text-primary fa fa-file-pdf-o\" title=\"Download Report\" electionId='" + row.Id
                        + "' electionName='" + row.Name
                        + "' electionCandidatesCount='" + row.NumberOfCandidates
                        + "' electionStartDate='" + row.StartDate
                        + "' electionHasNeutral='" + row.HasNeutral                    
                        + "' electionDuration='" + row.DurationInDays + "'></a> "
                        ;
                    return buttons;
                }
            }
        ]
        ;


    $(tableSelector).DataTable(
        {
            "processing": true,//whether to show 'processing' indicator when waiting for a processing result or not
            "serverSide": true,//for server side processing
            "filter": true,//this is for enabling filter (search box)
            "ajax": {
                "url": url,
                "type": 'POST',
                "datatype": 'json',
                "error": function (reason) {
                    //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

                    //so there is a server error, lets display the error msg
                    let errorParag = document.createElement("p");
                    let responseMsg = document.createElement("div");
                    responseMsg.className = "alert alert-danger";
                    errorParag.innerHTML = "<strong>" + resources[currentUserLanguage]["Error"] + "!</strong> " + reason.responseJSON.message;
                    responseMsg.appendChild(errorParag);
                    document.getElementById("previous-elections-area").appendChild(responseMsg);
                    //now lets hide the datatable wrapper (a div created by jquery which surrounds the table)
                    hideElement(document.getElementById("previous-elections-area").querySelector(".dataTables_wrapper"));
                }
            },
            "columnDefs": [
                { "type": "numeric-comma", targets: "_all" }
            ],
            "columns": columnsArray, 
            "language": getTranslatedDataTable()
        }
        
    );


    //the below line of code is special to jQuery, it adds a click event to an element which isn't drown in the dom yet
    //in our case it is adding the click event to two buttons in each row: 'Show Results button' and 'PDF button'
    $(tableSelector).on('click', 'a', getElectionResults);
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
    let clickedButton = event.target;

    
    displayElement(clickedButton.parentElement.querySelector(".spinner-border"));
    hideElement(clickedButton);
    
    //this function load the current election results using jQuery ajax
    $.ajax({
        type: "POST",
        url: "/Home/GetResultsOfElection",
        data: JSON.stringify(clickedButton.getAttribute("electionId")),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            //to know why I used 'response.responseJSON.message' to get the error text just log the response object and check its properties

            //so there is a server error, lets display the error msg

            document.getElementById("redModal").querySelector("h4").innerText = resources[currentUserLanguage]["Error"] + "!";
            document.getElementById("redModal").querySelector("p").innerText = response.responseJSON.message;
            $('#redModal').modal('show');
            //now lets hide the spinner
            hideElement(clickedButton.parentElement.querySelector(".spinner-border"));
            displayElement(clickedButton);
        },
        success: function (response) {
            
            //'response' represents the object returned from the api which is the an array of candodates ordered by their number of votes

            //lets hide the spinner and display the button

            
            hideElement(clickedButton.parentElement.querySelector(".spinner-border"));
            //below we didnt use 'block' we used 'inline-block' so that two buttons will be displayed next to each other
            clickedButton.style.display = "inline-block";


            //this function is released in two places:
            //1 - displaying the results of an election from the left side
            //2 - displaying the results of an election in a pdf file
            //so lets check which action to do based on which event.target launched this function

            
            let classes = clickedButton.className;
            if (classes.includes("results-in-pdf-btn")) {
                //so it is the pdf button
                let electionInfo = {};//empty object
                electionInfo.name = clickedButton.getAttribute("electionName");
                electionInfo.candidatesCount = clickedButton.getAttribute("electionCandidatesCount");
                electionInfo.startDate = clickedButton.getAttribute("electionStartDate");
                electionInfo.duration = clickedButton.getAttribute("electionDuration");
                electionInfo.hasNeutral = clickedButton.getAttribute("electionHasNeutral");
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
        para.innerText = resources[currentUserLanguage]["It seems this election had no Candidates!"];
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
    document.getElementById("close-button-container").querySelector("span").addEventListener("click", hideElectionResultsContainer);

}
function hideElectionResultsContainer() {
    document.getElementById("election-results-container-parent").className = "hidden_results_container";
}


function buildPdf(electionInfo, results) {
    console.log(results);

    let electionName = electionInfo.name;
    let candidatesCount = electionInfo.candidatesCount;
    let electionStartDate = electionInfo.startDate;
    let electionDuration = electionInfo.duration;
    let electionHasNeutral = electionInfo.hasNeutral
    let neutralVotes = 0;
    let totalVotes = 0;
    let myRows = [
        [{ text: resources[currentUserLanguage]['Rank'], style: 'tableHeader' }, { text: resources[currentUserLanguage]['Candidate'], style: 'tableHeader' }, { text: resources[currentUserLanguage]['Votes'], style: 'tableHeader' }]
    ];

    for (let i = 0; i < results.length; i++) {
            if (results[i].isNeutralOpinion == true) {
                //lets assign the neutral votes number to a variable
                neutralVotes = results[i].VotesCount;
            }
            
                //lets build the array which will be injected to the body of table in the document content below
                //candidates are brought ordered from the server so just push the m to the array
                myRows.push(
                    [
                        { text: i + 1 },
                        { text: results[i].FirstName + " " + results[i].LastName },
                        { text: results[i].VotesCount }
                    ]
                );
            
        
        
        totalVotes = totalVotes + results[i].VotesCount; 
    }

    var documentDefinition = {
        pageMargins: [20, 60, 40, 60],
        pageSize: 'A4',
        footer: //we're going to get the footer from the below function which returns a valid pdfMake element composed of {text, style}
            function (currentPage, pageCount) {
                return { text: currentPage.toString() + ' ' + resources[currentUserLanguage]['of'] + ' ' + pageCount, style: "footer" }
        },
        //the below represents the content of the document
        content:
            [
                {
                    text: resources[currentUserLanguage]['Election Results'], style: 'title'
                },
                {
                    text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                    style: "text"
                },
                //'pdfmake does not generate pdfs from the html. Rather, it generates them directly from javascript.',
                //'It is very fast, but very limited, especially compared to PHP alternatives.', 
                {
                    text: "- " + resources[currentUserLanguage]["Election Name"]+": " + electionName,
                    style: "points"
                },
                {
                    text: "- " + resources[currentUserLanguage]["Start Date"] + ": " + electionStartDate,
                    style: "points"
                },
                {
                    text: "- " + resources[currentUserLanguage]["Duration"] + ": " + electionDuration,
                    style: "points"
                },
                {
                    text: "- " + resources[currentUserLanguage]["Number of Candidates"] + ": " + candidatesCount,
                    style: "points"
                },
                {
                    text: resources[currentUserLanguage]["Results"] + ":",
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
                    text: resources[currentUserLanguage]["Notes"]+": ",
                    style: "notes"
                },
                {
                    text: resources[currentUserLanguage]["Neutral Votes"]+": " + neutralVotes + ".",
                    style: "points"
                },
                {
                    text: resources[currentUserLanguage]["Total Number of Votes"] + ": " + totalVotes + ".",
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