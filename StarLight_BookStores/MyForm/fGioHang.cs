using StarLight_BookStores.Controller;
using StarLight_BookStores.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace StarLight_BookStores.MyForm
{
    public partial class fGioHang : Form
    {
        GioHangController handler = new GioHangController();
        KhachHangController khHandler = new KhachHangController();
        SachController sHandler = new SachController();
        CtghController ctghHandler = new CtghController();
        XmlDocument doc = new XmlDocument();
        string fileName = @"..//..//MyData//data.xml";
        string maSach, maGHMoi = "";

        public fGioHang()
        {
            InitializeComponent();
        }

        private void fGioHang_Load(object sender, EventArgs e)
        {
            show();
        }

        public void show()
        {
            List<KhachHang> khList = new List<KhachHang>();
            khHandler.loadDataFromDoc(doc, fileName, khList);

            List<GioHang> list = new List<GioHang>();
            handler.loadDataFromDoc(doc, fileName, list);
            dgvGioHang.Rows.Clear(); //xoa het du lieu cac dong
            int rowIndex = 0; //chi so cua dong cua bang
            foreach (GioHang item in list)
            {
                dgvGioHang.Rows.Add();

                dgvGioHang.Rows[rowIndex].Cells[0].Value = item.MaGioHang;
                dgvGioHang.Rows[rowIndex].Cells[1].Value = item.MaKhachHang;
                foreach (KhachHang kh in khList)
                {
                    if (kh.MaKH == item.MaKhachHang)
                    {
                        dgvGioHang.Rows[rowIndex].Cells[2].Value = kh.TenKH;
                    }
                }
                dgvGioHang.Rows[rowIndex].Cells[3].Value = item.NgayMua.ToShortDateString();
                dgvGioHang.Rows[rowIndex].Cells[4].Value = item.TongTien.ToString();

                rowIndex++;
            }

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            
            List<Sach> sList = new List<Sach>();
            sHandler.loadDataFromDoc(doc, fileName, sList);
            cboTenSach.Items.Clear();
            foreach (Sach item in sList)
            {
                collection.Add(item.TenSach);
                cboTenSach.Items.Add(item.TenSach);
            }
            cboTenSach.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboTenSach.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboTenSach.AutoCompleteCustomSource = collection;
            cboSoLuong.DropDownHeight = cboSoLuong.Font.Height * 5; // set max item can display on combobox

            AutoCompleteStringCollection khCollection = new AutoCompleteStringCollection();
            cboKH.Items.Clear();
            foreach (KhachHang item in khList)
            {
                khCollection.Add(item.TenKH);
                cboKH.Items.Add(item.TenKH);
            }
            cboKH.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboKH.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboKH.AutoCompleteCustomSource = khCollection;
            cboKH.DropDownHeight = cboKH.Font.Height * 5;

            StringBuilder builder = new StringBuilder();
            maGHMoi = builder.Append("GH0").Append(handler.getMaxId()).ToString();

            txtGiaBan.Text = "";
            //txtMaGH.Text = "";
            //txtMaKH.Text = "";
            //cboKH.Text = "";
            cboSoLuong.Text = "";
            cboTenSach.Text = "";
        }

        public void showCTGH(string maGH)
        {
            List<Ctgh> ctkhList = new List<Ctgh>();
            ctghHandler.loadDataFromDoc(doc, fileName, ctkhList);

            List<Sach> sList = new List<Sach>();
            sHandler.loadDataFromDoc(doc, fileName, sList);

            dgvCTGH.Rows.Clear();

            int rowIndex = 0; //chi so cua dong cua bang
            foreach (Ctgh item in ctkhList)
            {
                //dgvCTGH.Rows.Add();

                if (item.MaGioHang == maGH)
                {
                    dgvCTGH.Rows.Add();
                    foreach (Sach s in sList)
                    {
                        if (s.MaSach == item.MaSach)
                        {
                            dgvCTGH.Rows[rowIndex].Cells[0].Value = s.LinhVuc;
                            dgvCTGH.Rows[rowIndex].Cells[1].Value = s.MaSach;
                            dgvCTGH.Rows[rowIndex].Cells[2].Value = s.TenSach;
                            dgvCTGH.Rows[rowIndex].Cells[3].Value = s.NamXB;
                            dgvCTGH.Rows[rowIndex].Cells[4].Value = s.NgonNgu;
                        }
                    }
                    dgvCTGH.Rows[rowIndex].Cells[5].Value = item.SoLuongMua.ToString();
                    dgvCTGH.Rows[rowIndex].Cells[6].Value = item.GiaBan;

                    rowIndex++;
                }                 
            }          
        }

        private bool notEmptyFields()
        {
            if (cboKH.Text.Trim() == "")
                return false;
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maGH, maKH, tenKH, ngayMua;
            maGH = maGHMoi;
            maKH = txtMaKH.Text;
            tenKH = cboKH.Text;
            ngayMua = dtpNgayMua.Text;

            if (!notEmptyFields()) //neu chua nhap day du thong tin
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                GioHang gh = new GioHang(maGH, maKH, DateTime.Parse(ngayMua), 0);
                if (!handler.themGioHang(gh)) //Neu them khong thanh cong
                    MessageBox.Show("Mã giỏ hàng: " + maGH + " đã tồn tại.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaGH.Focus(); //set focus
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maGH, maKH, tenKH, ngayMua;
            maGH = txtMaGH.Text;
            maKH = txtMaKH.Text;
            tenKH = cboKH.Text;
            ngayMua = dtpNgayMua.Text;

            if (!notEmptyFields()) //neu chua nhap day du thong tin
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                GioHang gh = new GioHang(maGH, maKH, DateTime.Parse(ngayMua), 0);
                if (!handler.suaGioHang(gh)) //Neu them khong thanh cong
                    MessageBox.Show("Mã giỏ hàng: " + maKH + " đã tồn tại.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaGH.Focus(); //set focus
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maGH = txtMaGH.Text;
            if (maGH.Trim() == "")
                MessageBox.Show("Xóa theo Mã Giỏ hàng, vui lòng không để trống", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (!handler.xoaGioHang(maGH)) //Neu xoa khong thanh cong
                    MessageBox.Show("Mã Giỏ hàng: " + maGH + " không tồn tại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaGH.Focus(); //set focus vao o text box EmpId
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvGioHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowSeleted = dgvGioHang.CurrentCell.RowIndex; //Lay ra dong hien tai dang duoc chon
            //hien thi cac thong tin o dong hien tai len o text box tuong ung
            txtMaGH.Text= (string)dgvGioHang.Rows[rowSeleted].Cells[0].Value;
            txtMaKH.Text = (string)dgvGioHang.Rows[rowSeleted].Cells[1].Value;
            cboKH.Text = (string)dgvGioHang.Rows[rowSeleted].Cells[2].Value;
            dtpNgayMua.Text = (string)dgvGioHang.Rows[rowSeleted].Cells[3].Value;

            showCTGH(txtMaGH.Text);
        }

        private void btnThemSach_Click(object sender, EventArgs e)
        {
            string tenSach;
            int soLuongMua;
            tenSach = cboTenSach.Text;
            soLuongMua = Convert.ToInt32(cboSoLuong.Text);

            if (cboTenSach.Text.Trim() == "" || cboSoLuong.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng điền tên sách muốn mua và số lượng muốn mua.", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string maGH, maSachMua;
                int soLuong;
                double giaBan;
                maGH = txtMaGH.Text;
                maSachMua = maSach;
                soLuong = Convert.ToInt32(cboSoLuong.Text);
                giaBan = soLuong * Convert.ToDouble(txtGiaBan.Text);
                Ctgh chiTiet = new Ctgh(maGH, maSachMua, soLuong, giaBan);
                if (!ctghHandler.themSachVaoGioHang(chiTiet)) //Neu them khong thanh cong
                    MessageBox.Show("Đã xảy ra lỗi, vui lòng kiểm tra lại.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //show(); //load lai table
                    showCTGH(txtMaGH.Text);
                    cboTenSach.Focus();

                    // cap nhat lai so luong sach
                    updateCombobox(maSachMua, soLuong, true);
                    
                    // cap nhat lai tong gia tien cua hoa don
                    updateGioHang(maGH, giaBan, true);

                    show();
                }
            }
        }

        private void updateGioHang(string maGH, double tien, bool add)
        {
            List<GioHang> ghList = new List<GioHang>();
            handler.loadDataFromDoc(doc, fileName, ghList);

            foreach (GioHang gh in ghList)
            {
                if (gh.MaGioHang == maGH)
                {
                    double t;
                    if (add == true)
                    {
                        t = gh.TongTien + tien;
                    }
                    else
                    {
                        t = gh.TongTien - tien;
                    }
                    GioHang newGH = new GioHang(gh.MaGioHang, gh.MaKhachHang,
                                        gh.NgayMua, t);
                    handler.suaGioHang(newGH);
                }
            }
        }

        private void updateCombobox(string maSachMua, int sl, bool add)
        {
            List<Sach> list = new List<Sach>();
            sHandler.loadDataFromDoc(doc, fileName, list);

            foreach (Sach item in list)
            {
                if (maSachMua == item.MaSach)
                {
                    int slMoi;
                    if (add == true)
                    {
                        slMoi = item.SoLuong - sl;
                    }
                    else
                    {
                        slMoi = item.SoLuong + sl;
                    }
                    Sach s = new Sach(item.LinhVuc, item.MaSach, item.TenSach, item.TacGia, item.NamXB,
                                        item.NgonNgu, slMoi, item.Gia);
                    sHandler.suaSach(s);

                    cboSoLuong.Items.Clear();
                    AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

                    if (slMoi <= 0)
                    {
                        cboSoLuong.Items.Add(0);
                        cboSoLuong.Text = "0";
                        cboSoLuong.Enabled = false;
                    }
                    else
                    {
                        for (int i = 1; i <= item.SoLuong - sl; i++)
                        {
                            collection.Add(i.ToString());
                            cboSoLuong.Items.Add(i);
                        }
                        cboSoLuong.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cboSoLuong.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        cboSoLuong.AutoCompleteCustomSource = collection;
                        cboSoLuong.DropDownHeight = cboSoLuong.Font.Height * 5; // set max item can display on combobox
                    }
                }
            }          
        }

        private void cboTenSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            int soLuongTon = 0;
            string tenSach = cboTenSach.Text;
            List<Sach> sList = new List<Sach>();
            sHandler.loadDataFromDoc(doc, fileName, sList);
            
            foreach (Sach item in sList)
            {
                if (item.TenSach == tenSach)
                {
                    soLuongTon = item.SoLuong;
                    txtGiaBan.Text = item.Gia.ToString();
                    maSach = item.MaSach;
                }
            }

            cboSoLuong.Items.Clear();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            for (int i = 1; i <= soLuongTon; i++)
            {
                collection.Add(i.ToString());
                cboSoLuong.Items.Add(i);
            }
            cboSoLuong.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboSoLuong.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboSoLuong.AutoCompleteCustomSource = collection;
            cboSoLuong.DropDownHeight = cboSoLuong.Font.Height * 5; // set max item can display on combobox
        }

        private void dgvCTGH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowSeleted = dgvCTGH.CurrentCell.RowIndex; //Lay ra dong hien tai dang duoc chon
            //hien thi cac thong tin o dong hien tai len o text box tuong ung
            cboTenSach.Text = (string)dgvCTGH.Rows[rowSeleted].Cells[2].Value;
            cboSoLuong.Text = (string)dgvCTGH.Rows[rowSeleted].Cells[5].Value;
        }

        private void btnSuaSach_Click(object sender, EventArgs e)
        {
            string tenSach;
            int soLuongMua;
            tenSach = cboTenSach.Text;
            soLuongMua = Convert.ToInt32(cboSoLuong.Text);

            if (cboTenSach.Text.Trim() == "" || cboSoLuong.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng điền tên sách muốn mua và số lượng muốn mua.", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string maGH, maSachMua;
                int soLuong;
                double giaBan;
                maGH = txtMaGH.Text;
                maSachMua = maSach;
                soLuong = Convert.ToInt32(cboSoLuong.Text);
                giaBan = soLuong * Convert.ToDouble(txtGiaBan.Text);
                Ctgh chiTiet = new Ctgh(maGH, maSachMua, soLuong, giaBan);
                if (!ctghHandler.suaSachTrongGioHang(chiTiet)) //Neu them khong thanh cong
                    MessageBox.Show("Đã xảy ra lỗi, vui lòng kiểm tra lại.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //show(); //load lai table
                    showCTGH(txtMaGH.Text);
                    cboTenSach.Focus();

                    updateGioHang(maGH, giaBan, true);
                    updateCombobox(maSachMua, soLuong, true);
                }

                show();
            }
        }

        private void cboKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            int soLuongTon = 0;
            string tenKH = cboKH.Text;
            List<KhachHang> khList = new List<KhachHang>();
            khHandler.loadDataFromDoc(doc, fileName, khList);

            foreach (KhachHang item in khList)
            {
                if (item.TenKH == tenKH)
                {
                    txtMaKH.Text = item.MaKH;
                }
            }
        }

        private void btnXoaSach_Click(object sender, EventArgs e)
        {
            List<Sach> list = new List<Sach>();
            sHandler.loadDataFromDoc(doc, fileName, list);
            string maSach = "";

            foreach (Sach item in list)
            {
                if (item.TenSach == cboTenSach.Text)
                {                   
                    maSach = item.MaSach;
                }
            }

            if (maSach.Trim() == "")
                MessageBox.Show("Vui lòng chọn cuốn sách cần xóa.", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (!ctghHandler.xoaSachTrongGioHang(txtMaGH.Text, maSach)) //Neu xoa khong thanh cong
                    MessageBox.Show("Cuốn sách: " + cboTenSach.Text + " không tồn tại trong giỏ hàng.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //show(); //load lai table
                    showCTGH(txtMaGH.Text);
                    cboTenSach.Focus(); //set focus vao o text box EmpId

                    updateCombobox(maSach, Convert.ToInt32(cboSoLuong.Text), false);

                    int soLuong;
                    double giaBan;
                    soLuong = Convert.ToInt32(cboSoLuong.Text);
                    giaBan = soLuong * Convert.ToDouble(txtGiaBan.Text);
                    updateGioHang(txtMaGH.Text, giaBan, false);
                }
            }

            show();
        }
    }
}
