using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                context.Response.WriteFile("~/App_Data/json1.json");
            }
            catch(IOException)
            {
                context.Response.StatusCode = 400;
            }
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