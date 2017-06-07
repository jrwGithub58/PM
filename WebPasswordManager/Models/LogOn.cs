using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebPasswordManager.Models
{
    public class LogOn
    {
        public string Password { get; set; }
        public bool ValidatePassword ()
        {
            if(String.IsNullOrWhiteSpace(this.Password))
            {
                return false;
            }

            var path = HttpContext.Current.Server.MapPath("~/App_Data/Security.txt");
            var f = File.ReadAllLines(path);

            var salt = f[0];
            var actualHash = f[1];

            var crypto = new SimpleCrypto.PBKDF2();
            var candidateHash = crypto.Compute(this.Password, salt);


            return crypto.Compare(actualHash, candidateHash);
        }
    }
}