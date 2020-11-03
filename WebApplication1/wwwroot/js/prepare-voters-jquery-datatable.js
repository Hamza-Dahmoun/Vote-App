//now lets make it a jquery datatables server side processing
document.addEventListener("DOMContentLoaded", preparejQueryDatatable());



function preparejQueryDatatable() {
    //this function passes some parameters to initializeDatatable() to get jQueryDatatables running
    let tableSelector = "#voters-table";
    let url = '/Voter/DataTable';
    let columnsArray =
        [//These are the columns to be displayed, and they are the fields of the voters objects brought from the server
            { "data": "Id", "visible": false, "searchable": false },
            { "data": "FirstName", "title": resources[currentUserLanguage]["FirstName"], "name": "FirstName", "visible": true, "searchable": true, "sortable": false },
            { "data": "LastName", "title": resources[currentUserLanguage]["LastName"], "name": "LastName", "visible": true, "searchable": true, "sortable": false },
            { "data": "StateName", "title": resources[currentUserLanguage]["State"], "visible": true, "searchable": true, "sortable": false }
        ]
        ;
    initializeDatatable(tableSelector, url, columnsArray, "Voter");
    //the above function exists in the file ~/js/jQueryDatatablesInitialization.js
}

