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
        #region IHttpModule Memberss

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += AuthenticateRequest;
        }

        #endregion

        public void AuthenticateRequest(Object source, EventArgs e)
        {
            HttpContext.Current.User = MyAuthentication.GetCurrentUser();
        }
    }
}
