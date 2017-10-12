
using Nancy;
using Nancy.Configuration;


namespace TestCoreNanny
{


    public class NannyBootstrapper : DefaultNancyBootstrapper
    {


        // Override with a valid password(albeit a really really bad one!)
        // to enable the diagnostics dashboard
        public override void Configure(INancyEnvironment environment)
        {
            // https://github.com/NancyFx/Nancy/issues/2632
            // Nancy.Json.JsonConfiguration.Default = new 

            // Nancy.Json.JsonConfiguration(

            //Nancy.Json.JsonConfiguration.Default.RetainCasing = true;
            //Nancy.Json.JsonSettings.RetainCasing = true;
            // base.Configure(environment);
            /*
            environment.Diagnostics(
                enabled: true,
                password: "password",
                path: "/_Nancy",
                cookieName: "__custom_cookie",
                slidingTimeout: 30,
                cryptographyConfiguration: CryptographyConfiguration.NoEncryption);
            */

            environment.Tracing(
                enabled: true,
                displayErrorTraces: true);
        }


    }


}
