using Microsoft.Extensions.Configuration;
using SharedModels;
using System;
using System.IO;

namespace SharedUtilities
{
    public class ConfigurationUtility
    {
		public static ConnectionStrings GetConnectionStrings()
		{

			string jsonFile = $"appsettings.json";

			var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile(jsonFile, optional: false, reloadOnChange: true)
			.AddEnvironmentVariables();

			IConfigurationRoot configuration = builder.Build();

			ConnectionStrings connectionStrings = new ConnectionStrings();
			configuration.GetSection("ConnectionStrings").Bind(connectionStrings);


			connectionStrings.PrimaryDatabaseConnectionString = connectionStrings.PrimaryDatabaseConnectionString;

			return connectionStrings;
		}

	}
}
