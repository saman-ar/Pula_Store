using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pula_Store.WebFramework.Extensions;

namespace Pula_Store.Site
{
	public class Program
	{
		public static async Task  Main(string[] args)
		{
			//CreateWebHostBuilder(args).Build().SeedData().GetAwaiter().GetResult().Run();
			CreateWebHostBuilder(args)
                .Build()
                .SeedDataAsync()
                .Result
                .Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			 WebHost.CreateDefaultBuilder(args)
				  .UseStartup<Startup>();


	}
}
