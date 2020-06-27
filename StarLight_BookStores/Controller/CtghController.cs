using StarLight_BookStores.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StarLight_BookStores.Controller
{
    class CtghController
    {
        private XmlDocument doc = new XmlDocument();
        private XmlElement root;
        private string fileName = @"..//..//MyData//data.xml";
        List<Ctgh> list;

        public CtghController()
        {
            doc.Load(fileName);
            root = doc.DocumentElement;
        }

        public void loadDataFromDoc(XmlDocument doc, string fileName, List<Ctgh> list)
        {
            doc = new XmlDocument(); //Tao moi mot doi tuong XmlDocumnent
            doc.Load(fileName); //Load file theo duong dan
            XmlNodeList ctghNode = doc.GetElementsByTagName("ChiTietGioHang");
            //Lay ra cac nut co the ten la sach
            foreach (XmlNode node in ctghNode) //Duyet tung nut
            {
                //Khai bao cac bien luu tri thong tin cua sach
                string maGH, maSach;
                int soLuongMua;
                double giaBan;

                maGH = node.ChildNodes[0].InnerText;
                maSach = node.ChildNodes[1].InnerText;
                soLuongMua = Convert.ToInt32(node.ChildNodes[2].InnerText);
                giaBan = Convert.ToDouble(node.ChildNodes[3].InnerText);

                Ctgh ctgh = new Ctgh(maGH, maSach, soLuongMua, giaBan);
                list.Add(ctgh);
            }
        }

        public bool isExistId(List<Ctgh> list, string id)
        {
            bool isExist = false;

            foreach (Ctgh item in list)
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

        public bool themSachVaoGioHang(Ctgh ctgh)
        {
            doc = new XmlDocument(); // Tao moi 1 doi tuong XmlDocument
            doc.Load(fileName); // Load file xml theo duong dan
            list = new List<Ctgh>(); // Tao moi 1 danh sach

            loadDataFromDoc(doc, fileName, list);
//            if (isExistId(list, ctgh.MaGioHang)) // Kiem tra da ton tai ma
//               return false; //da ton tai tra ve false
            //Khong roi vao hai truong hop day ta di thuc hien them

            XmlElement ctGioHang = doc.CreateElement("ChiTietGioHang");

            XmlElement maGioHang = doc.CreateElement("MaGioHang");
            maGioHang.InnerText = ctgh.MaGioHang;
            ctGioHang.AppendChild(maGioHang);

            XmlElement maSach = doc.CreateElement("MaSach");
            maSach.InnerText = ctgh.MaSach;
            ctGioHang.AppendChild(maSach);

            XmlElement soLuongMua = doc.CreateElement("SoLuongMua");
            soLuongMua.InnerText = Convert.ToString(ctgh.SoLuongMua);
            ctGioHang.AppendChild(soLuongMua);

            XmlElement giaBan = doc.CreateElement("GiaBan");
            giaBan.InnerText = Convert.ToString(ctgh.GiaBan);
            ctGioHang.AppendChild(giaBan);

            doc.DocumentElement.AppendChild(ctGioHang);
            //root.AppendChild(sach); //sau khi tạo xong thì thêm vào gốc root
            doc.Save(fileName); //lưu dữ liệu

            return true;
        }

        public bool suaSachTrongGioHang(Ctgh ctgh)
        {
            doc = new XmlDocument(); //Tao moi 1 doi tuong XmlDocument
            doc.Load(fileName); //Load file xml theo duong dan
            list = new List<Ctgh>();

            loadDataFromDoc(doc, fileName, list);
            if (!isExistId(list, ctgh.MaGioHang)) //Kiem tra ma co ton tai hay khong
                return false; //Neu khong tra ve false

            XmlNodeList ctghNode = doc.GetElementsByTagName("ChiTietGioHang"); //Lay danh sach cac nut sach tu tai lieu            

            foreach (XmlNode node in ctghNode)
            {
                string maGH = node.ChildNodes[0].InnerText; //Lay ra MaSach cua nut Sach hien tai
                if (maGH == ctgh.MaGioHang) //Neu MaSach cua nut hien tai trung voi MaSach can sua
                {
                    //Thuc hien sua doi thong tin cua nut nay
                    node.ChildNodes[0].InnerText = ctgh.MaGioHang;
                    node.ChildNodes[1].InnerText = ctgh.MaSach;
                    node.ChildNodes[2].InnerText = Convert.ToString(ctgh.SoLuongMua);
                    node.ChildNodes[3].InnerText = Convert.ToString(ctgh.GiaBan);
                    break;
                }
            }

            doc.Save(fileName); // Sau khi sua luu lai file

            return true; //tra ve true khi sua thanh cong
        }

        public bool xoaSachTrongGioHang(string maGH, string maSach)
        {
            doc = new XmlDocument();
            doc.Load(fileName);
            list = new List<Ctgh>();

            loadDataFromDoc(doc, fileName, list);
            if (!isExistId(list, maGH)) //Kiem tra ma co ton tai hay khong
                return false; //Neu khong tra ve false

            XmlNodeList ctghNode = doc.GetElementsByTagName("ChiTietGioHang"); //Lay danh sach cac nut

            foreach (XmlNode node in ctghNode) //Duyet danh sach
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
