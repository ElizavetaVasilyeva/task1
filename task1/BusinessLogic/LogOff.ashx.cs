using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task1
{
    /// <summary>
    /// Summary description for LogOff
    /// </summary>
    public class LogOff : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Request.Cookies["userName"] != null)
            {
                HttpContext.Current.Response.Cookies["userName"].Expires = DateTime.Now.AddHours(-2);
            }
            context.Response.Write("success");
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