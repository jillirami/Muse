let i = 0;

const loadNews = () => {
    let URL = "https://api.nytimes.com/svc/topstories/v2/home.json?api-key=yyIGJLQZ1v3jqGt29zh1JIb1vlOcI590";

    axios.get(URL)
        .then((response) => {
            if (i > response.data.results.length - 1) {
                i = 0;
            }

            $('#newsresponse').empty().append(response.data.results[i].abstract);
            $('#addnews').empty().append('Add News as Prompt');

            i++;
        })
        .catch((error) => {
            $('#newsresponse').empty().append('The end.');
            console.log(error, i);
        });
}

const toggleQuote = () => {
    $('#quoteresponse').toggle();
    $('#addquote').toggle();
}


const addQuote = () => {
    $('#prompt').val($('#quoteresponse').html());
}

const addNews = () => {
    $('#prompt').val($('#newsresponse').html());
}


$(document).ready(() => {
    $("#news").click(loadNews);
    $("#quote").click(toggleQuote);
    $('#addquote').click(addQuote);
    $('#addnews').click(addNews);
});