using PMP.AppServices.DTO_s.AccountDTO_s;
using PMP.AppServices.Services;
using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagementPlatform.Controllers
{
    public class AccountController : Controller
    {
        AccountService _accService;
        public AccountController()
        {   
            //make instance of AccountService passing context
            PMPDBEntities context = new PMPDBEntities();
            _accService = new AccountService(context);
        }
        //
        // GET: /Account/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDTO dto)
        {
            ViewBag.Error = null;
            if (ModelState.IsValid)
            {
                var login = _accService.Login(dto);
                //if user is loged in send him to welcome page
                if (login)
                    return RedirectToAction("Welcome", "Home");

                //else return error message to view
                ViewBag.Error = "Username or password not correct";
                return View();
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterDTO dto)
        {
            if (ModelState.IsValid) {
                ViewBag.Error = null;
                if (_accService.Register(dto))
                {
                    return RedirectToAction("Login");
                };

                ViewBag.Error = "User exists, please try with different Username or Password";
                return View(dto);
            }
            return View(dto);
        }

        public ActionResult SignOut()
        {
            _accService.SignOut();

            return RedirectToAction("Login");
        }

	}
}