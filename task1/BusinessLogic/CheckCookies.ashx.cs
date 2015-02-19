using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task1
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class CheckCookies : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var currentName = MyAuthentication.GetCurrentUser();
            if (currentName != null) context.Response.Write(currentName.Identity.Name);
            else
            {
                context.Response.Write("empty");
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