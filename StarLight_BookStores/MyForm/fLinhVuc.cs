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
    public partial class fLinhVuc : Form
    {
        LinhVucController handler = new LinhVucController();
        XmlDocument doc = new XmlDocument();
        string fileName = @"..//..//MyData//data.xml";
        string maLVMoi = "";

        public fLinhVuc()
        {
            InitializeComponent();
        }

        private void fLinhVuc_Load(object sender, EventArgs e)
        {
            show();
        }

        public void show()
        {
            List<LinhVuc> list = new List<LinhVuc>();
            //Tao moi 1 danh sach luu tru cac nhan vien
            handler.loadDataFromDoc(doc, fileName, list);
            //lay du lieu cac nhan vien tu file xml them vao danh sach nhan vien
            dataGridView1.Rows.Clear(); //xoa het du lieu cac dong
            int rowIndex = 0; //chi so cua dong cua bang
            foreach (LinhVuc item in list)
            {
                dataGridView1.Rows.Add();

                dataGridView1.Rows[rowIndex].Cells[0].Value = item.MaLinhVuc;
                dataGridView1.Rows[rowIndex].Cells[1].Value = item.TenLinhVuc;

                rowIndex++;
            }

            StringBuilder builder = new StringBuilder();
            maLVMoi = builder.Append("LV0").Append(handler.getMaxId()).ToString();

            txtMaLV.Text = "";
            txtTenLV.Text = "";
        }

        private bool notEmptyFields()
        {
            if (txtTenLV.Text.Trim() == "")
                return false;
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            /*fLinhVuc_Create f = new fLinhVuc_Create();
            this.Hide();
            f.ShowDialog();
            this.Show();*/

            string maLV, tenLV;
            maLV = maLVMoi;
            tenLV = txtTenLV.Text;

            if (!notEmptyFields()) //neu chua nhap day du thong tin
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                LinhVuc lv = new LinhVuc(maLV, tenLV);
                if (!handler.themLinhVuc(lv)) //Neu them khong thanh cong
                    MessageBox.Show("Mã lĩnh vực đã tồn tại: " + maLV, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaLV.Focus(); //set focus
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maLV, tenLV;
            maLV = txtMaLV.Text;
            tenLV = txtTenLV.Text;

            if (!notEmptyFields()) //neu chua nhap day du thong tin
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                LinhVuc lv = new LinhVuc(maLV, tenLV);
                if (!handler.suaLinhVuc(lv)) //Neu them khong thanh cong
                    MessageBox.Show("Mã lĩnh vực đã tồn tại: " + maLV, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaLV.Focus(); //set focus
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maLV = txtMaLV.Text;
            if (maLV.Trim() == "")
                MessageBox.Show("Xóa theo Mã Lĩnh Vực, vui lòng không để trống", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (!handler.xoaLinhVuc(maLV)) //Neu xoa khong thanh cong
                    MessageBox.Show("Mã lĩnh vực: " + maLV + " không tồn tại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else //Thanh cong
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); //load lai table
                    txtMaLV.Focus(); //set focus vao o text box EmpId
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowSeleted = dataGridView1.CurrentCell.RowIndex; //Lay ra dong hien tai dang duoc chon
            //hien thi cac thong tin o dong hien tai len o text box tuong ung
            txtMaLV.Text = (string)dataGridView1.Rows[rowSeleted].Cells[0].Value;
            txtTenLV.Text = (string)dataGridView1.Rows[rowSeleted].Cells[1].Value;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fLinhVuc_Activated(object sender, EventArgs e)
        {
            show();
        }
    }
}
