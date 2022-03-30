using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSmartphones.Context;
using WebBanSmartphones.Models;
namespace WebBanSmartphones.Controllers
{
    public class CartController : Controller
    {
        WebSiteBanHangEntities objWebSiteBanHangEntities = new WebSiteBanHangEntities();

        // GET: Card
        public ActionResult Index()
        {
            return View((List<CartModel>)Session["cart"]);
        }
        public ActionResult AddToCart(int id, int quantity)
        {
            if (Session["cart"] == null)
            {
                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel { Product = objWebSiteBanHangEntities.Product_2119110227.Find(id), Quantity = quantity});
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
               List<CartModel> cart = (List<CartModel>)Session["cart"];

               int index = isExist(id);
               if(index != -1)
               {
                    cart[index].Quantity += quantity;
               }
               else
               {
                    cart.Add(new CartModel {Product = objWebSiteBanHangEntities.Product_2119110227.Find(id), Quantity = quantity});

                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
               }
                Session["cart"] = cart;
            }
            return Json(new { Massage = "Thành Công", JsonRequestBehavior.AllowGet });
         }
         private int isExist(int id)
         {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.Id.Equals(id))
                    return i;
            return -1;
         }
        //xóa sản phẩm khỏi giỏ hàng theo id
        public ActionResult Remove(int Id)
        {
            List<CartModel> li = (List<CartModel>)Session["cart"];
            li.RemoveAll(x => x.Product.Id == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
    }
}