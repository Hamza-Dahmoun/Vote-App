//THIS FILE CONTAINS FRONTEND LOCALIZATION METHODS & AN OBJECT WHICH WORKS LIKE RESOURCES FILES CONTAINING MESSAGES TRANSLATION AND DATATABLE TRANSLATION
//METHODS THAT GET THE ASPNETCORE.CULTURE COOKIE IN ORDER TO SEE WHICH LANGUAGE IS USED SO (CHOSEN BY THE USER IN THE DROPDOWN LIST) SO
//THAT WE COULD DECIDE WHICH LANGUAGE WE USE WHEN DISPLAYING JQUERY DATATABLES AND MESSAGES WE DISPLAY IN THE FRONTEND

//declaring resources object to work just like resources files in the server
var resources = {
    en: {}, fr: {}
};

resources.en["hi"] = "hi";
resources.fr["hi"] = "salut";
resources.en["Error"] = "Error";
resources.fr["Error"] = "Erreur";
resources.en["There is no Election currently"] = "There is no Election currently";
resources.fr["There is no Election currently"] = "Il n'a pas d'Election pour le moment";
resources.en["Name"] = "Name";
resources.fr["Name"] = "Nom";
resources.en["Candidates"] = "Candidates";
resources.fr["Candidates"] = "Candidats";
resources.en["Date"] = "Date";
resources.fr["Date"] = "Date";
resources.en["Duration (days)"] = "Duration (days)";
resources.fr["Duration (days)"] = "Durée (jours)";
resources.en["Participation Rate"] = "Participation Rate";
resources.fr["Participation Rate"] = "Taux de Participation";
resources.en["Go Vote"] = "Go Vote";
resources.fr["Go Vote"] = "Allez Voter";
resources.en["Vote"] = "Vote";
resources.fr["Vote"] = "Voter";
resources.en["Show Details"] = "Show Details";
resources.fr["Show Details"] = "Show Details";
resources.en["It seems this election had no Candidates!"] = "It seems this election had no Candidates!";
resources.fr["It seems this election had no Candidates!"] = "It seems this election had no Candidates!";
resources.en["Rank"] = "Rank";
resources.fr["Rank"] = "Rang";
resources.en["Candidate"] = "Candidate";
resources.fr["Candidate"] = "Candidat";
resources.en["Votes"] = "Votes";
resources.fr["Votes"] = "Votes";
resources.en["of"] = "of";
resources.fr["of"] = "de";
resources.en["Election Results"] = "Election Results";
resources.fr["Election Results"] = "Résultats";
resources.en["Election Name"] = "Election Name";
resources.fr["Election Name"] = "Nom d'Election";
resources.en["Start Date"] = "Start Date";
resources.fr["Start Date"] = "Date Début";
resources.en["Duration"] = "Duration";
resources.fr["Duration"] = "Durée";
resources.en["Number of Candidates"] = "Number of Candidates";
resources.fr["Number of Candidates"] = "Nombre de Candidats";
resources.en["Results"] = "Results";
resources.fr["Results"] = "Résultats";
resources.en["Notes"] = "Notes";
resources.fr["Notes"] = "Remarque";
resources.en["Neutral Votes"] = "Neutral Votes";
resources.fr["Neutral Votes"] = "Opinion Neutre";
resources.en["Total Number of Votes"] = "Total Number of Votes";
resources.fr["Total Number of Votes"] = "Nombre Totale des votes";


//var language = "en";
//console.log(resources[language]["Error"]);




/************************************ THIS IS HOW TO STORE AND READ A COOKIE (source:https://developer.mozilla.org/en-US/docs/Web/API/Document/cookie)
document.cookie = "test1=Hello";
document.cookie = "test2=World";

const cookieValue = document.cookie
    .split('; ')
    .find(row => row.startsWith('test2'))
    .split('=')[1];

function alertCookieValue() {
    alert(cookieValue);
}
alertCookieValue();
*************************************/////////////////////////////
/************************************ source: https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/decodeURIComponent
The decodeURIComponent() function decodes a Uniform Resource Identifier (URI) component previously created by encodeURIComponent or by a similar routine.

 decodeURIComponent(encodedURI)

encodedURI
    An encoded component of a Uniform Resource Identifier.

Return value

A new string representing the decoded version of the given encoded Uniform Resource Identifier (URI) component.

*************************************/////////////////////////////

var currentUserLanguage = getLanguageCookieValue(".AspNetCore.Culture");
function getLanguageCookieValue(cookieName) {
    let cookieValue = document.cookie
        .split('; ')
        .find(row => row.startsWith(cookieName))
        .split('=')[1];
    //cookieValue should look like: OR

    //lets decode it
    cookieValue = decodeURIComponent(cookieValue)
    //now it looks like: c=en|cui=en OR c=fr|cui=fr

    //lts split it
    let myarray = cookieValue.split('|');
    //now it looks like: ['c=en', 'cui=en'] OR ..

    //lets split the first item and check the second item of the resulted array
    let mylanguage = myarray[0].split('=');
    //now it looks like: mylanguage=['c', 'en'] OR ..

    return mylanguage[1];
}

console.log(currentUserLanguage);
//console.log(decodeURIComponent(currentUserLanguage));




/*
var current = document.cookie.split('; ').reduce((r, v) => {
    const parts = v.split('=')
    return parts[0] === '.AspNetCore.Culture' ? decodeURIComponent(parts[1]) : r
}, '').split('|')[0].split('=')[1];
console.log(current);
*/
