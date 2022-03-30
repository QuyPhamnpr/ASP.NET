using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSmartphones.Context;
using static WebBanSmartphones.Common;

namespace WebBanSmartphones.Areas.Adminqp.Controllers
{
    public class ProductController : Controller
    {
        WebSiteBanHangEntities objWebSiteBanHangEntities = new WebSiteBanHangEntities();
        // GET: Adminqp/Product
        public ActionResult Index(string currentFileter, string SearchString, int? page)
        { 
            var lstProduct = new List<Product_2119110227>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFileter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = objWebSiteBanHangEntities.Product_2119110227.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objWebSiteBanHangEntities.Product_2119110227.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            this.LoadData();

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product_2119110227 objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {

                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    objWebSiteBanHangEntities.Product_2119110227.Add(objProduct);
                    objWebSiteBanHangEntities.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objWebSiteBanHangEntities.Product_2119110227.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objWebSiteBanHangEntities.Product_2119110227.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product_2119110227 objPro)
        {
            var objProduct = objWebSiteBanHangEntities.Product_2119110227.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebSiteBanHangEntities.Product_2119110227.Remove(objProduct);
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var objProduct = objWebSiteBanHangEntities.Product_2119110227.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit(int id, Product_2119110227 objProduct)
        {
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objProduct.Avatar = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
            }
            objWebSiteBanHangEntities.Entry(objProduct).State = EntityState.Modified;
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        void LoadData()
        {
            Common objCommon = new Common();

            //lay danh muc db
            var lstCat = objWebSiteBanHangEntities.Category_2119110227.ToList();
            //convert sang select list 
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            // lay thuong hieu db
            var lstBrand = objWebSiteBanHangEntities.Brand_2119110227.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");
            //Loai san pham
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }
    }
}