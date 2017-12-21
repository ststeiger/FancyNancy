// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace TestCoreNanny
{
  public class HomeController : Controller
  {
        public ContentResult Index(string id, string Controller, string subdomain, string aCtIoN)
        {
            string message = $@"<!DOCTYPE html>
<html>
<head>
  <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
  <meta charset=""utf-8"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
  <title>LoL</title>
</head>
<body>
    <h1>{subdomain}</h1>
    <h1>{Controller}</h1>
    <h1>{aCtIoN}</h1>
    <h1>{id}</h1>
</body>
</html>
";


            return Content(message, "text/html");
        }


        public ActionResult About()
        {
            return this.View();
        }
  }
}