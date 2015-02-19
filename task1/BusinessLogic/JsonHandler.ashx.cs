using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task1
{
    /// <summary>
    /// Summary description for JsonHandler
    /// </summary>
    public class JsonHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.WriteFile("~/App_Data/json1.json");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}