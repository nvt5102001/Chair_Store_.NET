using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blackWood.Models.Datas
{
    public class Cart
    {
        ShopGheEntities db = new ShopGheEntities();
        public string sMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnhSP { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return dDonGia * iSoLuong; }
        }
        public Cart(string MaSP)
        {
            sMaSP = MaSP;
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            sTenSP = sanpham.TenSP;
            sAnhSP = sanpham.AnhSP;
            dDonGia = double.Parse(sanpham.DonGia.ToString());
            iSoLuong = 1;
        }
    }
}
