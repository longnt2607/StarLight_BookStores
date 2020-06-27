using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarLight_BookStores.Model
{
    class Ctgh
    {
        string maGioHang, maSach;

        public string MaGioHang
        {
            get { return maGioHang; }
            set { maGioHang = value; }
        }

        public string MaSach
        {
            get { return maSach; }
            set { maSach = value; }
        }
        int soLuongMua;

        public int SoLuongMua
        {
            get { return soLuongMua; }
            set { soLuongMua = value; }
        }
        double giaBan;

        public double GiaBan
        {
            get { return giaBan; }
            set { giaBan = value; }
        }

        public Ctgh(string maGH, string maSach, int soLuong, double giaBan)
        {
            this.maGioHang = maGH;
            this.maSach = maSach;
            this.soLuongMua = soLuong;
            this.giaBan = giaBan;
        }
    }
}
