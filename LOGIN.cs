using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace invoice
{
    public partial class LOGIN : Form
    {
        koneksi conn = new koneksi();
        DataTable dtlogin = new DataTable();

        public LOGIN()
        {
            InitializeComponent();
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string login = "select username, nama, password from admin where username='" + txt_user.Text + "' and password='" + txt_pass.Text + "'";
            dtlogin = conn.BukaTabel(login);

            if (dtlogin.Rows.Count > 0)
            {
                txt_user.Text = ""; //menghilangkan inputan awal teks
                txt_pass.Text = "";
                this.Hide(); //menghilangkan / mengecilkan menu form login

                koneksi.nmuser = dtlogin.Rows[0][1].ToString();
                menu mn = new menu();
                mn.Show();
                mn.lb_user.Text = koneksi.nmuser;
            }
            else {
                MessageBox.Show("Data Tidak Ditemukan");
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }        
    }
}
