using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using blackWood.Models;

namespace blackWood.Controllers
{
    public class AccountController : Controller
    {
        ShopGheEntities db = new ShopGheEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "TaiKhoan1 , MatKhau, Status, TenNguoiDung, Email, Quyen")] TaiKhoan taikhoan)
        {
            taikhoan.Status = 1;
            taikhoan.Quyen = "user";
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(taikhoan);
                db.SaveChanges();
                return View("SignIn");
            }
            return View(taikhoan);
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(TaiKhoan objUser)
        {
            if (ModelState.IsValid)
            {
                //var obj = db.TaiKhoans.Where(x => x.TaiKhoan1.Equals(objUser.TaiKhoan1) &&
                // x.MatKhau.Equals(objUser.MatKhau)).FirstOrDefault();
                var user = db.TaiKhoans.Where(x => x.TaiKhoan1.Equals(objUser.TaiKhoan1) &&
                 x.MatKhau.Equals(objUser.MatKhau) && x.Quyen == "user").FirstOrDefault();
                var admin = db.TaiKhoans.Where(x => x.TaiKhoan1.Equals(objUser.TaiKhoan1) &&
                 x.MatKhau.Equals(objUser.MatKhau) && x.Quyen == "admin").FirstOrDefault();

                if (admin != null)
                {
                    Session["TaiKhoan1"] = admin.TaiKhoan1.ToString();
                    Session["TenNguoiDung"] = admin.TaiKhoan1.ToString();
                    return View("~/Areas/Admin/Views/TrangchuAd/Index.cshtml");
                }
                else if (user != null)
                {
                    Session["TaiKhoan1"] = user.TaiKhoan1.ToString();
                    Session["TenNguoiDung"] = user.TaiKhoan1.ToString();
                    return View("~/Views/About/Index.cshtml");
                }
                //if (obj != null)
                //{
                //    Session["TaiKhoan1"] = obj.TaiKhoan1.ToString();
                //    Session["TenNguoiDung"] = obj.TenNguoiDung.ToString();
                //    return View("~/Views/About/Index.cshtml");
                //}
            }
            return View(objUser);
        }
    }
}