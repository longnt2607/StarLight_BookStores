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
    public partial class fSach : Form
    {
        SachController handler = new SachController();
        LinhVucController lvHandler = new LinhVucController();
        TacGiaController tgHandler = new TacGiaController();
        XmlDocument doc = new XmlDocument();
        string fileName = @"..//..//MyData//data.xml";
        string maSachMoi = "";

        public fSach()
        {
            InitializeComponent();
        }

        private void fSach_Load(object sender, EventArgs e)
        {
            show();
        }

        public void show()
        {
            // load data to combo box linhvuc
            List<LinhVuc> lvList = new List<LinhVuc>();
            lvHandler.loadDataFromDoc(doc, fileName, lvList);

            cboLinhVuc.Items.Clear();

            foreach (LinhVuc lv in lvList)
            {
                cboLinhVuc.Items.Add(lv.TenLinhVuc);
            }

            // load data to combo box tacgia
            List<TacGia> tgList = new List<TacGia>();
            tgHandler.loadDataFromDoc(doc, fileName, tgList);

            cboTacGia.Items.Clear();

            foreach (TacGia tg in tgList)
            {
                cboTacGia.Items.Add(tg.TenTacGia);
            }

            List<Sach> list = new List<Sach>();
            //Tao moi 1 danh sach luu tru cac nhan vien
            handler.loadDataFromDoc(doc, fileName, list);                     

            //lay du lieu cac nhan vien tu file xml them vao danh sach nhan vien
            dgvDSS.Rows.Clear(); //xoa het du lieu cac dong
            int rowIndex = 0; //chi so cua dong cua bang
            foreach (Sach item in list)
            {
                dgvDSS.Rows.Add();

                dgvDSS.Rows[rowIndex].Cells[0].Value = item.LinhVuc;
                dgvDSS.Rows[rowIndex].Cells[1].Value = item.MaSach;
                dgvDSS.Rows[rowIndex].Cells[2].Value = item.TenSach;
                dgvDSS.Rows[rowIndex].Cells[3].Value = item.TacGia;
                dgvDSS.Rows[rowIndex].Cells[4].Value = item.NamXB;
                dgvDSS.Rows[rowIndex].Cells[5].Value = item.NgonNgu;
                dgvDSS.Rows[rowIndex].Cells[6].Value = item.SoLuong;
                dgvDSS.Rows[rowIndex].Cells[7].Value = item.Gia;

                rowIndex++;
            }

            StringBuilder builder = new StringBuilder();
            maSachMoi = builder.Append("SA0").Append(handler.getMaxId()).ToString();
            //MessageBox.Show(maSach);
            //txtMaSach.Text = maSachMoi;

            cboLinhVuc.Text = "";
            cboTacGia.Text = "";
            txtGia.Text = "";
            txtMaSach.Text = "";
            txtNamXB.Text = "";
            txtNgonNgu.Text = "";
            txtSoLuong.Text = "";
            txtTenSach.Text = "";
        }

        private bool notEmptyFields()
        {
            if (txtTenSach.Text.Trim() == "" || txtGia.Text.Trim() == ""
                || txtNgonNgu.Text.Trim() == ""
                || txtSoLuong.Text.Trim() == "" || cboLinhVuc.Text.Trim() == ""
                || cboTacGia.Text.Trim() == "" || txtNamXB.Text.Trim() == "")
                return false;
            return true;
        }

        private void btnLinhVuc_Click(object sender, EventArgs e)
        {
            fLinhVuc f = new fLinhVuc();
            this.Hide();
            f.ShowDialog();

            show();
            this.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            //show();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            /*fSach_Create f = new fSach_Create();
            this.Hide();
            f.ShowDialog();
            this.Show();*/
            string linhVuc, maSach, tenSach, tacGia, ngonNgu;
            int namXB, soLuong;
            double gia;
            //maSach = txtMaSach.Text;
            /*maSach = maSachMoi;
            linhVuc = cboLinhVuc.Text;
            tenSach = txtTenSach.Text;
            tacGia = cboTacGia.Text;
            namXB = Convert.ToInt32(txtNamXB.Text);
            ngonNgu = txtNgonNgu.Text;
            soLuong = Convert.ToInt32(txtSoLuong.Text);
            gia = Convert.ToDouble(txtGia.Text);*/

            if (!notEmptyFields()) //neu chua nhap day du thong tin
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                maSach = maSachMoi;
                linhVuc = cboLinhVuc.Text;
                tenSach = txtTenSach.Text;
                tacGia = cboTacGia.Text;
                namXB = Convert.ToInt32(txtNamXB.Text);
                ngonNgu = txtNgonNgu.Text;
                soLuong = Convert.ToInt32(txtSoLuong.Text);
                gia = Convert.ToDouble(txtGia.Text);

                Sach s = new Sach(linhVuc, maSach, tenSach, tacGia, namXB, ngonNgu, soLuong, gia);
                if (!handler.themSach(s)) //Neu them khong thanh cong
                    MessageBox.Show("Mã sách đã tồn tại: " + maSach, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaSach.Focus(); //set focus
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string linhVuc, maSach, tenSach, tacGia, ngonNgu;
            int namXB, soLuong;
            double gia;
            maSach = txtMaSach.Text;
            linhVuc = cboLinhVuc.Text;
            tenSach = txtTenSach.Text;
            tacGia = cboTacGia.Text;
            namXB = Convert.ToInt32(txtNamXB.Text);
            ngonNgu = txtNgonNgu.Text;
            soLuong = Convert.ToInt32(txtSoLuong.Text);
            gia = Convert.ToDouble(txtGia.Text);

            if (!notEmptyFields()) //neu chua nhap day du thong tin
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                Sach s = new Sach(linhVuc, maSach, tenSach, tacGia, namXB, ngonNgu, soLuong, gia);
                if (!handler.suaSach(s)) //Neu them khong thanh cong
                    MessageBox.Show("Mã sách đã tồn tại: " + maSach, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaSach.Focus(); //set focus
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSach = txtMaSach.Text;
            if (maSach.Trim() == "")
                MessageBox.Show("Xóa theo Mã Sách, vui lòng không để trống", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (!handler.xoaSach(maSach)) //Neu xoa khong thanh cong
                    MessageBox.Show("Mã sách: " + maSach + " không tồn tại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaSach.Focus(); //set focus vao o text box EmpId
                }
            }
        }

        private void dgvDSS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowSeleted = dgvDSS.CurrentCell.RowIndex; //Lay ra dong hien tai dang duoc chon
            //hien thi cac thong tin o dong hien tai len o text box tuong ung
            cboLinhVuc.Text = (string)dgvDSS.Rows[rowSeleted].Cells[0].Value;
            txtMaSach.Text = (string)dgvDSS.Rows[rowSeleted].Cells[1].Value;           
            txtTenSach.Text = (string)dgvDSS.Rows[rowSeleted].Cells[2].Value;
            cboTacGia.Text = (string)dgvDSS.Rows[rowSeleted].Cells[3].Value;
            txtNamXB.Text = (string)dgvDSS.Rows[rowSeleted].Cells[4].Value.ToString();
            txtNgonNgu.Text = (string)dgvDSS.Rows[rowSeleted].Cells[5].Value;
            txtSoLuong.Text = (string)dgvDSS.Rows[rowSeleted].Cells[6].Value.ToString();
            txtGia.Text = (string)dgvDSS.Rows[rowSeleted].Cells[7].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string linhVuc, maSach, tenSach, tacGia, ngonNgu, namXB, soLuong, gia;
            maSach = txtMaSach.Text;
            linhVuc = cboLinhVuc.Text;
            tenSach = txtTenSach.Text;
            tacGia = cboTacGia.Text;
            namXB = txtNamXB.Text;
            ngonNgu = txtNgonNgu.Text;
            soLuong = txtSoLuong.Text;
            gia = txtGia.Text;

            if (txtMaSach.Text.Trim() == "" 
                && cboLinhVuc.Text.Trim() == ""
                && txtTenSach.Text.Trim() == "" 
                && cboTacGia.Text.Trim() == ""
                && txtNamXB.Text.Trim() == ""
                && txtNgonNgu.Text.Trim() == ""
                && txtSoLuong.Text.Trim() == ""
                && txtGia.Text.Trim() == "") //neu chua nhap day du thong tin
                MessageBox.Show("Vui lòng điền thông tin muốn tìm.", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                List<Sach> list = new List<Sach>();
                handler.loadDataFromDoc(doc, fileName, list);
                dgvDSS.Rows.Clear(); //xoa het du lieu cac dong
                int rowIndex = 0; //chi so cua dong cua bang
                foreach (Sach item in list)
                {
                    dgvDSS.Rows.Add();

                    if (maSach == item.MaSach.ToString() || linhVuc == item.LinhVuc.ToString() 
                        || tenSach == item.TenSach.ToString() || tacGia == item.TacGia.ToString()
                        || namXB == item.NamXB.ToString() || ngonNgu == item.NgonNgu.ToString() 
                        || soLuong == item.SoLuong.ToString() || gia == item.Gia.ToString())
                    {                     
                        dgvDSS.Rows[rowIndex].Cells[0].Value = item.LinhVuc;
                        dgvDSS.Rows[rowIndex].Cells[1].Value = item.MaSach;
                        dgvDSS.Rows[rowIndex].Cells[2].Value = item.TenSach;
                        dgvDSS.Rows[rowIndex].Cells[3].Value = item.TacGia;
                        dgvDSS.Rows[rowIndex].Cells[4].Value = item.NamXB;
                        dgvDSS.Rows[rowIndex].Cells[5].Value = item.NgonNgu;
                        dgvDSS.Rows[rowIndex].Cells[6].Value = item.SoLuong;
                        dgvDSS.Rows[rowIndex].Cells[7].Value = item.Gia;

                        rowIndex++;
                    }                   
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            show();
            /*cboLinhVuc.Text = "";
            cboTacGia.Text = "";
            txtGia.Text = "";
            txtMaSach.Text = "";
            txtNamXB.Text = "";
            txtNgonNgu.Text = "";
            txtSoLuong.Text = "";
            txtTenSach.Text = "";*/
        }

        private void btnTacGia_Click(object sender, EventArgs e)
        {
            fTacGia f = new fTacGia();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}
