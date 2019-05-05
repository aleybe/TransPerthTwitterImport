using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using alexyz.dev.dbrepo;
using alexyz.dev.dbrepo.Models;
using HtmlAgilityPack;

//Notes :
//
//Mapping of the twitter data:
//
//*[@id="timeline"]/div/div[2]
// -> ol - //*[@id="stream-items-id"]
//     -> list items - //li[@data-item-type="tweet"]
//         -> content
//             -> js-tweet-text-container //*[@id="stream-item-tweet-1124573127382577152"]/div[1]/div[2]/div[2]/p



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
        
        public List<Tweet> ProcessTwitter()
        {
            var pageContent = GetHtmlData(_target).Result;
            var parsedTweets = ParseHtmlData(pageContent, _filterToFind);
            TweetDBUpdate(parsedTweets);
            
            return(parsedTweets);
        }
        
        private static async Task<string> GetHtmlData(string target)
        {
            var pageContents = "Nothing";
            HttpClient client = new HttpClient();

            var response = client.GetStringAsync(target);
            pageContents = await response;
            return pageContents;
        }

        public static List<Tweet> ParseHtmlData(string htmlInput, string filter)
        {
            HtmlDocument twitterDocument = new HtmlDocument();
            twitterDocument.LoadHtml(htmlInput);
            
            var toLookFor = filter;
            var twitterStream = twitterDocument.DocumentNode.SelectSingleNode(toLookFor).SelectNodes("./li");
            
            var tweetList = new List<Tweet>();
            
            foreach (var tweet in twitterStream)
            {
                var tweetText = tweet.SelectSingleNode(".//div[@class='content']/div[@class='js-tweet-text-container']/p").InnerText;
                
                var currentTweet = new Tweet()
                {
                    TweetText = tweetText,
                    Likes = 0,
                    Retweets = 0
                };
                
                tweetList.Add(currentTweet);
            }
            
            return (tweetList);
        }

        private void TweetDBUpdate(List<Tweet> tweetsToUpdate)
        {
            var tweetProcDbContext = new DatabaseContext();
            
            tweetProcDbContext.Tweets.AddRange(tweetsToUpdate);
            tweetProcDbContext.SaveChanges();
        }
    }
}