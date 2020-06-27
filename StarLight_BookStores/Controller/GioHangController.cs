using StarLight_BookStores.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StarLight_BookStores.Controller
{
    class GioHangController
    {
        private XmlDocument doc = new XmlDocument();
        private XmlElement root;
        private string fileName = @"..//..//MyData//data.xml";
        List<GioHang> list;

        public GioHangController()
        {
            doc.Load(fileName);
            root = doc.DocumentElement;
        }

        public int getMaxId()
        {
            int max = 0;
            string id = "";
            doc = new XmlDocument(); //Tao moi mot doi tuong XmlDocumnent
            doc.Load(fileName); //Load file theo duong dan
            XmlNodeList lvNode = doc.GetElementsByTagName("GioHang");
            //Lay ra cac nut co the ten la nhanvien
            foreach (XmlNode node in lvNode) //Duyet tung nut
            {
                id = node.ChildNodes[0].InnerText;
                max = int.Parse(id.Substring(3)) + 1; // SA0xx
            }
            return max;
        }

        public void loadDataFromDoc(XmlDocument doc, string fileName, List<GioHang> list)
        {
            doc = new XmlDocument(); //Tao moi mot doi tuong XmlDocumnent
            doc.Load(fileName); //Load file theo duong dan
            XmlNodeList ghNode = doc.GetElementsByTagName("GioHang");
            //Lay ra cac nut co the ten la sach
            foreach (XmlNode node in ghNode) //Duyet tung nut
            {
                //Khai bao cac bien luu tri thong tin cua sach
                string maGioHang, maKhachHang;
                DateTime ngayMua;
                double tongSoTien;

                maGioHang = node.ChildNodes[0].InnerText;
                maKhachHang = node.ChildNodes[1].InnerText;
                ngayMua = DateTime.Parse(node.ChildNodes[2].InnerText);
                tongSoTien = double.Parse(node.ChildNodes[3].InnerText);

                GioHang gh = new GioHang(maGioHang, maKhachHang, ngayMua, tongSoTien);
                list.Add(gh);
            }
        }

        public bool isExistId(List<GioHang> list, string id)
        {
            bool isExist = false;

            foreach (GioHang item in list)
            {
                string maGH = item.MaGioHang; //lay ra id
                if (maGH.ToLower() == id.ToLower()) //chuyen sang chu thuong va so sanh
                {
                    //neu ma hai chuoi bang nhau
                    isExist = true; //trung ma
                    break;
                }
            }

            return isExist;//tra ve ket qua
        }

        public bool themGioHang(GioHang gh)
        {
            doc = new XmlDocument(); // Tao moi 1 doi tuong XmlDocument
            doc.Load(fileName); // Load file xml theo duong dan
            list = new List<GioHang>(); // Tao moi 1 danh sach

            loadDataFromDoc(doc, fileName, list);
            if (isExistId(list, gh.MaGioHang)) // Kiem tra da ton tai ma
                return false; //da ton tai tra ve false
            //Khong roi vao hai truong hop day ta di thuc hien them

            XmlElement gioHang = doc.CreateElement("GioHang"); //tạo nút Sach

            XmlElement maGioHang = doc.CreateElement("MaGioHang"); 
            maGioHang.InnerText = gh.MaGioHang; 
            gioHang.AppendChild(maGioHang); 

            XmlElement maKhachHang = doc.CreateElement("MaKhachHang");
            maKhachHang.InnerText = gh.MaKhachHang;
            gioHang.AppendChild(maKhachHang);

            XmlElement ngayMua = doc.CreateElement("NgayMua");
            ngayMua.InnerText = Convert.ToString(gh.NgayMua);
            gioHang.AppendChild(ngayMua);

            XmlElement tongTien = doc.CreateElement("TongTien");
            tongTien.InnerText = Convert.ToString(gh.TongTien);
            gioHang.AppendChild(tongTien);

            doc.DocumentElement.AppendChild(gioHang);
            //root.AppendChild(sach); //sau khi tạo xong thì thêm vào gốc root
            doc.Save(fileName); //lưu dữ liệu

            return true;
        }

        public bool suaGioHang(GioHang gh)
        {
            doc = new XmlDocument(); //Tao moi 1 doi tuong XmlDocument
            doc.Load(fileName); //Load file xml theo duong dan
            list = new List<GioHang>();

            loadDataFromDoc(doc, fileName, list);
            if (!isExistId(list, gh.MaGioHang)) //Kiem tra ma co ton tai hay khong
                return false; //Neu khong tra ve false

            XmlNodeList sNode = doc.GetElementsByTagName("GioHang"); //Lay danh sach cac nut sach tu tai lieu            

            foreach (XmlNode node in sNode)
            {
                string maGH = node.ChildNodes[0].InnerText; //Lay ra MaSach cua nut Sach hien tai
                if (maGH == gh.MaGioHang) //Neu MaSach cua nut hien tai trung voi MaSach can sua
                {
                    //Thuc hien sua doi thong tin cua nut nay
                    node.ChildNodes[0].InnerText = gh.MaGioHang;
                    node.ChildNodes[1].InnerText = gh.MaKhachHang;
                    node.ChildNodes[2].InnerText = Convert.ToString(gh.NgayMua);
                    node.ChildNodes[3].InnerText = Convert.ToString(gh.TongTien);
                    break;
                }
            }

            doc.Save(fileName); // Sau khi sua luu lai file

            return true; //tra ve true khi sua thanh cong
        }

        public bool xoaGioHang(string maGH)
        {
            doc = new XmlDocument();
            doc.Load(fileName);
            list = new List<GioHang>();

            loadDataFromDoc(doc, fileName, list);
            if (!isExistId(list, maGH)) //Kiem tra ma co ton tai hay khong
                return false; //Neu khong tra ve false

            XmlNodeList ghNode = doc.GetElementsByTagName("GioHang"); //Lay danh sach cac nut

            foreach (XmlNode node in ghNode) //Duyet danh sach
            {
                string id = node.ChildNodes[0].InnerText; // Lay ra MaSach cua nut Sach hien tai
                if (id == maGH) //Neu MaSach cua nut hien tai trung voi MaSach can xoa
                {
                    doc.DocumentElement.RemoveChild(node);
                    break;
                }
            }

            doc.Save(fileName);

            return true;
        }
    }
}
