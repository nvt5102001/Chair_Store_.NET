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
    public class SanphamAdController : Controller
    {
        ShopGheEntities db = new ShopGheEntities();
        // GET: Admin/SanphamAd
        public ActionResult Index(int? page)
        {
            int pagesize = 9; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<SanPham> lstNuocHoa = db.SanPhams.ToList();
            if (lstNuocHoa.Count() == 0)
            {
                ViewBag.lstNuocHoa = "Không có sản phẩm thuộc loại này !!!";
            }
            ViewBag.lstNuocHoa = db.SanPhams.ToList();
            return View(lstNuocHoa.ToPagedList(pagenumber, pagesize));
        }
        public PartialViewResult LoaiPartial()
        {
            return PartialView(db.LoaiSps.ToList());
        }
        public PartialViewResult NsxPartial()
        {
            return PartialView(db.NuocSXes.ToList());
        }
        public PartialViewResult MauPartial()
        {
            return PartialView(db.Maus.ToList());
        }
        public PartialViewResult KichThuocPartial()
        {
            return PartialView(db.KichThuocs.ToList());
        }
        public ViewResult ChiTietSP(string MaSP = "SP001")
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpGet]
        public ActionResult ThemSanPham()
        {
            ViewBag.MaMau = new SelectList(db.Maus.ToList().OrderBy(n => n.MaMau), "MaMau", "TenMau");
            ViewBag.MaKT = new SelectList(db.KichThuocs.ToList().OrderBy(n => n.MaKT), "MaKT", "TenKT");
            ViewBag.MaNSX = new SelectList(db.NuocSXes.ToList().OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            ViewBag.MaLoai = new SelectList(db.LoaiSps.ToList().OrderBy(n => n.MaLoai), "MaLoai", "TenLoai");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSanPham([Bind(Include = "MaSP,TenSP,MaMau,MaKT,GioiThieu,Soluong,MaLoai,SoLuongTon,AnhSP,DonGia,MaNSX")] SanPham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanpham);
        }
        [HttpGet]
        public ActionResult SuaSanPham(string MaSP)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanpham = db.SanPhams.Find(MaSP);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMau = new SelectList(db.Maus.ToList().OrderBy(n => n.MaMau), "MaMau", "TenMau");
            ViewBag.MaKT = new SelectList(db.KichThuocs.ToList().OrderBy(n => n.MaKT), "MaKT", "TenKT");
            ViewBag.MaNSX = new SelectList(db.NuocSXes.ToList().OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            ViewBag.MaLoai = new SelectList(db.LoaiSps.ToList().OrderBy(n => n.MaLoai), "MaLoai", "TenLoai");
            return View(sanpham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSanPham([Bind(Include = "MaSP,TenSP,MaMau,MaKT,GioiThieu,Soluong,MaLoai,SoLuongTon,AnhSP,DonGia,MaNSX")] SanPham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult XoaSanPham(string MaSP)
        {
            SanPham sanpham = db.SanPhams.Single(n => n.MaSP == MaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }

        [HttpPost, ActionName("XoaSanPham")]
        public ActionResult XacNhanXoa(string MaSP)
        {
            SanPham sanpham = db.SanPhams.Single(n => n.MaSP == MaSP);
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult SearchSPAdmin(FormCollection f, int? page)
        {
            string searchkey = f["txtsearchsp"].ToString();
            ViewBag.keyword = searchkey;
            List<SanPham> lstKQ = db.SanPhams.Where(n => n.TenSP.Contains(searchkey)).ToList();
            int pagenumber = (page ?? 1);
            int pagesize = 12;
            if (lstKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có sản phẩm bạn đang tìm kiếm !";
                return View(db.SanPhams.OrderBy(n => n.TenSP).ToPagedList(pagenumber, pagesize));
            }
            ViewBag.Thongbao = "Đã tìm thấy " + lstKQ.Count + " sản phẩm ";
            return View(lstKQ.OrderBy(n => n.TenSP).ToPagedList(pagenumber, pagesize));
        }

        [HttpGet]
        public ActionResult SearchSPAdmin(int? page, string searchkey)
        {
            ViewBag.keyword = searchkey;
            List<SanPham> lstKQ = db.SanPhams.Where(n => n.TenSP.Contains(searchkey)).ToList();
            int pagenumber = (page ?? 1);
            int pagesize = 12;
            if (lstKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có sản phẩm bạn đang tìm kiếm !";
                return View(db.SanPhams.OrderBy(n => n.TenSP).ToPagedList(pagenumber, pagesize));
            }
            ViewBag.Thongbao = "Đã tìm thấy " + lstKQ.Count + " sản phẩm ";
            return View(lstKQ.OrderBy(n => n.TenSP).ToPagedList(pagenumber, pagesize));
        }




    }
}