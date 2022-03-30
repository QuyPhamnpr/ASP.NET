using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSmartphones.Context;
using WebBanSmartphones.Models;

namespace WebBanSmartphones.Controllers
{

    public class PaymentController : Controller
    {
        WebSiteBanHangEntities objWebSiteBanHangEntities = new WebSiteBanHangEntities();

        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                // lay tt tu gio hang tu bien session
                var lstCart = (List<CartModel>)Session["cart"];
                // gan du lieu cho order
                Order_2119110227 objOrder = new Order_2119110227();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objWebSiteBanHangEntities.Order_2119110227.Add(objOrder);

                objWebSiteBanHangEntities.SaveChanges();

                int intOrderId = objOrder.Id;
                List<OrderDetail_2119110227> lstOrderDetail = new List<OrderDetail_2119110227>();

                foreach(var item in lstCart)
                {
                    OrderDetail_2119110227 obj = new OrderDetail_2119110227();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objWebSiteBanHangEntities.OrderDetail_2119110227.AddRange(lstOrderDetail);
                objWebSiteBanHangEntities.SaveChanges();

            }
            return View();
        }
    }
}