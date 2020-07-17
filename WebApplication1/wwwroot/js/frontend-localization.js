﻿//THIS FILE CONTAINS FRONTEND LOCALIZATION METHODS & AN OBJECT WHICH WORKS LIKE RESOURCES FILES CONTAINING MESSAGES TRANSLATION AND DATATABLE TRANSLATION
//METHODS THAT GET THE ASPNETCORE.CULTURE COOKIE IN ORDER TO SEE WHICH LANGUAGE IS USED SO (CHOSEN BY THE USER IN THE DROPDOWN LIST) SO
//THAT WE COULD DECIDE WHICH LANGUAGE WE USE WHEN DISPLAYING JQUERY DATATABLES AND MESSAGES WE DISPLAY IN THE FRONTEND



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

var currentUserLanguage = getCookieValue(".AspNetCore.Culture");
function getCookieValue(cookieName) {
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
