using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using TransPerthImport.Interfaces;


namespace TransPerthImport.Helpers
{
    public static class ConfigurationHelper 
    {
        public static IConfigurationRoot SetupConfig()
        {
            //Build application configurations
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",  true, true);

            return builder.Build();
        }

        public static IConfigurationRoot config = SetupConfig();
    
    }
}