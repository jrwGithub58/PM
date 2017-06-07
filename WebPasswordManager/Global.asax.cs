using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebPasswordManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string Password = null;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Repositories.SqlitePasswordManager.Init();
            //SetSecurityFile("password123");
        }

        protected void SetSecurityFile(string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            string salt = crypto.GenerateSalt();
            string hash = crypto.Compute(password, salt);

            string path = Server.MapPath("~/App_Data/Security.txt");
            File.WriteAllLines(path, new[] { salt, hash });
        }
    }
}
