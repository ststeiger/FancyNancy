
using Nancy;


namespace TestCoreNanny
{


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
