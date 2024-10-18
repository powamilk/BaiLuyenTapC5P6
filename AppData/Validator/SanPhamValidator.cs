using AppData.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validator
{
    public class SanPhamValidator : AbstractValidator<SanPham>
    {
        public SanPhamValidator()
        {
            RuleFor(s => s.TenSanPham)
                .NotEmpty().WithMessage("Tên sản phẩm không được để trống");
            RuleFor(s => s.MaSanPham)
                .NotEmpty().WithMessage("Mã sản phẩm không được để trống");
            RuleFor(s => s.SoLuongTonKho)
                .GreaterThanOrEqualTo(0).WithMessage("Số lượng tồn kho phải lớn hơn hoặc bằng 0");
            RuleFor(s => s.DonGia)
                .GreaterThan(0).WithMessage("Đơn giá phải lớn hơn 0");
            RuleFor(s => s.NgayNhapKho)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày nhập kho không được trong tương lai");
            RuleFor(s => s.NhaCungCap)
                .NotEmpty().WithMessage("Nhà cung cấp không được để trống");
        }
    }
}
