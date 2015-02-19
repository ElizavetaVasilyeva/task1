using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;

namespace task1
{
    /// <summary>
    /// Summary description for LoginHandler
    /// </summary>
    public class LoginHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var user = context.Request.Params["email"].ToString();
            var pwd = context.Request.Params["pwd"].ToString();

            using (var db = new EntityModel())
            {
                var query = (from us in db.Users
                             where us.Email.Equals(user)
                             select new { password = us.Password }).SingleOrDefault();
                if (query != null && Crypto.VerifyHashedPassword(query.password, pwd))
                {
                    //context.User = new GenericPrincipal(new GenericIdentity(user), new string[0]);
                   //var principal=new GenericPrincipal(new GenericIdentity(user),null);
                   //Thread.CurrentPrincipal = principal;
                   //if (HttpContext.Current != null)
                   //{
                   //    HttpContext.Current.User = principal;
                   //}
                    //FormsAuthentication.SetAuthCookie(user, false);
                    MyAuthentication.AuthenticateUser(user, null);

                    //HttpContext.Current.Response.Redirect(MyAuthentication.RedirectUrl);
                    context.Response.Write("success");
                    //context.Response.Redirect("~/HomePage.html");
                }
                else
                {
                    context.Response.Write("fail");
                }
            }
            //MyAuthentication.AuthenticateUser(user, null);

            //HttpContext.Current.Response.Redirect(MyAuthentication.RedirectUrl);
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