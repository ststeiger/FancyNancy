
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


// using Microsoft.Extensions.Logging;
// using Microsoft.AspNetCore.Owin;


using Nancy.Owin;

using Microsoft.AspNetCore.Routing;

// using Microsoft.AspNetCore.Mvc.Internal;


// https://codeopinion.com/why-use-nancy/

namespace TestCoreNanny
{


    public class Startup
    {


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddMvc();
        } // End Sub ConfigureServices 


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } // End if (env.IsDevelopment()) 


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
            // app.UseOwin(x => { x.UseNancy(); });
            /*
            app.UseOwin(
                 delegate (
                     System.Action<
                        System.Func<
                              System.Func<IDictionary<string, object>, Task>
                            , System.Func<IDictionary<string, object>, Task>
                            >
                     > pipeline)
                 {
                     pipeline.UseNancy();
                 }
            );
            */

            app.UseStatusCodePages();


            /*
            app.UseMvc(delegate (Microsoft.AspNetCore.Routing.IRouteBuilder r)
            { 
                r.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
                
            });
            */

            // https://www.strathweb.com/2017/01/building-microservices-with-asp-net-core-without-mvc/
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing
            app.UseRouter(
                delegate(Microsoft.AspNetCore.Routing.IRouteBuilder r)
                {
                    IInlineConstraintResolver requiredService =
                        r.ServiceProvider.GetRequiredService<IInlineConstraintResolver>();

                    // r.DefaultHandler = new CustomRouter();

                    
                    r.Routes.Add( // (IRouter)
                                 new Route( new CustomRouter("LoL")
                                           , "lol/{id?}", requiredService)
                    );
                    
                    System.Console.WriteLine("hello");
                r.Routes.Add(new Route(new CustomRouteHandler(app)
                                       , "{controller=Home}/{action=Index}/{id?}"
                                       , requiredService));





                    InMemoryContactRepository contactRepo = new InMemoryContactRepository();
                    
                    r.MapGet("contacts", async (request, response, routeData) =>
                    {
                        string[] contacts = await contactRepo.GetAll();
                        await response.WriteJson(contacts);
                    });
                    
                    r.MapGetPost("hello", async (request, response, routeData) =>
                    {
                        string[] contacts = await contactRepo.GetAll();
                        await response.WriteJson(contacts);
                    });

                    // r.MapRoute("nae", "template", "default", "constrains", "tokens");
                } // End delegate(Microsoft.AspNetCore.Routing.IRouteBuilder r) 
                
            ); // End UseRouter 


            //de.stack.com / API
            //fr.stack.com / API

            //de.stack.com /static
            //de.stack.com/static
            
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

        } // End Sub Configure 


    } // End Class Startup 


} // End Namespace TestCoreNanny 
