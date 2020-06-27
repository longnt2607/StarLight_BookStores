using StarLight_BookStores.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StarLight_BookStores.Controller
{
    class LinhVucController
    {
        private XmlDocument doc = new XmlDocument();
        private XmlElement root;
        private string fileName = @"..//..//MyData//data.xml";
        List<LinhVuc> list;

        public LinhVucController()
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
            XmlNodeList lvNode = doc.GetElementsByTagName("LinhVuc");
            //Lay ra cac nut co the ten la nhanvien
            foreach (XmlNode node in lvNode) //Duyet tung nut
            {
                id = node.ChildNodes[0].InnerText;
                max = int.Parse(id.Substring(3)) + 1; // LV0xx
            }
            return max;
        }

        public void loadDataFromDoc(XmlDocument doc, string fileName, List<LinhVuc> list)
        {
            doc = new XmlDocument(); //Tao moi mot doi tuong XmlDocumnent
            doc.Load(fileName); //Load file theo duong dan
            XmlNodeList lvNode = doc.GetElementsByTagName("LinhVuc");
            //Lay ra cac nut co the ten la nhanvien
            foreach (XmlNode node in lvNode) //Duyet tung nut
            {
                //Khai bao cac bien luu tri thong tin cua nhan vien
                string maLV, tenLV;

                maLV = node.ChildNodes[0].InnerText; //Lay ra gia tri nut con cua nut LinhVuc co index = 0                
                tenLV = node.ChildNodes[1].InnerText;

                LinhVuc e = new LinhVuc(maLV, tenLV);
                list.Add(e);// them vao danh sach LinhVuc
            }
        }

        public bool isExistId(List<LinhVuc> list, string id)
        {
            bool isExist = false;

            foreach (LinhVuc item in list)
            {
                string maLV = item.MaLinhVuc; //lay ra id cua LinhVuc hien tai
                if (maLV.ToLower() == id.ToLower()) //chuyen sang chu thuong va so sanh
                {
                    //neu ma hai chuoi bang nhau
                    isExist = true; //trung ma
                    break;
                }
            }

            return isExist;//tra ve ket qua
        }

        public bool themLinhVuc(LinhVuc lv)
        {
            doc = new XmlDocument(); // Tao moi 1 doi tuong XmlDocument
            doc.Load(fileName); // Load file xml theo duong dan
            list = new List<LinhVuc>(); // Tao moi 1 danh sach cac linh vuc

            loadDataFromDoc(doc, fileName, list);
            if (isExistId(list, lv.MaLinhVuc)) // Kiem tra da ton tai ma
                return false; //da ton tai tra ve false
            //Khong roi vao hai truong hop day ta di thuc hien them nhan vien

            XmlElement linhvuc = doc.CreateElement("LinhVuc"); //tạo nút LinhVuc

            XmlElement maLinhVuc = doc.CreateElement("MaLinhVuc"); //tạo nút con của LinhVuc là MaLinhVuc
            maLinhVuc.InnerText = lv.MaLinhVuc; //gán giá trị cho MaLinhVuc
            linhvuc.AppendChild(maLinhVuc); //thêm MaLinhVuc vào trong LinhVuc(nhận MaLinhVuc là con)

            XmlElement tenLinhVuc = doc.CreateElement("TenLinhVuc");
            tenLinhVuc.InnerText = lv.TenLinhVuc;
            linhvuc.AppendChild(tenLinhVuc);

            doc.DocumentElement.AppendChild(linhvuc);
            //root.AppendChild(linhvuc); //sau khi tạo xong thì thêm LinhVuc vào gốc root
            doc.Save(fileName); //lưu dữ liệu

            return true;
        }

        public bool suaLinhVuc(LinhVuc lv)
        {
            doc = new XmlDocument(); //Tao moi 1 doi tuong XmlDocument
            doc.Load(fileName); //Load file xml theo duong dan
            list = new List<LinhVuc>();

            loadDataFromDoc(doc, fileName, list);
            if (!isExistId(list, lv.MaLinhVuc)) //Kiem tra ma co ton tai hay khong
                return false; //Neu khong tra ve false

            XmlNodeList lvNode = doc.GetElementsByTagName("LinhVuc"); //Lay danh sach cac nut linh vuc tu tai lieu            

            foreach (XmlNode node in lvNode)
            {
                string maLV = node.ChildNodes[0].InnerText; //Lay ra MaLinhVuc cua nut LinhVuc hien tai
                if (maLV == lv.MaLinhVuc) //Neu MaLinhVuc cua nut hien tai trung voi MaLinhVuc can sua
                {
                    //Thuc hien sua doi thong tin cua nut nay
                    node.ChildNodes[0].InnerText = lv.MaLinhVuc;
                    node.ChildNodes[1].InnerText = lv.TenLinhVuc;
                    break;
                }
            }

            doc.Save(fileName); // Sau khi sua luu lai file

            return true; //tra ve true khi sua thanh cong
        }

        public bool xoaLinhVuc(string maLV)
        {
            doc = new XmlDocument();
            doc.Load(fileName);
            list = new List<LinhVuc>();

            loadDataFromDoc(doc, fileName, list);
            if (!isExistId(list, maLV)) //Kiem tra ma co ton tai hay khong
                return false; //Neu khong tra ve false

            XmlNodeList lvNode = doc.GetElementsByTagName("LinhVuc"); //Lay danh sach cac nut LinhVuc tu tai lieu

            foreach (XmlNode node in lvNode) //Duyet danh sach cac nut LinhVuc
            {
                string id = node.ChildNodes[0].InnerText; // Lay ra MaLinhVuc cua nut LinhVuc hien tai
                if (id == maLV) //Neu MaLinhVuc cua nut hien tai trung voi MaLinhVuc can xoa
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
