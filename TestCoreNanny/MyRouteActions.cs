
using System.Reflection;
using System.Threading.Tasks;


using System.Threading;


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;


namespace TestCoreNanny
{


    public static class MyRouteActions
    {
        static MyRouteActions()
        { }


        // https://stackoverflow.com/questions/39674988/how-to-call-a-generic-async-method-using-reflection
        public static async Task<object> InvokeAsync(this MethodInfo @this
                                                     , object obj
                                                     , params object[] parameters)
        {
            dynamic awaitable = @this.Invoke(obj, parameters);
            await awaitable;

            return awaitable.GetAwaiter().GetResult();
        }


        // https://stackoverflow.com/questions/20350397/how-can-i-tell-if-a-c-sharp-method-is-async-await-via-reflection
        private static bool IsAsyncMethod(System.Type classType, string methodName)
        {
            // Obtain the method with the specified name.
            System.Reflection.MethodInfo method = classType.GetMethod(methodName);

            System.Type attType = typeof(System.Runtime.CompilerServices.AsyncStateMachineAttribute);

            // Obtain the custom attribute for the method. 
            // The value returned contains the StateMachineType property. 
            // Null is returned if the attribute isn't present for the method. 
            System.Runtime.CompilerServices.AsyncStateMachineAttribute attrib =
                (System.Runtime.CompilerServices.AsyncStateMachineAttribute)
                method.GetCustomAttribute(attType);

            return (attrib != null);
        }


        public static async Task DoSomething(Microsoft.AspNetCore.Http.HttpContext c)
        {

            string message = @"<!DOCTYPE html>
<html>
<head>
  <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
  <meta charset=""utf-8"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
  <title>LoL</title>
</head>
<body>
    <h1>howdy</h1>
</body>
</html>
";

            // message = "howdy dude";
            // c.Response.StatusCode = 200;
            c.Response.ContentType = "text/plain";


            // var ba = System.Text.Encoding.UTF8.GetBytes(message);
            // c.Response.Body.Write(ba, 0, ba.Length);
            // return Task.CompletedTask;


            await c.Response.WriteAsync(message);

            await Sleeper(c);

            // 
            await c.Response.WriteAsync(message);
        }


        public static async Task Sleeper(HttpContext c)
        { 
            // System.Threading.Thread.Sleep(10000);
            await System.Threading.Tasks.Task.Delay(1000);

            await c.Response.WriteAsync("hello");
            await Task.CompletedTask;
        }

    }


}
