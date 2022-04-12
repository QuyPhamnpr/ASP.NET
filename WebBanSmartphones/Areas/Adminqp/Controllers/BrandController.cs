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
    public class BrandController : Controller
    {
        WebSiteBanHangEntities objWebSiteBanHangEntities = new WebSiteBanHangEntities();
        // GET: Adminqp/Product
        public ActionResult Index(string currentFileter, string SearchString, int? page)
        {
            var lstBrand = new List<Brand_2119110227>();
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
                lstBrand = objWebSiteBanHangEntities.Brand_2119110227.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = objWebSiteBanHangEntities.Brand_2119110227.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

         

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand_2119110227 objBrand)
        {
         
            if (ModelState.IsValid)
            {
                try
                {

                    if (objBrand.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                        string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objBrand.Avatar = fileName;
                        objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/Brand/"), fileName));
                    }
                    objBrand.GreatedOnUtc = DateTime.Now;
                    objWebSiteBanHangEntities.Brand_2119110227.Add(objBrand);
                    objWebSiteBanHangEntities.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objBrand = objWebSiteBanHangEntities.Brand_2119110227.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objWebSiteBanHangEntities.Brand_2119110227.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand_2119110227 objPro)
        {
            var objBrand = objWebSiteBanHangEntities.Brand_2119110227.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebSiteBanHangEntities.Brand_2119110227.Remove(objBrand);
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var objBrand = objWebSiteBanHangEntities.Brand_2119110227.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Edit(int id, Brand_2119110227 objBrand)
        {
            if (objBrand.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                fileName = fileName + extension;
                objBrand.Avatar = fileName;
                objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/Brand/"), fileName));
            }
            objWebSiteBanHangEntities.Entry(objBrand).State = EntityState.Modified;
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}