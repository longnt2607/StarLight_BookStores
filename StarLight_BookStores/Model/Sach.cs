using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarLight_BookStores.Model
{
    class Sach
    {
        string linhVuc, maSach, tenSach, tacGia, ngonNgu;

        public string TacGia
        {
            get { return tacGia; }
            set { tacGia = value; }
        }

        public string NgonNgu
        {
            get { return ngonNgu; }
            set { ngonNgu = value; }
        }

        public string TenSach
        {
            get { return tenSach; }
            set { tenSach = value; }
        }

        public string MaSach
        {
            get { return maSach; }
            set { maSach = value; }
        }

        public string LinhVuc
        {
            get { return linhVuc; }
            set { linhVuc = value; }
        }

        int namXB, soLuong;

        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }

        public int NamXB
        {
            get { return namXB; }
            set { namXB = value; }
        }

        double gia;

        public double Gia
        {
            get { return gia; }
            set { gia = value; }
        }

        public Sach(string linhVuc, string maSach, string tenSach, string tacGia, int namXB, string ngonNgu, 
                    int soLuong, double gia)
        {
            this.gia = gia;
            this.linhVuc = linhVuc;
            this.maSach = maSach;
            this.namXB = namXB;
            this.ngonNgu = ngonNgu;
            this.soLuong = soLuong;
            this.tenSach = tenSach;
            this.tacGia = tacGia;
        }
    }
}
