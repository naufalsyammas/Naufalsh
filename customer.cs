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
    public partial class customer : Form
    {
        koneksi conn = new koneksi();
        DataTable dtcust = new DataTable();

        public customer()
        {
            InitializeComponent();
            TampilCustomer();
        }

        private void TampilCustomer()
        {
            DataTable dtampil = new DataTable();
            string cust = "select * from customer";
            dtampil = conn.BukaTabel(cust);
            grid_customer.DataSource = dtampil;
        }

        private void btn_tambah_Click(object sender, EventArgs e)
        {
            this.Text = "Customer - Tambah Data";
            txt_idcust.Clear();
            cb_jkitas.Text = null;
            txt_nokitas.Clear();
            txt_nama.Clear();
            txt_alamat.Clear();
            txt_notelp.Clear();
            txt_email.Clear();

            dtcust = conn.BukaTabel("select idcust from customer");

            if (dtcust.Rows.Count > 0)
            {
                string idc = dtcust.Rows[dtcust.Rows.Count - 1][0].ToString();
                int kd = int.Parse(idc.Substring(4,4)) + 1;
                idc = "0000" + kd.ToString();

                txt_idcust.Text = "CUST" + idc.Substring(idc.Length - 4, 4);
                txt_idcust.Enabled = false;

            }
            else {
                txt_idcust.Text = "CUST0001";
            }


        }

        private void btn_ubah_Click(object sender, EventArgs e)
        {
            this.Text = "Customer - Edit Data";
            txt_nama.Focus();

            txt_idcust.Text = grid_customer.CurrentRow.Cells[0].Value.ToString();
            cb_jkitas.Text = grid_customer.CurrentRow.Cells[1].Value.ToString();
            txt_nokitas.Text = grid_customer.CurrentRow.Cells[2].Value.ToString();
            txt_nama.Text = grid_customer.CurrentRow.Cells[3].Value.ToString();
            txt_ps.Text = grid_customer.CurrentRow.Cells[4].Value.ToString();
            txt_alamat.Text = grid_customer.CurrentRow.Cells[5].Value.ToString();
            txt_notelp.Text = grid_customer.CurrentRow.Cells[6].Value.ToString();
            txt_email.Text = grid_customer.CurrentRow.Cells[7].Value.ToString();
        }


        private void btn_simpan_Click(object sender, EventArgs e)
        {
            string SqlSimpan;

            if (txt_idcust.Text == "")
            {
                MessageBox.Show("ID Customer Tidak Boleh Kosong.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (this.Text == "Customer - Tambah Data")
                {
                    SqlSimpan = "insert into customer(idcust, jkitas, nkitas, namacust, perusahaan, alamat, notelp, email) " +
                            "values('" + txt_idcust.Text + "' , '" + cb_jkitas.Text + "' , '" + txt_nokitas.Text + "' , '" + txt_nama.Text + "' , '" + txt_ps.Text + "' , '" + txt_alamat.Text + "' , '" + txt_notelp.Text + "' , '" + txt_email.Text + "')";
                    conn.AksiQuery(SqlSimpan);
                    MessageBox.Show("Data Customer Berhasil Disimpan");
                    TampilCustomer();
                    this.Text = "customer";
                }
                else if (this.Text == "Customer - Edit Data")
                {
                    SqlSimpan = "update customer set jkitas='" + cb_jkitas.Text + "', nkitas='" + txt_nokitas.Text + "', namacust='" + txt_nama.Text + "',perusahaan='" + txt_ps.Text + "', alamat='" + txt_alamat.Text + "', notelp='" + txt_notelp.Text + "', email='" + txt_email.Text + "' where idcust='" + txt_idcust.Text + "'";
                    conn.AksiQuery(SqlSimpan);
                    MessageBox.Show("Edit Data Customer Berhasil", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_idcust.Clear();
                    cb_jkitas.Text = null; txt_nokitas.Clear();
                    txt_nama.Clear();
                    txt_ps.Clear();
                    txt_alamat.Clear();
                    txt_notelp.Clear();
                    txt_email.Clear();
                    TampilCustomer();
                }
            }
        }

        private void customer_Load(object sender, EventArgs e)
        {

        }

        private void btn_hapus_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grid_customer.SelectedRows)
            {
                string cst = grid_customer.Rows[grid_customer.CurrentRow.Index].Cells[0].Value.ToString();
                string SqlHapus = "delete from customer where idcust='" + cst + "'";

                conn.AksiQuery(SqlHapus);
                MessageBox.Show("Data Berhasil Dihapus.","SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            TampilCustomer();
        }
    }
}
