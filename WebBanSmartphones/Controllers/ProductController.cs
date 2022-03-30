using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSmartphones.Context;

namespace WebBanSmartphones.Controllers
{
    public class ProductController : Controller
    {
        WebSiteBanHangEntities objWebSiteBanHangEntities = new WebSiteBanHangEntities();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebSiteBanHangEntities.Product_2119110227.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        
    }
}