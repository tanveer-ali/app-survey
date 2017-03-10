using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace SurveyService.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
            : base("")
        {
            Get[@"/"] = parameters => Response.AsFile("index.html", "text/html");
        }
    }
}