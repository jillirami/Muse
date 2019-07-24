// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const loadQuote = () => {
    // reportStatus('Loading...');

    let URL = "https://www.forbes.com/forbesapi/thought/get.json?limit=1&start=51&stream=true"

    //let URL = "https://us1.locationiq.com/v1/search.php?key=4a4ef147541eb5&q=Mausoleum at Halicarnassus&format=json"

    // Actually load the wonders
    setTimeout(() => {
        axios.get(URL, {
            headers: {
                'Access-Control-Allow-Origin': '*',
            }
        }, { crossdomain: true })

            .then((response) => {
                // reportStatus(`success`);
                $('#quote').empty().append(`awesome`);
                console.log("woot wooT")
            })
            .catch((error) => {
                // reportStatus(`failure: ${error.message}`);
                $('#quote').empty().append('shoot!')
                console.log(error);
                console.log("nope")
            })
    })
}

$(document).ready(() => {
    loadQuote();
})