using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSmartphones.Context;

namespace WebBanSmartphones.Controllers
{
    public class BrandController : Controller
    {
        WebSiteBanHangEntities objWebSiteBanHangEntities = new WebSiteBanHangEntities();

        // GET: Brand
        public ActionResult Index()
        {
            var lstBrand = objWebSiteBanHangEntities.Brand_2119110227.ToList();
            return View(lstBrand);
        }
        public ActionResult ProductBrandList(int Id)
        {
            var lstProduct = objWebSiteBanHangEntities.Product_2119110227.Where(n=>n.BrandId==Id).ToList();
            return View(lstProduct);
        }

    }
}