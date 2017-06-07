using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPasswordManager.Models
{
    public class PasswordAccount
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AccountOwner { get; set; }
        public List<SecurityQuestion> SecurityQuestions { get; set; } = new List<SecurityQuestion>();
    }
}