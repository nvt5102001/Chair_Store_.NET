using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blackWood.Models;
using System.Net;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;

namespace blackWood.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        ShopGheEntities db = new ShopGheEntities();
        public ActionResult Index(int? page)
        {
            int pagesize = 9; // so san pham tren 1 trang
            int pagenumber = (page ?? 1); //so trang 
            List<SanPham> lstSanPham = db.SanPhams.ToList();
            if (lstSanPham.Count() == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm thuộc loại này!";
            }
            ViewBag.lstSanPham = db.SanPhams.ToList();
            return View(lstSanPham.ToPagedList(pagenumber, pagesize));
        }

        public ActionResult showImage(string MaSP = "SP001")
        {
            AnhSP sp = db.AnhSPs.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        public ViewResult ChitietSP(string MaSP = "SP001")
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        #region Tìm kiếm theo loại
        public PartialViewResult LoaiPartial()
        {
            return PartialView(db.LoaiSps.ToList());
        }

        public ViewResult SPTheoLoai(int? page, string MaLoai = "L001")
        {
            int pagesize = 9; // so san pham tren 1 trang
            int pagenumber = (page ?? 1); //so trang 

            LoaiSp lsp = db.LoaiSps.SingleOrDefault(n => n.MaLoai == MaLoai);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<SanPham> lstSanPham = db.SanPhams.Where(m => m.MaLoai == MaLoai).OrderBy
                (m => m.TenSP).ToList();
            if (lstSanPham.Count() == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm thuộc loại này!";
            }
            
                ViewBag.lstSanPham = db.SanPhams.ToList();
              
            
            return View(lstSanPham.ToPagedList(pagenumber, pagesize));
        }
        #endregion

        #region Tìm kiếm theo màu
        public PartialViewResult MauPartial()
        {
            return PartialView(db.Maus.ToList());
        }

        public ViewResult SPTheoMau(int? page, string MaMau = "M001")
        {
            int pagesize = 9; // so san pham tren 1 trang
            int pagenumber = (page ?? 1); //so trang 

            Mau lsp = db.Maus.SingleOrDefault(n => n.MaMau == MaMau);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<SanPham> lstSanPham = db.SanPhams.Where(m => m.MaMau == MaMau).OrderBy
                (m => m.TenSP).ToList();
            if (lstSanPham.Count() == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm thuộc loại này!";
            }
            ViewBag.lstSanPham = db.SanPhams.ToList();
            return View(lstSanPham.ToPagedList(pagenumber, pagesize));
        }
        #endregion

        #region Tìm kiếm theo kích thước
        public PartialViewResult KichThuocPartial()
        {
            return PartialView(db.KichThuocs.ToList());
        }

        public ViewResult SPTheoKichThuoc(int? page, string MaKT = "KT001")
        {
            int pagesize = 9; // so san pham tren 1 trang
            int pagenumber = (page ?? 1); //so trang 

            KichThuoc lsp = db.KichThuocs.SingleOrDefault(n => n.MaKT == MaKT);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<SanPham> lstSanPham = db.SanPhams.Where(m => m.MaKT == MaKT).OrderBy
                (m => m.TenSP).ToList();
            if (lstSanPham.Count() == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm thuộc loại này!";
            }
            ViewBag.lstSanPham = db.SanPhams.ToList();
            return View(lstSanPham.ToPagedList(pagenumber, pagesize));
        }
        #endregion

        #region Tìm kiếm theo quốc gia
        public PartialViewResult NsxPartial()
        {
            return PartialView(db.NuocSXes.ToList());
        }

        public ViewResult SPTheoNSX(int? page, string MaNSX = "NSX001")
        {
            int pagesize = 9; // so san pham tren 1 trang
            int pagenumber = (page ?? 1); //so trang 

            NuocSX lsp = db.NuocSXes.SingleOrDefault(n => n.MaNSX == MaNSX);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<SanPham> lstSanPham = db.SanPhams.Where(m => m.MaNSX == MaNSX).OrderBy
                (m => m.TenSP).ToList();
            if (lstSanPham.Count() == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm thuộc loại này!";
            }
            ViewBag.lstSanPham = db.SanPhams.ToList();
            return View(lstSanPham.ToPagedList(pagenumber, pagesize));
        }
        #endregion

        public ActionResult SPCungLoai(int? page)
        {
            int pagesize = 12; // so san pham tren 1 trang
            int pagenumber = (page ?? 1); //so trang 
            List<SanPham> lstSanPham = db.SanPhams.ToList();
            if (lstSanPham.Count() == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm thuộc loại này!";
            }
            ViewBag.lstSanPham = db.SanPhams.ToList();
            return View(lstSanPham.ToPagedList(pagenumber, pagesize));
        }

        // GET: Search
        [HttpPost]
        public ActionResult resultsSearch(FormCollection f, int? page)
        {
            string searchkey = f["txtsearchkey"].ToString();
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
        public ActionResult resultsSearch(int? page, string searchkey)
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