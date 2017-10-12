
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;


namespace TestCoreNanny
{


    public static class HttpRoutingExtensions
    {


        public static Task WriteJson<T>(this Microsoft.AspNetCore.Http.HttpResponse response, T obj)
        {
            response.ContentType = "application/json";
            return response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        } // End Function MapAny 


        public static IRouteBuilder MapAny(this IRouteBuilder builder, string template, System.Func<HttpRequest, HttpResponse, RouteData, Task> handler)
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
        } // End Function MapAny 


        public static IRouteBuilder MapGetPost(this IRouteBuilder builder, string template, System.Func<HttpRequest, HttpResponse, RouteData, Task> handler)
        {
            return builder
                .MapVerb(System.Net.Http.HttpMethod.Get.ToString(), template, handler)
                .MapVerb(System.Net.Http.HttpMethod.Post.ToString(), template, handler)
            ;
        } // End Function MapGetPost 


    } // End Class HttpExtensions 


} // End Namespace TestCoreNanny 
