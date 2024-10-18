using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class SanPham
    {
        public Guid ID { get; set; }
        public string TenSanPham { get; set; }
        public string MaSanPham { get; set; }
        public int SoLuongTonKho { get; set; }
        public decimal DonGia { get; set; }
        public DateTime NgayNhapKho { get; set; }
        public string NhaCungCap { get; set; }
    }
}
