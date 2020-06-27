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
    public partial class fKhachHang : Form
    {
        KhachHangController handler = new KhachHangController();
        XmlDocument doc = new XmlDocument();
        string fileName = @"..//..//MyData//data.xml";
        string maKHMoi = "";

        public fKhachHang()
        {
            InitializeComponent();
        }

        private void fKhachHang_Load(object sender, EventArgs e)
        {
            show();
        }

        public void show()
        {
            List<KhachHang> list = new List<KhachHang>();
            //Tao moi 1 danh sach luu tru cac khach hang
            handler.loadDataFromDoc(doc, fileName, list);
            //lay du lieu cac khach hang tu file xml them vao danh sach khach hang
            dgvDSKH.Rows.Clear(); //xoa het du lieu cac dong
            int rowIndex = 0; //chi so cua dong cua bang
            foreach (KhachHang item in list)
            {
                dgvDSKH.Rows.Add();

                dgvDSKH.Rows[rowIndex].Cells[0].Value = item.MaKH;
                dgvDSKH.Rows[rowIndex].Cells[1].Value = item.TenKH;
                dgvDSKH.Rows[rowIndex].Cells[2].Value = item.DiaChi;
                dgvDSKH.Rows[rowIndex].Cells[3].Value = item.Sdt;
                dgvDSKH.Rows[rowIndex].Cells[4].Value = item.GioiTinh;

                rowIndex++;
            }

            txtDiaChi.Text = "";
            txtMaKH.Text = "";
            txtSDT.Text = "";
            txtTenKH.Text = "";
            cboGioiTinh.Text = "";

            StringBuilder builder = new StringBuilder();
            maKHMoi = builder.Append("KH0").Append(handler.getMaxId()).ToString();
        }

        private bool notEmptyFields()
        {
            if (txtTenKH.Text.Trim() == "" || txtSDT.Text.Trim() == ""
                || txtDiaChi.Text.Trim() == "" || cboGioiTinh.Text.Trim() == "")
                return false;
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maKH, tenKH, diaChi, sdt, gioiTinh;
            maKH = maKHMoi;
            tenKH = txtTenKH.Text;
            diaChi = txtDiaChi.Text;
            sdt = txtSDT.Text;
            gioiTinh = cboGioiTinh.Text;

            if (!notEmptyFields()) //neu chua nhap day du thong tin
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                KhachHang kh = new KhachHang(maKH, tenKH, diaChi, sdt, gioiTinh);
                if (!handler.themKhachHang(kh)) //Neu them khong thanh cong
                    MessageBox.Show("Mã khách hàng: " + maKH + " đã tồn tại.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaKH.Focus(); //set focus
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maKH, tenKH, diaChi, sdt, gioiTinh;
            maKH = txtMaKH.Text;
            tenKH = txtTenKH.Text;
            diaChi = txtDiaChi.Text;
            sdt = txtSDT.Text;
            gioiTinh = cboGioiTinh.Text;

            if (!notEmptyFields()) //neu chua nhap day du thong tin
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                KhachHang kh = new KhachHang(maKH, tenKH, diaChi, sdt, gioiTinh);
                if (!handler.suaKhachHang(kh)) //Neu them khong thanh cong
                    MessageBox.Show("Mã khách hàng: " + maKH + " đã tồn tại.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaKH.Focus(); //set focus
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text;
            if (maKH.Trim() == "")
                MessageBox.Show("Xóa theo Mã Khách Hàng, vui lòng không để trống", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (!handler.xoaKhachHang(maKH)) //Neu xoa khong thanh cong
                    MessageBox.Show("Mã khách hàng: " + maKH + " không tồn tại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaKH.Focus(); //set focus vao o text box EmpId
                }
            }
        }

        private void dgvDSKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowSeleted = dgvDSKH.CurrentCell.RowIndex; //Lay ra dong hien tai dang duoc chon
            //hien thi cac thong tin o dong hien tai len o text box tuong ung
            txtMaKH.Text = (string)dgvDSKH.Rows[rowSeleted].Cells[0].Value;
            txtTenKH.Text = (string)dgvDSKH.Rows[rowSeleted].Cells[1].Value;
            txtDiaChi.Text = (string)dgvDSKH.Rows[rowSeleted].Cells[2].Value;
            txtSDT.Text = (string)dgvDSKH.Rows[rowSeleted].Cells[3].Value;
            cboGioiTinh.Text = (string)dgvDSKH.Rows[rowSeleted].Cells[4].Value;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maKH, tenKH, diaChi, sdt, gioiTinh;           
            maKH = txtMaKH.Text;
            tenKH = txtTenKH.Text;
            diaChi = txtDiaChi.Text;
            sdt = txtSDT.Text;
            gioiTinh = cboGioiTinh.Text;

            if (maKH.Trim() == ""
                && tenKH.Trim() == ""
                && diaChi.Trim() == ""
                && sdt.Trim() == ""
                && gioiTinh.Trim() == "")
                MessageBox.Show("Vui lòng điền thông tin muốn tìm.", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                List<KhachHang> list = new List<KhachHang>();
                handler.loadDataFromDoc(doc, fileName, list);
                dgvDSKH.Rows.Clear(); //xoa het du lieu cac dong
                int rowIndex = 0; //chi so cua dong cua bang
                foreach (KhachHang item in list)
                {
                    dgvDSKH.Rows.Add();

                    if (maKH == item.MaKH || tenKH == item.TenKH
                        || diaChi == item.DiaChi || sdt == item.Sdt
                        || gioiTinh == item.GioiTinh)
                    {
                        dgvDSKH.Rows[rowIndex].Cells[0].Value = item.MaKH;
                        dgvDSKH.Rows[rowIndex].Cells[1].Value = item.TenKH;
                        dgvDSKH.Rows[rowIndex].Cells[2].Value = item.DiaChi;
                        dgvDSKH.Rows[rowIndex].Cells[3].Value = item.Sdt;
                        dgvDSKH.Rows[rowIndex].Cells[4].Value = item.GioiTinh;

                        rowIndex++;
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            show();
        }
    }
}
