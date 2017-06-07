using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPasswordManager.Models;

namespace WebPasswordManager.Controllers
{
    public class LogOnController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index ( Models.LogOn model )
        {
            if(!model.ValidatePassword())
            {
                ModelState.AddModelError(nameof(model.Password), "Invalid Password");
                return View();
            }

            MvcApplication.Password = model.Password;
            return RedirectToAction("Index", "PasswordAccount");
        }
    }
}