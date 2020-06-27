using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarLight_BookStores.MyForm
{
    public partial class fMenu : Form
    {
        public fMenu()
        {
            InitializeComponent();
        }

        private void btnGioHang_Click(object sender, EventArgs e)
        {
            fGioHang f = new fGioHang();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            fKhachHang f = new fKhachHang();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnSach_Click(object sender, EventArgs e)
        {
            fSach f = new fSach();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fTacGia f = new fTacGia();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}
