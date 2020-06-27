using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarLight_BookStores.Model
{
    class KhachHang
    {
        private string maKH, tenKH, diaChi, sdt, gioiTinh;

        public string GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }

        public string Sdt
        {
            get { return sdt; }
            set { sdt = value; }
        }

        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }

        public string TenKH
        {
            get { return tenKH; }
            set { tenKH = value; }
        }

        public string MaKH
        {
            get { return maKH; }
            set { maKH = value; }
        }

        public KhachHang(string maKH, string tenKH, string diaChi, string sdt, string gioiTinh)
        {
            this.diaChi = diaChi;
            this.gioiTinh = gioiTinh;
            this.maKH = maKH;
            this.sdt = sdt;
            this.tenKH = tenKH;
        }
    }
}
