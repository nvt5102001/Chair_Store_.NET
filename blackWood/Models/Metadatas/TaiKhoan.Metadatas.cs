using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace blackWood.Models
{
    [MetadataTypeAttribute(typeof(TaiKhoanMetadata))]
    public partial class TaiKhoan
    {
        internal sealed class TaiKhoanMetadata
        {

            [Display(Name = "Tên tài khoản")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            public string TaiKhoan1 { get; set; }

            [Display(Name = "Mật khẩu")]
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Mật khẩu chứa ít nhất 1 kí tự thường , 1 kí tự hoa , số và số kí tự lớn hơn 8 và nhỏ hơn 32")]
            public string MatKhau { get; set; }

            [Display(Name = "Trạng thái")]
            public int Status { get; set; }

            [Display(Name = "Tên người dùng")]
            public string TenNguoiDung { get; set; }

            //[Display(Name = "Số điện thoại")]
            //[RegularExpression(@"(84|0[3|5|7|8|9])+([0-9]{8})", ErrorMessage = "Số điện thoại chứa 10 số ")]
            //public string DienThoai { get; set; }

            [Display(Name = "Email")]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "VD: abc@gmail.com")]
            public string Email { get; set; }

            [Display(Name = "Quyền")]
            public string Quyen { get; set; }
            //
        }

    }
}