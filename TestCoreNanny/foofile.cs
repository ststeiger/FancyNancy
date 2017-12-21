
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

#if false

    public static class Foofile
    {
        

        public static IApplicationBuilder UseMvc(this IApplicationBuilder app)
        {
            return app.UseMvc(routes =>
            {
            });
        } // End Function UseMvc 



        public static IApplicationBuilder UseMvc(
            this IApplicationBuilder app,
            System.Action<IRouteBuilder> configureRoutes)
        {
            // Throws if service is not registered.
            // MvcServicesHelper.ThrowIfMvcNotRegistered(app.ApplicationServices);


            // Verify if AddMvc was done before calling UseMvc
            // We use the MvcMarkerService to make sure if all the services were added.

            RouteBuilder routeBuilder = new RouteBuilder(app)
            {
                DefaultHandler = new CustomRouter("LoL"),
                // ServiceProvider = app.ApplicationServices
            };

            // app.ApplicationServices.GetRequiredService<IInlineConstraintResolver>();

            IInlineConstraintResolver requiredService = 
                routeBuilder.ServiceProvider.GetRequiredService<IInlineConstraintResolver>();

            routeBuilder.Routes.Add((IRouter)
                                    new Route(routeBuilder.DefaultHandler
                                              , "hell/{id?}", requiredService)
            );
            

            routeBuilder.Routes.Add((IRouter)
                                    new Route(routeBuilder.DefaultHandler
                                        , "MyRoute", "hellis/{id?}"
                                        , new RouteValueDictionary("defaults")
                                        , // (IDictionary<string, object>)
                                          new RouteValueDictionary("constraints")
                                        , new RouteValueDictionary("dataTokens")
                                        , requiredService
                                    )
            );
            
            // return app.UseRouter(routeBuilder.Build());
            return app;
        } // End Function UseMvc 


    } // End Class

    #endif 

} // End Namespace 
