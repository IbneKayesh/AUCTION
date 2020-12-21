using auction.Dal;
using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Controllers
{
    public class HomeController : Controller
    {
        User_DAL _d = new User_DAL();
        public ActionResult Index()
        {
            return View();
        }        
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(V_USER_LOGIN lgn, string redirect)
        {
            if (ModelState.IsValid)
            {
                bool isValidUser =_d.UserLogin(lgn);
                if (isValidUser)
                {
                    List<MNUP_MNUC> d = _d.SessionMenuList(lgn.USER_TEXT).ToList();
                    if (d != null)
                    {
                        Session["MenuList"] = d;
                        Session["UserId"] = lgn.USER_TEXT;
                    }
                    if (Url.IsLocalUrl(redirect) && redirect.Length > 1 && redirect.StartsWith("/") && !redirect.StartsWith("//") && !redirect.StartsWith("/\\"))
                    {
                        return Redirect(redirect);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            ViewBag.MSG="invalid user Id/password";
            return View(lgn);
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return Redirect("Login");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(V_USER_PASS_CHANGE UCP)
        {
            if (ModelState.IsValid)
            {
                string UserId = string.Empty;
                try
                {
                    UserId = Session["UserId"].ToString();
                }
                catch (Exception)
                {
                  return  RedirectToAction("Login");
                }
                if (!string.IsNullOrWhiteSpace(UserId))
                {
                    if (UCP.NEW_PASS == UCP.CONF_PASS && UCP.NEW_PASS.Length>=5)
                    {
                        UCP.USER_TEXT = UserId;
                        bool result = _d.ChangePassword(UCP);
                        if (result)
                        {
                            TempData["msg"] = "Swal.fire({position:'top-end',icon: 'success', title:'Successfully changed',  showConfirmButton: false,  timer: 1500})";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["msg"] = "Swal.fire({icon: 'error',title: 'sorry',text: 'your old Password not match!'})";
                            return View(UCP);
                        }
                    }
                }
            }
            TempData["msg"] = "Swal.fire({icon: 'error',title: 'sorry',text: 'Invalid user/password or old Password not match or Password requirements are not matched!'})";
            return View(UCP);
        }

    }
}