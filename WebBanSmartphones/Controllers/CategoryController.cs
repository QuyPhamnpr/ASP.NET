using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSmartphones.Context;

namespace WebBanSmartphones.Controllers
{
    public class CategoryController : Controller
    {
        WebSiteBanHangEntities objWebSiteBanHangEntities = new WebSiteBanHangEntities();
        // GET: Category
        public ActionResult Category()
        {
            var lstCategory = objWebSiteBanHangEntities.Category_2119110227.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objWebSiteBanHangEntities.Product_2119110227.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }

}