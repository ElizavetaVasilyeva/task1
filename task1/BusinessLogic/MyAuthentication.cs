using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace task1
{
    public class MyAuthentication
    {
        private static string userNameStr = "userName";

        public static void AuthenticateUser(string userName, string[] roles)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(userNameStr, userName));
            HttpContext.Current.Response.Cookies[userNameStr].Expires = DateTime.Now.AddHours(2);
        }

        public static IPrincipal GetCurrentUser()
        {
            if (HttpContext.Current.Request.Cookies[userNameStr] != null)
            {
                return new GenericPrincipal(new GenericIdentity(HttpContext.Current.Request.Cookies[userNameStr].Value), null);
            }
            else
            {
                return null;
            }
        }
    }
}