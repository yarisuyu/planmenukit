using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Menukit.Controllers
{
    public class AccountController : Controller
    {
        private IFormsAuth formsAuth;
        public AccountController(IFormsAuth formsAuth)
        {
            this.formsAuth = formsAuth;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Login()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string name, string password, string returnUrl)
        {
            if (formsAuth.Authenticate(name, password))
            {
                // Назначить место перенаправления по умолчанию,
                // если оно не назначено
                returnUrl = returnUrl ?? Url.Action("Index", "Admin");

                // Установить cookie-набор и перенаправить
                formsAuth.SetAuthCookie(name, false);
                return Redirect(returnUrl);
            }
            else
            {
                ViewData["lastLoginFailed"] = true;
                return View();
            }
        }
    }
}