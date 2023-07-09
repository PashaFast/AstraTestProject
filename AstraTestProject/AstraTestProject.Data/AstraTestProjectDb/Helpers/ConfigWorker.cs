using Microsoft.Extensions.Configuration;
using System.IO;

namespace AstraTestProject.Data.AstraTestProjectDb.Helpers
{
    public class ConfigWorker
    {
        private static IConfigurationRoot config;
        private static IConfigurationRoot GetConfig()
        {
            if (config == null)
                config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            return config;
        }
        public static string GetSqlConnection(string sql)
        {
            return GetConfig()[sql];
        }
    }
}
