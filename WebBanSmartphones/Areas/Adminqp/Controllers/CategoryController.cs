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
    public class CategoryController : Controller
    {
        WebSiteBanHangEntities objWebSiteBanHangEntities = new WebSiteBanHangEntities();
        // GET: Adminqp/Product
        public ActionResult Index(string currentFileter, string SearchString, int? page)
        {
            var lstCategory = new List<Category_2119110227>();
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
                lstCategory = objWebSiteBanHangEntities.Category_2119110227.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = objWebSiteBanHangEntities.Category_2119110227.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            this.LoadData();

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category_2119110227 objCategory)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {

                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objCategory.Avatar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objCategory.CreatedOnUtc = DateTime.Now;
                    objWebSiteBanHangEntities.Category_2119110227.Add(objCategory);
                    objWebSiteBanHangEntities.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCategory = objWebSiteBanHangEntities.Category_2119110227.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objWebSiteBanHangEntities.Category_2119110227.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Delete(Category_2119110227 objPro)
        {
            var objCategory = objWebSiteBanHangEntities.Category_2119110227.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebSiteBanHangEntities.Category_2119110227.Remove(objCategory);
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var objCategory = objWebSiteBanHangEntities.Category_2119110227.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category_2119110227 objCategory)
        {
            if (objCategory.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                fileName = fileName + extension;
                objCategory.Avatar = fileName;
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objWebSiteBanHangEntities.Entry(objCategory).State = EntityState.Modified;
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