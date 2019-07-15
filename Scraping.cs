// attempt using html agility package
// using System;
// using HtmlAgilityPack;

// namespace ScrapingAttempt
// {
//     class DownloadPageHttpClient
//     {    static void Main()
//         {

//             string Url = "https://www.forbes.com/quotes/";
//             var doc = new HtmlWeb().Load(Url);
//             doc.OptionAutoCloseOnEnd = true;

//             var htmlBody = doc.DocumentNode.SelectSingleNode("//body");
//             var quote = htmlBody.SelectSingleNode("//cite");

//             Console.WriteLine(doc.DocumentNode.OuterHtml);
//         }
//     }
// }




//  attempt using webclient class of system.net
using System;
using System.Net;
using Newtonsoft.Json;

namespace ScrapingAttemptDos
{
    // using OpenScraping;
    // using OpenScraping.Config;
    class Scraping
    {
        static void Scrape(string[] args)
        {
            WebClient client = new WebClient();
            string downloadString = client.DownloadString("https://www.forbes.com/quotes/");

            Console.WriteLine(downloadString);
            var result = JsonConvert.SerializeObject(downloadString);
            // Console.WriteLine(result);

            // var jsonConfig = @"
            // {
            //     'quote': '//blockquote[contains(@class, 'buddle')]//p',
            //     'author': '//footer[contains(@class, 'author')]//cite',
            // }";

            // var config = StructuredDataConfig.ParseJsonString(jsonConfig);


            // var openScraping = new StructuredDataExtractor(config);
            // var scrapingResults = openScraping.Extract(downloadString);

            // Console.WriteLine(JsonConvert.SerializeObject(scrapingResults, Formatting.Indented));
        }
    }
}