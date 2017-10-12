using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Owin;


using Microsoft.AspNetCore.Routing;


using Nancy;
using Nancy.Configuration;
// using Nancy.Owin;


namespace TestCoreNanny
{

    public class DemoBootstrapper : DefaultNancyBootstrapper
    {
        // Override with a valid password(albeit a really really bad one!)
        // to enable the diagnostics dashboard
        public override void Configure(INancyEnvironment environment)
        {
            // https://github.com/NancyFx/Nancy/issues/2632
            // Nancy.Json.JsonConfiguration.Default = new 

                // Nancy.Json.JsonConfiguration(

            //Nancy.Json.JsonConfiguration.Default.RetainCasing = true;
            //Nancy.Json.JsonSettings.RetainCasing = true;
            // base.Configure(environment);
            /*
            environment.Diagnostics(
                enabled: true,
                password: "password",
                path: "/_Nancy",
                cookieName: "__custom_cookie",
                slidingTimeout: 30,
                cryptographyConfiguration: CryptographyConfiguration.NoEncryption);
            */

            environment.Tracing(
                enabled: true,
                displayErrorTraces: true);
        }
    }

    // http://www.dotnetjalps.com/2017/01/nacyfx-with-aspnet-core.html
    // https://www.hanselman.com/blog/ExploringAMinimalWebAPIWithNETCoreAndNancyFX.aspx
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => "Hello from Nancy running on CoreCLR");
            Get("/test/{name}", args => new Person() { Name = args.name });
            Get("/testy/{name}", delegate (dynamic args){
                return Response.AsJson(new Person() { Name = args.name });
            });
        }
    }


    public class Person
    {
        public string Name { get; set; }
    }


}
