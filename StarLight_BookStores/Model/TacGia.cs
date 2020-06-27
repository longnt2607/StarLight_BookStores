using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarLight_BookStores.Model
{
    class TacGia
    {
        string maTacGia, tenTacGia, diaChi, sdt, email, gioiTinh, quocTich;

        public TacGia(string maTacGia, string tenTacGia, string diaChi, string sdt, 
                        string email, string gioiTinh, string quocTich)
        {
            this.maTacGia = maTacGia;
            this.tenTacGia = tenTacGia;
            this.diaChi = diaChi;
            this.sdt = sdt;
            this.email = email;
            this.gioiTinh = gioiTinh;
            this.quocTich = quocTich;
        }

        public string MaTacGia
        {
            get { return maTacGia; }
            set { maTacGia = value; }
        }

        public string TenTacGia
        {
            get { return tenTacGia; }
            set { tenTacGia = value; }
        }

        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }

        public string Sdt
        {
            get { return sdt; }
            set { sdt = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }

        public string QuocTich
        {
            get { return quocTich; }
            set { quocTich = value; }
        }
    }
}
