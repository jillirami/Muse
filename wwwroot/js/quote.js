const loadQuote = () => {
  // reportStatus('Loading...');
    

  let URL = "https://us1.locationiq.com/v1/search.php?key=4a4ef147541eb5&q=Mausoleum at Halicarnassus&format=json"
        
          // Actually load the wonders
    setTimeout(() => {
        axios.get(URL)
        .then((response) => {
            // reportStatus(`success`);
            $('#quote').empty().append(`awesome`);
            console.log("woot woor")
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