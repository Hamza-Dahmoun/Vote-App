
function initializeDatatable(tableSelector, url, columnsArray, model) {
    //This function takes the parameters and initialize a jquuery dtatable
    //In addition to the columns in columnsArray, we should be added one item which is the column of the action buttons (edit - delete - details)
    columnsArray.push(
        {
            "data": null, "searchable": false, "sortable": false,
            "render": function (data, type, row, meta) {
                var buttons =
                    "<a class='table-button button-edit' title='Edit' href=" + model+'/Edit/' + row.Id + "><i class='fa fa-pencil'></i></a>" + " " +
                    "<a class='table-button button-details' title='Details' href=" + model +'/Details/' + row.Id + "><i class='fa fa-file-text'></i></a>" + " " +
                    "<a class='table-button button-delete' title='Delete' href=" + model +'/Delete/' + row.Id + "><i class='fa fa-trash'></i></a>"
                    ;
                return buttons;
            }
        }
    );
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
            "columns": columnsArray
        }
    );
}








//    THIS IS HOW YOU INITIALIZE A JQUERY DATATABLE IF YOU HAVE ONLY ONE TABLE(CONSIDER IT AS THE REFERENCE, IT WAS USED FOR VOTERS TABLE)
//function initializeDatatable() {
//    $("#voters-table").DataTable(
//        {
//            "processing": true,//whether to show 'processing' indicator when waiting for a processing result or not
//            "serverSide": true,//for server side processing
//            "filter": true,//this is for disable filter (search box)
//            "ajax": {
//                "url": '/Voter/DataTable',
//                "type": 'POST',
//                "datatype": 'json'
//            },
//            "columnDefs": [
//                { "type": "numeric-comma", targets: "_all" }
//            ],
//            "columns":
//                [//These are the columns to be displayed, and they are the fields of the voters objects brought from the server
//                    { "data": "Id", "visible": false, "searchable": false },
//                    { "data": "FirstName", "title": "FirstName", "name": "FirstName", "visible": true, "searchable": true, "sortable": false },
//                    { "data": "LastName", "title": "Last Name", "name": "LastName", "visible": true, "searchable": true, "sortable": false },
//                    { "data": "State.Name", "title": "State", "visible": true, "searchable": true, "sortable": false },
//                    {
//                        "data": null, "searchable": false, "sortable": false,
//                        "render": function (data, type, row, meta) {
//                            var buttons =
//                                "<a class='table-button button-edit' title='Edit' href=" + '@Url.Action("Edit")/' + row.Id + "><i class='fa fa-pencil'></i></a>" + " " +
//                                "<a class='table-button button-details' title='Details' href=" + '@Url.Action("Details")/' + row.Id + "><i class='fa fa-file-text'></i></a>" + " " +
//                                "<a class='table-button button-delete' title='Delete' href=" + '@Url.Action("Delete")/' + row.Id + "><i class='fa fa-trash'></i></a>"
//                                ;
//                            return buttons;
//                        }
//                    }
//                ]
//        }
//    );
//}
