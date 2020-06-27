using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarLight_BookStores.Model
{
    class LinhVuc
    {
        string maLinhVuc, tenLinhVuc;

        public string TenLinhVuc
        {
            get { return tenLinhVuc; }
            set { tenLinhVuc = value; }
        }

        public string MaLinhVuc
        {
            get { return maLinhVuc; }
            set { maLinhVuc = value; }
        }

        public LinhVuc(string maLV, string tenLV)
        {
            this.maLinhVuc = maLV;
            this.tenLinhVuc = tenLV;
        }
    }
}
