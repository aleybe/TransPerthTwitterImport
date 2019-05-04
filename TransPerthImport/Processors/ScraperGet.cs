using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

    
namespace TransPerthImport.Processors
{
    public class ScraperProcess
    {

        private static string _target;
        private static string _filterToFind;
        
        public ScraperProcess(string filterToFind = "//*[@id='stream-items-id']", string Target = "www.google.com.au")
        {
            _filterToFind = filterToFind;
            _target = Target;
        }
        
        public List<string> ProcessTwitter()
        {
            var pageContent = GetHtmlData(_target).Result;
            return(ParseHtmlData(pageContent, _filterToFind));
        }
        
        private static async Task<string> GetHtmlData(string target)
        {
            var pageContents = "Nothing";
            HttpClient client = new HttpClient();

            var response = client.GetStringAsync(target);
            pageContents = await response;
            return pageContents;
        }

        public static List<string> ParseHtmlData(string htmlInput, string filter)
        {
            HtmlDocument twitterDocument = new HtmlDocument();
            twitterDocument.LoadHtml(htmlInput);
            
            var toLookFor = filter;
            var twitterStream = twitterDocument.DocumentNode.SelectSingleNode(toLookFor).SelectNodes("./li");
            
            List<string> listOfTweets = new List<string>();
            
            foreach (var tweet in twitterStream)
            {
                var tweetText = tweet.SelectSingleNode(".//div[@class='content']/div[@class='js-tweet-text-container']/p").InnerText;
                listOfTweets.Add(tweetText);
            }
            
            //get individual nodes
            
            //*[@id="timeline"]/div/div[2]
            // -> ol - //*[@id="stream-items-id"]
            //     -> list items - //li[@data-item-type="tweet"]
            //         -> content
            //             -> js-tweet-text-container //*[@id="stream-item-tweet-1124573127382577152"]/div[1]/div[2]/div[2]/p

            return (listOfTweets);

        }
    }
}