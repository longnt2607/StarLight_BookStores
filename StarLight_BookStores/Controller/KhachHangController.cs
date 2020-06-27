using StarLight_BookStores.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StarLight_BookStores.Controller
{
    class KhachHangController
    {
        private XmlDocument doc = new XmlDocument();
        private XmlElement root;
        private string fileName = @"..//..//MyData//data.xml";
        List<KhachHang> list;

        public KhachHangController()
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
            XmlNodeList lvNode = doc.GetElementsByTagName("KhachHang");
            //Lay ra cac nut co the ten la nhanvien
            foreach (XmlNode node in lvNode) //Duyet tung nut
            {
                id = node.ChildNodes[0].InnerText;
                max = int.Parse(id.Substring(3)) + 1; // SA0xx
            }
            return max;
        }

        public void loadDataFromDoc(XmlDocument doc, string fileName, List<KhachHang> list)
        {
            doc = new XmlDocument(); //Tao moi mot doi tuong XmlDocumnent
            doc.Load(fileName); //Load file theo duong dan
            XmlNodeList khNode = doc.GetElementsByTagName("KhachHang");
            //Lay ra cac nut co the ten la KhachHang
            foreach (XmlNode node in khNode) //Duyet tung nut
            {
                //Khai bao cac bien luu tri thong tin cua khach hang
                string maKH, tenKH, sdt, diaChi, gioiTinh;

                maKH = node.ChildNodes[0].InnerText; //Lay ra gia tri nut con cua nut KhachHang co index = 0                
                tenKH = node.ChildNodes[1].InnerText;
                diaChi = node.ChildNodes[2].InnerText;
                sdt = node.ChildNodes[3].InnerText;
                gioiTinh = node.ChildNodes[4].InnerText;

                KhachHang e = new KhachHang(maKH, tenKH, diaChi, sdt, gioiTinh);
                list.Add(e);// them vao danh sach KhachHang
            }
        }

        public bool isExistId(List<KhachHang> list, string id)
        {
            bool isExist = false;

            foreach (KhachHang item in list)
            {
                string maKH = item.MaKH; //lay ra id cua KhachHang hien tai
                if (maKH.ToLower() == id.ToLower()) //chuyen sang chu thuong va so sanh
                {
                    //neu ma hai chuoi bang nhau
                    isExist = true; //trung ma
                    break;
                }
            }

            return isExist;//tra ve ket qua
        }

        public bool themKhachHang(KhachHang kh)
        {
            doc = new XmlDocument(); // Tao moi 1 doi tuong XmlDocument
            doc.Load(fileName); // Load file xml theo duong dan
            list = new List<KhachHang>(); // Tao moi 1 danh sach cac linh vuc

            loadDataFromDoc(doc, fileName, list);
            if (isExistId(list, kh.MaKH)) // Kiem tra da ton tai ma
                return false; //da ton tai tra ve false
            //Khong roi vao hai truong hop day ta di thuc hien them khach hang

            XmlElement khachHang = doc.CreateElement("KhachHang"); //tạo nút KhachHang

            XmlElement maKH = doc.CreateElement("MaKH"); //tạo nút con của KhachHang là MaKH
            maKH.InnerText = kh.MaKH; //gán giá trị cho MaKH
            khachHang.AppendChild(maKH); //thêm MaKH vào trong KhachHang(nhận MaKH là con)

            XmlElement tenKH = doc.CreateElement("TenKH");
            tenKH.InnerText = kh.TenKH;
            khachHang.AppendChild(tenKH);

            XmlElement diaChi = doc.CreateElement("DiaChi");
            diaChi.InnerText = kh.DiaChi;
            khachHang.AppendChild(diaChi);

            XmlElement sdt = doc.CreateElement("SDT");
            sdt.InnerText = kh.Sdt;
            khachHang.AppendChild(sdt);

            XmlElement gioiTinh = doc.CreateElement("GioiTinh");
            gioiTinh.InnerText = kh.GioiTinh;
            khachHang.AppendChild(gioiTinh);

            doc.DocumentElement.AppendChild(khachHang);
            //root.AppendChild(khachHang); //sau khi tạo xong thì thêm KhachHang vào gốc root
            doc.Save(fileName); //lưu dữ liệu

            return true;
        }

        public bool suaKhachHang(KhachHang kh)
        {
            doc = new XmlDocument(); //Tao moi 1 doi tuong XmlDocument
            doc.Load(fileName); //Load file xml theo duong dan
            list = new List<KhachHang>();

            loadDataFromDoc(doc, fileName, list);
            if (!isExistId(list, kh.MaKH)) //Kiem tra ma co ton tai hay khong
                return false; //Neu khong tra ve false

            XmlNodeList khNode = doc.GetElementsByTagName("KhachHang"); //Lay danh sach cac nut khach hang tu tai lieu            

            foreach (XmlNode node in khNode)
            {
                string maLV = node.ChildNodes[0].InnerText; //Lay ra MaKH cua nut KhachHang hien tai
                if (maLV == kh.MaKH) //Neu MaKH cua nut hien tai trung voi MaKH can sua
                {
                    //Thuc hien sua doi thong tin cua nut nay
                    node.ChildNodes[0].InnerText = kh.MaKH;
                    node.ChildNodes[1].InnerText = kh.TenKH;
                    node.ChildNodes[2].InnerText = kh.DiaChi;
                    node.ChildNodes[3].InnerText = kh.Sdt;
                    node.ChildNodes[4].InnerText = kh.GioiTinh;
                    break;
                }
            }

            doc.Save(fileName); // Sau khi sua luu lai file

            return true; //tra ve true khi sua thanh cong
        }

        public bool xoaKhachHang(string maKH)
        {
            doc = new XmlDocument();
            doc.Load(fileName);
            list = new List<KhachHang>();

            loadDataFromDoc(doc, fileName, list);
            if (!isExistId(list, maKH)) //Kiem tra ma co ton tai hay khong
                return false; //Neu khong tra ve false

            XmlNodeList khNode = doc.GetElementsByTagName("KhachHang"); //Lay danh sach cac nut KhachHang tu tai lieu

            foreach (XmlNode node in khNode) //Duyet danh sach cac nut KhachHang
            {
                string id = node.ChildNodes[0].InnerText; // Lay ra MaKH cua nut KhachHang hien tai
                if (id == maKH) //Neu MaKH cua nut hien tai trung voi MaKH can xoa
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
