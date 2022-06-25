using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blackWood.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;
using System.Net;
using System.ComponentModel.DataAnnotations.Schema;

namespace blackWood.Areas.Admin.Controllers
{
    public class NguoidungAdController : Controller
    {
        // GET: Admin/NguoidungAd
        ShopGheEntities db = new ShopGheEntities();
        public ActionResult Index(int? page)
        {
            int pagesize = 9; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<TaiKhoan> lstNuocHoa = db.TaiKhoans.ToList();
            if (lstNuocHoa.Count() == 0)
            {
                ViewBag.lstNuocHoa = "Không có sản phẩm thuộc loại này !!!";
            }
            ViewBag.lstNuocHoa = db.TaiKhoans.ToList();
            return View(lstNuocHoa.ToPagedList(pagenumber, pagesize));
        }
     
        [HttpGet]
        public ActionResult ThemND()
        {
          
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemND([Bind(Include = "TaiKhoan1,MatKhau,Status,TenNguoiDung,Email,Quyen")] TaiKhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(taikhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taikhoan);
        }



        [HttpGet]
        public ActionResult SuaND(string TaiKhoan1)
        {
            if (TaiKhoan1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taikhoan3 = db.TaiKhoans.Find(TaiKhoan1);
            if (taikhoan3 == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan3);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaND([Bind(Include = "TaiKhoan1,MatKhau,Status,TenNguoiDung,Email,Quyen")] TaiKhoan taikhoan3)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taikhoan3).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult XoaND(string TaiKhoan1)
        {
            TaiKhoan taikhoan = db.TaiKhoans.Single(n => n.TaiKhoan1 == TaiKhoan1);
            if (taikhoan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(taikhoan);
        }

        [HttpPost, ActionName("XoaND")]
        public ActionResult XacNhanXoa(string TaiKhoan1)
        {
            TaiKhoan taikhoan = db.TaiKhoans.Single(n => n.TaiKhoan1 == TaiKhoan1);
            db.TaiKhoans.Remove(taikhoan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult SearchNDAdmin(FormCollection f, int? page)
        {
            string searchkey = f["txtsearchnd"].ToString();
            ViewBag.keyword = searchkey;
            List<TaiKhoan> lstKQ = db.TaiKhoans.Where(n => n.TaiKhoan1.Contains(searchkey)).ToList();
            int pagenumber = (page ?? 1);
            int pagesize = 12;
            if (lstKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có người dùng bạn đang tìm kiếm !";
                return View(db.TaiKhoans.OrderBy(n => n.TaiKhoan1).ToPagedList(pagenumber, pagesize));
            }
            ViewBag.Thongbao = "Đã tìm thấy " + lstKQ.Count + " sản phẩm ";
            return View(lstKQ.OrderBy(n => n.TaiKhoan1).ToPagedList(pagenumber, pagesize));
        }

        [HttpGet]
        public ActionResult SearchNDAdmin(int? page, string searchkey)
        {
            ViewBag.keyword = searchkey;
            List<TaiKhoan> lstKQ = db.TaiKhoans.Where(n => n.TaiKhoan1.Contains(searchkey)).ToList();
            int pagenumber = (page ?? 1);
            int pagesize = 12;
            if (lstKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có người dùng bạn đang tìm kiếm !";
                return View(db.TaiKhoans.OrderBy(n => n.TaiKhoan1).ToPagedList(pagenumber, pagesize));
            }
            ViewBag.Thongbao = "Đã tìm thấy " + lstKQ.Count + " sản phẩm ";
            return View(lstKQ.OrderBy(n => n.TaiKhoan1).ToPagedList(pagenumber, pagesize));
        }

    }
}