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
// using Nancy.Owin;


namespace TestCoreNanny
{


    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => "Hello from Nancy running on CoreCLR");
            Get("/test/{name}", args => new Person() { Name = args.name });
        }
    }


    public class Person
    {
        public string Name { get; set; }
    }


}
