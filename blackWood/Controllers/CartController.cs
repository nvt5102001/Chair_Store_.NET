using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blackWood.Models;
using blackWood.Models.Datas;

namespace blackWood.Controllers
{
    public class CartController : Controller
    {
        ShopGheEntities db = new ShopGheEntities();
        // GET: Cart
        public List<Cart> LayGioHang()
        {
            List<Cart> lstGioHang = Session["Cart"] as List<Cart>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<Cart>();
                Session["Cart"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(string sMaSP, string strUrl)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> lstGioHang = LayGioHang();
            Cart spgh = lstGioHang.Find(n => n.sMaSP == sMaSP);
            if (spgh == null)
            {
                spgh = new Cart(sMaSP);
                lstGioHang.Add(spgh);
                return Redirect(strUrl);
            }
            else
            {
                spgh.iSoLuong++;
                return Redirect(strUrl);
            }
        }
        public ActionResult Cart()
        {
            if (Session["Cart"] == null)
            {
                RedirectToAction("Index", "Shop");
            }
            List<Cart> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }

        public ActionResult SaveOrder(string fir, string last, string addr, string email, string phone)
        {

                DonDatHang ddh = new DonDatHang();
                {
                    ddh.SoHD = "HD010";
                    ddh.MaKH = "KH007";
                    ddh.NameKH = fir + " " + last;
                    ddh.EmailKH = email;
                    ddh.SdtKH = phone;
                    ddh.NoiNhan = addr;
                    ddh.NgayBan = DateTime.Now.Date;
                    ddh.TrangThai = 1;
                    
                }
                db.DonDatHangs.Add(ddh);
                db.SaveChanges();

            List<Cart> lstGioHang = Session["Cart"] as List<Cart>;

            for (int i = 0; i < lstGioHang.Count; i++)
            {
                ChiTietDDH ctddh = new ChiTietDDH()
                {
                    SoHD = "HD010",
                    MaSP = lstGioHang[i].sMaSP,
                    SoLuong = lstGioHang[i].iSoLuong,
                    DonGia = lstGioHang[i].dDonGia
                    
                };
                db.ChiTietDDHs.Add(ctddh);
                db.SaveChanges();
             }
            ViewBag.TongTien = TongTien();
            return RedirectToAction("Index", "Shop");
        }



        public ActionResult CheckOut()
        {
            if (Session["Cart"] == null)
            {
                RedirectToAction("Index", "Shop");
            }
            List<Cart> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        private int TongSoLuong()
        {
            int iTongSL = 0;
            List<Cart> lstGioHang = Session["Cart"] as List<Cart>;
            if (lstGioHang != null)
            {
                iTongSL = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSL;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<Cart> lstGioHang = Session["Cart"] as List<Cart>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }
        public ActionResult CartPartial()
        {
            if (TongSoLuong() != 0)
            {
                ViewBag.TongSoLuong = TongSoLuong();
            }
            return PartialView();
        }
        public ActionResult SuaGioHang()
        {
            if (Session["Cart"] == null)
            {
                RedirectToAction("Index", "Shop");
            }
            List<Cart> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult CapNhatGioHang(string sMaSP, FormCollection f)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> lstGioHang = LayGioHang();
            //kiểm tra sản phẩm có trong giỏ hàng không
            Cart sp = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("SuaGioHang");
        }
        public ActionResult XoaGioHang(string sMaSP, FormCollection f)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> lstGioHang = LayGioHang();
            //kiểm tra sản phẩm có trong giỏ hàng không
            Cart sp = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.sMaSP == sMaSP);
            }
            if (lstGioHang.Count == 0)
            {
                RedirectToAction("Index", "SanPham");
            }
            return RedirectToAction("SuaGioHang");
        }

        //Đặt hàng
        public ActionResult DatHang()
        {
            if (Session["TenNguoiDung"] == null || Session["TenNguoiDung"].ToString() == "")
            {
                return RedirectToAction("Login", "NguoiDung");
            }
            if (Session["Cart"] == null)
            {
                RedirectToAction("Index", "Shop");
            }
            DonDatHang ddh = new DonDatHang();
            KhachHang objUser = (KhachHang)Session["TenNguoiDung"];
            List<Cart> gh = LayGioHang();
            // luu thong tin khach hang
            //ddh.MaKH = objUser.;
            ddh.NgayBan = DateTime.Now;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            // luu chi tiet hoa don
            foreach (var item in gh)
            {
                ChiTietDDH cthd = new ChiTietDDH();
                cthd.SoHD = ddh.SoHD;
                cthd.MaSP = item.sMaSP;
                cthd.SoLuong = item.iSoLuong;
                db.ChiTietDDHs.Add(cthd);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Shop");
        }
    }
}