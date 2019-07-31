// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let i = 0;

const loadNews = () => {
    // reportStatus('Loading...');

    let URL = "https://api.nytimes.com/svc/topstories/v2/home.json?api-key=yyIGJLQZ1v3jqGt29zh1JIb1vlOcI590";

    axios.get(URL)
        .then((response) => {
            if (i > response.data.results.length - 1) {
                i = 0;
            }

            // reportStatus(`success`);
            $('#newsresponse').empty().append(response.data.results[i].abstract);
            //console.log(response.data.results[0]);
             
            i++;
        })
        .catch((error) => {
            // reportStatus(`failure: ${error.message}`);
            $('#newsresponse').empty().append('The end.');
            console.log(error, i);
        });
}

const toggleQuote = () => {
    $('#quoteresponse').toggle();
}





$(document).ready(() => {
    $("#news").click(loadNews);
    $("#quote").click(toggleQuote);
});