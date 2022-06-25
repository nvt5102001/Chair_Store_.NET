using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blackWood.Models.Datas
{
    public class NguoiDung
    {
        ShopGheEntities db = new ShopGheEntities();
        public IEnumerable<TaiKhoan> GetTK()
        {
            return db.TaiKhoans.ToList();
        }
        public TaiKhoan Get(string id)
        {
            return db.TaiKhoans.First(m => m.TaiKhoan1.CompareTo(id) == 0);
        }
        public void Them(TaiKhoan n)
        {
            db.TaiKhoans.Add(n);
            db.SaveChanges();
        }
        public void Sua(TaiKhoan n)
        {
            TaiKhoan nv = Get(n.TaiKhoan1);
            nv.MatKhau = n.MatKhau; 
            nv.Status = n.Status;
            nv.TenNguoiDung = n.TenNguoiDung;
            nv.Email = n.Email;
            nv.Quyen = n.Quyen;
            db.SaveChanges();
        }
        public void Xoa(string id)
        {
            TaiKhoan n = Get(id);
            db.TaiKhoans.Remove(n);
            db.SaveChanges();
        }
    }
}