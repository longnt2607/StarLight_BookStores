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
    public partial class fTacGia : Form
    {
        TacGiaController handler = new TacGiaController();
        XmlDocument doc = new XmlDocument();
        string fileName = @"..//..//MyData//data.xml";
        string maTGMoi = "";

        public fTacGia()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            /*fTacGia_Create f = new fTacGia_Create();
            this.Hide();
            f.ShowDialog();
            this.Show();*/
            string maTacGia, tenTacGia, diaChi, Sdt, Email, GioiTinh, QuocTich;
            maTacGia = maTGMoi;
            tenTacGia = txtTenTG.Text;
            diaChi = txtDiaChi.Text;
            Sdt = txtSDT.Text;
            Email = txtEmail.Text;
            GioiTinh = cboGioiTinh.Text;
            QuocTich = cboQuocTich.Text;

            if (!notEmptyFields()) //neu chua nhap day du thong tin
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                TacGia TG = new TacGia(maTacGia, tenTacGia, diaChi, Sdt, Email, GioiTinh, QuocTich);
                if (!handler.themTacGia(TG)) //Neu them khong thanh cong
                    MessageBox.Show("Mã lĩnh vực đã tồn tại: " + maTacGia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaTG.Focus(); //set focus
                }
            }
        }

        private void fTacGia_Load(object sender, EventArgs e)
        {
            show();
        }

        public void show()
        {
            List<TacGia> list = new List<TacGia>();
            //Tao moi 1 danh sach luu tru cac nhan vien
            handler.loadDataFromDoc(doc, fileName, list);
            //lay du lieu cac nhan vien tu file xml them vao danh sach nhan vien
            dataGridView1.Rows.Clear(); //xoa het du lieu cac dong
            int rowIndex = 0; //chi so cua dong cua bang
            foreach (TacGia item in list)
            {
                dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells[0].Value = item.MaTacGia;
                dataGridView1.Rows[rowIndex].Cells[1].Value = item.TenTacGia;
                dataGridView1.Rows[rowIndex].Cells[2].Value = item.DiaChi;
                dataGridView1.Rows[rowIndex].Cells[3].Value = item.Sdt;
                dataGridView1.Rows[rowIndex].Cells[4].Value = item.Email;
                dataGridView1.Rows[rowIndex].Cells[5].Value = item.GioiTinh;
                dataGridView1.Rows[rowIndex].Cells[6].Value = item.QuocTich;

                rowIndex++;
            }

            StringBuilder builder = new StringBuilder();
            maTGMoi = builder.Append("TG0").Append(handler.getMaxId()).ToString();

            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtMaTG.Text = "";
            txtSDT.Text = "";
            txtTenTG.Text = "";
            cboGioiTinh.Text = "";
            cboQuocTich.Text = "";
        }

        private bool notEmptyFields()
        {
            if (txtTenTG.Text.Trim() == "" 
                || txtDiaChi.Text.Trim() == "" 
                || txtSDT.Text.Trim() == "" || txtEmail.Text.Trim() == "" 
                || cboGioiTinh.Text.Trim() == "" || cboQuocTich.Text.Trim() == "")
                return false;
            return true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maTacGia, tenTacGia, diaChi, Sdt, Email, GioiTinh, QuocTich;
            maTacGia = txtMaTG.Text;
            tenTacGia = txtTenTG.Text;
            diaChi = txtDiaChi.Text;
            Sdt = txtSDT.Text;
            Email = txtEmail.Text;
            GioiTinh = cboGioiTinh.Text;
            QuocTich = cboQuocTich.Text;

            if (!notEmptyFields()) //neu chua nhap day du thong tin
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                TacGia TG = new TacGia(maTacGia, tenTacGia, diaChi, Sdt, Email, GioiTinh, QuocTich);
                if (!handler.suaTacGia(TG))
                    MessageBox.Show("Mã lĩnh vực đã tồn tại: " + maTacGia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaTG.Focus(); //set focus
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maTacGia = txtMaTG.Text;
            if (maTacGia.Trim() == "")
                MessageBox.Show("Xóa theo Mã tác giả, vui lòng không để trống", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (!handler.xoaTacGia(maTacGia)) //Neu xoa khong thanh cong
                    MessageBox.Show("Mã tác giả: " + maTacGia + " không tồn tại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaTG.Focus(); //set focus vao o text box EmpId
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string maTacGia, tenTacGia, diaChi, Sdt, Email, GioiTinh, QuocTich;
            maTacGia = txtMaTG.Text;
            tenTacGia = txtTenTG.Text;
            diaChi = txtDiaChi.Text;
            Sdt = txtSDT.Text;
            Email = txtEmail.Text;
            GioiTinh = cboGioiTinh.Text;
            QuocTich = cboQuocTich.Text;

            if (txtMaTG.Text.Trim() == "" && txtTenTG.Text.Trim() == ""
                && txtDiaChi.Text.Trim() == "" && txtSDT.Text.Trim() == ""
                && txtEmail.Text.Trim() == "" && cboGioiTinh.Text.Trim() == ""
                && cboQuocTich.Text.Trim() == "") //neu chua nhap day du thong tin
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                List<TacGia> list = new List<TacGia>();
                handler.loadDataFromDoc(doc, fileName, list);
                dataGridView1.Rows.Clear(); //xoa het du lieu cac dong
                int rowIndex = 0; //chi so cua dong cua bang
                foreach (TacGia item in list)
                {
                    dataGridView1.Rows.Add();

                    if (maTacGia == item.MaTacGia.ToString() || tenTacGia == item.TenTacGia.ToString()
                        || diaChi == item.DiaChi.ToString() || Sdt == item.Sdt.ToString()
                        || Email == item.Email.ToString() || GioiTinh == item.GioiTinh.ToString()
                        || QuocTich == item.QuocTich.ToString())
                    {
                        dataGridView1.Rows[rowIndex].Cells[0].Value = item.MaTacGia;
                        dataGridView1.Rows[rowIndex].Cells[1].Value = item.TenTacGia;
                        dataGridView1.Rows[rowIndex].Cells[2].Value = item.DiaChi;
                        dataGridView1.Rows[rowIndex].Cells[3].Value = item.Sdt;
                        dataGridView1.Rows[rowIndex].Cells[4].Value = item.Email;
                        dataGridView1.Rows[rowIndex].Cells[5].Value = item.GioiTinh;
                        dataGridView1.Rows[rowIndex].Cells[6].Value = item.QuocTich;

                        rowIndex++;
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowSeleted = dataGridView1.CurrentCell.RowIndex; //Lay ra dong hien tai dang duoc chon
            //hien thi cac thong tin o dong hien tai len o text box tuong ung
            txtMaTG.Text = (string)dataGridView1.Rows[rowSeleted].Cells[0].Value;
            txtTenTG.Text = (string)dataGridView1.Rows[rowSeleted].Cells[1].Value;
            txtDiaChi.Text = (string)dataGridView1.Rows[rowSeleted].Cells[2].Value;
            txtSDT.Text = (string)dataGridView1.Rows[rowSeleted].Cells[3].Value;
            txtEmail.Text = (string)dataGridView1.Rows[rowSeleted].Cells[4].Value;
            cboGioiTinh.Text = (string)dataGridView1.Rows[rowSeleted].Cells[5].Value;
            cboQuocTich.Text = (string)dataGridView1.Rows[rowSeleted].Cells[6].Value;
        }
    }
}
