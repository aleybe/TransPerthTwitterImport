using System.Dynamic;
using Microsoft.Extensions.Configuration;
using TransPerthImport.Helpers;

namespace TransPerthImport.Interfaces
{
    public interface IConfigurationHelper
    {
        IConfigurationHelper Config { get; set; }
    }
}