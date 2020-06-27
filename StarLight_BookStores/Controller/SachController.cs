using StarLight_BookStores.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StarLight_BookStores.Controller
{
    class SachController
    {
        private XmlDocument doc = new XmlDocument();
        private XmlElement root;
        private string fileName = @"..//..//MyData//data.xml";
        List<Sach> list;

        public SachController()
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
            XmlNodeList lvNode = doc.GetElementsByTagName("Sach");
            //Lay ra cac nut co the ten la nhanvien
            foreach (XmlNode node in lvNode) //Duyet tung nut
            {
                id = node.ChildNodes[1].InnerText;
                max = int.Parse(id.Substring(3)) + 1; // SA0xx
            }
            return max;
        }

        public void loadDataFromDoc(XmlDocument doc, string fileName, List<Sach> list)
        {
            doc = new XmlDocument(); //Tao moi mot doi tuong XmlDocumnent
            doc.Load(fileName); //Load file theo duong dan
            XmlNodeList sNode = doc.GetElementsByTagName("Sach");
            //Lay ra cac nut co the ten la sach
            foreach (XmlNode node in sNode) //Duyet tung nut
            {
                //Khai bao cac bien luu tri thong tin cua sach
                string linhVuc, maSach, tenSach, tacGia, ngonNgu;
                int namXB, soLuong;
                double gia;

                linhVuc = node.ChildNodes[0].InnerText; //Lay ra gia tri nut con cua nut Sach co index = 0                
                maSach = node.ChildNodes[1].InnerText;
                tenSach = node.ChildNodes[2].InnerText;
                tacGia = node.ChildNodes[3].InnerText;
                namXB = int.Parse(node.ChildNodes[4].InnerText);
                ngonNgu = node.ChildNodes[5].InnerText;
                soLuong = int.Parse(node.ChildNodes[6].InnerText);
                gia = double.Parse(node.ChildNodes[7].InnerText);

                Sach e = new Sach(linhVuc, maSach, tenSach, tacGia, namXB, ngonNgu, soLuong, gia);
                list.Add(e);// them vao danh sach Sach
            }
        }

        public bool isExistId(List<Sach> list, string id)
        {
            bool isExist = false;

            foreach (Sach item in list)
            {
                string maSach = item.MaSach; //lay ra id cua Sach hien tai
                if (maSach.ToLower() == id.ToLower()) //chuyen sang chu thuong va so sanh
                {
                    //neu ma hai chuoi bang nhau
                    isExist = true; //trung ma
                    break;
                }
            }

            return isExist;//tra ve ket qua
        }

        public bool themSach(Sach s)
        {
            doc = new XmlDocument(); // Tao moi 1 doi tuong XmlDocument
            doc.Load(fileName); // Load file xml theo duong dan
            list = new List<Sach>(); // Tao moi 1 danh sach cac sach

            loadDataFromDoc(doc, fileName, list);
            if (isExistId(list, s.MaSach)) // Kiem tra da ton tai ma
                return false; //da ton tai tra ve false
            //Khong roi vao hai truong hop day ta di thuc hien them sach

            XmlElement sach = doc.CreateElement("Sach"); //tạo nút Sach

            XmlElement linhVuc = doc.CreateElement("TenLinhVuc");
            linhVuc.InnerText = s.LinhVuc;
            sach.AppendChild(linhVuc);

            XmlElement maSach = doc.CreateElement("MaSach"); //tạo nút con của Sach là MaSach
            maSach.InnerText = s.MaSach; //gán giá trị cho MaSach
            sach.AppendChild(maSach); //thêm MaSach vào trong Sach(nhận MaSach là con)

            XmlElement tenSach = doc.CreateElement("TenSach");
            tenSach.InnerText = s.TenSach;
            sach.AppendChild(tenSach);

            XmlElement tacGia = doc.CreateElement("TenTacGia");
            tacGia.InnerText = s.TacGia;
            sach.AppendChild(tacGia);

            XmlElement namXB = doc.CreateElement("NamXuatBan");
            namXB.InnerText = Convert.ToString(s.NamXB);
            sach.AppendChild(namXB);

            XmlElement ngonNgu = doc.CreateElement("NgonNgu");
            ngonNgu.InnerText = s.NgonNgu;
            sach.AppendChild(ngonNgu);

            XmlElement soLuong = doc.CreateElement("SoLuong");
            soLuong.InnerText = Convert.ToString(s.SoLuong);
            sach.AppendChild(soLuong);

            XmlElement gia = doc.CreateElement("Gia");
            gia.InnerText = Convert.ToString(s.Gia);
            sach.AppendChild(gia);

            doc.DocumentElement.AppendChild(sach);
            //root.AppendChild(sach); //sau khi tạo xong thì thêm Sach vào gốc root
            doc.Save(fileName); //lưu dữ liệu

            return true;
        }

        public bool suaSach(Sach s)
        {
            doc = new XmlDocument(); //Tao moi 1 doi tuong XmlDocument
            doc.Load(fileName); //Load file xml theo duong dan
            list = new List<Sach>();

            loadDataFromDoc(doc, fileName, list);
            if (!isExistId(list, s.MaSach)) //Kiem tra ma co ton tai hay khong
                return false; //Neu khong tra ve false

            XmlNodeList sNode = doc.GetElementsByTagName("Sach"); //Lay danh sach cac nut sach tu tai lieu            

            foreach (XmlNode node in sNode)
            {
                string maSach = node.ChildNodes[1].InnerText; //Lay ra MaSach cua nut Sach hien tai
                if (maSach == s.MaSach) //Neu MaSach cua nut hien tai trung voi MaSach can sua
                {
                    //Thuc hien sua doi thong tin cua nut nay
                    node.ChildNodes[0].InnerText = s.LinhVuc;
                    node.ChildNodes[1].InnerText = s.MaSach;
                    node.ChildNodes[2].InnerText = s.TenSach;
                    node.ChildNodes[3].InnerText = s.TacGia;
                    node.ChildNodes[4].InnerText = Convert.ToString(s.NamXB);
                    node.ChildNodes[5].InnerText = s.NgonNgu;
                    node.ChildNodes[6].InnerText = Convert.ToString(s.SoLuong);
                    node.ChildNodes[7].InnerText = Convert.ToString(s.Gia);
                    break;
                }
            }

            doc.Save(fileName); // Sau khi sua luu lai file

            return true; //tra ve true khi sua thanh cong
        }

        public bool xoaSach(string maSach)
        {
            doc = new XmlDocument();
            doc.Load(fileName);
            list = new List<Sach>();

            loadDataFromDoc(doc, fileName, list);
            if (!isExistId(list, maSach)) //Kiem tra ma co ton tai hay khong
                return false; //Neu khong tra ve false

            XmlNodeList sNode = doc.GetElementsByTagName("Sach"); //Lay danh sach cac nut Sach tu tai lieu

            foreach (XmlNode node in sNode) //Duyet danh sach cac nut Sach
            {
                string id = node.ChildNodes[1].InnerText; // Lay ra MaSach cua nut Sach hien tai
                if (id == maSach) //Neu MaSach cua nut hien tai trung voi MaSach can xoa
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
