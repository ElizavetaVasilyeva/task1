using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace task1
{
    public class AuthModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += AuthenticateRequest;
            //context.EndRequest += EndRequest;
        }

        #endregion

        public void AuthenticateRequest(Object source, EventArgs e)
        {
            // HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;
            //string filePath = context.Request.FilePath;
            //string fileExtension =
            //    VirtualPathUtility.GetExtension(filePath);
            HttpContext.Current.User = MyAuthentication.GetCurrentUser();
            //if (!fileExtension.Equals(".html"))
            //{
            //}
            //else{
            //    context.Response.Write(user.Identity.Name);
            //}
            //}
            //else
            //{
            //    context.Response.Write("savecookie");
            //}
            // var app = (HttpApplication)source;
            // var context = app.Context;
            //if(context.User==null)
            //{ 
            //    var auth = context.Request.Cookies.Get("AUTH");
            //    if (auth != null && !string.IsNullOrWhiteSpace(auth.Value))
            //    {
            //        var ticket = FormsAuthentication.Decrypt(auth.Value);
            //        context.User=new GenericPrincipal(new GenericIdentity(user), new string[0]);
            //    }
            //}
            // context.Response.Cookies.Set(CreateCookie(user));
            //string userName;
            //var context = HttpContext.Current;
            //var formAuthCookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            //var isAuthenticated = context.Request.IsAuthenticated;

            //if (isAuthenticated || formAuthCookie != null)
            //{
            //    if (!isAuthenticated)
            //    {
            //        var ticket = FormsAuthentication.Decrypt(formAuthCookie.Value);
            //        userName = ticket.Name;
            //    }
            //    else
            //    {
            //        userName = context.User.Identity.Name;
            //    }

            //    var prin = (IPrincipal)context.Cache[userName];

            //    if (prin != null)
            //    {
            //        context.User = prin;
            //    }
            }
            //var context = HttpContext.Current;
            //var request = HttpContext.Current.Request;
            //    HttpCookie authCookie = request.Cookies[FormsAuthentication.FormsCookieName];
            //    if (authCookie != null)
            //    {
            //        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            //         //var roles = authTicket.UserData.Split('|');
            //        var user = new GenericPrincipal(context.User.Identity, null);
            //        context.User = Thread.CurrentPrincipal = user;
        //HttpApplication app = (HttpApplication)source;
        //HttpContext context = app.Context;
        //if (!CustomAuthenticationProvider.Authenticate(app.Context))
        //{
        //    app.Context.Response.Status = "401 Unauthorized";
        //    app.Context.Response.StatusCode = 401;
        //    app.Context.Response.End();
        //} 

        // HttpContext context = ((HttpApplication)sender).Context;

        //if (context.User != null)
        //    return;
   HttpCookie CreateCookie(string login)
        {
            var ticket = new FormsAuthenticationTicket(1,
       login,
       DateTime.Now,
       DateTime.Now.Add(FormsAuthentication.Timeout),
       false,
       string.Empty,
       FormsAuthentication.FormsCookiePath);
            var encrypt = FormsAuthentication.Encrypt(ticket);
            var auth = new HttpCookie("AUTH")
            {
                Value = encrypt,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };
            return auth;
        }

        public void EndRequest(Object source, EventArgs e)
        {

        }
    }
}
