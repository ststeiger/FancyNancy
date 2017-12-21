
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;


namespace TestCoreNanny
{


    // When in doubt, have a look at 
    // Microsoft.AspNetCore.Mvc.Internal.MvcRouteHandler : IRouter
    public class CustomRouter : IRouter
    {
        private string _name;


        public CustomRouter() : this("Unnamed Router")
        { } // End Constructor  


        public CustomRouter(string name)
        {
            _name = name;
        } // End Constructor 


        VirtualPathData IRouter.GetVirtualPath(VirtualPathContext context)
        {
            throw new System.NotImplementedException();
        } // End Function GetVirtualPath 


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
            
            context.Handler =  new RequestDelegate(
                delegate (HttpContext c)
                {
                    System.Reflection.MethodInfo tt = 
                    typeof(MyRouteActions).GetMethod("DoSomething", 
                                                       System.Reflection.BindingFlags.Public 
                                                     | System.Reflection.BindingFlags.Static);
                    
                    Task t = (Task)tt.Invoke(null, new object[] { c });
                    // t.Wait();
                    return t;
                    
                    // Task a = (Task) tt.InvokeAsync(null, c);
                    // await a;
                    // return Task.CompletedTask;
                    
                    // await MyRouteActions.InvokeAsync(tt, null, c);
                }
            );

            /*
            context.Handler = new RequestDelegate(
                async delegate (HttpContext c)
                {
                    // c.Response.StatusCode = 200;
                    c.Response.ContentType = "text/html";
                    // context.HttpContext.Response.WriteAsync(message);
                    await c.Response.WriteAsync(message);


                    // await context.HttpContext.Response.WriteAsync(message);

                    // For when delegate isn't async 
                    //return Task.CompletedTask;
                }
            );
            */

            // context.HttpContext.Response.ContentType = "text/html";
            // context.HttpContext.Response.WriteAsync(message);
            // context.HttpContext.Response.StatusCode = 200;


            // https://stackoverflow.com/questions/32582232/imlementing-a-custom-irouter-in-asp-net-5-vnext-mvc-6

            return Task.CompletedTask;
        } // End Function RouteAsync 


    } // End Class CustomRouter 


} // End Namespace TestCoreNanny 
