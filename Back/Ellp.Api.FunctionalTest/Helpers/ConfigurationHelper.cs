using Microsoft.Extensions.Configuration;
using System.IO;

public static class ConfigurationHelper
{
    public static IConfiguration Configuration { get; }

    static ConfigurationHelper()
    {
     
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}
