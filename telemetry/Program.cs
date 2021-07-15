using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using mystrongboxwebapi.core;

namespace mystrongboxwebapi.telemetry
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
        .AddCommandLine(args)
        .Build();

            ResultAction<IWebHostBuilder> res = BuildServerBase.BuildServer(args, config.GetValue<string>("config"), DependencyInjection.RegisterServices, null, DependencyInjection.RegisterSwagger);
            if (res.IsOk)
            {
                res.datas.Build().Run();
            }
            else
            {
                Console.WriteLine(res.error.Description);
            }

        }
    }
}
