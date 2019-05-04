using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using TransPerthImport.Helpers;
using TransPerthImport.Processors;


namespace TransPerthImport
{
    
    public class Program
    {

        public static void Main(String[] args)
        {

            ConfigurationHelper.SetupConfig();
            

            Console.WriteLine(ConfigurationHelper.config.GetConnectionString("DynamoDB")); //HACK find this
            var wantedValue = ConfigurationHelper.config["TargetAddress"];
                
            Console.WriteLine(wantedValue);
            Console.WriteLine("Got here");

            TransPerthImport.Processors.ScraperProcess.ProcessTwitter();

            var data = TransPerthImport.Processors.ScraperProcess.DataProvided;
            
            Console.WriteLine("Made it here");
            // TODO : Get rid of bad connection string

        }
        
        
        // Need to import data from multiple sources. 
        
        // Twitter Import
        
        // Need to sort the data to find the appropriate information
        
        // Feed the data back up to the database.

    }
}