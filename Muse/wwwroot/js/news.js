// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const loadNews = () => {
    // reportStatus('Loading...');

    let URL = "https://api.nytimes.com/svc/topstories/v2/home.json?api-key="

    // Actually load the wonders
    setTimeout(() => {
        axios.get(URL)
            .then((response) => {
                // reportStatus(`success`);
                $('#news').empty().append(response);
                console.log(response)
            })
            .catch((error) => {
                // reportStatus(`failure: ${error.message}`);
                $('#news').empty().append('shoot!')
                console.log(error);
                console.log("nope")
            })
    })
}

$(document).ready(() => {
    loadNews();
})