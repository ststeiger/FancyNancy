
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace TestCoreNanny
{


    public class Program
    {


        public static void Main(string[] args)
        {
            /*
            var uri = new Uri("http://localhost:3579");
            var uris = new[] { uri };
            var hostConfig = new Nancy.Hosting.Self.HostConfiguration();
            hostConfig.UrlReservations.CreateAutomatically = true;
            NancyHost nancyHost = new NancyHost(new Uri("http://+:80"), new DefaultNancyBootstrapper(), hostConfigs);
            // https://github.com/NancyFx/Nancy/blob/master/src/Nancy.Hosting.Self/NetSh.cs
            // https://github.com/NancyFx/Nancy/blob/master/src/Nancy.Hosting.Self/NancyHost.cs
            // https://github.com/NancyFx/Nancy/blob/master/src/Nancy.Hosting.Self/UacHelper.cs
            // https://github.com/NancyFx/Nancy/blob/master/src/Nancy.Hosting.Self/UrlReservations.cs
            */
            // http add urlacl url = http://+:8888/nancy/ user=Everyone
            // http add urlacl url = http://127.0.0.1:8888/nancy/ user=Everyone
            // http add urlacl url = http://+:8889/nancytoo/ user=Everyone
            // "netsh" string.Format("http add urlacl url=\"{0}\" user=\"{1}\"", url, user);

            BuildWebHost(args).Run();
        }


        public static IWebHost BuildWebHost(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", true)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();
        }


    }


}
