using StarLight_BookStores.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StarLight_BookStores.Controller
{
    class TacGiaController
    {
        private XmlDocument doc = new XmlDocument();
        private XmlElement root;
        private string fileName = @"..//..//MyData//data.xml";
        List<TacGia> list;

        public TacGiaController()
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
            XmlNodeList tgNode = doc.GetElementsByTagName("TacGia");
            //Lay ra cac nut co the ten la nhanvien
            foreach (XmlNode node in tgNode) //Duyet tung nut
            {
                id = node.ChildNodes[0].InnerText;
                max = int.Parse(id.Substring(3)) + 1; // TG0xx
            }
            return max;
        }

        public void loadDataFromDoc(XmlDocument doc, string fileName, List<TacGia> list)
        {
            doc = new XmlDocument(); //Tao moi mot doi tuong XmlDocumnent
            doc.Load(fileName); //Load file theo duong dan
            XmlNodeList tgNode = doc.GetElementsByTagName("TacGia");
            //Lay ra cac nut co the ten la nhanvien
            foreach (XmlNode node in tgNode) //Duyet tung nut
            {
                //Khai bao cac bien luu tri thong tin cua nhan vien
                string maTG, tenTG, diaChi, sdt, email, gioiTinh, quocTich;

                maTG = node.ChildNodes[0].InnerText; //Lay ra gia tri nut con cua nut LinhVuc co index = 0                
                tenTG = node.ChildNodes[1].InnerText;
                diaChi = node.ChildNodes[2].InnerText;
                sdt = node.ChildNodes[3].InnerText;
                email = node.ChildNodes[4].InnerText;
                gioiTinh = node.ChildNodes[5].InnerText;
                quocTich = node.ChildNodes[6].InnerText;

                TacGia tg = new TacGia(maTG, tenTG, diaChi, sdt, email, gioiTinh, quocTich);
                list.Add(tg);// them vao danh sach LinhVuc
            }
        }

        public bool isExistId(List<TacGia> list, string id)
        {
            bool isExist = false;

            foreach (TacGia item in list)
            {
                string maTacGia = item.MaTacGia; //lay ra id cua LinhVuc hien tai
                if (maTacGia.ToLower() == id.ToLower()) //chuyen sang chu thuong va so sanh
                {
                    //neu ma hai chuoi bang nhau
                    isExist = true; //trung ma
                    break;
                }
            }

            return isExist;//tra ve ket qua
        }

        public bool themTacGia(TacGia TG)
        {
            doc = new XmlDocument(); // Tao moi 1 doi tuong XmlDocument
            doc.Load(fileName); // Load file xml theo duong dan
            list = new List<TacGia>(); // Tao moi 1 danh sach cac linh vuc

            loadDataFromDoc(doc, fileName, list);
            if (isExistId(list, TG.MaTacGia)) // Kiem tra da ton tai ma
                return false; //da ton tai tra ve false
            //Khong roi vao hai truong hop day ta di thuc hien them nhan vien

            XmlElement tacgia = doc.CreateElement("TacGia"); //tạo nút LinhVuc

            XmlElement maTacGia = doc.CreateElement("MaTacGia"); //tạo nút con của LinhVuc là MaLinhVuc
            maTacGia.InnerText = TG.MaTacGia; //gán giá trị cho MaLinhVuc
            tacgia.AppendChild(maTacGia); //thêm MaLinhVuc vào trong LinhVuc(nhận MaLinhVuc là con)

            XmlElement tenTacGia = doc.CreateElement("TenTacGia");
            tenTacGia.InnerText = TG.TenTacGia;
            tacgia.AppendChild(tenTacGia);

            XmlElement diaChi = doc.CreateElement("DiaChi");
            diaChi.InnerText = TG.DiaChi;
            tacgia.AppendChild(diaChi);

            XmlElement sdt = doc.CreateElement("SDT");
            sdt.InnerText = TG.Sdt;
            tacgia.AppendChild(sdt);

            XmlElement email = doc.CreateElement("Email");
            email.InnerText = TG.Email;
            tacgia.AppendChild(email);

            XmlElement gioiTinh = doc.CreateElement("GioiTinh");
            gioiTinh.InnerText = TG.GioiTinh;
            tacgia.AppendChild(gioiTinh);

            XmlElement quocTich = doc.CreateElement("QuocTich");
            quocTich.InnerText = TG.QuocTich;
            tacgia.AppendChild(quocTich);

            doc.DocumentElement.AppendChild(tacgia);
            //root.AppendChild(linhvuc); //sau khi tạo xong thì thêm LinhVuc vào gốc root
            doc.Save(fileName); //lưu dữ liệu

            return true;
        }

        public bool suaTacGia(TacGia TG)
        {
            doc = new XmlDocument(); //Tao moi 1 doi tuong XmlDocument
            doc.Load(fileName); //Ltoad file xml theo duong dan
            list = new List<TacGia>();

            loadDataFromDoc(doc, fileName, list);
            if (!isExistId(list, TG.MaTacGia)) //Kiem tra ma co ton tai hay khong
                return false; //Neu khong tra ve false

            XmlNodeList tgNode = doc.GetElementsByTagName("TacGia"); //Lay danh sach cac nut linh vuc tu tai lieu            

            foreach (XmlNode node in tgNode)
            {
                string maTacGia = node.ChildNodes[0].InnerText; //Lay ra MaLinhVuc cua nut LinhVuc hien tai
                if (maTacGia == TG.MaTacGia) //Neu MaLinhVuc cua nut hien tai trung voi MaLinhVuc can sua
                {
                    //Thuc hien sua doi thong tin cua nut nay
                    node.ChildNodes[0].InnerText = TG.MaTacGia;
                    node.ChildNodes[1].InnerText = TG.TenTacGia;
                    node.ChildNodes[2].InnerText = TG.DiaChi;
                    node.ChildNodes[3].InnerText = TG.Sdt;
                    node.ChildNodes[4].InnerText = TG.Email;
                    node.ChildNodes[5].InnerText = TG.GioiTinh;
                    node.ChildNodes[6].InnerText = TG.QuocTich;
                    break;
                }
            }

            doc.Save(fileName); // Sau khi sua luu lai file

            return true; //tra ve true khi sua thanh cong
        }

        public bool xoaTacGia(string maTacGia)
        {
            doc = new XmlDocument();
            doc.Load(fileName);
            list = new List<TacGia>();

            loadDataFromDoc(doc, fileName, list);
            if (!isExistId(list, maTacGia)) //Kiem tra ma co ton tai hay khong
                return false; //Neu khong tra ve false

            XmlNodeList lvNode = doc.GetElementsByTagName("TacGia"); //Lay danh sach cac nut LinhVuc tu tai lieu

            foreach (XmlNode node in lvNode) //Duyet danh sach cac nut LinhVuc
            {
                string id = node.ChildNodes[0].InnerText; // Lay ra MaLinhVuc cua nut LinhVuc hien tai
                if (id == maTacGia) //Neu MaLinhVuc cua nut hien tai trung voi MaLinhVuc can xoa
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
