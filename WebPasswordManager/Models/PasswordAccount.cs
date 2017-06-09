using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebPasswordManager.Models
{
    public class PasswordAccount
    {
        public int Id { get; set; }
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }
        public string Url { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Display(Name = "Account Owner")]
        public string AccountOwner { get; set; }
        [Display(Name = "Security Questions")]
        public string SecurityQuestions { get; set; }
    }
}