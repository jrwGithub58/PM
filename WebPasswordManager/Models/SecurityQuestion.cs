using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPasswordManager.Models
{
    public class SecurityQuestion
    {
        int Id { get; set; }
        string Question { get; set; }
        string Answer { get; set; }
        int AccountId { get; set; }
    }
}