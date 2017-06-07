using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPasswordManager.Models;
using WebPasswordManager.Repositories;
using Newtonsoft.Json;

namespace WebPasswordManager.Controllers
{
    public class PasswordAccountController : Controller
    {
        IPManger passwordManager = new PManager();
        // GET: PasswordAccount
        public ActionResult Index()        {
            
            PasswordAccounts model = new PasswordAccounts();

            model.Accounts = passwordManager.GetAllAccounts();

            return View(model);
        }

        public ActionResult GetViewAccountById(int id)
        {
            var account = passwordManager.GetAccountByID(id);

            return View(account);
        }

        public ActionResult RemoveAccount(int id)
        {
            passwordManager.DeleteAccount(id);

            return RedirectToAction("Index", "PasswordAccount");
        }

        [HttpGet]
        public ActionResult AddAccount()
        {
            var model = new PasswordAccount();
            return View("AddPasswordAccount", model);
        }

        [HttpGet]
        public ActionResult EditAccount(int id)
        {
            var model = passwordManager.GetAccountByID(id);
            return View("EditPasswordAccount", model);
        }

        [HttpPost]
        public ActionResult SaveAccount(PasswordAccount model)
        {
            passwordManager.SaveAccount(model);
            return View("Index");
        }
    }
}