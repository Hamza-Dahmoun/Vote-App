//now lets make it a jquery datatables server side processing
//document.addEventListener("DOMContentLoaded", initializeDatatable);
document.addEventListener("DOMContentLoaded", preparejQueryDatatable());



function preparejQueryDatatable() {
    //this function passes some parameters to initializeDatatable() to get jQueryDatatables running
    let tableSelector = "#voters-table";
    let url = '/Voter/DataTable';
    let columnsArray =
        [//These are the columns to be displayed, and they are the fields of the voters objects brought from the server
            { "data": "Id", "visible": false, "searchable": false },
            { "data": "FirstName", "title": "First Name", "name": "FirstName", "visible": true, "searchable": true, "sortable": false },
            { "data": "LastName", "title": "Last Name", "name": "LastName", "visible": true, "searchable": true, "sortable": false },
            { "data": "State.Name", "title": "State", "visible": true, "searchable": true, "sortable": false }
        ]
        ;
    initializeDatatable(tableSelector, url, columnsArray, "Voter");
    //the above function exists in the file ~/js/jQueryDatatablesInitialization.js
}

