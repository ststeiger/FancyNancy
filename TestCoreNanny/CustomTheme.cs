
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Remotion.Linq.Clauses;


namespace TestCoreNanny
{
    
    
    public class CustomTheme
    : Microsoft.AspNetCore.Mvc.Razor.IViewLocationExpander
    {
        

        /// <summary>
        /// Invoked by a <see cref="T:Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine" /> to determine the values that would be consumed by this instance
        /// of <see cref="T:Microsoft.AspNetCore.Mvc.Razor.IViewLocationExpander" />. The calculated values are used to determine if the view location
        /// has changed since the last time it was located.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Razor.ViewLocationExpanderContext" /> for the current view location
        /// expansion operation.</param>
        void IViewLocationExpander.PopulateValues(Microsoft.AspNetCore.Mvc.Razor.ViewLocationExpanderContext context)
        {
            
        }




        /// <summary>
        /// Invoked by a <see cref="T:Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine" /> to determine potential locations for a view.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Razor.ViewLocationExpanderContext" /> for the current view location
        /// expansion operation.</param>
        /// <param name="viewLocations">The sequence of view locations to expand.</param>
        /// <returns>A list of expanded view locations.</returns>
        IEnumerable<string> IViewLocationExpander.ExpandViewLocations(
            ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            
            return viewLocations;
        }



    } // End CLass



    
    public class AppTenant
    {
        public string Name { get; set; }
        public string[] Hostnames { get; set; }
        public string Theme { get; set; }
        public string ConnectionString { get; set; }
    }
    
    
    public static class foo
    {
        public static T GetTenant<T>(this HttpContext context) where T : new()
        { 
            return new T();
        }

    }
    
    
    public class TenantViewLocationExpander : IViewLocationExpander
    {
        private const string THEME_KEY = "theme";

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values[THEME_KEY] = context.ActionContext.HttpContext
                .GetTenant<AppTenant>()?.Theme;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context
            , IEnumerable<string> viewLocations)
        {
            string theme = null;
            if (context.Values.TryGetValue(THEME_KEY, out theme))
            {
                viewLocations = new[] {
                        $"/Themes/{theme}/{{1}}/{{0}}.cshtml",
                        $"/Themes/{theme}/Shared/{{0}}.cshtml",
                    }
                    .Concat(viewLocations);
            }


            // System.Collections.Generic.List<string> ls = null;
            // ls.Concat(ls);
            
            
            return viewLocations;
        }
    }
    
    
}