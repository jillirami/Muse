// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let i = 0;

const loadNews = () => {
    // reportStatus('Loading...');

    let URL = "https://api.nytimes.com/svc/topstories/v2/home.json?api-key=yyIGJLQZ1v3jqGt29zh1JIb1vlOcI590"

    setTimeout(() => {
        axios.get(URL)
            .then((response) => {
                // reportStatus(`success`);
                $('#newsresponse').empty().append(response.data.results[i].abstract);
                console.log(response.data.results[0]);
                if (i < response.data.results.length) {
                    i++;
                } else {
                    i = 0;
                };
            })
            .catch((error) => {
                // reportStatus(`failure: ${error.message}`);
                $('#newsresponse').empty().append('shoot!');
                console.log(error);
                console.log("nope");
            })
    })
}

$(document).ready(() => {
    $("#news").click(loadNews);
});