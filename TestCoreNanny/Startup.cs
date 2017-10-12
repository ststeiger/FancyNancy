using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Owin;
using Nancy;
using Nancy.Owin;

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;



namespace TestCoreNanny
{
    public static class HttpExtensions
    {
        public static Task WriteJson<T>(this HttpResponse response, T obj)
        {
            response.ContentType = "application/json";
            return response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }



        public static IRouteBuilder MapAny(this IRouteBuilder builder, string template, Func<HttpRequest, HttpResponse, RouteData, Task> handler)
        {
            return builder
                .MapVerb(System.Net.Http.HttpMethod.Get.ToString(), template, handler)
                .MapVerb(System.Net.Http.HttpMethod.Post.ToString(), template, handler)
                .MapVerb(System.Net.Http.HttpMethod.Put.ToString(), template, handler)
                .MapVerb(System.Net.Http.HttpMethod.Delete.ToString(), template, handler)
                .MapVerb(System.Net.Http.HttpMethod.Head.ToString(), template, handler)
                .MapVerb(System.Net.Http.HttpMethod.Options.ToString(), template, handler)
                .MapVerb(System.Net.Http.HttpMethod.Trace.ToString(), template, handler)
            ;
        }


        public static IRouteBuilder MapGetPost(this IRouteBuilder builder, string template, Func<HttpRequest, HttpResponse, RouteData, Task> handler)
        {
            return builder
                .MapVerb(System.Net.Http.HttpMethod.Get.ToString(), template, handler)
                .MapVerb(System.Net.Http.HttpMethod.Post.ToString(), template, handler)
            ;
        }


    }


    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public class InMemoryContactRepository
        {
            public async Task<string[]> GetAll()
            {
                return await Task.FromResult("a,b,c".Split(','));
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors()
            //app.UseCors(
            //    delegate(Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder x )
            //    {
            //    }
            //);

            // https://www.codeproject.com/Articles/1122162/Implement-Owin-Pipeline-using-Asp-net-Core
            // https://stackoverflow.com/questions/11425095/benefits-of-using-nancyfx
            // http://blog.jonathanchannon.com/2012/12/19/why-use-nancyfx/
            // https://codeopinion.com/why-use-nancy/
            app.UseOwin(x => { x.UseNancy(); });

            /*
            app.UseOwin(
                delegate (Action<Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>> pipeline)
                {
                    pipeline.UseNancy();
                }
            );
            */

            // https://www.strathweb.com/2017/01/building-microservices-with-asp-net-core-without-mvc/
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing
            app.UseRouter(delegate(Microsoft.AspNetCore.Routing.IRouteBuilder r)
            {
                var contactRepo = new InMemoryContactRepository();

                r.MapGet("contacts", async (request, response, routeData) =>
                {
                    var contacts = await contactRepo.GetAll();
                    await response.WriteJson(contacts);
                });

                r.MapGetPost("hello", async (request, response, routeData) =>
                {
                    var contacts = await contactRepo.GetAll();
                    await response.WriteJson(contacts);
                });
            }
            );



            //de.stack.com / API
            //fr.stack.com / API

            //de.stack.com /static
            //de.stack.com/static







            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
