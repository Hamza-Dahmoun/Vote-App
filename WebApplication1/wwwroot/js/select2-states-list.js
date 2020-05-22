$(function () {
    $("#stateId").select2({
        ajax: {
            type: "POST",
            url: "/Voter/States",
            dataType: 'json',
            delay: 250,//wait for 250ms after user stopped tying, then send the request to the server
            data: function (params) {
                return {
                    q: params.term, // search term
                    page: params.page
                };
            },
            processResults:
                function (data) {
                    //data is an object we've got from server and has one property 'states' which is an array of {id: .., text: ..}
                    //console.log(data.states);                        
                    return {
                        results: data.states
                    };
                },

        },
        minimumInputLength: 1, // Minimum length of input in search box before ajax call triggers        

    });
})
