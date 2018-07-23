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
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void menu_Load(object sender, EventArgs e)
        {

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_plgn_Click(object sender, EventArgs e)
        {
            customer cs = new customer(); //memanggil nama form
            cs.ShowDialog();
        }

        private void btn_kayu_Click(object sender, EventArgs e)
        {
            kayu ky = new kayu(); //memanggil nama form
            ky.ShowDialog();
        }

        private void btn_jasa_Click(object sender, EventArgs e)
        {
            jasa js = new jasa(); //memanggil nama form
            js.ShowDialog();
        }

        private void btn_inv_Click(object sender, EventArgs e)
        {
            invoice iv = new invoice(); //memanggil nama form
            iv.ShowDialog();
        }

    }
}
