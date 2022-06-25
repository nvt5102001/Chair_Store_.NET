using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace blackWood.Models.Metadatas
{
    [MetadataTypeAttribute(typeof(SanPhamMetadata))]
    public class SanPham
    {
        internal sealed class SanPhamMetadata
        {
            [Display(Name = "Mã sản phẩm")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            public string MaSP { get; set; }

            [Display(Name = "Tên sản phẩm")]
            public string TenSP { get; set; }

            [Display(Name = "Thông tin sản phẩm")]
            public string GioiThieu { get; set; }

            [Display(Name = "Số lượng")]
            public Nullable<int> SoLuong { get; set; }

            [Display(Name = "Số lượng tồn")]
            public Nullable<int> SoLuongTon { get; set; }

            [Display(Name = "Đơn giá bán")]
            public Nullable<long> DonGia { get; set; }
           
            [Display(Name = "Thời gian bảo hành")]
            public string ThoiGianBaoHanh { get; set; }
        }
    }
}