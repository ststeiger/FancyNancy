
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Logging;
// using Microsoft.AspNetCore.Owin;



using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;


namespace TestCoreNanny
{


    public class LoLRouteHandler : IRouter
    {
        private string _name;

        public LoLRouteHandler() : this("LoL")
        { }

        public LoLRouteHandler(string name)
        {
            _name = name;
        }

        VirtualPathData IRouter.GetVirtualPath(VirtualPathContext context)
        {
            throw new System.NotImplementedException();
        }


        // async 
        Task IRouter.RouteAsync(RouteContext context)
        {
            // Just adding values...
            // http://azurecoder.net/2017/07/09/routing-middleware-custom-irouter/
            string requestpath = context.HttpContext.Request.Path.Value;




            string routeValues = string.Join("", context.RouteData.Values);
            string message = string.Format("{0} Values={1} ", _name, routeValues);

            message = @"<!DOCTYPE html>
<html>
<head>
  <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
  <meta charset=""utf-8"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
  <title>LoL</title>
</head>
<body>
    <h1>LoL</h1>
</body>
</html>
";
            
            context.HttpContext.Response.ContentType = "text/html";
            context.HttpContext.Response.WriteAsync(message);
            context.HttpContext.Response.StatusCode = 200;


            // https://stackoverflow.com/questions/32582232/imlementing-a-custom-irouter-in-asp-net-5-vnext-mvc-6


            return Task.CompletedTask;
        }

    }



    public static class Foofile
    {
        

        public static IApplicationBuilder UseMvc(this IApplicationBuilder app)
        {
            return app.UseMvc(routes =>
            {
            });
        }



        public static IApplicationBuilder UseMvc(
            this IApplicationBuilder app,
            System.Action<IRouteBuilder> configureRoutes)
        {
            // MvcServicesHelper.ThrowIfMvcNotRegistered(app.ApplicationServices);


            // Verify if AddMvc was done before calling UseMvc
            // We use the MvcMarkerService to make sure if all the services were added.

            var routeBuilder = new RouteBuilder(app)
            {
                DefaultHandler = new LoLRouteHandler("LoL"),
                // ServiceProvider = app.ApplicationServices
            };


            // app.ApplicationServices.GetRequiredService<IInlineConstraintResolver>();

            IInlineConstraintResolver requiredService = 
                routeBuilder.ServiceProvider.GetRequiredService<IInlineConstraintResolver>();

            routeBuilder.Routes.Add((IRouter)
                                    new Route(routeBuilder.DefaultHandler
                                              , "hell/{id}", requiredService)
                                   );

            /*
            routeBuilder.Routes.Add((IRouter)
                                    new Route(routeBuilder.DefaultHandler
                                              , "MyRoute", "hell/{id}"
                                        , new RouteValueDictionary("defaults")
                                        , (IDictionary<string, object>)
                                        new RouteValueDictionary("constraints")
                                        , new RouteValueDictionary("dataTokens")
                                        , requiredService
                                    )
            );
            */



            /*
            configureRoutes(routes);

            // Adding the attribute route comes after running the user-code because
            // we want to respect any changes to the DefaultHandler.
            routes.Routes.Insert(0, AttributeRouting.CreateAttributeMegaRoute(
                routes.DefaultHandler,
                app.ApplicationServices));
            */

            //var y = new Microsoft.AspNetCore.Routing.Template.;

            // var x = new RouteCollection();



            return app.UseRouter(routeBuilder.Build());
        }

    }
}
