using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSmartphones.Context;

namespace WebBanSmartphones.Controllers
{
    public class ContactController : Controller
    {
        WebSiteBanHangEntities objWebSiteBanHangEntities = new WebSiteBanHangEntities();

        // GET: Contact
        public ActionResult Index()
        {
            var lstContact = objWebSiteBanHangEntities.Contact_2119110227.ToList();
            return View(lstContact);
        }
    }
}