using auction.Dal;
using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Controllers
{
    public class SuperAdminController : Controller
    {
        User_DAL _d = new User_DAL();
        Common_DAL _c = new Common_DAL();

        [jAuth(MenuId = 951)]
        public ActionResult CreateUser()
        {
            return View();
        }
        [jAuth(MenuId = 951)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(T_USER U)
        {
            if (ModelState.IsValid)
            {
                _d.User(u: U);
                TempData["MSG"] = "Swal.fire('success','user created sucessfully : " + U.USER_TEXT + " - " + U.USER_NAME + "','success')";
                return Redirect("CreateUser");
            }
            return View(U);
        }
        [jAuth(MenuId = 951)]
        public ActionResult FindUser(string USER_TEXT)
        {
            List<T_USER> d = _d.UserList(USER_TEXT).ToList();
            if (d == null)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = d.ToList() }, JsonRequestBehavior.AllowGet);
        }


        [jAuth(MenuId = 952)]
        public ActionResult MenuUser()
        {
            return View();
        }
        [jAuth(MenuId = 952)]
        [HttpPost]
        public ActionResult MenuUser(string USER_TEXT, int MNUC_TEXT)
        {
            T_USMC um = new T_USMC();
            um.USER_TEXT = USER_TEXT;
            um.MNUC_TEXT = MNUC_TEXT;
            if (ModelState.IsValid)
            {
                bool result = _d.UserMenu(USER_TEXT, MNUC_TEXT);
                if (result)
                {
                    return Json(new { success = true, responseText = "Menu action sucessfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "Menu action failed" }, JsonRequestBehavior.AllowGet);
        }
        [jAuth(MenuId = 952)]
        public ActionResult FindMenu(string USER_TEXT)
        {
            List<V_MENU> d = _d.MenuList(USER_TEXT).ToList();
            if (d == null)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = d.ToList() }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult FindUserName(string USER_TEXT)
        {
            string d = _d.UserName(USER_TEXT);
            if (d == null)
            {
                return Json(new { data = "No user found" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = d }, JsonRequestBehavior.AllowGet);
        }


        [jAuth(MenuId = 953)]
        public ActionResult WhCoUser()
        {
            return View();
        }
        [jAuth(MenuId = 953)]
        public ActionResult FindWhCo(string USER_TEXT, string AMSP_FLAG)
        {
            List<V_AMSP_USER> d = _d.WhCoList(USER_TEXT,AMSP_FLAG).ToList();
            if (d == null)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = d.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [jAuth(MenuId = 953)]
        public ActionResult WhCoAction(string OID, string USER_TEXT)
        {
            V_AMSP_USER um = new V_AMSP_USER();
            um.WCRT_USER = USER_TEXT;
            um.AMSP_NAME = OID;
            if (ModelState.IsValid)
            {
                bool result = _d.UserWhCoAction(um);
                if (result)
                {
                    return Json(new { success = true, responseText = "Wh/Co action sucessfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "Wh/Co action failed" }, JsonRequestBehavior.AllowGet);
        }

        [jAuth(MenuId = 954)]
        public ActionResult MailRecipient()
        {
            DropDownFor_MailRecipient();
            return View();
        }
        [jAuth(MenuId = 954)]
        public ActionResult FindMailRecipient(string USER_TEXT)
        {
            List<T_MAIL> d = _d.EMailRecipient(USER_TEXT).ToList();
            if (d == null)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = d.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [jAuth(MenuId = 954)]
        public ActionResult SetMailRecipient(string AT, string USER_TEXT, string MAIL_TYPE, string MAIL_INBX)
        {
            if (ModelState.IsValid 
                && !string.IsNullOrWhiteSpace(AT)
                && !string.IsNullOrWhiteSpace(USER_TEXT)
                && !string.IsNullOrWhiteSpace(MAIL_TYPE)
                && !string.IsNullOrWhiteSpace(MAIL_INBX))
            {
                bool result = _d.UserMailRecipient(AT, USER_TEXT, MAIL_TYPE, MAIL_INBX);
                if (result)
                {
                    return Json(new { success = true, responseText = "Mail recipient action sucessfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "Mail recipient action failed" }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public void DropDownFor_MailRecipient()
        {
            var d1 = _c.DropDownList_MAIL_TYPE();
            ViewBag.MAIL_TYPE = d1;
            var d2 = _c.DropDownList_MAIL_INBX();
            ViewBag.MAIL_INBX = d2;
        }
    }
}