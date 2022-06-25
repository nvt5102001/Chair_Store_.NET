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
    public class KhachhangAdController : Controller
    {
        ShopGheEntities db = new ShopGheEntities();
        // GET: Admin/KhachhangAd
        public ActionResult Index(int? page)
        {
            int pagesize = 9; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<KhachHang> lstNuocHoa = db.KhachHangs.ToList();
            if (lstNuocHoa.Count() == 0)
            {
                ViewBag.lstNuocHoa = "Không có sản phẩm thuộc loại này !!!";
            }
            ViewBag.lstNuocHoa = db.KhachHangs.ToList();
            return View(lstNuocHoa.ToPagedList(pagenumber, pagesize));
        }

        [HttpGet]
        public ActionResult ThemKH()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemKH([Bind(Include = "MaKH,TenKH,SDT,DiaChi,Mail")] KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kh);
        }



        [HttpGet]
        public ActionResult SuaKH(string MaKH)
        {
            if (MaKH == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang kh = db.KhachHangs.Find(MaKH);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaKH([Bind(Include = "MaKH,TenKH,SDT,DiaChi,Mail")] KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult XoaKH(string MaKH)
        {
            KhachHang kh = db.KhachHangs.Single(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpPost, ActionName("XoaKH")]
        public ActionResult XacNhanXoa(string MaKH)
        {
            KhachHang kh = db.KhachHangs.Single(n => n.MaKH == MaKH);
            db.KhachHangs.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult SearchKHAdmin(FormCollection f, int? page)
        {
            string searchkey = f["txtsearchkh"].ToString();
            ViewBag.keyword = searchkey;
            List<KhachHang> lstKQ = db.KhachHangs.Where(n => n.TenKH.Contains(searchkey)).ToList();
            int pagenumber = (page ?? 1);
            int pagesize = 12;
            if (lstKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có khách hàng bạn đang tìm kiếm !";
                return View(db.KhachHangs.OrderBy(n => n.TenKH).ToPagedList(pagenumber, pagesize));
            }
            ViewBag.Thongbao = "Đã tìm thấy " + lstKQ.Count + " sản phẩm ";
            return View(lstKQ.OrderBy(n => n.TenKH).ToPagedList(pagenumber, pagesize));
        }

        [HttpGet]
        public ActionResult SearchKHAdmin(int? page, string searchkey)
        {
            ViewBag.keyword = searchkey;
            List<KhachHang> lstKQ = db.KhachHangs.Where(n => n.TenKH.Contains(searchkey)).ToList();
            int pagenumber = (page ?? 1);
            int pagesize = 12;
            if (lstKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có khách hàng bạn đang tìm kiếm !";
                return View(db.KhachHangs.OrderBy(n => n.TenKH).ToPagedList(pagenumber, pagesize));
            }
            ViewBag.Thongbao = "Đã tìm thấy " + lstKQ.Count + " sản phẩm ";
            return View(lstKQ.OrderBy(n => n.TenKH).ToPagedList(pagenumber, pagesize));
        }
    }
}