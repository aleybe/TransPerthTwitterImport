using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
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

            Console.WriteLine(SiteToTarget);
            
            var scraper = new Processors.ScraperProcess(Target: SiteToTarget);
            var returnedValue = scraper.ProcessTwitter();

            foreach (var tweet in returnedValue)
            {
                Console.WriteLine(tweet + " END of Tweet");
            }
        }
        
        // Need to import data from multiple sources. 
        
        // Twitter Import
        
        // Need to sort the data to find the appropriate information
        
        // Feed the data back up to the database.

    }
}