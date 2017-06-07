using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPasswordManager.Models
{
    public class SecurityQuestion
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int AccountId { get; set; }
        public int Id { get; set; }
    }
}