using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarLight_BookStores.Model
{
    class GioHang
    {
        string maGioHang, maKhachHang;

        public GioHang(string maGH, string maKH, DateTime ngayMua, double tongTien)
        {
            this.maGioHang = maGH;
            this.maKhachHang = maKH;
            this.ngayMua = ngayMua;
            this.tongTien = tongTien;
        }

        public string MaGioHang
        {
            get { return maGioHang; }
            set { maGioHang = value; }
        }

        public string MaKhachHang
        {
            get { return maKhachHang; }
            set { maKhachHang = value; }
        }
        DateTime ngayMua;

        public DateTime NgayMua
        {
            get { return ngayMua; }
            set { ngayMua = value; }
        }

        double tongTien;

        public double TongTien
        {
            get { return tongTien; }
            set { tongTien = value; }
        }
    }
}
