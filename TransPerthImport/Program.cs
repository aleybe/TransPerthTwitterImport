using System;
using TransPerthImport.Helpers;
using TransPerthImport.Processors;


namespace TransPerthImport
{
    
    public class Program
    {

        static void Main(String[] args)
        {
            ConfigurationHelper.SetupConfig();

            var SiteToTarget = ConfigurationHelper.config["TargetAddress"];

            var scraper = new ScraperProcess(Target: SiteToTarget);
            var returnedValue = scraper.ProcessTwitter();

            foreach (var tweet in returnedValue)
            {
                Console.WriteLine(tweet + " END of Tweet");
            }
        }
        
    }
}