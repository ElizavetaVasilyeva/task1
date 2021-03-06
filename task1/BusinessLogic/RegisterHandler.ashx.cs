﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Web.Helpers;
using System.Web.Security;


namespace task1
{
    /// <summary>
    /// Summary description for RegisterHandler
    /// </summary>
    public class RegisterHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var user = context.Request.Params["email"].ToString();
                var pwd = context.Request.Params["pwd"].ToString();

                var newUser = new User
                {
                    Email = user,
                    Password = Crypto.HashPassword(pwd)
                };

                using (var db = new EntityModel())
                {
                    try
                    {
                        db.Users.Add(newUser);
                        db.SaveChanges();
                    }
                    catch
                    {
                        context.Response.Write("fail");
                    }
                };
                context.Response.Write("success");
            }
            catch(NullReferenceException)
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