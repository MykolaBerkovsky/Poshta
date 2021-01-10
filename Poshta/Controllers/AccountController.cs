using Poshta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Poshta.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                USER user = null;
                using (ModelDB db = new ModelDB())
                {
                    user = db.USER.FirstOrDefault(u => u.login == model.Login && u.password == model.Password && u.stan_u == 1);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.contact_number.ToString(), true);
                    if(user.id_role == 1)
                    {
                        return RedirectToAction("Index", "PACKAGEs1");
                    }
                    else if (user.id_role == 2)
                    {
                        return RedirectToAction("Zapit1", "MENEGER");
                    }
                    else if (user.id_role == 3)
                    {
                        return RedirectToAction("Index", "NAKLADNAs");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Sorry, unrecognized login or password or user is liberated.");
                }
            }

            return View(model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}