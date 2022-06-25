using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blackWood.Models;
using PagedList;
using System.Data.Entity;
using System.Net;



namespace blackWood.Areas.Admin.Controllers
{
    public class DonhangAdController : Controller
    {
        // GET: Admin/DonhangAd
        ShopGheEntities db = new ShopGheEntities();
        public ActionResult Index(int? page)
        {
            int pagesize = 6; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<DonDatHang> lstHDB = db.DonDatHangs.OrderByDescending(n => n.SoHD).ToList();
            ViewBag.lstHDB = db.DonDatHangs.ToList();
            return View(lstHDB.ToPagedList(pagenumber, pagesize));
        }

        public ActionResult ChitietHD(string SoHD)
        {
            double total = 0;
            DonDatHang dh = db.DonDatHangs.SingleOrDefault(n => n.SoHD == SoHD);
            total = (double)dh.ChiTietDDHs.Sum(n => n.DonGia * n.SoLuong);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.total = total;
            return View(dh);
        }

       

        [HttpGet]
        public ActionResult SuaHD(string SoHD)
        {
            if (SoHD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang hdb = db.DonDatHangs.Find(SoHD);
            if (hdb == null)
            {
                return HttpNotFound();
            }
            return View(hdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaHD([Bind(Include = "SoHD,MaKH,NgayBan,NoiNhan,TrangThai")] DonDatHang hdb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hdb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult XoaCTHDB(string SoHD)
        {
            DonDatHang hdb = db.DonDatHangs.Single(n => n.SoHD == SoHD);
            if (hdb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hdb);
        }

        [HttpPost, ActionName("XoaCTHDB")]
        public ActionResult XacNhanXoa(string SoHD)
        {
            DonDatHang hdb = db.DonDatHangs.Single(n => n.SoHD == SoHD);
            db.DonDatHangs.Remove(hdb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}