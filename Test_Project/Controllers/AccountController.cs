using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test_Project.Models;

namespace Test_Project.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        AccountOperation AccountOperation;
        //
        // GET: /Account/
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignUp(Account model)
        {
            AccountOperation = new AccountOperation();
            List<Account> UserDtl = new List<Account>();
            UserDtl = AccountOperation.GetUserDetail();

            if (UserDtl.Where(x => x.Email == model.Email).Select(x => x).Count() != 0)
            {
                ModelState.AddModelError("", "Email Alredy Exist!!");
                return View();
            }
            else if (model.Pass != model.ConfirmePass)
            {
                ModelState.AddModelError("", "PassWord Not Match");
                return View();
            }
            else
            {
                AccountOperation.InsertUser(model);
                return RedirectToAction("LogIn");
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Account model)
        {

            AccountOperation = new AccountOperation();
            List<Account> UserDtl = AccountOperation.GetUserDetail();
            if (UserDtl.Where(x => x.UserName == model.UserName && x.Pass == model.Pass).Select(x => x).Count() > 0)
            {
                Session["UserId"] = UserDtl.Where(x => x.UserName == model.UserName && x.Pass == model.Pass).Select(x => x.Id).FirstOrDefault();
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("Detail");
            }
            else
            {
                ModelState.AddModelError("", "Invalid User Name or Password !!");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Detail()
        {
            AccountOperation = new AccountOperation();
            return View(AccountOperation.GetDetails(Convert.ToInt32(Session["UserId"])));
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}