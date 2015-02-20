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
            try
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
                        MyAuthentication.AuthenticateUser(user, null);
                        context.Response.Write("success");
                    }
                    else
                    {
                        context.Response.Write("fail");
                    }
                }
            }
            catch (NullReferenceException)
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