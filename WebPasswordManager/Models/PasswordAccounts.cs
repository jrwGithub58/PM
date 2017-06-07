using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPasswordManager.Models
{
    public class PasswordAccounts
    {
        public IEnumerable<PasswordAccount> Accounts { get; set; }
    }
}