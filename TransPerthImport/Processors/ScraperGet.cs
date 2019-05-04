using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using static System.Net.WebRequest;
using TransPerthImport.Helpers;
    
namespace TransPerthImport.Processors
{
    public static class ScraperProcess
    {

        private static string Target = ConfigurationHelper.config["TargetAddress"];

        public static string DataProvided;
        
        public static async void ProcessTwitter()
        {
            var pageContent = await GetHtmlData();
            DataProvided = ParseHtmlData(pageContent);
        }
        
        public static async Task<string> GetHtmlData()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(Target);
            var pageContents = await response.Content.ReadAsStringAsync();
            
            Console.WriteLine("Collected Data");

            return pageContents;
        }

        public static string ParseHtmlData(string htmlInput)
        {
            HtmlDocument twitterDocument = new HtmlDocument();
            twitterDocument.LoadHtml(htmlInput);
            
            var toLookFor = "(//div[contains(@id, 'stream-items-id)]//p[0])";
            var tweetsAvailable = twitterDocument.DocumentNode.SelectSingleNode(toLookFor).InnerText;

            return (tweetsAvailable);


        }
    }
}