using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;



using Microsoft.Extensions.DependencyInjection;


namespace TestCoreNanny
{

    /*
    public class AreaRouter : MvcRouteHandler, IRouter
    {
        public AreaRouter()
        { }


        public new async Task RouteAsync(RouteContext context)
        {
            string url = context.HttpContext.Request.Headers["HOST"];
            var splittedUrl = url.Split('.');

            if (splittedUrl.Length > 0 && splittedUrl[0] == "admin")
            {
                context.RouteData.Values.Add("area", "Admin");
            }

            await base.RouteAsync(context);
        }
    }
    */

    public class HostRouteData : RouteData
    {

        public HostRouteData(IApplicationBuilder app)
        {
            this.Routers.Clear();

            MvcRouteHandler rh = app.ApplicationServices.
               GetRequiredService<MvcRouteHandler>();

            this.Routers.Add(new CustomRouteHandler(app));
        }
    }



    // https://stackoverflow.com/questions/40512403/area-and-subdomain-routing
    // https://blog.maartenballiauw.be/post/2009/05/20/aspnet-mvc-domain-routing.html
    // http://benjii.me/2015/02/subdomain-routing-in-asp-net-mvc/
    public class CustomRouteHandler : IRouter
    {

        MvcRouteHandler m_routeHandler;


        public CustomRouteHandler(IApplicationBuilder app)
        {
            System.Console.WriteLine("in the ctors");

            this.m_routeHandler = app.ApplicationServices.
               GetRequiredService<MvcRouteHandler>();
        }

        // https://stackoverflow.com/questions/278668/is-it-possible-to-make-an-asp-net-mvc-route-based-on-a-subdomain
        VirtualPathData IRouter.GetVirtualPath(VirtualPathContext context)
        {
            if (context.HttpContext.Request.Host != null
                && context.HttpContext.Request.Host.HasValue)
            { 
                string subd = context.HttpContext.Request.Host.Value;
                context.Values["subdomain"] = subd;
            }

            return this.m_routeHandler.GetVirtualPath(context);
        }

        Task IRouter.RouteAsync(RouteContext context)
        {
            /*
            string url = context.HttpContext.Request.Headers["HOST"];
            string[] splittedUrl = url.Split('.');

            if (splittedUrl.Length > 0 && splittedUrl[0] == "admin")
            {
                context.RouteData.Values.Add("subdomain", "Admin");
            }
            */
            if (context.HttpContext.Request.Host != null && context.HttpContext.Request.Host.HasValue)
            {
                context.RouteData.Values.Add("subdomain", context.HttpContext.Request.Host.Value);
            }

            return this.m_routeHandler.RouteAsync(context);
        }
    }
    
    
    public class SubdomainRoute : RouteBase
    {

        public SubdomainRoute(string template, string name, IInlineConstraintResolver res) 
            :base(template, name, res, null,null, null)
        { }

        private MvcRouteHandler m_defaultHandler;

        protected override Task OnRouteMatched(RouteContext context)
        {
            throw new NotImplementedException();
        }

        protected override VirtualPathData OnVirtualPathGenerated(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public void aSubdomainRoute(IApplicationBuilder app)
        {
            this.m_defaultHandler = app.ApplicationServices.
                GetRequiredService<MvcRouteHandler>();
        }

        public override Task RouteAsync(RouteContext context)
        {
            //return base.RouteAsync(context);

            var httpContext = context.HttpContext;

            // httpContext.Request.Path.Value

            if (httpContext.Request == null
                || httpContext.Request.Host == null
                || !httpContext.Request.Host.HasValue
               )
            {
                return null;
            }




            string host = httpContext.Request.Host.Value;
            var index = host.IndexOf('.');

            // httpContext.Request.Path.

            string[] segments = null; // httpContext.Request.Url.PathAndQuery.TrimStart('/').Split('/');

            if (index < 0)
            {
                return null;
            }

            var subdomain = host.Substring(0, index);
            string[] blacklist = { "www", "yourdomain", "mail" };

            if (blacklist.Contains(subdomain))
            {
                return null;
            }

            string controller = (segments.Length > 0) ? segments[0] : "Home";
            string action = (segments.Length > 1) ? segments[1] : "Index";


            //var rd = new RouteData(this.m_defaultHandler);
            // var rd = new RouteData(this);



            // var routeData = new RouteData(this, new MvcRouteHandler());


            // var x = new MvcRouteHandler(ActionInvokerFactory,;

            /*
            routeData.Values.Add("controller", controller); //Goes to the relevant Controller  class
            routeData.Values.Add("action", action); //Goes to the relevant action method on the specified Controller
            routeData.Values.Add("subdomain", subdomain); //pass subdomain as argument to action method
            */
            //return routeData;

            return Task.CompletedTask;
        }

        public override VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            // return base.GetVirtualPath(context);
            return null;
        }


    }


}
