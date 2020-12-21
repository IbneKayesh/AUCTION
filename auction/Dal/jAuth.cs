using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Dal
{
    public class jAuth : ActionFilterAttribute
    {
        public int MenuId { get; set; }
        public jAuth()
        {
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string redirectUrl = string.Format("?redirect={0}", filterContext.HttpContext.Request.Url.PathAndQuery);
            if (HttpContext.Current.Session["MenuList"] == null)
            {
                CleanSession();
                filterContext.Result = new RedirectResult("~/Home/Login" + redirectUrl, true);
            }
            var MenuMaster = (List<auction.Models.MNUP_MNUC>)HttpContext.Current.Session["MenuList"];
            if (MenuMaster == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Login" + redirectUrl, true);
                return;
            }
            bool IsPermitted = MenuMaster.Where(p => p.MNUC_TEXT == MenuId).Any();
            if (IsPermitted)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                CleanSession();
                filterContext.HttpContext.Response.Redirect("~/Home/Login", true);
                return;
            }
        }
        public void CleanSession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}